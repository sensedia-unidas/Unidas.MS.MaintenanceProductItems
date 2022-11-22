using Newtonsoft.Json;

namespace OuroVerde.Maintenance.Domain.Model
{
    public class ItensIntegrationLogViewModel
    {
        public ItensIntegrationLogViewModel()
        {

        }

        [JsonProperty("id")]
        public int Id { get; private set; }

        [JsonProperty("idSalesForce")]
        public string IdSalesForce { get; set; }

        [JsonProperty("crmNumeroItem")]
        public string CRMNumeroItem { get; set; }

        [JsonProperty("createdDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [JsonProperty("modifiedDateTime")]
        public DateTime? ModifiedDateTime { get; set; }

        [JsonProperty("crm_product_recVersion")]
        public int CRM_Product_RecVersion { get; set; }

        [JsonProperty("crm_productOv_recVersion")]
        public int CRM_ProductOV_RecVersion { get; set; }

        [JsonProperty("crm_productTranslation_recVersion")]
        public int CRM_ProductTranslation_RecVersion { get; set; }

        [JsonProperty("crm_productItem_recVersion")]
        public int CRM_ProductItem_RecVersion { get; set; }

        [JsonProperty("crm_erp_recVersion")]
        public int CRM_Erp_RecVersion { get; set; }

        [JsonProperty("discount")]
        public decimal Discount { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("checkHierarchyChanges")]
        public string checkHierarchyChanges { get; set; }

        [JsonProperty("product")]
        public long Product { get; set; }

        [JsonProperty("configuration")]
        public string Configuration { get; set; }

        [JsonProperty("partNumber")]
        public string PartNumber { get; set; }

        [JsonProperty("tamanho")]
        public string Tamanho { get; set; }

        [JsonProperty("stopped")]
        public bool Stopped { get; set; }

        [JsonProperty("stoppedQuotation")]
        public bool StoppedQuotation { get; set; }

        [JsonProperty("integrated")]
        public bool Integrated { get; set; }
    }
}
