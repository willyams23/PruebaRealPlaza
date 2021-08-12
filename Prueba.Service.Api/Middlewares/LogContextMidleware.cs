using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prueba.Infrastructure.Crosscutting;

namespace Prueba.Service.Api.Middlewares
{
    public class LogContextMidleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogContextMidleware> _logger;

        public LogContextMidleware(RequestDelegate next, ILogger<LogContextMidleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var traceContext= GetTraceContext(context.Request);
            using (_logger.BeginScope(traceContext))
            {
                _logger.LogInformation("Inicio de servicio");

                await _next.Invoke(context);

                _logger.LogInformation("Fin de servicio");
            }
        }

        private Dictionary<string,object> GetTraceContext(HttpRequest request)
        {
            var valuesTrace = new Dictionary<string, object>();
            var country_code = request.Headers["country-code"].ToString();
            var terminal = request.HttpContext.Connection.RemoteIpAddress.ToString();
            var transaction_id = request.Headers["transaction-id"].ToString();
            var application_user = request.Headers["application-user"].ToString();
            var application_code = request.Headers["application-code"].ToString();
            var client_version = request.Headers["client-version"].ToString();

            if (string.IsNullOrEmpty(country_code))
            {
                country_code = string.Empty;
            }

            if (string.IsNullOrEmpty(transaction_id))
            {
                transaction_id = Guid.NewGuid().ToString();
            }

            HttpAuxiliaryContext.SetCurrentTransaction(transaction_id);

            valuesTrace.Add("country_code", country_code);
            valuesTrace.Add("terminal", terminal);
            valuesTrace.Add("application_user", application_user);
            valuesTrace.Add("application_code", application_code);
            valuesTrace.Add("client_version", client_version);
            valuesTrace.Add("transaction_id", transaction_id);

            return valuesTrace;
        }
    }
}
