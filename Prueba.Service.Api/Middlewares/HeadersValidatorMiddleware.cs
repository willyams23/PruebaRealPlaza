using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prueba.Infrastructure.Crosscutting;
using Prueba.Infrastructure.Crosscutting.ExceptionsTypes;

namespace Prueba.Service.Api.Middlewares
{
    public class HeadersValidatorMiddleware
    {
        private readonly RequestDelegate next;

        public HeadersValidatorMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            ValidateHeadersParams(context.Request);
            await next.Invoke(context);
        }

        private void ValidateHeadersParams(HttpRequest request)
        {
            var customHeaders = Utils.GetCustomHeaders(request);

            var country_code = customHeaders.CountryCode;
            var transaction_id = customHeaders.TransactionId;
            var client_version = customHeaders.ClientVersion;

            var errors = new List<string>();

            if (string.IsNullOrEmpty(country_code))
                errors.Add(string.Format(Constants.Labels.HeadersMandatory, Constants.Headers.CountryCode));
            else if (country_code.Length > 2)
                errors.Add(string.Format(Constants.Labels.HeadersMaxLength, Constants.Headers.CountryCode, "2"));

            if (string.IsNullOrEmpty(client_version))
                errors.Add(string.Format(Constants.Labels.HeadersMandatory, Constants.Headers.ClientVersion));
            else if (client_version.Length > 10)
                errors.Add(string.Format(Constants.Labels.HeadersMaxLength, Constants.Headers.ClientVersion, "10"));

            Guid transaction_id_guid = Guid.Empty;
            if (!Guid.TryParse(transaction_id, out transaction_id_guid))
                errors.Add(string.Format(Constants.Labels.HeadersMandatory, Constants.Headers.TransactionId));

            if (errors.Count > 0)
                ExceptionManager.GenerarAppExcepcionValidacion(Constants.Labels.HeadersMissing, errors);

        }
    }
}
