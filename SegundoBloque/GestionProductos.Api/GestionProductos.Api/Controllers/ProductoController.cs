using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GestionProductos.Api.DTOs;
using GestionProductos.Api.Services;

namespace GestionProductos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _service;

        public ProductosController(IProductoService service)
        {
            _service = service;
        }

        // GET: api/productos
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            List<ProductoDto> productos = _service.ObtenerTodos();
            return Ok(productos);
        }

        // GET: api/productos/{id}
        [HttpGet("{id:int}")]
        public IActionResult ObtenerPorId(int id)
        {
            var producto = _service.ObtenerPorId(id);
            if (producto == null)
            {
                return NotFound(new { mensaje = $"No se encontró el producto con ID {id}." });
            }

            return Ok(producto);
        }

        // POST: api/productos
        [HttpPost]
        public IActionResult Crear([FromBody] CreateProductoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nuevoProducto = _service.Crear(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoProducto.Id }, nuevoProducto);
        }

        // PUT: api/productos/{id}
        [HttpPut("{id:int}")]
        public IActionResult Actualizar(int id, [FromBody] CreateProductoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool actualizado = _service.Actualizar(id, dto);
            if (!actualizado)
            {
                return NotFound(new { mensaje = $"No se pudo actualizar. El producto con ID {id} no existe." });
            }

            return NoContent();
        }

        // DELETE: api/productos/{id}
        [HttpDelete("{id:int}")]
        public IActionResult Eliminar(int id)
        {
            bool eliminado = _service.Eliminar(id);
            if (!eliminado)
            {
                return NotFound(new { mensaje = $"No se pudo eliminar. El producto con ID {id} no existe." });
            }

            return NoContent();
        }
    }
}