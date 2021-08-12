using Prueba.Infrastructure.Data.Context.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Prueba.Domain.Aggregates.ProductoAgg;

namespace Prueba.Infrastructure.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        #region IDbSet Members
        public virtual DbSet<Producto> Producto { get; set; }


        #endregion

        #region Model Creating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductoEntityTypeConfiguration());
            SetNotDeleteCascade(modelBuilder);
        }
        private void SetNotDeleteCascade(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
        }

        #endregion
    }
}