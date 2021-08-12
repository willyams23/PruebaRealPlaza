using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Infrastructure.Crosscutting
{
    public class HttpAuxiliaryContext
    {
        private static string CurrentTransactionId { get; set; }
        public static void SetCurrentTransaction(string currentTransactionIdParameter = "")
        {
            CurrentTransactionId = currentTransactionIdParameter;
        }

        public static string GetCurrentTransactionId()
        {
            if (CurrentTransactionId == null)
            {
                Guid guid = Guid.NewGuid();
                CurrentTransactionId = guid.ToString();
            }

            return CurrentTransactionId;
        }
    }
}
