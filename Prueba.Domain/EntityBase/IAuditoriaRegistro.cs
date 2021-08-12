using System;

namespace Prueba.Domain.EntityBase
{
    public interface IAuditoriaRegistro
    {
        public DateTime FechaRegistro { get; set; }

        public string UsuarioRegistro { get; set; }
    }
}
