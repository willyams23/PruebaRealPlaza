using Microsoft.EntityFrameworkCore;
using Prueba.Domain.Aggregates.ProductoAgg;
using Prueba.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Prueba.Infrastructure.Data.Repositories
{
    public class ProductoRepository : Repository<Producto>, IProductoRepository
    {
        readonly AppDbContext dbContext;

        #region Constructor
        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public ProductoRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext as AppDbContext;
        }

        public async Task<List<ProductoResponseReadOnly>> ListarProducto()
        {

            var resultado = await (from tablaProducto in dbContext.Producto
                                   select (new ProductoResponseReadOnly()
                                   {
                                       Id = tablaProducto.Id,
                                       Nombre = tablaProducto.Nombre,
                                       Precio = tablaProducto.Precio,
                                       Stock = tablaProducto.Stock,
                                       FechaRegistro = tablaProducto.FechaRegistro,
                                   }))
                                  .OrderBy(x => x.Id)
                                  .ToListAsync();

            return resultado;
        }

        public async Task<ProductoResponseReadOnly> ObtenerRegistro(int Id)
        {
            var resultado = await (from tablaProducto in dbContext.Producto
                                   where tablaProducto.Id == Id
                                   select (new ProductoResponseReadOnly()
                                   {
                                       Id = tablaProducto.Id,
                                       Nombre = tablaProducto.Nombre,
                                       Precio = tablaProducto.Precio,
                                       Stock = tablaProducto.Stock,
                                       FechaRegistro = tablaProducto.FechaRegistro,
                                   }))
                               .OrderBy(x => x.Id)
                               .ToListAsync();

            return resultado.FirstOrDefault();
        }
        #endregion
    }
}
