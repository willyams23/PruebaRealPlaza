using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prueba.Application.Dtos;
using Prueba.Service.Api.Models.Tokens;

namespace Prueba.Service.Api.Models
{
    public class AutenticateUser
    {
        public GenerateTokenResponseModel Token { set; get; }
    }
}
