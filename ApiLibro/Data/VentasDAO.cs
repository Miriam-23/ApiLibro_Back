using ApiLibro.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ApiLibro.Data
{
    public class VentasDAO
    {
        string connectionString =
            ConfigurationManager.ConnectionStrings["LibreriaDBConnection"].ConnectionString;

        // GET ALL
        public List<Ventas> GetAll()
        {
            List<Ventas> lista = new List<Ventas>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Ventas";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Ventas()
                    {
                        Id = (int)reader["Id"],
                        CarritoId = (int)reader["CarritoId"],
                        UserId = (int)reader["UserId"],
                        Fecha = (DateTime)reader["Fecha"],
                        Total = (decimal)reader["Total"]
                    });
                }
            }
            return lista;
        }

        // GET BY ID
        public Ventas GetById(int id)
        {
            Ventas V = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Ventas WHERE Id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    V = new Ventas()
                    {

                        Id = (int)reader["Id"],
                        CarritoId = (int)reader["CarritoId"],
                        UserId = (int)reader["UserId"],
                        Fecha = (DateTime)reader["Fecha"],
                        Total = (decimal)reader["Total"]

                    };
                }
            }
            return V;
        }

        // INSERT
        public void Insert(Ventas V)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query =
                "INSERT INTO Ventas (CarritoId,UserId,Total) VALUES(@CarritoId,@userId,@total)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@carritoId", V.CarritoId);
                cmd.Parameters.AddWithValue("@userId", V.UserId);
                cmd.Parameters.AddWithValue("@total", V.Total);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


    }
}