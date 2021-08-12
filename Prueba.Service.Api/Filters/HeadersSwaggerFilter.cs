using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Prueba.Infrastructure.Crosscutting;

namespace Prueba.Service.Api.Filters
{
    public class HeadersSwaggerFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            #region Parameters

            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = Constants.Headers.CountryCode,
                In = ParameterLocation.Header,
                Description = "código del país",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString("pe"),
                }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = Constants.Headers.ClientVersion,
                In = ParameterLocation.Header,
                Description = "versión cliente Rest",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString("1.0"),
                }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = Constants.Headers.TransactionId,
                In = ParameterLocation.Header,
                Description = "",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString(""),
                }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = Constants.Headers.ContentType,
                In = ParameterLocation.Header,
                Description = "",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString("application/json"),
                }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = Constants.Headers.ApplicationUser,
                In = ParameterLocation.Header,
                Description = "",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString("lrojas"),
                }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = Constants.Headers.ApplicationCode,
                In = ParameterLocation.Header,
                Description = "",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString("APPRWARE"),
                }
            });

            #endregion

            #region Parameter authorization

            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

            if (operation.Security == null)
            {
                operation.Security = new List<OpenApiSecurityRequirement>();
            }

            var scheme = new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" } };
            operation.Security.Add(new OpenApiSecurityRequirement
            {
                [scheme] = new List<string>()
            });

            #endregion

        }
    }
}
