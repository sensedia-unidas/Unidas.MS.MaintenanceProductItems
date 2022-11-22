using OuroVerde.Maintenance.Domain.Core.Domain;
using System.Diagnostics.Eventing.Reader;

namespace OuroVerde.Maintenance.Domain.Model
{
    public class ItensIntegrationLog: Entity
    {
        public ItensIntegrationLog(int id,
            string idSalesForce,
            string crmNumeroItem,
            DateTime createdDateTime,
            DateTime? modifiedDateTime,
            int crm_Product_RecVersion,
            int crm_ProductOV_RecVersion,
            int crm_ProductTranslation_RecVersion,
            int crm_ProductItem_RecVersion,
            decimal discount,
            decimal price,
            int type,
            string checkHierarchyChanges,
            long product,
            string configuration,
            string partNumber,
            string tamanho,
            bool stopped,
            bool stoppedQuotation,
            bool integrated
            )
        {
            Id = id;
            IdSalesForce = idSalesForce;
            CRMNumeroItem = crmNumeroItem;
            CreatedDateTime = createdDateTime;
            ModifiedDateTime = modifiedDateTime;
            CRM_Product_RecVersion = crm_Product_RecVersion;
            CRM_ProductOV_RecVersion = crm_ProductOV_RecVersion;
            CRM_ProductTranslation_RecVersion = crm_ProductTranslation_RecVersion;
            CRM_ProductItem_RecVersion = crm_ProductItem_RecVersion;
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
