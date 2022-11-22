using OuroVerde.Maintenance.Domain.Core.Domain;

namespace OuroVerde.Maintenance.Domain.Model
{
    public class ProductSalesForce: Entity
    {
        public ProductSalesForce()
        {

        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ProductCode { get; private set; }
        public bool IsActive { get; private set; }
        public string Family { get; private set; }
        public string Itens_de_serie__c { get; private set; }
        public string Subcategoria__c { get; private set; }
        public string TipoCarroceria__c { get; private set; }
        public decimal Desconto__c { get; private set; }
        public decimal Preco_Publico__c { get; private set; }
        public string Configuracao__c { get; private set; }
        public string Tamanho__c { get; private set; }
        public string RecordTypeId { get; private set; }
        public int ProductRecVersion { get; private set; }
        public int ProductTranslationRecVersion { get; private set; }
        public int ProductOvRecVersion { get; private set; }
        public int ProductItemRecVersion { get; private set; }
        public int ProductHierarchyRecVersion { get; private set; }
        public int ErpRecVersion { get; private set; }
        public int Type { get; private set; }
        public string ConfigurationName { get; private set; }
        public string Size { get; private set; }
        public string PartNumber { get; private set; }
        public int CRM_ProductRecVersion { get; private set; }
        public string IdSalesForce { get; private set; }
        public string checkHierarchyChanges { get; private set; }
        public List<EaiVwConsultarItemSubMarca2> Configurations { get; private set; }
        public string LogConfiguration { get; private set; }
        public string Unidade { get; private set; }
        public bool Tax { get; private set; }
    }
}
