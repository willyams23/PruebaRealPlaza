using System;

namespace Prueba.Service.Api.Models.Tokens
{
    public class GenerateTokenResponseModelFactory
    {
        public static GenerateTokenResponseModel Create(string access_token, DateTime expiration_time, 
            string token_type, string transaction_id)
        {
            return new GenerateTokenResponseModel
            {
                access_token = access_token,
                expiration_time = expiration_time,
                token_type = token_type,
                transaction_id = transaction_id
            };
        }
    }
}
