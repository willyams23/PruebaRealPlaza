using System.Collections.Generic;

namespace Prueba.Service.Api.Models.ResponseBase
{
    public class ApiNotFoundRequestResponse : ApiResponse
    {
        public ApiNotFoundRequestResponse(string codigo, string error)
        {
            Errors = new List<ApiError>() { new ApiError { Codigo = codigo, Mensaje = error } };
        }
    }
}
