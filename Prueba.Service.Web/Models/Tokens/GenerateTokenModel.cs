using System.Collections.Generic;

namespace Prueba.Service.Web.Models.Tokens
{
    public class GenerateTokenModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public string Email { get; set; }

        public List<string> Permisos { get; set; }
    }
}
