using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Infrastructure.Crosscutting
{
    public class RespuestaTxn
    {
        public bool Exito { set; get; }
        public string MensajeError { set; get; }
        public decimal IdRegistroEntidad { set; get; }
    }
}
