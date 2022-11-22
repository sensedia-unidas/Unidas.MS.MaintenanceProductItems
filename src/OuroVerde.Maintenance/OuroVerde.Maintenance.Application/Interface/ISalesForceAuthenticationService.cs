namespace Unidas.MS.Maintenance.Application.Interface
{
    public interface ISalesForceAuthenticationService
    {
        Task<string> GetTokenAuthentication();

    }
}
