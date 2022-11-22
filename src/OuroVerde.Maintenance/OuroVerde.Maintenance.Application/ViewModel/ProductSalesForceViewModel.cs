using Newtonsoft.Json;
using OuroVerde.Maintenance.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Unidas.MS.Maintenance.Application.ViewModel
{
    public class ProductSalesForceViewModel
    {
        [JsonConstructor]
        public ProductSalesForceViewModel()
        {

        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("productCode")]
        public string ProductCode { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("family")]
        public string Family { get; set; }

        [JsonProperty("itens_de_serie_c")]
        public string Itens_de_serie_c { get; set; }

        [JsonProperty("subCategoria_c")]
        public string Subcategoria__c { get; set; }

        [JsonProperty("")]
        public string TipoCarroceria__c { get; set; }

        [JsonProperty("desconto_c")]
        public decimal Desconto__c { get; set; }

        [JsonProperty("preco_publico_c")]
        public decimal Preco_Publico__c { get; set; }

        [JsonProperty("configuracao_c")]
        public string Configuracao__c { get; set; }

        [JsonProperty("tamanho_c")]
        public string Tamanho__c { get; set; }

        [JsonProperty("recordTypeId")]
        public string RecordTypeId { get; set; }

        [JsonProperty("productRecVersion")]
        public int ProductRecVersion { get; set; }

        [JsonProperty("productTranslationRecVersion")]
        public int ProductTranslationRecVersion { get; set; }

        [JsonProperty("productOvRecVersion")]
        public int ProductOvRecVersion { get; set; }

        [JsonProperty("productItemRecVersion")]
        public int ProductItemRecVersion { get; set; }

        [JsonProperty("productHirarchyRecVersion")]
        public int ProductHierarchyRecVersion { get; set; }

        [JsonProperty("erpRecVersion")]
        public int ErpRecVersion { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("configurationName")]
        public string ConfigurationName { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("partNumber")]
        public string PartNumber { get; set; }

        [JsonProperty("crm_productRecVersion")]
        public int CRM_ProductRecVersion { get; set; }

        [JsonProperty("idSalesForce")]
        public string IdSalesForce { get; set; }

        [JsonProperty("checkHierarchyChanges")]
        public string checkHierarchyChanges { get; set; }

        [JsonProperty("logConfiguration")]
        public string LogConfiguration { get; set; }

        [JsonProperty("unidade")]
        public string Unidade { get; set; }

        [JsonProperty("tax")]
        public bool Tax { get; set; }

        public bool Stopped { get; set; }
        public bool StoppedQuotation { get; set; }

        [ScaffoldColumn(false)]
        public List<EaiVwConsultarItemSubMarca2> Configurations { get; set; }

    }
}
