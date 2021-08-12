using System.Collections.Generic;

namespace Prueba.Service.Api.Models.Tokens
{
    public class GenerateTokenModelFactory
    {
        public static GenerateTokenModel Create(int id, string email, 
            string nombre, string apellidos, List<string> permisos)
        {
            return new GenerateTokenModel
            {
                Id = id,
                Email = email,
                Nombre = nombre,
                Apellidos = apellidos,
                Permisos = permisos
            };
        }
    }
}
