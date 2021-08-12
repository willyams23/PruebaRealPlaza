using System;

namespace Prueba.Service.Web.Models.Tokens
{
    public class GenerateTokenResponseModel
    {
        public string access_token { get; set; }

        public string token_type { get; set; }

        public DateTime expiration_time { get; set; }

        public string transaction_id { get; set; }
    }
}
