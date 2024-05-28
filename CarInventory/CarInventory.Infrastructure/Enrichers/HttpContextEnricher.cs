using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace CarInventory.Api.Util.Enrichers
{
    public class HttpContextEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var httpContext = GetHttpContext();
            if (httpContext != null)
            {
                var traceId = httpContext.TraceIdentifier;
                var traceIdProperty = new LogEventProperty("TraceIdentifier", new ScalarValue(traceId));
                logEvent.AddPropertyIfAbsent(traceIdProperty);
            }
        }

        private static HttpContext GetHttpContext()
        {
            return new HttpContextAccessor().HttpContext;
        }
    }
}
