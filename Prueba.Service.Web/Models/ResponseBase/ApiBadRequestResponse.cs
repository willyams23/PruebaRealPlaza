using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prueba.Service.Web.Models.ResponseBase
{
    public class ApiBadRequestResponse : ApiResponse
    {
        public ApiBadRequestResponse(string idTransaction, List<ApiError> errors)
        {
            TransactionId = idTransaction;
            Errors = errors;
        }

        public ApiBadRequestResponse(string idTransaction, string codigo, string error)
        {
            TransactionId = idTransaction;
            Errors = new List<ApiError>()
            {
                new ApiError { Codigo = codigo, Mensaje = error }
            };
        }

        public ApiBadRequestResponse(ModelStateDictionary modelState)
        {
            if (modelState.IsValid)
            {
                throw new ArgumentException("ModelState must be invalid", nameof(modelState));
            }

            Errors = modelState.SelectMany(x => x.Value.Errors)
                .Select(x => new ApiError { Codigo = string.Empty, Mensaje = x.ErrorMessage }).ToList();
        }
    }
}
