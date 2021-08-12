using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.Service.Api
{
    public class MessagesHttpStatusCode
    {
        public static string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Successful operation",
                201 => "Successful operation. Resource Created",
                400 => "Bad Request (the parameter sent are invalid)",
                401 => "Unauthorized",
                404 => "Resource not found",
                405 => "Method Not allowed",
                406 => "Content Type Are not Allowed",
                415 => "Content Type is wrong",
                500 => "Internal error server",
                _ => null,
            };
        }
    }
}
