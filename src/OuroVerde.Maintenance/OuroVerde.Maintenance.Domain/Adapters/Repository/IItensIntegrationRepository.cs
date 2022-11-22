using OuroVerde.Maintenance.Domain.Model;

namespace OuroVerde.Maintenance.Domain.Adapters.Repository
{
    public interface IItensIntegrationRepository: IRepositoryBase<ItensIntegrationLog>
    {
        Task<List<ProductSalesForce>> SendOperationalServicesToProduct();
        Task InsertItensInLog(ItensIntegrationLog log);
        Task UpdateItensLog(ItensIntegrationLog log);
        Task<ItensIntegrationLog> GetItensInLog(string IdSalesForce);
        Task<List<ItensIntegrationLog>> getIntegratedProducts();
        Task<string> GetBusinessArea(string AMItemMajorGroupId, string AMItemMinorGroupId, string ItemGroupId);
        Task<List<ProductSalesForce>> SendItemToProducts(bool tax = false);
        Task<List<ProductSalesForce>> GetCommercialServicesToSend();
        Task InsertServicesInLog(ServiceIntegrationLog log);
        Task UpdateServicesLog(ServiceIntegrationLog log);
        Task<ServiceIntegrationLog> GetServicesInLog(string IdSalesForce);
        Task<List<ItensIntegrationLog>> sendItensActived();
    }
}
