using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OuroVerde.Maintenance.Application.Interface;
using OuroVerde.Maintenance.Domain.Core.Mediator;
using OuroVerde.Maintenance.Domain.Model;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Unidas.MS.Maintenance.Domain.Adapters.Repository.Queue;
using Microsoft.Extensions.Logging;
using Unidas.MS.Maintenance.Infra.CrossCutting.Shared.Common;
using Microsoft.Azure.Amqp.Framing;

namespace Unidas.MS.Maintenance.Application.AppServices
{
    public class ItensIntegrationService : IItensIntegrationService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        private readonly IQueueConnectorAdapter _queueConnectorAdapter;
        private readonly ILogger<ItensIntegrationService> _logger;

        private readonly AppSettings _appSettings;

        public ItensIntegrationService(IMapper mapper,
                                       IMediatorHandler mediator,
                                       IQueueConnectorAdapter queueConnectorAdapter,
                                       ILogger<ItensIntegrationService> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
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

        private async Task<string> CreateItensProduct(ProductSalesForceViewModel product, string token)
        {
            return null;
        }

        private async Task<string> UpdateProduct(ProductSalesForceViewModel product, string token)
        {
            return null;
        }

        private async Task<string> CreateItensProduct(ProductSalesForceViewModel product)
        {
            return null;
        }

        private async Task<string> UpdateProduct(ProductSalesForceViewModel product)
        {
            return null;
        }

        private async Task<string> UpdateItensProductActived(ItensIntegrationLogViewModel product)
        {
            return null;
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
    }
}
