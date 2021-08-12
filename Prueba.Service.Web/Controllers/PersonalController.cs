using Prueba.Application.Contracts;
using Prueba.Application.Dtos.ParametrosGenerales;
using Prueba.Application.Dtos.Personal;
using Prueba.Infrastructure.Crosscutting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.Service.Web.Controllers
{
    public class PersonalController : Controller
    {
        private readonly IDepartamentoAppService departamentoAppService;
        private readonly IServicioAppService servicioAppService;
        private readonly IParametrosGeneralesAppService parametrosGeneralesAppService;

        private readonly IPersonalAppService personalAppService;

        public PersonalController(IPersonalAppService personalAppService,
            IDepartamentoAppService departamentoAppService,
            IServicioAppService servicioAppService,
            IParametrosGeneralesAppService parametrosGeneralesAppService)
        {
            this.personalAppService = personalAppService;
            this.departamentoAppService = departamentoAppService;
            this.servicioAppService = servicioAppService;
            this.parametrosGeneralesAppService = parametrosGeneralesAppService;
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [MapToApiVersion("1")]
        public async Task<IActionResult> Index(int? pageSize, int? page, string Search)
        {
            List<ParametrosGeneralesResponseDto> ListarPaginas = await parametrosGeneralesAppService.ListarPaginas();

            List<SelectListItem> Paginas = ListarPaginas.ConvertAll(x =>
            {
                return new SelectListItem()
                {
                    Text = x.Descripcion.ToString(),
                    Value = x.Codigo.ToString(),
                    Selected = false
                };
            });

            ViewBag.Paginas = Paginas;

            PersonalListarRequestDto Filtro = new PersonalListarRequestDto();
            Filtro.Search = Search;
            Filtro.PaginacionTop = (pageSize ?? Constants.Pagina.PageSize);
            Filtro.PaginacionNroPagina = (page ?? 1);

            PersonalListarResponseDto ListarPersonal = await personalAppService.FiltrarPersonal(Filtro);
            ListarPersonal.ValoresQueryString = new RouteValueDictionary();

            return View(ListarPersonal);
        }
    }
}
