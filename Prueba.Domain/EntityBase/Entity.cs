using System;

namespace Prueba.Domain.EntityBase
{
    public class Entity : EntityBase<int>, IAuditoriaRegistro, IAuditoriaModificacion
    {
        public bool Activo { get; set; }

        #region Auditoria Registro

        public DateTime FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; }

        #endregion

        #region Auditoria Modificacion

        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }

        #endregion
    }

    public class EntityBase<T>
    {
        public T Id { get; set; }

    }
}
