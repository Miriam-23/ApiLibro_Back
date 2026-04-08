using ApiLibro.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ApiLibro.Data
{
    public class HistorialCarDAO
    {
        string connectionString =
           ConfigurationManager.ConnectionStrings["LibreriaDBConnection"].ConnectionString;
        // GET ALL
        public List<HistorialCar> GetAll()
        {
            List<HistorialCar> lista = new List<HistorialCar>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM HistorialCar";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new HistorialCar()
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

        // GET BY ID
        public HistorialCar GetById(int Id)
        {
            HistorialCar H = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM HistorialCar WHERE Id = @id;";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", Id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    H = new HistorialCar()
                    {
                        Id = (int)(reader["Id"]),
                        UserId = (int)reader["UserId"],
                        LibroId = (int)reader["LibroId"],
                        Cantidad = (int)reader["Cantidad"],
                        Precio = (decimal)reader["Precio"]

                    };
                }
            }
            return H;
        }


        // GET BY USERID
        public List<HistorialCarDTO> GetByUserId(int userId)
        {
            List<HistorialCarDTO> lista = new List<HistorialCarDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                string query = @"SELECT 
                                    v.Id AS VentaId,
                                    v.UserId AS UserId,
                                    v.Fecha AS Fecha,
                                    v.Total AS Total,
                                    Hc.Cantidad AS Cantidad,
                                    Hc.Precio AS Precio,
                                    l.Titulo AS Titulo
                                FROM Ventas v
                                INNER JOIN HistorialCar Hc ON v.CarritoId = Hc.Id
                                INNER JOIN Libros l ON Hc.LibroId = l.Id;";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new HistorialCarDTO()
                    {
                        Id = (int)(reader["VentaId"]),
                        UserId = (int)reader["UserId"],
                        Fecha = (DateTime)reader["Fecha"],
                        Cantidad = (int)reader["Cantidad"],
                        Precio = (decimal)reader["Precio"],
                        Titulo = reader["Titulo"].ToString(),
                        Total = (decimal)reader["Total"]
                    });
                }
            }
            return lista;
        }

        // INSERT
        public void Insert(HistorialCar H)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query =
                "INSERT INTO HistorialCar (UserId,LibroId,Cantidad,Precio) VALUES(@userid,@libroid,@cantidad,@precio)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userid", H.UserId);
                cmd.Parameters.AddWithValue("@libroid", H.LibroId);
                cmd.Parameters.AddWithValue("@cantidad", H.Cantidad);
                cmd.Parameters.AddWithValue("@precio", H.Precio);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // UPDATE
        public void Update(int id, HistorialCar H)
        {
            
        }

        // DELETE
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM HistorialCar WHERE Id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
