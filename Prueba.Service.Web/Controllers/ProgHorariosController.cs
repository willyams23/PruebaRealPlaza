using Prueba.Application.Contracts;
using Prueba.Application.Dtos.Departamento;
using Prueba.Application.Dtos.Horario;
using Prueba.Application.Dtos.ParametrosGenerales;
using Prueba.Application.Dtos.Personal;
using Prueba.Application.Dtos.Servicio;
using Prueba.Infrastructure.Crosscutting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.Service.Web.Controllers
{
    public class ProgHorariosController : Controller
    {
        private readonly IDepartamentoAppService departamentoAppService;
        private readonly IParametrosGeneralesAppService parametrosGeneralesAppService;
        private readonly IHorarioAppService horarioAppService;
        private readonly IServicioAppService servicioAppService;

        public ProgHorariosController(IDepartamentoAppService departamentoAppService,
            IParametrosGeneralesAppService parametrosGeneralesAppService,
            IHorarioAppService horarioAppService,
            IServicioAppService servicioAppService)
        {
            this.departamentoAppService = departamentoAppService;
            this.parametrosGeneralesAppService = parametrosGeneralesAppService;
            this.horarioAppService = horarioAppService;
            this.servicioAppService = servicioAppService;
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [MapToApiVersion("1")]
        public async Task<IActionResult> Index(int lst_Anio)
        {
            var customHeaders = Utils.GetCustomHeaders(Request);
            var sAnio = DateTime.Now.Year.ToString();
            var sMes = DateTime.Now.Month.ToString();


            List<DepartamentoResponseDto> ListarDepartamentos = await departamentoAppService.ListarDepartamento();
            ListarDepartamentos.Insert(0, new DepartamentoResponseDto() { IdDepartamento = Constants.Combo.ValorDefecto, Descripcion = Constants.Combo.TextoDefecto });

            List<SelectListItem> Departamentos = ListarDepartamentos.ConvertAll(x =>
            {
                return new SelectListItem()
                {
                    Text = x.Descripcion.ToString(),
                    Value = x.IdDepartamento.ToString(),
                    Selected = false
                };
            });

            ViewBag.Departamentos = Departamentos;

            List<ParametrosGeneralesResponseDto> ListarPeriodos = await parametrosGeneralesAppService.ListarPeriodo();
            ListarPeriodos.Insert(0, new ParametrosGeneralesResponseDto() { Codigo = Constants.Combo.ValorDefecto.ToString(), Descripcion = Constants.Combo.TextoDefecto });

            List<SelectListItem> Periodos = new List<SelectListItem>();
            foreach (var item in ListarPeriodos)
            {
                SelectListItem sel = new SelectListItem();
                sel.Text = item.Descripcion.ToString();
                sel.Value = item.Codigo.ToString();
                sel.Selected = (item.Codigo == sAnio ? true : false);

                Periodos.Add(sel);
            }

            ViewBag.Periodos = Periodos;


            List<ParametrosGeneralesResponseDto> ListarMeses = await parametrosGeneralesAppService.ListarMeses();
            ListarMeses.Insert(0, new ParametrosGeneralesResponseDto() { Codigo = Constants.Combo.ValorDefecto.ToString(), Descripcion = Constants.Combo.TextoDefecto });

            List<SelectListItem> Meses = new List<SelectListItem>();
            foreach (var item in ListarMeses)
            {
                SelectListItem sel = new SelectListItem();
                sel.Text = item.Descripcion.ToString();
                sel.Value = item.Codigo.ToString();
                sel.Selected = (item.Codigo == sMes ? true : false);

                Meses.Add(sel);
            }

            ViewBag.Meses = Meses;


            List<HorarioResponseDto> ListarHorarios = await horarioAppService.ListarHorario();
            ListarHorarios.Insert(0, new HorarioResponseDto() { IdHorario = Constants.Combo.ValorDefecto, Descripcion = Constants.Combo.TextoDefecto });

            List<SelectListItem> Horarios = ListarHorarios.ConvertAll(x =>
            {
                return new SelectListItem()
                {
                    Text = x.IdHorario != 0 ? x.CodigoHorario + " - " + x.Descripcion.ToString() : x.Descripcion.ToString(),
                    Value = x.IdHorario.ToString(),
                    Selected = false
                };
            });

            ViewBag.Horarios = Horarios;

            return View();
            //return Ok(ApiResponseFactory.CreateOk(customHeaders.TransactionId, resultado));
        }


        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [MapToApiVersion("1")]
        public async Task<JsonResult> DepartamentoChanged(int IdDepartamento)
        {
            List<ServicioResponseDto> ListarServicios = await servicioAppService.BuscarServicio(IdDepartamento);
            ListarServicios.Insert(0, new ServicioResponseDto() { IdServicio = Constants.Combo.ValorDefecto, Descripcion = Constants.Combo.TextoDefecto });

            List<SelectListItem> Servicios = ListarServicios.ConvertAll(x =>
            {
                return new SelectListItem()
                {
                    Text = x.Descripcion.ToString(),
                    Value = x.IdServicio.ToString()
                };
            });
            //ViewBag.Servicios = Servicios;
            return Json(Servicios);
        }
    }
}
