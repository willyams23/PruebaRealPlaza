using System.Collections.Generic;

namespace Prueba.Service.Web.Models.ResponseBase
{
    public class ApiErrorResponse : ApiResponse
    {
        public ApiErrorResponse(string codigo, string error)
        {
            Errors = new List<ApiError>() { new ApiError { Codigo = codigo, Mensaje = error } };
        }
    }
}
