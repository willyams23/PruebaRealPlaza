using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Prueba.Infrastructure.Crosscutting;
using Prueba.Service.Web.Jwt;
using Prueba.Service.Web.Models.Tokens;

namespace Prueba.Service.Web.Filters
{
    public class AuthorizationCustomAttribute : TypeFilterAttribute
    {
        public AuthorizationCustomAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }

        public AuthorizationCustomAttribute(string claimValue) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(ClaimTypes.NameIdentifier, claimValue) };
        }
    }

    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        readonly Claim _claim;
        readonly IJwtUtility _jwtGenerator;
        readonly Infrastructure.Crosscutting.Encrypt.Encrypt _encrypt;

        public ClaimRequirementFilter(Claim claim,
            IJwtUtility jwtGenerator,
            Infrastructure.Crosscutting.Encrypt.Encrypt encrypt)
        {
            _claim = claim;
            _jwtGenerator = jwtGenerator;
            _encrypt = encrypt;
        }

        //TODO: Falta realizar test
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "");
            token = token.Replace("bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var principalToken = _jwtGenerator.GetPrincipalToken(token);
            if (principalToken == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var claim = (principalToken as ClaimsPrincipal).FindFirst(_claim.Type)?.Value;
            if (claim == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (_claim.Value.Equals(Constants.LoggingProperties.ClaimAccionGenerica))
            {
                return;
            }

            var claimDecifrado = _encrypt.DecryptString(claim);
            var jwtValues = JsonSerializer.Deserialize<GenerateTokenModel>(claimDecifrado);
            if (jwtValues.Permisos == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var hasClaim = jwtValues.Permisos.Any(c => c == _claim.Value);
            if (!hasClaim)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
