using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Prueba.Application.Util
{
    public class Hashing
    {
        public static string ObtenerSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public static string ObtenerCadenaHash(string Cadena, byte[] salt)
        {
            string CadenaHashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: Cadena,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return CadenaHashed;
        }
    }
}