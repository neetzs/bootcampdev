using System;
using System.Collections.Generic;
using System.Linq;
using GestionProductos.Api.DTOs;
using GestionProductos.Api.Models;
using GestionProductos.Api.Repositories;

namespace GestionProductos.Api.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<ProductoDto> ObtenerTodos()
        {
            var productos = _repository.ObtenerTodos();
            return productos.Select(p => MapToDto(p)).ToList();
        }

        public ProductoDto? ObtenerPorId(int id)
        {
            var producto = _repository.ObtenerPorId(id);
            return producto == null ? null : MapToDto(producto);
        }

        public ProductoDto Crear(CreateProductoDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Categoria = dto.Categoria,
                Activo = true
            };

            _repository.Agregar(producto);
            return MapToDto(producto);
        }

        public bool Actualizar(int id, CreateProductoDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var productoExistente = _repository.ObtenerPorId(id);
            if (productoExistente == null)
                return false;

            productoExistente.Nombre = dto.Nombre;
            productoExistente.Precio = dto.Precio;
            productoExistente.Categoria = dto.Categoria;

            return _repository.Actualizar(productoExistente);
        }

        public bool Eliminar(int id)
        {
            return _repository.Eliminar(id);
        }

        private static ProductoDto MapToDto(Producto entity)
        {
            return new ProductoDto
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Precio = entity.Precio,
                Categoria = entity.Categoria,
                Activo = entity.Activo
            };
        }
    }
}