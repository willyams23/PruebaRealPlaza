﻿using System.Text.Json.Serialization;

namespace Prueba.Service.Api.Models.ResponseBase
{
    public class ApiError
    {
        [JsonPropertyName("codigo")]
        public string Codigo { get; set; }
        [JsonPropertyName("mensaje")]
        public string Mensaje { get; set; }
    }
}
