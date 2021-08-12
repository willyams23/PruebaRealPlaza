using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Prueba.Service.Web.Models.Settings;
using Prueba.Service.Web.Models.Tokens;
using encryptClass = Prueba.Infrastructure.Crosscutting.Encrypt.Encrypt;

namespace Prueba.Service.Web.Jwt
{
    public class JwtUtility : IJwtUtility
    {
        private readonly encryptClass _encrypt;
        private readonly JwtSettings _jwtSettings;

        public JwtUtility(encryptClass encrypt, IOptions<JwtSettings> jwtSettings)
        {
            _encrypt = encrypt;
            _jwtSettings = jwtSettings.Value;
        }

        public Tuple<string,DateTime> GenerateJwt(GenerateTokenModel token_param)
        {
            var header = new JwtHeader(
            new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.InternalSecretKey)
                ),
                SecurityAlgorithms.HmacSha256)
            );

            var subEncrypt = _encrypt.EncryptString(JsonSerializer.Serialize(token_param));
            var jtiEncrypt = _encrypt.EncryptString(Guid.NewGuid().ToString());
            var claims = new Claim[]
            {
                new Claim("sub", subEncrypt),
                new Claim("jti", jtiEncrypt)
            };

            var tokenExpire = GetExpirationJwt();
            var payload = new JwtPayload(
                issuer: "template-ef-core-api",
                audience: "template-ef-core-web",
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: tokenExpire
            );

            var token = new JwtSecurityToken(header, payload);
            var jwt =  new JwtSecurityTokenHandler().WriteToken(token);

            return new Tuple<string, DateTime>(jwt, tokenExpire);
        }

        private DateTime GetExpirationJwt()
        {
            var api_seconds = double.Parse(_jwtSettings.ExpirationTime);
            return DateTime.UtcNow.AddSeconds(api_seconds);
        }

        public TokenValidationParameters GetJwtValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true,
                LifetimeValidator = LifetimeValidator,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.InternalSecretKey))
            };
        }
        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }

        public IPrincipal GetPrincipalToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetJwtValidationParameters();

            try
            {
                SecurityToken validatedToken;
                IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public GenerateTokenModel ObtenerDatosUsuario(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "");
            token = token.Replace("bearer ", "");

            var clainType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            var principalToken = GetPrincipalToken(token);
            var claim = (principalToken as ClaimsPrincipal).FindFirst(clainType)?.Value;

            var claimDecifrado = _encrypt.DecryptString(claim);
            var jwtValues = JsonSerializer.Deserialize<GenerateTokenModel>(claimDecifrado);

            return jwtValues;
        }
    }
}
