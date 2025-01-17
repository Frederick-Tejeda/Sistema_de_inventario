using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inicio.Modelos;

namespace Inicio.Controladores
{
    public class ProveedoresControlador
    {
        private string connectionString = $"Data Source=Inventario.db;Version=3;";
        public void AgregarProveedor(Proveedor proveedor)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT OR IGNORE INTO Proveedor (nombreEmpresa, direccion, telefono) VALUES (@nombreEmpresa, @direccion, @telefono)";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreEmpresa", proveedor.nombreEmpresa);
                    command.Parameters.AddWithValue("@direccion", proveedor.direccion);
                    command.Parameters.AddWithValue("@telefono", proveedor.telefono);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Proveedor> ObtenerProveedores()
        {
            List<Proveedor> proveedores = new List<Proveedor>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Proveedor";
                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        proveedores.Add(new Proveedor
                        {
                            idProveedor = Convert.ToInt32(reader["idProveedor"]),
                            nombreEmpresa = reader["nombreEmpresa"].ToString(),
                            telefono = Convert.ToInt64(reader["telefono"]),
                            direccion = reader["direccion"].ToString()
                        });
                    }
                }
            }
            return proveedores;
        }
        public void ActualizarProveedor(string nombreEmpresa, int idProveedor, string direccion, long telefono)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Proveedor WHERE IdProveedor = @id"; // Consulta con parámetro @id
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", idProveedor); // Asignar el valor al parámetro

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Leer el resultado (si existe un producto con ese ID)
                        {
                            query = "UPDATE Proveedor SET nombreEmpresa = @nombreEmpresa, direccion = @direccion, telefono = @telefono WHERE idProveedor = @idProveedor";
                            using (var nuevoCommand = new SQLiteCommand(query, connection))
                            {
                                nuevoCommand.Parameters.AddWithValue("@nombreEmpresa", nombreEmpresa);
                                nuevoCommand.Parameters.AddWithValue("@idProveedor", idProveedor);
                                nuevoCommand.Parameters.AddWithValue("@direccion", direccion);
                                nuevoCommand.Parameters.AddWithValue("@telefono", telefono);

                                nuevoCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            Console.WriteLine("No se encontró ningún proveedor con ese ID.");
                            MessageBox.Show($"No se encontro el proveedor con id {idProveedor}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

            }
        }
        public List<Proveedor> BuscarProveedor(string dato, string tipoDato)
        {
            List<Proveedor> proveedores = new List<Proveedor>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query;
                if (tipoDato == "id")
                {
                    query = "SELECT * FROM Proveedor WHERE idProveedor = @dato";
                }
                else
                {
                    query = "SELECT * FROM Proveedor WHERE nombreEmpresa = @dato";
                }
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@dato", dato); // Asignar el valor al parámetro

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            proveedores.Add(new Proveedor
                            {
                                idProveedor = Convert.ToInt32(reader["idProveedor"]),
                                nombreEmpresa = reader["nombreEmpresa"].ToString(),
                                direccion = reader["direccion"].ToString(),
                                telefono = Convert.ToInt64(reader["telefono"])
                            });
                        }
                    }
                }
            }
            return proveedores;
        }
        public void EliminarProveedor(string id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Proveedor WHERE idProveedor = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id); // Asignar el valor al parámetro
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
