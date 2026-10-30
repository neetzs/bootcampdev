using System.Collections.Generic;
using GestionProductos.Api.DTOs;

namespace GestionProductos.Api.Services
{
    public interface IProductoService
    {
        List<ProductoDto> ObtenerTodos();
        ProductoDto? ObtenerPorId(int id);
        ProductoDto Crear(CreateProductoDto dto);
        bool Actualizar(int id, CreateProductoDto dto);
        bool Eliminar(int id);
    }
}