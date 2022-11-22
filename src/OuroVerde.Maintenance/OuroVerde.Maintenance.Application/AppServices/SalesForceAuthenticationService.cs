using Microsoft.Extensions.Configuration;
using System.Net;
using Unidas.MS.Maintenance.Application.Interface;
using Unidas.MS.Maintenance.Application.ViewModel;

namespace Unidas.MS.Maintenance.Application.AppServices
{
    public class SalesForceAuthenticationService : ISalesForceAuthenticationService
    {
        private readonly IConfiguration _configuration;

        public SalesForceAuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetTokenAuthentication()
        {
            return await Task.Run(() =>
            {
                var data = new SalesForceAuthenticationViewModel
                {
                    ClientId = _configuration.GetSection("ClientId").Value,
                    ClientSecret = _configuration.GetSection("ClientSecret").Value,
                    UserName = _configuration.GetSection("UserName").Value,
                    Password = _configuration.GetSection("Password").Value
                };

                var result = new HttpClient().GetAsync(_configuration.GetSection("UrlSF").Value).Result;
                return result.Content.ReadAsStringAsync().Result;
            });            
        }
    }
}
