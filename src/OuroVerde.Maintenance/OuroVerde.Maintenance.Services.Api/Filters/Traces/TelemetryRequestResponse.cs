using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using OuroVerde.Maintenance.Services.Api.Filters.Traces;

namespace Unidas.MS.Maintenance.Services.Api.Filters.Traces
{
    public class TelemetryRequestResponse : ITelemetryInitializer
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TelemetryRequestResponse(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Initialize(ITelemetry telemetry)
        {
            var requestTelemetry = telemetry as RequestTelemetry;

            if (requestTelemetry != null)
            {
                string id = _httpContextAccessor.HttpContext.TraceIdentifier;
                var request = GlobalStoredTraces.GetRequestTrace(id);
                var response = GlobalStoredTraces.GetResponseTrace(id);
                requestTelemetry.Properties.Add("requestBody", request.Body);
                requestTelemetry.Properties.Add("responseBody", response.Body);
            }
        }
    }
}
