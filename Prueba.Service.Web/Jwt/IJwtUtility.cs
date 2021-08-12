using System;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Prueba.Service.Web.Models.Tokens;

namespace Prueba.Service.Web.Jwt
{
    public interface IJwtUtility
    {
        Tuple<string,DateTime> GenerateJwt(GenerateTokenModel token_param);
        IPrincipal GetPrincipalToken(string token);
        GenerateTokenModel ObtenerDatosUsuario(HttpContext context);
    }
}
