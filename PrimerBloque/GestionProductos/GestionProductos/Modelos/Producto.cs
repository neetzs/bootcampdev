using System;
using System.Collections.Generic;
using System.Text;

namespace GestionProductos.Modelos
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public bool Activo { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Nombre} | Categoría: {Categoria} | Precio: ${Precio:F2} | Estado: {(Activo ? "Activo" : "Inactivo")}";
        }
    }
}
