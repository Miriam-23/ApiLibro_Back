using ApiLibro.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ApiLibro.Data
{
    public class CarritoDAO
    {
        string connectionString =
            ConfigurationManager.ConnectionStrings["LibreriaDBConnection"].ConnectionString;
        // GET ALL
        public List<Carrito> GetAll()
        {
            List<Carrito> lista = new List<Carrito>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Carrito";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Carrito()
                    {

                        Id = (int)reader["Id"],
                        UserId = (int)reader["UserId"],
                        LibroId = (int)reader["LibroId"],
                        Cantidad = (int)reader["Cantidad"],
                        Precio = (decimal)reader["Precio"]

                    });
                }
            }
            return lista;
        }

        // GET BY USERID
        public Carrito GetById(int Id)
        {
            Carrito C = null;
            List<CarritoDTO> lista = new List<CarritoDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                string query = "SELECT * FROM Carrito WHERE Id = @id;";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", Id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    C = new Carrito()
                    {
                        Id = (int)(reader["Id"]),
                        UserId = (int)reader["UserId"],
                        LibroId = (int)reader["LibroId"],
                        Cantidad = (int)reader["Cantidad"],
                        Precio = (decimal)reader["Precio"]

                    };
                }
            }
            return C;
        }


        // GET BY USERID
        public List<CarritoDTO> GetByUserId(int userId)
        {
            List<CarritoDTO> lista = new List<CarritoDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                
                string query = @"SELECT 
                                    C.Id AS Id,
                                    C.UserId AS UserId, 
                                    C.LibroId AS LibroId,
                                    C.Cantidad AS Cantidad, 
                                    C.Precio AS Precio, 
                                    L.Titulo AS Titulo, 
                                    L.Imagen AS Imagen
                                FROM Carrito C 
                                INNER JOIN Libros L ON C.LibroId = L.Id 
                                WHERE C.UserId = @userId;";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add( new CarritoDTO()
                    {
                        Id= (int)(reader["Id"]),
                        UserId = (int)reader["UserId"],
                        LibroId = (int)reader["LibroId"],
                        Cantidad = (int)reader["Cantidad"],
                        Precio = (decimal)reader["Precio"],
                        Titulo = reader["Titulo"].ToString(),
                        Imagen = reader["Imagen"].ToString()

                    });
                }
            }
            return lista;
        }

        // INSERT
        public void Insert(Carrito C)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query =
                "INSERT INTO Carrito (UserId,LibroId,Cantidad,Precio) VALUES(@userid,@libroid,@cantidad,@precio)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userid", C.UserId);
                cmd.Parameters.AddWithValue("@libroid", C.LibroId);
                cmd.Parameters.AddWithValue("@cantidad", C.Cantidad);
                cmd.Parameters.AddWithValue("@precio", C.Precio);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // UPDATE
        public void Update(int id, Carrito C)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Carrito SET Cantidad=@cantidad WHERE Id=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@cantidad", C.Cantidad);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        
        // Vaciar carrito tras compra (marca CompraRealizada y borra items)
        public void VaciarCarrito(int userId)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1️⃣ Marcar items como comprados
                var marcarCmd = new SqlCommand("UPDATE Carrito SET CompraRealizada=1 WHERE UserId=@userId", conn);
                marcarCmd.Parameters.AddWithValue("@userId", userId);
                marcarCmd.ExecuteNonQuery();

                // 2️⃣ Borrar items del carrito (trigger moverá al historial)
                var deleteCmd = new SqlCommand("DELETE FROM Carrito WHERE UserId=@userId", conn);
                deleteCmd.Parameters.AddWithValue("@userId", userId);
                deleteCmd.ExecuteNonQuery();
            }
        }

        // DELETE
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Carrito WHERE Id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}