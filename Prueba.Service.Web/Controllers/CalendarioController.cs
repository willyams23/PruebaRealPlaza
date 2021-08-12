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
    public class CalendarioController : Controller
    {
        private readonly IParametrosGeneralesAppService parametrosGeneralesAppService;
        private readonly IPersonalAppService personalAppService;

        public CalendarioController(IParametrosGeneralesAppService parametrosGeneralesAppService,
            IPersonalAppService personalAppService)
        {
            this.parametrosGeneralesAppService = parametrosGeneralesAppService;
            this.personalAppService = personalAppService;
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [MapToApiVersion("1")]
        public async Task<IActionResult> Index(string documento, int? anio, int? mes)
        {
            var customHeaders = Utils.GetCustomHeaders(Request);
            var sAnio = DateTime.Now.Year;
            var sMes = DateTime.Now.Month;
            var NameMes = string.Empty;

            sAnio = (anio == null ? sAnio : Convert.ToInt32(anio));
            sMes = (mes == null ? sMes : Convert.ToInt32(mes));

            List<ParametrosGeneralesResponseDto> ListarPeriodos = await parametrosGeneralesAppService.ListarPeriodo();
            ListarPeriodos.Insert(0, new ParametrosGeneralesResponseDto() { Codigo = Constants.Combo.ValorDefecto.ToString(), Descripcion = Constants.Combo.TextoDefecto });


            List<SelectListItem> Periodos = new List<SelectListItem>();
            foreach (var item in ListarPeriodos)
            {
                SelectListItem sel = new SelectListItem();
                sel.Text = item.Descripcion.ToString();
                sel.Value = item.Codigo.ToString();
                sel.Selected = (item.Codigo == sAnio.ToString() ? true : false);

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
                sel.Selected = (item.Codigo == sMes.ToString() ? true : false);
                if (item.Codigo == sMes.ToString())
                {
                    NameMes = item.Descripcion.ToUpper();
                }
                Meses.Add(sel);
            }

            ViewBag.Meses = Meses;


            //documento = documento == null ? "44147945" : documento;

            CalendarioListarRequestDto Filtro = new CalendarioListarRequestDto();
            Filtro.PaginacionTop = 50;
            Filtro.PaginacionNroPagina = 1;

            Filtro.CodigoParametro = 2;
            Filtro.IdServicio = 0;
            Filtro.Anio = sAnio;
            Filtro.Mes = sMes;
            Filtro.IdPersonal = 0;
            Filtro.Documento = documento == null ? "": documento;

            CalendarioListarResponseDto ListarCalendario = await personalAppService.FiltrarCalendario(Filtro);
            List<SemanaResponseDto> ListaSemana = new List<SemanaResponseDto>();

            ListaSemana = ResponseSemanas(ListarCalendario);

            ListarCalendario.Semanas = ListaSemana;
            ListarCalendario.ValoresQueryString = new RouteValueDictionary();
            ListarCalendario.documento = documento;
            ListarCalendario.NameMes = NameMes;

            return View(ListarCalendario);
        }

        #region MatrizSemanas
        public List<SemanaResponseDto> ResponseSemanas(CalendarioListarResponseDto ListarCalendario)
        {
            List<SemanaResponseDto> ListaSemana = new List<SemanaResponseDto>();

            SemanaResponseDto Semana = new SemanaResponseDto();

            //--#################
            DateTime dfechaIni = ListarCalendario.Calendarios[0].Fecha;
            int WEEKDAY = (byte)dfechaIni.DayOfWeek;
            DateTime dfechaAnterior = new DateTime();

            if (WEEKDAY != 1)
            {
                if (WEEKDAY == 0) //Si es domingo => 7
                {
                    WEEKDAY = 7;
                }

                if (WEEKDAY > 1)
                {
                    dfechaAnterior = dfechaIni.AddDays(-(WEEKDAY - 1));
                }
                else
                {
                    dfechaAnterior = dfechaIni.AddDays(-1);
                }

                for (int i = 0; i < 7; i++)
                {
                    CalendarioResponseDto item = new CalendarioResponseDto();
                    item.Anio = dfechaAnterior.Year;
                    item.Mes = dfechaAnterior.Month;
                    item.Dia = dfechaAnterior.Day;
                    item.Fecha = dfechaAnterior;
                    item.FlagMesAnterior = 1;

                    if (dfechaAnterior < dfechaIni)
                    {
                        WEEKDAY = (byte)dfechaAnterior.DayOfWeek;

                        switch (WEEKDAY)
                        {
                            case 0:
                                Semana.Domingo = item;
                                ListaSemana.Add(Semana);
                                break;
                            case 1:
                                Semana = new SemanaResponseDto();
                                Semana.Lunes = item;
                                break;
                            case 2:
                                Semana.Martes = item;
                                break;
                            case 3:
                                Semana.Miercoles = item;
                                break;
                            case 4:
                                Semana.Jueves = item;
                                break;
                            case 5:
                                Semana.Viernes = item;
                                break;
                            case 6:
                                Semana.Sabado = item;
                                break;
                            default:
                                break;
                        }
                    }

                    dfechaAnterior = dfechaAnterior.AddDays(1);
                }
            }

            //--#################

            foreach (CalendarioResponseDto item in ListarCalendario.Calendarios)
            {
                DateTime Fecha = item.Fecha;
                WEEKDAY = (byte)Fecha.DayOfWeek;

                switch (WEEKDAY)
                {
                    case 0:
                        Semana.Domingo = item;
                        ListaSemana.Add(Semana);
                        break;
                    case 1:
                        Semana = new SemanaResponseDto();
                        Semana.Lunes = item;
                        break;
                    case 2:
                        Semana.Martes = item;
                        break;
                    case 3:
                        Semana.Miercoles = item;
                        break;
                    case 4:
                        Semana.Jueves = item;
                        break;
                    case 5:
                        Semana.Viernes = item;
                        break;
                    case 6:
                        Semana.Sabado = item;
                        break;
                    default:
                        break;
                }
            }

            if (WEEKDAY > 0)
            {
                DateTime dfechaMesNext = new DateTime();
                dfechaMesNext = dfechaIni.AddMonths(1);

                for (int i = 0; i < 7; i++)
                {
                    CalendarioResponseDto item = new CalendarioResponseDto();
                    item.Anio = dfechaMesNext.Year;
                    item.Mes = dfechaMesNext.Month;
                    item.Dia = dfechaMesNext.Day;
                    item.Fecha = dfechaMesNext;
                    item.FlagMesAnterior = 1;

                    WEEKDAY = (byte)dfechaMesNext.DayOfWeek;


                    if (WEEKDAY > 0)
                    {
                        switch (WEEKDAY)
                        {
                            case 1:
                                Semana = new SemanaResponseDto();
                                Semana.Lunes = item;
                                break;
                            case 2:
                                Semana.Martes = item;
                                break;
                            case 3:
                                Semana.Miercoles = item;
                                break;
                            case 4:
                                Semana.Jueves = item;
                                break;
                            case 5:
                                Semana.Viernes = item;
                                break;
                            case 6:
                                Semana.Sabado = item;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        Semana.Domingo = item;
                        ListaSemana.Add(Semana);
                    }

                    dfechaMesNext = dfechaMesNext.AddDays(1);
                }
            }

            return ListaSemana;
        }

        #endregion

    }
}
