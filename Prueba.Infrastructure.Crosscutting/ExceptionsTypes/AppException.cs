using System;
using System.Collections.Generic;

namespace Prueba.Infrastructure.Crosscutting.ExceptionsTypes
{
    public class AppException : Exception
    {
        private readonly string codigo;
        private readonly TipoException tipo;
        private readonly List<string> mensajes;
        public AppException(TipoException tipo, string codigo, string message, List<string> mensajes, Exception ex) : base(message, ex)
        {
            this.codigo = codigo;
            this.tipo = tipo;
            this.mensajes = mensajes;
        }

        /// <summary>
        /// Contiene el código de la excepción manejada
        /// </summary>
        public TipoException Tipo
        {
            get
            {
                return tipo;
            }
        }

        /// <summary>
        /// Obtiene el código, mensaje y acción si hubiera en formato cadena para mostrar.
        /// </summary>

        public List<string> Mensajes
        {
            get
            {
                return mensajes;
            }
        }

        public string Codigo
        {
            get
            {
                return codigo;
            }
        }
    }
}
