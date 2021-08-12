using Prueba.Application.Contracts;
using Prueba.Application.Implementations;
using Prueba.Domain;
using Prueba.Infrastructure.Data.Context;
using Prueba.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Prueba.Domain.Aggregates.ProductoAgg;

namespace Prueba.Service.SeedWork
{
    public class InjectionsSettings
    {
        public static void ConfigureServiceCrossCutting(IServiceCollection services)
        {
            //UnitOfWork
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            #region Inject App Service
            services.AddTransient<IProductoAppService, ProductoAppService>();
            services.AddTransient<IProductoDapperAppService, ProductoDapperAppService>();
            #endregion

            #region Inject Repositories
            services.AddTransient<IProductoRepository, ProductoRepository>();
            services.AddTransient<IProductoDapperRepository, ProductoDapperRepository>();
            #endregion

        }
    }
}
