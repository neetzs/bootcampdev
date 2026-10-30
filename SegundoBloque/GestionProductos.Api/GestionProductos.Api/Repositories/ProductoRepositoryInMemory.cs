using System;
using System.Collections.Generic;
using System.Linq;
using GestionProductos.Api.Models;

namespace GestionProductos.Api.Repositories
{
    public class ProductoRepositoryInMemory : IProductoRepository
    {
        // Colección estática para persistir los datos entre solicitudes HTTP Scoped
        private static readonly List<Producto> _productos = new List<Producto>
        {
            new Producto { Id = 1, Nombre = "Teclado Mecánico RGB", Categoria = "Periféricos", Precio = 45000.50m, Activo = true },
            new Producto { Id = 2, Nombre = "Mouse Inalámbrico", Categoria = "Periféricos", Precio = 22000.00m, Activo = true },
            new Producto { Id = 3, Nombre = "Monitor 24'' 144Hz", Categoria = "Monitores", Precio = 185000.00m, Activo = true },
            new Producto { Id = 4, Nombre = "Auriculares Gamer", Categoria = "Audio", Precio = 35400.00m, Activo = false }
        };

        private static int _nextId = 5;
        private static readonly object _lock = new object();

        public List<Producto> ObtenerTodos()
        {
            lock (_lock)
            {
                return _productos.ToList();
            }
        }

        public Producto? ObtenerPorId(int id)
        {
            lock (_lock)
            {
                return _productos.FirstOrDefault(p => p.Id == id);
            }
        }

        public void Agregar(Producto producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));

            lock (_lock)
            {
                producto.Id = _nextId++;
                _productos.Add(producto);
            }
        }

        public bool Actualizar(Producto producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));

            lock (_lock)
            {
                var existente = _productos.FirstOrDefault(p => p.Id == producto.Id);
                if (existente == null)
                    return false;

                existente.Nombre = producto.Nombre;
                existente.Precio = producto.Precio;
                existente.Categoria = producto.Categoria;
                existente.Activo = producto.Activo;
                return true;
            }
        }

        public bool Eliminar(int id)
        {
            lock (_lock)
            {
                var existente = _productos.FirstOrDefault(p => p.Id == id);
                if (existente == null)
                    return false;

                return _productos.Remove(existente);
            }
        }
    }
}