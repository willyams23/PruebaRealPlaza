using Dapper;
using Microsoft.Data.SqlClient;
using Prueba.Domain.Aggregates.ProductoAgg;
using Prueba.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Infrastructure.Data.Repositories
{
    public class ProductoDapperRepository : Repository<Producto>, IProductoDapperRepository
    {
        readonly AppDbContext dbContext;

        #region Constructor
        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public ProductoDapperRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext as AppDbContext;
        }

        string Conexion = "Application Name=SIGPER;Connect Timeout=10;Server=DESKTOP-3UI3UMM;Database=DB_PRUEBA;User Id=sa; Password=Password@SQL123456;";

        public async Task<List<ProductoResponseReadOnly>> ListarProducto()
        {
            using (IDbConnection con = new SqlConnection(Conexion))
            {
                if (con.State == ConnectionState.Closed) con.Open();

                var result = await con.QueryAsync<ProductoResponseReadOnly>("USP_Producto_LIST",
                    null,
                    commandType: CommandType.StoredProcedure);

                if (result != null && result.Count() > 0)
                {
                    return result.ToList();
                }
            }

            return new List<ProductoResponseReadOnly>();
        }

        public async Task<ProductoResponseReadOnly> ObtenerRegistro(int Id)
        {
            using (IDbConnection con = new SqlConnection(Conexion))
            {
                if (con.State == ConnectionState.Closed) con.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@p_Id", Id);

                var result = await con.QueryAsync<ProductoResponseReadOnly>("USP_Producto_BUSCAR",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                if (result != null && result.Count() > 0)
                {
                    return result.FirstOrDefault();
                }
            }

            return new ProductoResponseReadOnly();
        }

        public async Task<ProductoResponseReadOnly> CrearRegistro(ProductoRequestReadOnly producto)
        {
            using (IDbConnection con = new SqlConnection(Conexion))
            {
                if (con.State == ConnectionState.Closed) con.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@p_Id", 0);
                parameters.Add("@p_Nombre", producto.Nombre);
                parameters.Add("@p_Precio", producto.Precio);
                parameters.Add("@p_Stock", producto.Stock);
                parameters.Add("@p_FechaRegistro", producto.FechaRegistro);

                var result = await con.QueryAsync<ProductoResponseReadOnly>("USP_Producto_CREAR",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                if (result != null && result.Count() > 0)
                {
                    return result.FirstOrDefault();
                }
            }

            return new ProductoResponseReadOnly();
        }

        public async Task<ProductoResponseReadOnly> EditarRegistro(ProductoRequestReadOnly producto)
        {
            using (IDbConnection con = new SqlConnection(Conexion))
            {
                if (con.State == ConnectionState.Closed) con.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@p_Id", producto.Id);
                parameters.Add("@p_Nombre", producto.Nombre);
                parameters.Add("@p_Precio", producto.Precio);
                parameters.Add("@p_Stock", producto.Stock);
                parameters.Add("@p_FechaRegistro", producto.FechaRegistro);

                var result = await con.QueryAsync<ProductoResponseReadOnly>("USP_Producto_CREAR",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                if (result != null && result.Count() > 0)
                {
                    return result.FirstOrDefault();
                }
            }

            return new ProductoResponseReadOnly();
        }

        public async Task<ProductoResponseReadOnly> EliminarRegistro(int Id)
        {

            ProductoResponseReadOnly objRegistro = this.ObtenerRegistro(Id).Result;

            using (IDbConnection con = new SqlConnection(Conexion))
            {
                if (con.State == ConnectionState.Closed) con.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@p_Id", Id);
               
                var result = await con.QueryAsync<ProductoResponseReadOnly>("USP_Producto_Eliminar",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                if (result != null)
                {
                    return objRegistro;
                    //return result.FirstOrDefault();
                }
            }

            return new ProductoResponseReadOnly();
        }

        #endregion
    }
}