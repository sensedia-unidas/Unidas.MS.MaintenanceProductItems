using OuroVerde.Maintenance.Domain.Core.Messaging;
using OuroVerde.Maintenance.Domain.Model;

namespace OuroVerde.Maintenance.Domain.Commands
{
    public abstract class ProductSalesForceCommand: Command
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string ProductCode { get; protected set; }
        public bool IsActive { get; protected set; }
        public string Family { get; protected set; }
        public string Itens_de_serie__c { get; protected set; }
        public string Subcategoria__c { get; protected set; }
        public string TipoCarroceria__c { get; protected set; }
        public decimal Desconto__c { get; protected set; }
        public decimal Preco_Publico__c { get; protected set; }
        public string Configuracao__c { get; protected set; }
        public string Tamanho__c { get; protected set; }
        public string RecordTypeId { get; protected set; }
        public int ProductRecVersion { get; protected set; }
        public int ProductTranslationRecVersion { get; protected set; }
        public int ProductOvRecVersion { get; protected set; }
        public int ProductItemRecVersion { get; protected set; }
        public int ProductHierarchyRecVersion { get; protected set; }
        public int ErpRecVersion { get; protected set; }
        public int Type { get; protected set; }
        public string ConfigurationName { get; protected set; }
        public string Size { get; protected set; }
        public string PartNumber { get; protected set; }
        public int CRM_ProductRecVersion { get; protected set; }
        public string IdSalesForce { get; protected set; }
        public string checkHierarchyChanges { get; protected set; }
        public List<EaiVwConsultarItemSubMarca2> Configurations { get; protected set; }
        public string LogConfiguration { get; protected set; }
        public string Unidade { get; protected set; }
        public bool Tax { get; protected set; }
    }
}
