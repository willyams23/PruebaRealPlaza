using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prueba.Domain.Aggregates.ProductoAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Infrastructure.Data.Context.Mapping
{
    public class ProductoEntityTypeConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("Producto");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
               .HasColumnName("Id")
               .IsRequired();

            builder.Property(e => e.Nombre)
                .HasColumnName("Nombre")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Precio)
            .HasColumnName("Precio");

            builder.Property(e => e.Stock)
            .HasColumnName("Stock");

            builder.Property(e => e.FechaRegistro)
             .HasColumnName("FechaRegistro");
        }
    }
}
