using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Domain.Aggregates.ProductoAgg
{
    public interface IProductoRepository : IRepository<Producto>
    {
        Task<List<ProductoResponseReadOnly>> ListarProducto();
        Task<ProductoResponseReadOnly> ObtenerRegistro(int Id);
    }
}
