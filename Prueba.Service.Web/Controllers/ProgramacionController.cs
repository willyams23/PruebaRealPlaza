using Prueba.Application.Contracts;
using Prueba.Application.Dtos.Departamento;
using Prueba.Service.Web.Models.ResponseBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.Service.Web.Controllers
{
    public class ProgramacionController : Controller
    {
        private readonly IDepartamentoAppService departamentoAppService;
        public ProgramacionController(IDepartamentoAppService departamentoAppService)
        {
            this.departamentoAppService = departamentoAppService;
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [MapToApiVersion("1")]
        public async Task<IActionResult> Index()
        {
            var customHeaders = Utils.GetCustomHeaders(Request);

            var resultado = await departamentoAppService.ListarDepartamento();

            return View(resultado);
            //return Ok(ApiResponseFactory.CreateOk(customHeaders.TransactionId, resultado));
        }
    }
}
