using Inicio.Modelos;
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
    public class CategoriasControlador
    {
            private string connectionString = $"Data Source=Inventario.db;Version=3;";
            public void AgregarCategoria(Categoria categoria)
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT OR IGNORE INTO Categoria (nombreCategoria, idCategoria, descripcionCategoria) VALUES (@nombreCategoria, @idCategoria, @descripcionCategoria)";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nombreCategoria", categoria.nombreCategoria);
                        command.Parameters.AddWithValue("@idCategoria", categoria.idCategoria);
                        command.Parameters.AddWithValue("@descripcionCategoria", categoria.descripcionCategoria);

                        command.ExecuteNonQuery();
                    }
                }
            }

            public List<Categoria> ObtenerCategorias()
            {
                List<Categoria> categorias = new List<Categoria>();
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Categoria";
                    using (var command = new SQLiteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categorias.Add(new Categoria
                            {
                                idCategoria = Convert.ToInt32(reader["idCategoria"]),
                                nombreCategoria = reader["nombreCategoria"].ToString(),
                                descripcionCategoria = reader["descripcionCategoria"].ToString()
                            });
                        }
                    }
                }
                return categorias;
            }
            public void ActualizarCategoria(string nombreCategoria, int idCategoria, string descripcionCategoria)
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Categoria WHERE IdCategoria = @id"; // Consulta con parámetro @id
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", idCategoria); // Asignar el valor al parámetro

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Leer el resultado (si existe un producto con ese ID)
                            {
                                query = "UPDATE Categoria SET nombreCategoria = @nombre, descripcionCategoria = @descripcionCategoria WHERE idCategoria = @id";
                                using (var nuevoCommand = new SQLiteCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@nombreCategoria", nombreCategoria);
                                    command.Parameters.AddWithValue("@idCategoria", idCategoria);
                                    command.Parameters.AddWithValue("@descripcionCategoria", descripcionCategoria);

                                    nuevoCommand.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                Console.WriteLine("No se encontró ningún producto con ese ID.");
                                MessageBox.Show($"No se encontro el producto con id {idCategoria}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }

                }
            }
            public List<Categoria> BuscaCategoria(string dato, string tipoDato)
            {
                List<Categoria> categorias = new List<Categoria>();
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query;
                    if (tipoDato == "id")
                    {
                        query = "SELECT * FROM Categoria WHERE idCategoria = @dato";
                    }
                    else
                    {
                        query = "SELECT * FROM Categoria WHERE nombreCategoria = @dato";
                    }
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@dato", dato); // Asignar el valor al parámetro

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                categorias.Add(new Categoria
                                {
                                    idCategoria = Convert.ToInt32(reader["idCategoria"]),
                                    nombreCategoria = reader["nombreCategoria"].ToString(),
                                    descripcionCategoria = reader["descripcionCategoria"].ToString()
                                });
                            }
                        }
                    }
                }
                return categorias;
            }
            public void EliminarCategoria(string id)
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Categoria WHERE idCategoria = @id";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id); // Asignar el valor al parámetro
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
}
