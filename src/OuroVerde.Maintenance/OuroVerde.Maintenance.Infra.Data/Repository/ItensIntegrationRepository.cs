using OuroVerde.Maintenance.Domain.Adapters.Repository;
using OuroVerde.Maintenance.Domain.Model;
using OuroVerde.Maintenance.Infra.Data.Context;
using OuroVerde.Maintenance.Infra.Data.Repository;

namespace Unidas.MS.Maintenance.Infra.Data.Repository
{
    public class ItensIntegrationRepository : RepositoryBase<ItensIntegrationLog>, IItensIntegrationRepository
    {
        protected new readonly SensediaContext _context;

        public ItensIntegrationRepository(SensediaContext context) : base(context)
        {
            _context = context;
        }

        public Task<string> GetBusinessArea(string AMItemMajorGroupId, string AMItemMinorGroupId, string ItemGroupId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductSalesForce>> GetCommercialServicesToSend()
        {
            throw new NotImplementedException();
        }

        public Task<List<ItensIntegrationLog>> getIntegratedProducts()
        {
            throw new NotImplementedException();
        }

        public Task<ItensIntegrationLog> GetItensInLog(string IdSalesForce)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceIntegrationLog> GetServicesInLog(string IdSalesForce)
        {
            throw new NotImplementedException();
        }

        public Task InsertItensInLog(ItensIntegrationLog log)
        {
            throw new NotImplementedException();
        }

        public Task InsertServicesInLog(ServiceIntegrationLog log)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductSalesForce>> SendItemToProducts(bool tax = false)
        {
            throw new NotImplementedException();
        }

        public Task<List<ItensIntegrationLog>> sendItensActived()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductSalesForce>> SendOperationalServicesToProduct()
        {
            throw new NotImplementedException();
        }

        public Task UpdateItensLog(ItensIntegrationLog log)
        {
            throw new NotImplementedException();
        }

        public Task UpdateServicesLog(ServiceIntegrationLog log)
        {
            throw new NotImplementedException();
        }
    }
}
