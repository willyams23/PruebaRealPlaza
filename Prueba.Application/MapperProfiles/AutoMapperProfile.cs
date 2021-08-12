using AutoMapper;
using Prueba.Application.Dtos.Producto;
using Prueba.Domain.Aggregates.ProductoAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.MapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Entity to Dto
            CreateMap<ProductoRequestReadOnly, ProductoRequestDto>();
            CreateMap<ProductoResponseReadOnly, ProductoResponseDto>();
            CreateMap<ProductoResponseReadOnly, Producto>();
            CreateMap<Producto, ProductoRequestDto>();
            CreateMap<Producto, ProductoResponseDto>();

            #endregion

            #region Dto to Entity
            CreateMap<ProductoRequestDto, ProductoRequestReadOnly>();
            CreateMap<ProductoResponseDto, ProductoResponseReadOnly>();
            //CreateMap<Producto, ProductoResponseReadOnly>();
            CreateMap<ProductoRequestDto, Producto>();
            CreateMap<ProductoResponseDto, Producto>();
            #endregion
        }
    }
}
