using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using Prueba.Infrastructure.Crosscutting;

namespace Prueba.Service.Api.Models.ResponseBase
{
    public class ApiResponse
    {
        [JsonPropertyName("data")]
        public object Data { get; set; }

        [JsonPropertyName("errors")]
        public List<ApiError> Errors { get; set; }

        [JsonPropertyName("transaction_id")]
        public string TransactionId { get; set; }

        public ApiResponse()
        {
            TransactionId = HttpAuxiliaryContext.GetCurrentTransactionId();
        }
    }
}
