using System;
using System.Collections;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inicio.Modelos
{
    public class ValidacionDeCampos
    {
        public string[] ValidarProductos(string nombre, string idProveedor, string idCategoria, string precio, string cantidad, string mode, string idProducto)
        {

            string[] result = new string[2];

            if(mode == "update" && string.IsNullOrWhiteSpace(idProducto)){
                result[0] = "error";
                result[1] = "El campo id del producto no debe estar vacío";
                return result;
            }
            if (mode == "update")
            {
                if (string.IsNullOrWhiteSpace(idProducto))
                {
                    result[0] = "error";
                    result[1] = "El campo id del producto no debe estar vacío";
                    return result;
                }else if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(idProveedor) || string.IsNullOrWhiteSpace(idCategoria) || string.IsNullOrWhiteSpace(precio) || string.IsNullOrWhiteSpace(cantidad))
                {
                    result[0] = "error";
                    result[1] = "Los campos deben estar llenos para actualizar los datos";
                    return result;
                }
                else
                {
                    result[0] = "success";
                    return result;
                }
            }
            if (mode == "select")
            {
                if (string.IsNullOrWhiteSpace(idProducto) && string.IsNullOrWhiteSpace(nombre))
                {
                    result[0] = "error";
                    result[1] = "Al menos uno de los campos id o nombre del producto no debe estar vacío";
                    return result;
                }
                else
                {
                    result[0] = "success";
                    return result;
                }   
            }
            if(mode == "delete")
            {
                if (string.IsNullOrWhiteSpace(idProducto))
                {
                    result[0] = "error";
                    result[1] = "El campo \"id\" no debe estar vacío";
                    return result;
                }
                else
                {
                    result[0] = "success";
                    return result;
                }
            }

            if (string.IsNullOrWhiteSpace(idProducto) && string.IsNullOrWhiteSpace(nombre))
            {
                result[0] = "error";
                result[1] = "Al menos uno de los campos id o nombre del producto no debe estar vacío";
                return result;
            }

            if (string.IsNullOrWhiteSpace(idProveedor) || string.IsNullOrWhiteSpace(idCategoria) || string.IsNullOrWhiteSpace(precio) || string.IsNullOrWhiteSpace(cantidad))
            {
                result[0] = "error";
                result[1] = "Ningun campo debe estar vacío";
                return result;
            }

            if (!double.TryParse(precio, NumberStyles.Any, CultureInfo.InvariantCulture, out double nuevaPrecio))
            {
                result[0] = "error";
                result[1] = "El precio del producto debe ser un número válido.";
                return result;
            }

            if (!int.TryParse(cantidad, out int nuevaCantidad))
            {
                result[0] = "error";
                result[1] = "La cantidad del producto debe ser un número entero válido..";
                return result;
            }
            result[0] = "success";
            return result;

        }
        public string[] ValidarProveedores(string empresa, string direccion, string telefono, string mode, string id)
        {
            string[] result = new string[2];
            if (mode == "insert")
            {
                if (string.IsNullOrWhiteSpace(empresa) || string.IsNullOrWhiteSpace(direccion) || string.IsNullOrWhiteSpace(telefono))
                {
                    result[0] = "error";
                    result[1] = "Los campos empresa, direccion y telefono deben estar llenos para agregar el proveedor";
                    return result;
                }
                else if (!string.IsNullOrWhiteSpace(empresa) && !string.IsNullOrWhiteSpace(direccion) && !string.IsNullOrWhiteSpace(telefono))
                {
                    result[0] = "success";
                    return result;
                }
                else
                {
                    result[0] = "error";
                    result[1] = "Algo ha salido mal, trate mas tarde";
                    return result;
                }
            }
            if (mode == "update")
            {

                if (string.IsNullOrWhiteSpace(id))
                {
                    result[0] = "error";
                    result[1] = "El campo id de la categoria no debe estar vacío";
                    return result;
                }
                if (!int.TryParse(id, out int nuevoId))
                {
                    result[0] = "error";
                    result[1] = "El id de la categoria debe ser un número entero válido..";
                    return result;
                }
                else if (string.IsNullOrWhiteSpace(empresa) || string.IsNullOrWhiteSpace(direccion))
                {
                    result[0] = "error";
                    result[1] = "Los campos deben estar llenos para actualizar los datos";
                    return result;
                }
                else
                {
                    result[0] = "success";
                    return result;
                }
            }
            if (mode == "select")
            {
                if (string.IsNullOrWhiteSpace(id) && string.IsNullOrWhiteSpace(empresa))
                {
                    result[0] = "error";
                    result[1] = "Al menos uno de los campos id o nombre de la categoria no debe estar vacío";
                    return result;
                }
                else
                {
                    result[0] = "success";
                    return result;
                }
            }
            if (mode == "delete")
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    result[0] = "error";
                    result[1] = "El campo \"id\" no debe estar vacío";
                    return result;
                }
                else if (!int.TryParse(id, out int nuevoId))
                {
                    result[0] = "error";
                    result[1] = "El id de la categoria debe ser un número entero válido..";
                    return result;
                }
                else
                {
                    result[0] = "success";
                    return result;
                }
            }
            result[0] = "success";
            return result;
        }
        public string[] ValidarCategorias(string nombre, string descripcion, string mode, string id)
        { 

            string[] result = new string[2];

            if (mode == "insert")
            {
                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(descripcion))
                {
                    result[0] = "error";
                    result[1] = "Los campos nombre y descripcion deben estar llenos para agregar la categoria";
                    return result;
                }
                else if(!string.IsNullOrWhiteSpace(nombre) && !string.IsNullOrWhiteSpace(descripcion))
                {
                    result[0] = "success";
                    return result;
                }
                else
                {
                    result[0] = "error";
                    result[1] = "Algo ha salido mal, trate mas tarde";
                    return result;
                }
            }
            if (mode == "update") {
            
                if (string.IsNullOrWhiteSpace(id))
                {
                    result[0] = "error";
                    result[1] = "El campo id de la categoria no debe estar vacío";
                    return result;
                }
                if (!int.TryParse(id, out int nuevoId))
                {
                    result[0] = "error";
                    result[1] = "El id de la categoria debe ser un número entero válido..";
                    return result;
                }
                else if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(descripcion))
                {
                    result[0] = "error";
                    result[1] = "Los campos deben estar llenos para actualizar los datos";
                    return result;
                }
                else
                {
                    result[0] = "success";
                    return result;
                }
            }
            if (mode == "select")
            {
                if (string.IsNullOrWhiteSpace(id) && string.IsNullOrWhiteSpace(nombre))
                {
                    result[0] = "error";
                    result[1] = "Al menos uno de los campos id o nombre de la categoria no debe estar vacío";
                    return result;
                }else 
                {
                    result[0] = "success";
                    return result;
                }
            }
            if (mode == "delete")
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    result[0] = "error";
                    result[1] = "El campo \"id\" no debe estar vacío";
                    return result;
                }
                else if (!int.TryParse(id, out int nuevoId))
                {
                    result[0] = "error";
                    result[1] = "El id de la categoria debe ser un número entero válido..";
                    return result;
                }
                else
                {
                    result[0] = "success";
                    return result;
                }
            }
            result[0] = "success";
            return result;
        }
    }
}
