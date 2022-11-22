using OuroVerde.Maintenance.Domain.Core.Domain;
using System;

namespace OuroVerde.Maintenance.Domain.Model
{
    public class ServiceIntegrationLog: Entity
    {
        public ServiceIntegrationLog()
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
