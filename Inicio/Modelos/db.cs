using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Inicio.Vistas.Modelos
{
    public class Database
    {
        private string dbPath = $"Data Source=Inventario.db;Version=3;";

        public void InitializeDatabase()
        {
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
            }

            using (var connection = new SQLiteConnection(dbPath))
            {
                connection.Open();
                // Usuarios
                string query;
                query = "CREATE TABLE IF NOT EXISTS User (Id INTEGER PRIMARY KEY AUTOINCREMENT, Username TEXT UNIQUE, Password TEXT);";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                // Ingresar admin
                query = "INSERT OR IGNORE INTO User (Username, Password) VALUES ('admin', '1234');";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                // Proveedor
                query = "CREATE TABLE IF NOT EXISTS Proveedor (idProveedor INTEGER PRIMARY KEY AUTOINCREMENT, nombreEmpresa TEXT UNIQUE NOT NULL, direccion TEXT, telefono INTEGER);";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                // Categoria
                query = "CREATE TABLE IF NOT EXISTS Categoria (idCategoria INTEGER PRIMARY KEY AUTOINCREMENT, nombreCategoria TEXT UNIQUE NOT NULL, descripcion TEXT);";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                // Producto
                query = "CREATE TABLE IF NOT EXISTS Producto (idProducto INTEGER PRIMARY KEY AUTOINCREMENT, nombreProducto TEXT, idCategoria INTEGER, precioProducto REAL, cantidadProducto INTEGER, idProveedor INTEGER, FOREIGN KEY (idCategoria) REFERENCES Categoria(idCategoria), FOREIGN KEY (idProveedor) REFERENCES Proveedor(idProveedor));";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                // Insertamos el usuario Admin por defecto
                query = "INSERT OR IGNORE INTO User (Username, Password) VALUES ('admin', '1234');";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        public bool InsertUser(string username, string password)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                connection.Open();
                string query = "INSERT OR IGNORE INTO User (Username, Password) VALUES (@username, @password);";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    try
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool ValidateUser(string username, string password)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM User WHERE Username = @username AND Password = @password;";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }
    }
    }
