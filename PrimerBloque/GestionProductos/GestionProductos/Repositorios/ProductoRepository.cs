using GestionProductos.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionProductos.Repositorios
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly List<Producto> _productos;

        public ProductoRepository()
        {
            // Seed data inicial
            _productos = new List<Producto>
            {
                new Producto { Id = 1, Nombre = "Teclado Mecánico RGB", Categoria = "Periféricos", Precio = 45000.50m, Activo = true },
                new Producto { Id = 2, Nombre = "Mouse Inalámbrico", Categoria = "Periféricos", Precio = 22000.00m, Activo = true },
                new Producto { Id = 3, Nombre = "Monitor 24'' 144Hz", Categoria = "Monitores", Precio = 185000.00m, Activo = true },
                new Producto { Id = 4, Nombre = "Auriculares Gamer", Categoria = "Audio", Precio = 35400.00m, Activo = false }
            };
        }

        public void Agregar(Producto producto)
        {
            if (producto == null)
            {
                throw new ArgumentNullException(nameof(producto), "El producto no puede ser nulo.");
            }

            _productos.Add(producto);
        }

        public List<Producto> ObtenerTodos()
        {
            return _productos.ToList();
        }

        public Producto? ObtenerPorId(int id)
        {
            return _productos.FirstOrDefault(p => p.Id == id);
        }

        public List<Producto> BuscarPorCategoria(string categoria)
        {
            return _productos
                .Where(p => p.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}

