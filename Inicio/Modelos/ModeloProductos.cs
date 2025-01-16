using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inicio.Modelos
{
    public class Producto
    {
        public string nombreProducto { get; set; }
        public int idProducto { get; set; }
        public int idCategoria { get; set; }
        public double precioProducto { get; set; }
        public int cantidadProducto { get; set; }
        public int idProveedor { get; set; }

    }
}
