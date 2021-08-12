using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Infrastructure.Crosscutting.Encrypt
{
    public class EncryptionSettings
    {
        public string Key { get; set; }

        public string IV { get; set; }
    }
}
