using Newtonsoft.Json;
using System;

namespace OuroVerde.Maintenance.Domain.Model
{
    public class ServiceIntegrationLogViewModel
    {
        public ServiceIntegrationLogViewModel()
        {

        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("idSalesForce")]
        public string IdSalesForce { get; set; }

        [JsonProperty("crmNumeroItem")]
        public string CRMNumeroItem { get; set; }

        [JsonProperty("createdDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [JsonProperty("modifiedDateTime")]
        public DateTime? ModifiedDateTime { get; set; }

        [JsonProperty("serviceType")]
        public string ServiceType { get; set; }

        [JsonProperty("stopped")]
        public bool Stopped { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }
    }
}
