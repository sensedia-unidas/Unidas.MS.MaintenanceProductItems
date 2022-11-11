using System;

namespace OuroVerde.Maintenance.Domain.Model
{
    public class ServiceIntegrationLogViewModel
    {
        public ServiceIntegrationLogViewModel()
        {

        }

        public int Id { get; set; }
        public string IdSalesForce { get; set; }
        public string CRMNumeroItem { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public string ServiceType { get; set; }
        public bool Stopped { get; set; }
        public int Type { get; set; }
    }
}
