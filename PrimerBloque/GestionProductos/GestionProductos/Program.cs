using GestionProductos.Modelos;
using GestionProductos.Repositorios;
using System;
using System.Collections.Generic;

namespace GestionProductos.Consola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IProductoRepository repo = new ProductoRepository();

            Console.WriteLine("==============================================");
            Console.WriteLine("   SISTEMA DE GESTION DE PRODUCTOS EN MEMORIA ");
            Console.WriteLine("==============================================\n");

            // 1. Obtener todos los productos iniciales
            Console.WriteLine("--- 1. Catalogo Completo (Seed Data) ---");
            ImprimirCatalogo(repo.ObtenerTodos());

            // 2. Busqueda por ID existente
            Console.WriteLine("\n--- 2. Busqueda por ID ---");
            int idExistente = 2;
            Producto? encontrado = repo.ObtenerPorId(idExistente);
            if (encontrado != null)
            {
                Console.WriteLine($"[Encontrado ID {idExistente}]: {encontrado}");
            }
            else
            {
                Console.WriteLine($"[Alerta] No existe el producto con ID {idExistente}.");
            }

            // Busqueda por ID inexistente para validar robustez
            int idInexistente = 99;
            Producto? noExiste = repo.ObtenerPorId(idInexistente);
            if (noExiste == null)
            {
                Console.WriteLine($"[Verificacion ID {idInexistente}]: Producto no encontrado (comportamiento esperado).");
            }

            // 3. Filtrar por Categoria
            string categoriaABuscar = "Periféricos";
            Console.WriteLine($"\n--- 3. Filtrado por Categoria: '{categoriaABuscar}' ---");
            List<Producto> filtrados = repo.BuscarPorCategoria(categoriaABuscar);
            ImprimirCatalogo(filtrados);

            // 4. Agregar un nuevo producto
            Console.WriteLine("\n--- 4. Incorporacion de Nuevo Producto ---");
            Producto nuevoProducto = new Producto
            {
                Id = 5,
                Nombre = "Pad Mouse XXL",
                Categoria = "Periféricos",
                Precio = 12500.00m,
                Activo = true
            };

            repo.Agregar(nuevoProducto);
            Console.WriteLine($"Producto agregado exitosamente: {nuevoProducto.Nombre}");

            // 5. Verificar lista actualizada
            Console.WriteLine("\n--- 5. Catalogo Actualizado post-insercion ---");
            ImprimirCatalogo(repo.ObtenerTodos());

            Console.WriteLine("\nEjecucion completada. Presione cualquier tecla para salir...");
            Console.ReadKey();
        }

        private static void ImprimirCatalogo(List<Producto> productos)
        {
            if (productos.Count == 0)
            {
                Console.WriteLine("No se encontraron registros.");
                return;
            }

            foreach (var prod in productos)
            {
                Console.WriteLine(prod);
            }
        }
    }
}