using System.Collections.Generic;

namespace Prueba.Service.Web.Models.ResponseBase
{
    public class ApiResponseFactory
    {
        public static ApiResponse CreateOk(string idTransaction, object data)
        {
            return new ApiOkResponse(idTransaction, data);
        }

        public static ApiResponse CreateBadRequest(string idTransaction, string codigo, string error)
        {
            return new ApiBadRequestResponse(idTransaction, codigo, error);
        }

        public static ApiResponse CreateBadRequest(string idTransaction, List<ApiError> errors)
        {
            return new ApiBadRequestResponse(idTransaction, errors);
        }

        public static ApiResponse CreateUnauthorized(string idTransaction, List<ApiError> errors)
        {
            return new ApiBadRequestResponse(idTransaction, errors);
        }
    }
}
