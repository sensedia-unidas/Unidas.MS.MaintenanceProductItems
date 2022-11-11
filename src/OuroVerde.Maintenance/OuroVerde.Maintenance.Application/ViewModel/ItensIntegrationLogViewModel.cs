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
        public string IdSalesForce { get; private set; }

        [JsonProperty("crmNumeroItem")]
        public string CRMNumeroItem { get; private set; }

        [JsonProperty("createdDateTime")]
        public DateTime CreatedDateTime { get; private set; }

        [JsonProperty("modifiedDateTime")]
        public DateTime? ModifiedDateTime { get; private set; }

        [JsonProperty("crm_product_recVersion")]
        public int CRM_Product_RecVersion { get; private set; }

        [JsonProperty("crm_productOv_recVersion")]
        public int CRM_ProductOV_RecVersion { get; private set; }

        [JsonProperty("crm_productTranslation_recVersion")]
        public int CRM_ProductTranslation_RecVersion { get; private set; }

        [JsonProperty("crm_productItem_recVersion")]
        public int CRM_ProductItem_RecVersion { get; private set; }

        [JsonProperty("crm_erp_recVersion")]
        public int CRM_Erp_RecVersion { get; private set; }

        [JsonProperty("discount")]
        public decimal Discount { get; private set; }

        [JsonProperty("price")]
        public decimal Price { get; private set; }

        [JsonProperty("type")]
        public int Type { get; private set; }

        [JsonProperty("checkHierarchyChanges")]
        public string checkHierarchyChanges { get; private set; }

        [JsonProperty("product")]
        public long Product { get; private set; }

        [JsonProperty("configuration")]
        public string Configuration { get; private set; }

        [JsonProperty("partNumber")]
        public string PartNumber { get; private set; }

        [JsonProperty("tamanho")]
        public string Tamanho { get; private set; }

        [JsonProperty("stopped")]
        public bool Stopped { get; private set; }

        [JsonProperty("stoppedQuotation")]
        public bool StoppedQuotation { get; private set; }

        [JsonProperty("integrated")]
        public bool Integrated { get; private set; }
    }
}
