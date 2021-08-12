using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
//using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Prueba.Infrastructure.Crosscutting;
using Prueba.Infrastructure.Crosscutting.ExceptionsTypes;
using Prueba.Service.Web.Models.ResponseBase;

namespace Prueba.Service.Web.Middlewares
{
    public class HttpStatusCodeResponseMiddleware
    {
        private readonly RequestDelegate next;
        private object response = new object();
        //private readonly ILogger logger;        
        private readonly ILogger<HttpStatusCodeResponseMiddleware> logger;

        public HttpStatusCodeResponseMiddleware(RequestDelegate next, ILogger<HttpStatusCodeResponseMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                var customHeaders = Utils.GetCustomHeaders(context.Request);
                if (ex is AppException)
                {
                    var exApp = (AppException)ex;
                    if (exApp.Tipo == TipoException.ReglaNegocio)
                    {
                        context.Response.StatusCode = Constants.HttpStatusCodes.BadRequest;
                    }
                    else if (exApp.Tipo == TipoException.NoAutorizado)
                    {
                        context.Response.StatusCode = Constants.HttpStatusCodes.Unauthorized;
                    }
                    else if (exApp.Tipo == TipoException.Interna)
                    {
                        logger.LogError(ex, Constants.Labels.OriginError);
                        context.Response.StatusCode = Constants.HttpStatusCodes.InternalServerError;
                    }
                    else
                        context.Response.StatusCode = Constants.HttpStatusCodes.BadRequest;

                    //Convertir errores de AppException a errores de ApiError
                    var errors = new List<ApiError>();
                    if (exApp.Mensajes != null && exApp.Mensajes.Count > 0)
                    {
                        errors = exApp.Mensajes.Select(p => new ApiError { Codigo = string.Empty, Mensaje = p }).ToList();
                    }
                    else if (!string.IsNullOrEmpty(exApp.Codigo) || !string.IsNullOrEmpty(exApp.Message))
                    {
                        errors.Add(new ApiError { Codigo = exApp.Codigo, Mensaje = exApp.Message });
                    }
                    //
                    response = ApiResponseFactory.CreateBadRequest(customHeaders.TransactionId, errors);
                }
                else if (ex is ArgumentException)
                {
                    context.Response.StatusCode = Constants.HttpStatusCodes.BadRequest;
                    response = ApiResponseFactory.CreateBadRequest(customHeaders.TransactionId, string.Empty, ex.Message);
                }
                else if (ex is NotFoundException)
                {
                    context.Response.StatusCode = Constants.HttpStatusCodes.NotFound;
                    response = ApiResponseFactory.CreateBadRequest(customHeaders.TransactionId, string.Empty, ex.Message);
                }
                else
                {
                    logger.LogError(ex, Constants.Labels.GenericError);
                    context.Response.StatusCode = Constants.HttpStatusCodes.InternalServerError;
                    response = new ApiErrorResponse(string.Empty, ex.Message);
                }

                if (!context.Response.HasStarted)
                {
                    context.Response.ContentType = "application/json";
                    var json = JsonSerializer.Serialize(response);
                    await context.Response.WriteAsync(json);
                }
            }
        }
    }
}
