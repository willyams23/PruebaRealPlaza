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
    public class ProductoAppService: IProductoAppService
    {
        private IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public ProductoAppService(IProductoRepository productoRepository, IUnitOfWork unitOfWork, IMapper mapper)
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
                Producto producto = _mapper.Map<ProductoRequestDto, Producto>(productoRequest);
                producto.Id = 0;
                this._productoRepository.Add(producto);

                await _unitOfWork.CommitAsync();

                return _mapper.Map<ProductoResponseDto>(producto);
            }

            throw new ArgumentNullException("Objeto: ProductoRequestDto");
        }

        public async Task<ProductoResponseDto> EditarRegistro(ProductoRequestDto productoRequest)
        {
            if (productoRequest != null)
            {
                Producto producto = this._productoRepository.Get(productoRequest.Id);
                producto.Id = productoRequest.Id;
                producto.Nombre = productoRequest.Nombre;
                producto.Precio = productoRequest.Precio;
                producto.Stock = productoRequest.Stock;
                producto.FechaRegistro = DateTime.Now;

                this._productoRepository.Modify(producto);

                await _unitOfWork.CommitAsync();

                return _mapper.Map<ProductoResponseDto>(producto);
            }

            throw new ArgumentNullException("Objeto: ProductoRequestDto");
        }

        public async Task<ProductoResponseDto> EliminarRegistro(int Id)
        {
            if (Id != 0)
            {
                ProductoResponseReadOnly productoResponse = await this._productoRepository.ObtenerRegistro(Id);

                Producto producto = _mapper.Map<ProductoResponseReadOnly, Producto>(productoResponse);

                this._productoRepository.Remove(producto);

                await _unitOfWork.CommitAsync();

                return _mapper.Map<ProductoResponseDto>(producto);
            }

            throw new ArgumentNullException("Variable: Id");
        }
    }
}
