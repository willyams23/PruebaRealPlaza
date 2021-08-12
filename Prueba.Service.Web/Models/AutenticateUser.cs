using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prueba.Application.Dtos;
using Prueba.Application.Dtos.Pais;
using Prueba.Domain.Aggregates.PaisAgg;
//using Prueba.Domain.Aggregates.UsuarioAgg;
using Prueba.Service.Web.Models.Tokens;

namespace Prueba.Service.Web.Models
{
    public class AutenticateUser
    {
        //public UsuarioAutenticado UsuarioAutenticado { set; get; }
        public GenerateTokenResponseModel Token { set; get; }
        public PaisResponseDto Pais { set; get; }
        //public List<OpcionesMenuResponseDto> OpcionesMenu { set; get; }
    }
}
