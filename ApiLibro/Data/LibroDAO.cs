using ApiLibro.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ApiLibro.Data
{
    public class LibroDAO
    {
        string connectionString =
            ConfigurationManager.ConnectionStrings["LibreriaDBConnection"].ConnectionString;
        // GET ALL
        public List<Libro> GetAll()
        {
            List<Libro> lista = new List<Libro>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Libros";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Libro()
                    {
                        Id = (int)reader["Id"],
                        Titulo = reader["Titulo"].ToString(),
                        Autor = reader["Autor"].ToString(),
                        Precio = (decimal)reader["Precio"],
                        Stock = (int)reader["Stock"],
                        Imagen = reader["Imagen"].ToString()

                    });
                }
            }
            return lista;
        }

        // GET BY ID
        public Libro GetById(int id)
        {
            Libro L = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Libros WHERE Id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    L = new Libro()
                    {

                        Id = (int)reader["Id"],
                        Titulo = reader["Titulo"].ToString(),
                        Autor = reader["Autor"].ToString(),
                        Precio = (decimal)reader["Precio"],
                        Stock = (int)reader["Stock"],
                        Imagen = reader["Imagen"].ToString()

                    };
                }
            }
            return L;
        }

        // INSERT
        public void Insert(Libro L)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query =
                "INSERT INTO Libros (Titulo,Autor,Precio,Stock,Imagen) VALUES(@titulo,@autor,@precio,@stock,@imagen)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@titulo", L.Titulo);
                cmd.Parameters.AddWithValue("@autor", L.Autor);
                cmd.Parameters.AddWithValue("@precio", L.Precio);
                cmd.Parameters.AddWithValue("@stock", L.Stock);
                cmd.Parameters.AddWithValue("@imagen", L.Imagen);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // UPDATE
        public void Update(int id, Libro L)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Libros SET Stock=@stock WHERE Id=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@stock", L.Stock);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // DELETE
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Libros WHERE Id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}