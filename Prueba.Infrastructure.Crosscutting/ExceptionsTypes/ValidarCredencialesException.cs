using System;
using System.Collections.Generic;

namespace Prueba.Infrastructure.Crosscutting.ExceptionsTypes
{
    public class ValidarCredencialesException : Exception
    {
        public List<string> ErrorsMessages { get; set; }

        public ValidarCredencialesException(string mensajeError)
        {
            ErrorsMessages = new List<string>
            {
                mensajeError
            };
        }
    }
}
