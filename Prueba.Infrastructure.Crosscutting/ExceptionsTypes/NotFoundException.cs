using System;
using Prueba.Infrastructure.Crosscutting.Resources;

namespace Prueba.Infrastructure.Crosscutting.ExceptionsTypes
{
    public class NotFoundException : Exception
    {
        private string _errorMessage;
        public NotFoundException(string errorMessage)
        {
            _errorMessage = errorMessage;
        }

        public NotFoundException()
        {
            _errorMessage = Messages.NotFoundResource;
        }

        public override string Message
        {
            get
            {
                return _errorMessage;
            }
        }
    }
}
