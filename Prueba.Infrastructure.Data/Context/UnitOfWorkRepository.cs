using Prueba.Domain;
using Prueba.Domain.Aggregates.ProductoAgg;
using Prueba.Infrastructure.Data.Repositories;

namespace Prueba.Infrastructure.Data.Context
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        public IProductoRepository productoRepository { get; }

        public UnitOfWorkRepository(AppDbContext context)
        {
            productoRepository = new ProductoRepository(context);
        }
    }
}
