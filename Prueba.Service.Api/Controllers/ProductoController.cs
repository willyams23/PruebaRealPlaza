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
    public class ProductoController : Controller
    {
        private readonly IProductoAppService productoAppService;

        public ProductoController(IProductoAppService productoAppService)
        {
            this.productoAppService = productoAppService;
        }

        [HttpGet]
        [Route("ListarProductos")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [MapToApiVersion("1")]
        public async Task<IActionResult> ListarProductos()
        {
            var customHeaders = Utils.GetCustomHeaders(Request);

            var resultado = await productoAppService.ListarProducto();

            return Ok(ApiResponseFactory.CreateOk(customHeaders.TransactionId, resultado));
        }

        [HttpGet("BuscarProductoPorId{id}")]
        [Route("BuscarProductoPorId")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [MapToApiVersion("1")]
        public async Task<IActionResult> BuscarProducto([FromQuery] int Id)
        {
            var customHeaders = Utils.GetCustomHeaders(Request);

            var resultado = await productoAppService.ObtenerRegistro(Id);

            return Ok(ApiResponseFactory.CreateOk(customHeaders.TransactionId, resultado));
        }

        [HttpPost]
        [Route("CrearProducto")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [MapToApiVersion("1")]
        public async Task<IActionResult> CrearProducto(ProductoRequestDto producto)
        {
            Headers customHeaders = Utils.GetCustomHeaders(Request);

            ProductoResponseDto resultado = await productoAppService.CrearRegistro(producto);

            return Ok(ApiResponseFactory.CreateOk(customHeaders.TransactionId, resultado));
        }

        [HttpPatch]
        [Route("EditarProducto")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [MapToApiVersion("1")]
        public async Task<IActionResult> EditarRegistro(ProductoRequestDto producto)
        {
            Headers customHeaders = Utils.GetCustomHeaders(Request);

            ProductoResponseDto resultado = await productoAppService.EditarRegistro(producto);

            return Ok(ApiResponseFactory.CreateOk(customHeaders.TransactionId, resultado));
        }

        [HttpDelete("EliminarProducto")]
        [Route("EliminarProducto")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [MapToApiVersion("1")]
        public async Task<IActionResult> EliminarProducto([FromQuery] int Id)
        {
            var customHeaders = Utils.GetCustomHeaders(Request);

            var resultado = await productoAppService.EliminarRegistro(Id);

            return Ok(ApiResponseFactory.CreateOk(customHeaders.TransactionId, resultado));
        }

    }
}
