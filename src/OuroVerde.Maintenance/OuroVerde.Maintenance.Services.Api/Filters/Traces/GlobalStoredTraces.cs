using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace OuroVerde.Maintenance.Services.Api.Filters.Traces;

/// <summary>
/// Classe para preparação de um Storage Space 
/// para Raw Body
/// </summary>
public class GlobalStoredTraces
{
    public static readonly ConcurrentDictionary<string, GlobalStoredTraces> CurrentTraces = new ConcurrentDictionary<string, GlobalStoredTraces>();

    public string Id { get; private set; }
    public string Body { get; set; }
    public bool HasBody => !String.IsNullOrEmpty(Body);

    public static readonly GlobalStoredTraces Empty = new GlobalStoredTraces();

    public static GlobalStoredTraces AddRequestTrace(HttpContext context)
    {
        var trace = new GlobalStoredTraces()
        {
            Id = context.TraceIdentifier,
            Body = GetHttpRequest(context),
        };
        CurrentTraces.TryAdd($"Request_{trace.Id}", trace);
        return trace;
    }

    public static GlobalStoredTraces GetRequestTrace(string id)
    {
        if (CurrentTraces.TryRemove($"Request_{id}", out var trace))
            return trace;
        else
            return GlobalStoredTraces.Empty;
    }

    public static GlobalStoredTraces AddResponseTrace(ResultExecutedContext context)
    {
        var trace = new GlobalStoredTraces()
        {
            Id = context.HttpContext.TraceIdentifier,
            Body = GetHttpResult(context.Result),
        };
        CurrentTraces.TryAdd($"Response_{trace.Id}", trace);
        return trace;
    }

    public static GlobalStoredTraces GetResponseTrace(string id)
    {
        if (CurrentTraces.TryRemove($"Response_{id}", out var trace))
            return trace;
        else
            return GlobalStoredTraces.Empty;
    }

    private static string GetHttpRequest(HttpContext context)
    {
        string body;

        if (context?.Request?.Body == null) return string.Empty;
        if (!context.Request.Body.CanRead) return string.Empty;
        if (!context.Request.Body.CanSeek) return string.Empty;

        context.Request.Body.Position = 0;
        using (var reader = new System.IO.StreamReader(context.Request.Body))
        {
            body = reader.ReadToEnd();
        }

        return body;
    }

    private static string GetHttpResult(Microsoft.AspNetCore.Mvc.IActionResult result)
    {
        var objectResult = result as Microsoft.AspNetCore.Mvc.ObjectResult;

        if (objectResult != null)
            return JsonConvert.SerializeObject(objectResult.Value);
        else
            return JsonConvert.SerializeObject(result);
    }
}