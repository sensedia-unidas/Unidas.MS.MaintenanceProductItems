namespace OuroVerde.Maintenance.Domain.Model
{
    public class ProductSalesForceViewModel
    {
        public ProductSalesForceViewModel()
        {

        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public bool IsActive { get; set; }
        public string Family { get; set; }
        public string Itens_de_serie__c { get; set; }
        public string Subcategoria__c { get; set; }
        public string TipoCarroceria__c { get; set; }
        public decimal Desconto__c { get; set; }
        public decimal Preco_Publico__c { get; set; }
        public string Configuracao__c { get; set; }
        public string Tamanho__c { get; set; }
        public string RecordTypeId { get; set; }
        public int ProductRecVersion { get; set; }
        public int ProductTranslationRecVersion { get; set; }
        public int ProductOvRecVersion { get; set; }
        public int ProductItemRecVersion { get; set; }
        public int ProductHierarchyRecVersion { get; set; }
        public int ErpRecVersion { get; set; }
        public int Type { get; set; }
        public string ConfigurationName { get; set; }
        public string Size { get; set; }
        public string PartNumber { get; set; }
        public int CRM_ProductRecVersion { get; set; }
        public string IdSalesForce { get; set; }
        public string checkHierarchyChanges { get; set; }
        public List<EaiVwConsultarItemSubMarca2> Configurations { get; set; }
        public string LogConfiguration { get; set; }
        public string Unidade { get; set; }
        public bool Tax { get; set; }
    }
}
