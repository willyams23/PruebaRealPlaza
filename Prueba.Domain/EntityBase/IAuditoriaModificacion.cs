using System;

namespace Prueba.Domain.EntityBase
{
    public interface IAuditoriaModificacion
    {
        public DateTime? FechaModificacion { get; set; }

        public string UsuarioModificacion { get; set; }
    }
}
