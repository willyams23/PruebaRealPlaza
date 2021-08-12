using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Domain.Aggregates.ProductoAgg
{
    public interface IProductoDapperRepository : IRepository<Producto>
    {
        Task<List<ProductoResponseReadOnly>> ListarProducto();
        Task<ProductoResponseReadOnly> ObtenerRegistro(int Id);
        Task<ProductoResponseReadOnly> CrearRegistro(ProductoRequestReadOnly producto);
        Task<ProductoResponseReadOnly> EditarRegistro(ProductoRequestReadOnly producto);
        Task<ProductoResponseReadOnly> EliminarRegistro(int Id);

    }
}
