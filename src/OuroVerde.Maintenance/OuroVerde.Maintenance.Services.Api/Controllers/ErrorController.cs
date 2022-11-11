using Microsoft.AspNetCore.Mvc;
using OuroVerde.Maintenance.Infra.CrossCutting.Shared;

namespace OuroVerde.Maintenance.Services.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorController : ApiController
    {
        [Route("error")]
        public ErrorResponse Error()
        {
            return new ErrorResponse();
        }
    }
}
