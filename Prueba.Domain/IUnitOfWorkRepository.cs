using Prueba.Domain.Aggregates.ProductoAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Domain
{
    public interface IUnitOfWorkRepository
    {
        IProductoRepository productoRepository { get; }
    }
}
