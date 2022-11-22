using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OuroVerde.Maintenance.Application.Interface;
using OuroVerde.Maintenance.Domain.Model;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Unidas.MS.Maintenance.Domain.Adapters.Repository.Queue;
using Microsoft.Extensions.Logging;
using Unidas.MS.Maintenance.Infra.CrossCutting.Shared.Common;
using Unidas.MS.Maintenance.Application.ViewModel;
using OuroVerde.Maintenance.Domain.Adapters.Repository;
using Microsoft.Azure.Amqp.Framing;
using Unidas.MS.Maintenance.Application.Interface;

namespace Unidas.MS.Maintenance.Application.AppServices
{
    public class ItensIntegrationService : IItensIntegrationService
    {
        private readonly IItensIntegrationRepository _repository;
        private readonly ISalesForceAuthenticationService _salesForceAuthentitcation;
        private readonly IMapper _mapper;
        private readonly IQueueConnectorAdapter _queueConnectorAdapter;
        private readonly ILogger<ItensIntegrationService> _logger;

        private readonly AppSettings _appSettings;

        public ItensIntegrationService(IItensIntegrationRepository repository,
                                       ISalesForceAuthenticationService salesForceAuthentitcation,
                                       IMapper mapper,
                                       IQueueConnectorAdapter queueConnectorAdapter,
                                       ILogger<ItensIntegrationService> logger)
        {
            _repository = repository;
            _salesForceAuthentitcation = salesForceAuthentitcation;
            _mapper = mapper;
            _queueConnectorAdapter = queueConnectorAdapter;
            _logger = logger;
        }

        public async Task<ActionResult> sendItensActived()
        {
            var itemsToProducts = await _queueConnectorAdapter.ConsumeMessageObject<ItensIntegrationLogViewModel>();

            if (itemsToProducts == null)
                return new OkResult();

            foreach (var item in itemsToProducts)
            {
                try
                {
                    await UpdateItensProductActived(item);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Erro na aplicacao ov-api-maintenance, classe ItensIntegrationService, metodo sendItensActived, para o item {item}, dando a mensagem {message}", JsonConvert.SerializeObject(item), ex.Message);
                    Console.WriteLine(ex.Message);
                }
            }
            return new OkResult();
        }

        public async Task<ActionResult> sendItensQueue()
        {
            var itemsToProducts = await _queueConnectorAdapter.ConsumeMessageObject<ProductSalesForceViewModel>();

            if (itemsToProducts == null)
                return new OkResult();

            foreach (var item in itemsToProducts)
            {
                if (string.IsNullOrEmpty(item.IdSalesForce))
                    await CreateItensProduct(item);
                else
                    await UpdateProduct(item);
            }
            return new OkResult();
        }

        #region Codigo para transferência e Comunicacao com o Repositorio e Fila

        private async Task<string> CreateProduct(ProductSalesForceViewModel product, string token)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(_appSettings.SalesForce.Url + "Product2")
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var RecordTypeId = !product.Tax ? product.RecordTypeId : _appSettings.SalesForce.OperacionalService_RecordTypeId;

                var content = new StringContent(JsonConvert.SerializeObject(new
                {
                    product.Name,
                    product.ProductCode,
                    product.Description,
                    product.IsActive,
                    product.Family,
                    RecordTypeId,
                    product.Desconto__c,
                    product.Itens_de_serie_c,
                    product.Preco_Publico__c,
                    product.Subcategoria__c,
                    product.TipoCarroceria__c,
                    product.Configuracao__c,
                    product.Tamanho__c,
                    Unidade__c = product.Unidade
                }), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_appSettings.SalesForce.Url + "Product2", content);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    var resultObject = JToken.Parse(await response.Content.ReadAsStringAsync()).Root["id"];

