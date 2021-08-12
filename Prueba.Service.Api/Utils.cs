using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prueba.Infrastructure.Crosscutting;

namespace Prueba.Service.Api
{
    public static class Utils
    {
        public static Dictionary<string, string> GetTraceContext(HttpRequest request)
        {
            var valuesTrace = new Dictionary<string, string>();
            var country_code = request.Headers[Constants.Headers.CountryCode].ToString();
            var terminal = request.HttpContext.Connection.RemoteIpAddress.ToString();
            var transaction_id = request.Headers[Constants.Headers.TransactionId].ToString();
            var client_version = request.Headers[Constants.Headers.ClientVersion].ToString();
            if (string.IsNullOrEmpty(transaction_id))
            {
                transaction_id = Guid.NewGuid().ToString();
                request.Headers[Constants.Headers.TransactionId] = transaction_id;
            }
            valuesTrace.Add(Constants.Headers.CountryCode, country_code);
            valuesTrace.Add(Constants.Headers.Terminal, terminal);
            valuesTrace.Add(Constants.Headers.ClientVersion, client_version);
            valuesTrace.Add(Constants.Headers.TransactionId, transaction_id);
            return valuesTrace;
        }

        public static Headers GetCustomHeaders(HttpRequest request)
        {
            return new Headers
            {
                Authorization = request.Headers[Constants.Headers.Authorization],
                ApplicationUser = request.Headers[Constants.Headers.ApplicationUser],
                ApplicationCode = request.Headers[Constants.Headers.ApplicationCode],
                CountryCode = request.Headers[Constants.Headers.CountryCode],
                ClientVersion = request.Headers[Constants.Headers.ClientVersion],
                Terminal = request.Headers[Constants.Headers.Terminal],
                TransactionId = request.Headers[Constants.Headers.TransactionId]
            };
        }
    }

    public class Headers
    {
        public string CountryCode { get; set; }
        public string TransactionId { get; set; }
        public string ClientVersion { get; set; }
        public string ApplicationCode { get; set; }
        public string ApplicationUser { get; set; }
        public string Authorization { get; set; }
        public string Terminal { get; set; }
    }
}
