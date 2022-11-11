namespace OuroVerde.Maintenance.Domain.Model
{
    public class ItensIntegrationLogViewModel
    {
        public ItensIntegrationLogViewModel()
        {

        }

        public int Id { get; private set; }
        public string IdSalesForce { get; private set; }
        public string CRMNumeroItem { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
        public DateTime? ModifiedDateTime { get; private set; }
        public int CRM_Product_RecVersion { get; private set; }
        public int CRM_ProductOV_RecVersion { get; private set; }
        public int CRM_ProductTranslation_RecVersion { get; private set; }
        public int CRM_ProductItem_RecVersion { get; private set; }
        public int CRM_Erp_RecVersion { get; private set; }
        public decimal Discount { get; private set; }
        public decimal Price { get; private set; }
        public int Type { get; private set; }
        public string checkHierarchyChanges { get; private set; }
        public long Product { get; private set; }
        public string Configuration { get; private set; }
        public string PartNumber { get; private set; }
        public string Tamanho { get; private set; }
        public bool Stopped { get; private set; }
        public bool StoppedQuotation { get; private set; }
        public bool Integrated { get; private set; }
    }
}
