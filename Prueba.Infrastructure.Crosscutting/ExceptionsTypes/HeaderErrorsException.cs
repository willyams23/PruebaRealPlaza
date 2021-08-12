using System;
using System.Collections.Generic;

namespace Prueba.Infrastructure.Crosscutting.ExceptionsTypes
{
    public class HeaderErrorsException : Exception
    {
        public List<string> ErrorsMessages { get; set; }

        public HeaderErrorsException(List<string> errorsMessages)
        {
            ErrorsMessages = errorsMessages;
        }
    }
}
