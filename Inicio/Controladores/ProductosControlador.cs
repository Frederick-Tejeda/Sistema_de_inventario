using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inicio.Modelos;

namespace Inicio.Controladores
{
    public class ProductosControlador
    {
        private string connectionString = $"Data Source=Inventario.db;Version=3;";

        public void AgregarProducto(Producto producto)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT OR IGNORE INTO Producto (nombreProducto, idCategoria, precioProducto, cantidadProducto, idProveedor) VALUES (@nombreProducto, @idCategoria, @precioProducto, @cantidadProducto, @idProveedor)";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreProducto", producto.nombreProducto);
                    //command.Parameters.AddWithValue("@idProducto", producto.idProducto);
                    command.Parameters.AddWithValue("@idCategoria", producto.idCategoria);
                    command.Parameters.AddWithValue("@precioProducto", producto.precioProducto);
                    command.Parameters.AddWithValue("@cantidadProducto", producto.cantidadProducto);
                    command.Parameters.AddWithValue("@idProveedor", producto.idProveedor);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Producto> ObtenerProductos()
        {
            List<Producto> productos = new List<Producto>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Producto";
                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productos.Add(new Producto
                        {
                            idProducto = Convert.ToInt32(reader["idProducto"]),
                            nombreProducto = reader["nombreProducto"].ToString(),
                            idCategoria = Convert.ToInt32(reader["idCategoria"]),
                            idProveedor = Convert.ToInt32(reader["idProveedor"]),
                            precioProducto = Convert.ToDouble(reader["precioProducto"]),
                            cantidadProducto = Convert.ToInt32(reader["cantidadProducto"])
                        });
                    }
                }
            }
            return productos;
        }
        public void ActualizarProducto(string nombre, int proveedor, int cantidad, int categoria, double precio, int id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Producto WHERE IdProducto = @id"; // Consulta con parámetro @id
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id); // Asignar el valor al parámetro

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Leer el resultado (si existe un producto con ese ID)
                        {
                            query = "UPDATE Producto SET nombreProducto = @nombre, idProveedor = @proveedor, cantidadProducto = @cantidad, idCategoria = @categoria, precioProducto = @precio WHERE idProducto = @id";
                            using (var nuevoCommand = new SQLiteCommand(query, connection))
                            {
                                nuevoCommand.Parameters.AddWithValue("@nombre", nombre);
                                nuevoCommand.Parameters.AddWithValue("@proveedor", proveedor);
                                nuevoCommand.Parameters.AddWithValue("@cantidad", cantidad);
                                nuevoCommand.Parameters.AddWithValue("@categoria", categoria);
                                nuevoCommand.Parameters.AddWithValue("@precio", precio);
                                nuevoCommand.Parameters.AddWithValue("@id", id);
                                nuevoCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            Console.WriteLine("No se encontró ningún producto con ese ID.");
                            MessageBox.Show($"No se encontro el producto con id {id}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
               
            }
        }
        public List<Producto> BuscarProducto(string dato, string tipoDato)
        {
            List<Producto> productos = new List<Producto>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query;
                if (tipoDato == "id")
                {
                    query = "SELECT * FROM Producto WHERE idProducto = @dato";
                }
                else
                {
                    query = "SELECT * FROM Producto WHERE nombreProducto = @dato";
                }
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@dato", dato); // Asignar el valor al parámetro

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productos.Add(new Producto
                            {
                                idProducto = Convert.ToInt32(reader["idProducto"]),
                                nombreProducto = reader["nombreProducto"].ToString(),
                                idCategoria = Convert.ToInt32(reader["idCategoria"]),
                                idProveedor = Convert.ToInt32(reader["idProveedor"]),
                                precioProducto = Convert.ToDouble(reader["precioProducto"]),
                                cantidadProducto = Convert.ToInt32(reader["cantidadProducto"])
                            });
                        }
                    }
                }
            }
            return productos;
        }
        public void EliminarProducto(string id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Producto WHERE idProducto = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id); // Asignar el valor al parámetro
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
