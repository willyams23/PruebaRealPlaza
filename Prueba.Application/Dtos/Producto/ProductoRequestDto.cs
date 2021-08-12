﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Dtos.Producto
{
    public class ProductoRequestDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