                    var itensIntegrationViewModel = new ItensIntegrationLogViewModel()
                    {
                        IdSalesForce = resultObject.Root["id"].ToString(),
                        CRMNumeroItem = product.ProductCode,
                        CreatedDateTime = DateTime.Now,
                        CRM_Product_RecVersion = product.ProductRecVersion,
                        CRM_ProductTranslation_RecVersion = product.ProductTranslationRecVersion,
                        CRM_ProductOV_RecVersion = product.ProductOvRecVersion,
                        CRM_ProductItem_RecVersion = product.ProductItemRecVersion,
                        CRM_Erp_RecVersion = product.ErpRecVersion,
                        Discount = product.Desconto__c,
                        Price = product.Preco_Publico__c,
                        Type = product.Type,
                        checkHierarchyChanges = product.checkHierarchyChanges,
                        Product = 0,
                        Stopped = false,
                        StoppedQuotation = false
                    };

                    await _repository.InsertItensInLog(_mapper.Map<ItensIntegrationLog>(itensIntegrationViewModel));

                    return resultObject.ToString();
                }
                else
                {
                    string idSalesForce = "Erro ao Integrar";
                    bool integrated = false;
                    var resultObject = JToken.Parse(await response.Content.ReadAsStringAsync());

                    if (resultObject.First["message"].ToString().Contains("duplicates"))
                    {
                        string[] splitMsg = resultObject.First["message"].ToString().Split("id: ");
                        idSalesForce = splitMsg[1];
                        integrated = true;

                        var itensIntegrationViewModel = new ItensIntegrationLogViewModel()
                        {
                            IdSalesForce = resultObject.Root["id"].ToString(),
                            CRMNumeroItem = product.ProductCode,
                            CreatedDateTime = DateTime.Now,
                            CRM_Product_RecVersion = product.ProductRecVersion,
                            CRM_ProductTranslation_RecVersion = product.ProductTranslationRecVersion,
                            CRM_ProductOV_RecVersion = product.ProductOvRecVersion,
                            CRM_ProductItem_RecVersion = product.ProductItemRecVersion,
                            CRM_Erp_RecVersion = product.ErpRecVersion,
                            Discount = product.Desconto__c,
                            Price = product.Preco_Publico__c,
                            Type = product.Type,
                            checkHierarchyChanges = product.checkHierarchyChanges,
                            Product = 0,
                            Stopped = false,
                            StoppedQuotation = false
                        };

                        await _repository.InsertItensInLog(_mapper.Map<ItensIntegrationLog>(itensIntegrationViewModel));
                        return idSalesForce;
                    }
                    else
                    {
                        _logger.LogInformation("Falha na aplicacao ov-api-maintenance, classe ItensIntegrationService, metodo CreateProduct, para o product {product}, dando a mensagem {message}", JsonConvert.SerializeObject(product), resultObject.First["message"].ToString());

                        return resultObject.First["message"].ToString() + " - " + resultObject.First["errorCode"].ToString();
                    }

                }
            }
            catch (Exception e)
            {
                _logger.LogError("Erro na aplicacao ov-api-maintenance, classe ItensIntegrationService, metodo CreateProduct, para o product {product}, dando a mensagem {message}", JsonConvert.SerializeObject(product), e.Message);

                return e.Message;
            }
        }

        private async Task<string> UpdateProduct(ProductSalesForceViewModel product, string token)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(_appSettings.SalesForce.Url + "Product2")
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var RecordTypeId = !product.Tax ? product.RecordTypeId : _appSettings.SalesForce.OperacionalService_RecordTypeId;

                var content = new StringContent(JsonConvert.SerializeObject(new
                {
                    product.Name,
                    product.ProductCode,
                    product.Description,
                    product.IsActive,
                    product.Family,
                    RecordTypeId,
                    product.Desconto__c,
                    product.Itens_de_serie_c,
                    product.Preco_Publico__c,
                    product.Subcategoria__c,
                    product.TipoCarroceria__c,
                    product.Configuracao__c,
                    product.Tamanho__c,
                    Unidade__c = product.Unidade
                }), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_appSettings.SalesForce.Url + "Product2", content);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    var resultObject = JToken.Parse(await response.Content.ReadAsStringAsync()).Root["id"];

                    var itensIntegrationViewModel = new ItensIntegrationLogViewModel()
                    {
                        IdSalesForce = resultObject.Root["id"].ToString(),
                        CRMNumeroItem = product.ProductCode,
                        CreatedDateTime = DateTime.Now,
                        CRM_Product_RecVersion = product.ProductRecVersion,
                        CRM_ProductTranslation_RecVersion = product.ProductTranslationRecVersion,
                        CRM_ProductOV_RecVersion = product.ProductOvRecVersion,
                        CRM_ProductItem_RecVersion = product.ProductItemRecVersion,
                        CRM_Erp_RecVersion = product.ErpRecVersion,
                        Discount = product.Desconto__c,
                        Price = product.Preco_Publico__c,
                        Type = product.Type,
                        checkHierarchyChanges = product.checkHierarchyChanges,
                        Product = 0,
                        Stopped = false,
                        StoppedQuotation = false
                    };

                    await _repository.InsertItensInLog(_mapper.Map<ItensIntegrationLog>(itensIntegrationViewModel));

                    return resultObject.ToString();
                }
                else
                {
                    string idSalesForce = "Erro ao Integrar";
                    bool integrated = false;
                    var resultObject = JToken.Parse(await response.Content.ReadAsStringAsync());

                    if (resultObject.First["message"].ToString().Contains("duplicates"))
                    {
                        string[] splitMsg = resultObject.First["message"].ToString().Split("id: ");
                        idSalesForce = splitMsg[1];
                        integrated = true;

                        var itensIntegrationViewModel = new ItensIntegrationLogViewModel()
                        {
                            IdSalesForce = idSalesForce,
                            CRMNumeroItem = product.ProductCode,
                            CreatedDateTime = DateTime.Now,
                            CRM_Product_RecVersion = product.ProductRecVersion,
                            CRM_ProductTranslation_RecVersion = product.ProductTranslationRecVersion,
                            CRM_ProductOV_RecVersion = product.ProductOvRecVersion,
                            CRM_ProductItem_RecVersion = product.ProductItemRecVersion,
                            CRM_Erp_RecVersion = product.ErpRecVersion,
                            Discount = product.Desconto__c,
                            Price = product.Preco_Publico__c,
                            Type = product.Type,
                            checkHierarchyChanges = product.checkHierarchyChanges,
                            Product = 0,
                            Stopped = false,
                            Integrated = integrated,
                            StoppedQuotation = false
                        };

                        await _repository.InsertItensInLog(_mapper.Map<ItensIntegrationLog>(itensIntegrationViewModel));

                        return idSalesForce;
                    }
                    else
                    {
                        _logger.LogInformation("Falha na aplicacao ov-api-maintenance, classe ItensIntegrationService, metodo CreateProduct, para o product {product}, dando a mensagem {message}", JsonConvert.SerializeObject(product), resultObject.First["message"].ToString());

                        return resultObject.First["message"].ToString() + " - " + resultObject.First["errorCode"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Erro na aplicacao ov-api-maintenance, classe ItensIntegrationService, metodo CreateProduct, para o product {product}, dando a mensagem {message}", JsonConvert.SerializeObject(product), e.Message);

                return e.Message;
            }
        }

        private async Task<string> CreateItensProduct(ProductSalesForceViewModel product)
        {
            string token = _salesForceAuthentitcation.GetTokenAuthentication().Result;

            return await CreateItensProduct(product, token);
        }

        private async Task<string> CreateItensProduct(ProductSalesForceViewModel product, string token)
        {
            try
            {
                if (product.Configurations.Count == 0) return string.IsNullOrEmpty(product.IdSalesForce) ? await CreateProduct(product, token) : await UpdateProduct(product, token);
                else
                {
                    foreach (var item in product.Configurations)
                    {
                        try
                        {
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri(_appSettings.SalesForce.Url + "Product2")
                            };

                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                            var RecordTypeId = !product.Tax ? product.RecordTypeId : _appSettings.SalesForce.OperacionalService_RecordTypeId;

                            var content = new StringContent(JsonConvert.SerializeObject(new
                            {
                                Name = product.Name + " " + item.MarcaNome + " " + item.Tamanho + " " + item.PartNumber,
                                product.ProductCode,
                                product.Description,
                                product.IsActive,
                                product.Family,
                                RecordTypeId,
                                product.Desconto__c,
                                product.Itens_de_serie_c,
                                product.Preco_Publico__c,
                                product.Subcategoria__c,
                                product.TipoCarroceria__c,
                                Configuracao__c = item.MarcaId,
                                Tamanho__c = item.Tamanho,
                                Unidade__c = product.Unidade

                            }), Encoding.UTF8, "application/json");

                            var response = await client.PostAsync(_appSettings.SalesForce.Url + "Product2", content);

                            if (response.StatusCode == HttpStatusCode.Created)
                            {
                                var resultObject = JToken.Parse(await response.Content.ReadAsStringAsync()).Root["id"];

                                var itensIntegrationLogViewModel = new ItensIntegrationLogViewModel()
                                {
                                    IdSalesForce = resultObject.Root["id"].ToString(),
                                    CRMNumeroItem = product.ProductCode,
                                    CreatedDateTime = DateTime.Now,
                                    CRM_Product_RecVersion = product.ProductRecVersion,
                                    CRM_ProductTranslation_RecVersion = product.ProductTranslationRecVersion,
                                    CRM_ProductOV_RecVersion = product.ProductOvRecVersion,
                                    CRM_ProductItem_RecVersion = product.ProductItemRecVersion,
                                    CRM_Erp_RecVersion = product.ErpRecVersion,
                                    Discount = product.Desconto__c,
                                    Price = product.Preco_Publico__c,
                                    Type = product.Type,
                                    checkHierarchyChanges = product.checkHierarchyChanges,
                                    Product = item.Product,
                                    Configuration = item.MarcaId,
                                    PartNumber = item.PartNumber,
                                    Tamanho = item.Tamanho,
                                    Stopped = false,
                                    StoppedQuotation = false,
                                    Integrated = true
                                };

                                await _repository.InsertItensInLog(_mapper.Map<ItensIntegrationLog>(itensIntegrationLogViewModel));
                            }
                            else
                            {
                                string idSalesForce = "Erro ao Integrar";
                                bool integrated = false;
                                var resultObject = JToken.Parse(await response.Content.ReadAsStringAsync());

                                if (resultObject.First["message"].ToString().Contains("duplicates"))
                                {
                                    string[] splitMsg = resultObject.First["message"].ToString().Split("id: ");
                                    idSalesForce = splitMsg[1];
                                    integrated = true;

                                    var itensIntegrationLogViewModel = new ItensIntegrationLogViewModel()
                                    {
                                        IdSalesForce = idSalesForce,
                                        CRMNumeroItem = product.ProductCode,
                                        CreatedDateTime = DateTime.Now,
                                        CRM_Product_RecVersion = product.ProductRecVersion,
                                        CRM_ProductTranslation_RecVersion = product.ProductTranslationRecVersion,
                                        CRM_ProductOV_RecVersion = product.ProductOvRecVersion,
                                        CRM_ProductItem_RecVersion = product.ProductItemRecVersion,
                                        CRM_Erp_RecVersion = product.ErpRecVersion,
                                        Discount = product.Desconto__c,
                                        Price = product.Preco_Publico__c,
                                        Type = product.Type,
                                        checkHierarchyChanges = product.checkHierarchyChanges,
                                        Product = item.Product,
                                        Integrated = integrated,
                                        Configuration = item.MarcaId,
                                        PartNumber = item.PartNumber,
                                        Tamanho = item.Tamanho,
                                        Stopped = false,
                                        StoppedQuotation = false
                                    };

                                    await _repository.InsertItensInLog(_mapper.Map<ItensIntegrationLog>(itensIntegrationLogViewModel));
                                }
                                _logger.LogInformation("Falha na aplicacao ov-api-maintenance, classe ItensIntegrationService, metodo CreateItensProduct, para o product {product}, dando a mensagem {message}", JsonConvert.SerializeObject(product), resultObject.First["message"].ToString());

                                //return resultObject.First["message"].ToString() + " - " + resultObject.First["errorCode"].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("Erro na aplicacao ov-api-maintenance, classe ItensIntegrationService, metodo CreateItensProduct, para o product {product}, dando a mensagem {message}", JsonConvert.SerializeObject(product), ex.Message);
                        }
                    }
                }
                return "Sucesso";
            }
            catch (Exception e)
            {
                _logger.LogError("Erro na aplicacao ov-api-maintenance, classe ItensIntegrationService, metodo CreateItensProduct, para o product {product}, dando a mensagem {message}", JsonConvert.SerializeObject(product), e.Message);

                return e.Message;
            }
        }

        private async Task<string> UpdateProduct(ProductSalesForceViewModel product)
        {
            string token = _salesForceAuthentitcation.GetTokenAuthentication().Result;

            var client = new HttpClient
            {
                BaseAddress = new Uri(_appSettings.SalesForce.Url + "Product2")
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var RecordTypeId = !product.Tax ? product.RecordTypeId : _appSettings.SalesForce.OperacionalService_RecordTypeId;

            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                product.Name,
                product.ProductCode,
                product.Description,
                product.IsActive,
                product.Family,
                RecordTypeId,
                product.Desconto__c,
                product.Itens_de_serie_c,
                product.Preco_Publico__c,
                product.Subcategoria__c,
                product.TipoCarroceria__c,
                product.Configuracao__c,
                product.Tamanho__c,
                Unidade__c = product.Unidade
            }), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(_appSettings.SalesForce.Url + "Product2", content);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                var resultObject = JToken.Parse(await response.Content.ReadAsStringAsync()).Root["id"];

                var itensIntegrationViewModel = new ItensIntegrationLogViewModel()
                {
                    IdSalesForce = resultObject.Root["id"].ToString(),
                    CRMNumeroItem = product.ProductCode,
                    CreatedDateTime = DateTime.Now,
                    CRM_Product_RecVersion = product.ProductRecVersion,
                    CRM_ProductTranslation_RecVersion = product.ProductTranslationRecVersion,
                    CRM_ProductOV_RecVersion = product.ProductOvRecVersion,
                    CRM_ProductItem_RecVersion = product.ProductItemRecVersion,
                    CRM_Erp_RecVersion = product.ErpRecVersion,
                    Discount = product.Desconto__c,
                    Price = product.Preco_Publico__c,
                    Type = product.Type,
                    checkHierarchyChanges = product.checkHierarchyChanges,
                    Product = 0,
                    Stopped = false,
                    StoppedQuotation = false
                };

                await _repository.InsertItensInLog(_mapper.Map<ItensIntegrationLog>(itensIntegrationViewModel));

                return resultObject.ToString();
            }
            else
            {
                string idSalesForce = "Erro ao Integrar";
                bool integrated = false;
                var resultObject = JToken.Parse(await response.Content.ReadAsStringAsync());

                if (resultObject.First["message"].ToString().Contains("duplicates"))
                {
                    string[] splitMsg = resultObject.First["message"].ToString().Split("id: ");
                    idSalesForce = splitMsg[1];
                    integrated = true;

                    var itensIntegrationViewModel = new ItensIntegrationLogViewModel()
                    {
                        IdSalesForce = idSalesForce,
                        CRMNumeroItem = product.ProductCode,
                        CreatedDateTime = DateTime.Now,
                        CRM_Product_RecVersion = product.ProductRecVersion,
                        CRM_ProductTranslation_RecVersion = product.ProductTranslationRecVersion,
                        CRM_ProductOV_RecVersion = product.ProductOvRecVersion,
                        CRM_ProductItem_RecVersion = product.ProductItemRecVersion,
                        CRM_Erp_RecVersion = product.ErpRecVersion,
                        Discount = product.Desconto__c,
                        Price = product.Preco_Publico__c,
                        Type = product.Type,
                        checkHierarchyChanges = product.checkHierarchyChanges,
                        Product = 0,
                        Stopped = false,
                        Integrated = integrated,
                        StoppedQuotation = false
                    };

                    await _repository.InsertItensInLog(_mapper.Map<ItensIntegrationLog>(itensIntegrationViewModel));

                    return idSalesForce;
                }
                else
                {
                    _logger.LogInformation("Falha na aplicacao ov-api-maintenance, classe ItensIntegrationService, metodo CreateProduct, para o product {product}, dando a mensagem {message}", JsonConvert.SerializeObject(product), resultObject.First["message"].ToString());

                    return resultObject.First["message"].ToString() + " - " + resultObject.First["errorCode"].ToString();
                }
            }
        }

        private async Task<string> UpdateProduct(List<ProductSalesForceViewModel> products)
        {
            string result = "";

            foreach (var product in products)
            {
                await UpdateProduct(product);
            }

            return result;
        }

        private async Task<string> UpdateItensProductActived(ItensIntegrationLogViewModel product)
        {
            string token = _salesForceAuthentitcation.GetTokenAuthentication().Result;

            return await UpdateItensProductActived(product, token);
        }

        private async Task<string> UpdateItensProductActived(ItensIntegrationLogViewModel product, string token)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(_appSettings.SalesForce.Url + "Product2")
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(JsonConvert.SerializeObject(new
                {
                    IsActive = !product.Stopped,

                }), Encoding.UTF8, "application/json");

                var response = await client.PatchAsync(_appSettings.SalesForce.Url + "Product2/" + product.IdSalesForce, content);

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    /*
                    await _genericMethods.LogOnBizTalk("Atualizado o Produto no Sales Force", product.IdSalesForce + " - Criado com sucesso", JsonConvert.SerializeObject(product), true);

                    var oldLog = await _repository.GetItensInLog(product.IdSalesForce);
                    oldLog.ModifiedDateTime = DateTime.Now;
                    oldLog.Stopped = product.Stopped;
                    await _repository.UpdateItensLog(oldLog);

                    return product.IdSalesForce;
                    */
                }
                else
                {
                    var resultObject = JToken.Parse(await response.Content.ReadAsStringAsync());
                    _logger.LogInformation("Falha na aplicacao ov-api-maintenance, classe ItensIntegrationService, metodo UpdateItensProductActived, para o product {product}, dando a mensagem {message}", JsonConvert.SerializeObject(product), resultObject.First["message"].ToString() + " - " + resultObject.First["errorCode"].ToString());

                    return resultObject.First["message"].ToString() + " - " + resultObject.First["errorCode"].ToString();
                }

                return null;
            }
            catch (Exception e)
            {
                //await _genericMethods.LogOnBizTalk("Atualização do Produto Sales Force", "Erro ao atualizar o produto " + e.Message, JsonConvert.SerializeObject(product), false);
                _logger.LogError("Erro na aplicacao ov-api-maintenance, classe ItensIntegrationService, metodo UpdateItensProductActived, para o product {product}, dando a mensagem {message}", JsonConvert.SerializeObject(product), e.Message);

                return e.Message;
            }
        }

        #endregion
    }
}
