using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using OuroVerde.Maintenance.Domain.Core.Domain;
using System.Net;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace OuroVerde.Maintenance.Services.Api.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment env;
        private readonly ILogger logger;

        public HttpGlobalExceptionFilter(IHostingEnvironment env, ILogger logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            if (context.Exception is DomainException businessException)
            {
                var json = new JsonError
                {
                    errorCode = businessException.ErrorCode,
                    errorMessage = context.Exception.Message,
                    remoteAddress = context.HttpContext.Request.Host.Host,
                    path = context.HttpContext.Request.Path.ToString()
                };

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                var errorCode = (context.Exception is DomainException) ? ((DomainException)context.Exception).ErrorCode : string.Empty;

                var json = new JsonError
                {
                    errorCode = errorCode,
                    errorMessage = context.Exception.Message,
                    remoteAddress = context.HttpContext.Request.Host.Host,
                    path = context.HttpContext.Request.Path.ToString()
                };

                if (env.IsDevelopment())
                {
                    json.DeveloperMessage = context.Exception;
                }

                if (errorCode.ToUpper() == "INT-1"
                || errorCode.ToUpper() == "CVT-4")
                {
                    context.Result = new BadRequestObjectResult(json);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
                else
                {
                    context.Result = new InternalServerErrorObjectResult(json);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
            }
            context.ExceptionHandled = true;
        }

        public class JsonErrorResponse
        {
            public string[] Messages { get; set; }

            public object DeveloperMessage { get; set; }
        }

        public class JsonError
        {
            public string errorCode { get; set; }
            public string errorMessage { get; set; }
            public string remoteAddress { get; set; }
            public string path { get; set; }
            public object DeveloperMessage { get; set; }
        }
    }
}
