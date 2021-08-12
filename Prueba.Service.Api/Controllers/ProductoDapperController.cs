using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prueba.Application.Contracts;
using Prueba.Application.Dtos.Producto;
using Prueba.Service.Api.Models.ResponseBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.Service.Api.Controllers
{
    [ApiController]
    public class ProductoDapperController : Controller
    {
        private readonly IProductoDapperAppService productoAppService;

        public ProductoDapperController(IProductoDapperAppService productoAppService)
        {
            this.productoAppService = productoAppService;
        }

        [HttpGet]
        [Route("ListarProductosDapper")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [MapToApiVersion("1")]
        public async Task<IActionResult> ListarProductosDapper()
        {
            var customHeaders = Utils.GetCustomHeaders(Request);

            var resultado = await productoAppService.ListarProducto();

            return Ok(ApiResponseFactory.CreateOk(customHeaders.TransactionId, resultado));
        }

        [HttpGet("BuscarProductoDapperPorId{id}")]
        [Route("BuscarProductoDapperPorId")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [MapToApiVersion("1")]
        public async Task<IActionResult> BuscarProductoDapper([FromQuery] int Id)
        {
            var customHeaders = Utils.GetCustomHeaders(Request);

            var resultado = await productoAppService.ObtenerRegistro(Id);

            return Ok(ApiResponseFactory.CreateOk(customHeaders.TransactionId, resultado));
        }


        [HttpPost]
        [Route("CrearProductoDapper")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [MapToApiVersion("1")]
        public async Task<IActionResult> CrearProductoDapper(ProductoRequestDto producto)
        {
            Headers customHeaders = Utils.GetCustomHeaders(Request);

            ProductoResponseDto resultado = await productoAppService.CrearRegistro(producto);

            return Ok(ApiResponseFactory.CreateOk(customHeaders.TransactionId, resultado));
        }

        [HttpPatch]
        [Route("EditarProductoDapper")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [MapToApiVersion("1")]
        public async Task<IActionResult> EditarRegistroDapper(ProductoRequestDto producto)
        {
            Headers customHeaders = Utils.GetCustomHeaders(Request);

            ProductoResponseDto resultado = await productoAppService.EditarRegistro(producto);

            return Ok(ApiResponseFactory.CreateOk(customHeaders.TransactionId, resultado));
        }

        [HttpDelete("EliminarProductoDapper")]
        [Route("EliminarProductoDapper")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [MapToApiVersion("1")]
        public async Task<IActionResult> EliminarProductoDapper([FromQuery] int Id)
        {
            var customHeaders = Utils.GetCustomHeaders(Request);

            var resultado = await productoAppService.EliminarRegistro(Id);

            return Ok(ApiResponseFactory.CreateOk(customHeaders.TransactionId, resultado));
        }
    }
}
