using Prueba.Application.Dtos.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Contracts
{
    public interface IProductoAppService
    {
        Task<List<ProductoResponseDto>> ListarProducto();
        Task<ProductoResponseDto> ObtenerRegistro(int Id);
        Task<ProductoResponseDto> CrearRegistro(ProductoRequestDto producto);
        Task<ProductoResponseDto> EditarRegistro(ProductoRequestDto producto);
        Task<ProductoResponseDto> EliminarRegistro(int Id);
    }
}
