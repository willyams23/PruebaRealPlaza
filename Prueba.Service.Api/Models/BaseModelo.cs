using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.Service.Api.Models
{
    public class BaseModelo
    {
        public bool SeguirPaginando { get; set; }
        public int TotalRegistros { get; set; }
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanioPagina { get; set; }
        public RouteValueDictionary ValoresQueryString { get; set; }
    }
}
