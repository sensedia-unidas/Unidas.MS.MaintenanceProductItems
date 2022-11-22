using Newtonsoft.Json;

namespace Unidas.MS.Maintenance.Application.ViewModel
{
    public class SalesForceAuthenticationViewModel
    {
        public SalesForceAuthenticationViewModel()
        {

        }

        [JsonProperty("urlSF")]
        public string UrlSF { get; set; }

        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        [JsonProperty("clientSecret")]
        public string ClientSecret { get; set; }

        [JsonProperty("userName")]
        public string  UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
