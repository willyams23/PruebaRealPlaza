using System;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Prueba.Service.Api.Models.Tokens;

namespace Prueba.Service.Api.Jwt
{
    public interface IJwtUtility
    {
        Tuple<string,DateTime> GenerateJwt(GenerateTokenModel token_param);
        IPrincipal GetPrincipalToken(string token);
        GenerateTokenModel ObtenerDatosUsuario(HttpContext context);
    }
}
