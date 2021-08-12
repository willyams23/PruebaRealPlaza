using AutoMapper;
using Prueba.Application.Contracts;
using Prueba.Application.Dtos.Producto;
using Prueba.Domain;
using Prueba.Domain.Aggregates.ProductoAgg;
using Prueba.Infrastructure.Crosscutting.ExceptionsTypes;
using Prueba.Infrastructure.Crosscutting.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Implementations
{
    public class ProductoDapperAppService : IProductoDapperAppService
    {
        private IProductoDapperRepository _productoRepository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public ProductoDapperAppService(IProductoDapperRepository productoRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._productoRepository = productoRepository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<List<ProductoResponseDto>> ListarProducto()
        {
            List<ProductoResponseReadOnly> listarProducto = await this._productoRepository.ListarProducto();
            if (listarProducto != null)
            {
                return _mapper.Map<List<ProductoResponseDto>>(listarProducto);
            }
            throw new NotFoundException(Messages.NotFoundResource);
        }

        public async Task<ProductoResponseDto> ObtenerRegistro(int Id)
        {
            if (Id != 0)
            {
                ProductoResponseReadOnly producto = await this._productoRepository.ObtenerRegistro(Id);

                if (producto != null)
                {
                    return _mapper.Map<ProductoResponseDto>(producto);
                }

                throw new NotFoundException(Messages.NotFoundResource);
            }

            throw new ArgumentNullException("Variable: Id");
        }

        public async Task<ProductoResponseDto> CrearRegistro(ProductoRequestDto productoRequest)
        {
            if (productoRequest != null)
            {
                ProductoRequestReadOnly producto = _mapper.Map<ProductoRequestDto, ProductoRequestReadOnly>(productoRequest);

                ProductoResponseReadOnly respuesta = await this._productoRepository.CrearRegistro(producto);

                return _mapper.Map<ProductoResponseDto>(respuesta);
            }

            throw new ArgumentNullException("Objeto: ProductoRequestDto");
        }

        public async Task<ProductoResponseDto> EditarRegistro(ProductoRequestDto productoRequest)
        {
            if (productoRequest != null)
            {
                ProductoRequestReadOnly producto = _mapper.Map<ProductoRequestDto, ProductoRequestReadOnly>(productoRequest);

                ProductoResponseReadOnly respuesta = await this._productoRepository.EditarRegistro(producto);

                return _mapper.Map<ProductoResponseDto>(respuesta);
            }

            throw new ArgumentNullException("Objeto: ProductoRequestDto");
        }

        public async Task<ProductoResponseDto> EliminarRegistro(int Id)
        {
            if (Id != 0)
            {
                ProductoResponseReadOnly producto = await this._productoRepository.EliminarRegistro(Id);

                if (producto != null)
                {
                    return _mapper.Map<ProductoResponseDto>(producto);
                }

                throw new NotFoundException(Messages.NotFoundResource);
            }

            throw new ArgumentNullException("Variable: Id");

        }
    }
}
