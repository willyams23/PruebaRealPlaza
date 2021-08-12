﻿namespace Prueba.Service.Api.Models.ResponseBase
{
    public class ApiOkResponse : ApiResponse
    {
        public ApiOkResponse(string idTransaction, object data)
        {
            this.TransactionId = idTransaction;
            this.Data = data;
        }
    }
}
