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
        //conexion con la base de datos
        string connectionString =
            ConfigurationManager.ConnectionStrings["LibreriaDBConnection"].ConnectionString;
        
        // GET ALL
        public List<Libro> GetAll() //metodo que devuleve el catalogo de libros
        {
            //se crea una lista vacia donde se guardaran los libros
            List<Libro> lista = new List<Libro>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //consulta SQL de los libros
                string query = "SELECT * FROM Libros";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                //se ejecuta la consulta y permite leer los resultados
                SqlDataReader reader = cmd.ExecuteReader();

                //se recorren los resultados fila por fila
                while (reader.Read())
                {
                    //se crea un objeto Libro con los datos de la fila
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
            return lista;//devuelve la lista con todos los libros encontrados
        }

        // GET BY ID
        public Libro GetById(int id)//metodo que busca un libro en especifico por su id
        {
            //variable donde se guardara el libro encontrado
            Libro L = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //consulta con parametro para evitar ataques de inyección SQL
                string query = "SELECT * FROM Libros WHERE Id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                //consulta con parametro para evitar ataques de inyección SQL
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    //se construye el objeto Libro con los datos de la fila encontrada
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
            return L;//devuelve el libro 
        }

        // INSERT
        public void Insert(Libro L)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Consulta para insertar datos
                string query =
                "INSERT INTO Libros (Titulo,Autor,Precio,Stock,Imagen) VALUES(@titulo,@autor,@precio,@stock,@imagen)";
                SqlCommand cmd = new SqlCommand(query, conn);
                // Se asignan los valores a los parámetros
                cmd.Parameters.AddWithValue("@titulo", L.Titulo);
                cmd.Parameters.AddWithValue("@autor", L.Autor);
                cmd.Parameters.AddWithValue("@precio", L.Precio);
                cmd.Parameters.AddWithValue("@stock", L.Stock);
                cmd.Parameters.AddWithValue("@imagen", L.Imagen);
                conn.Open();
                // Ejecuta la consulta pero no devuelve datos
                cmd.ExecuteNonQuery(); 
            }
        }

        // UPDATE
        public void Update(int id, Libro L)//metodo para actualizar el sotck de un libro
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Consulta para actualizar solo el stock
                string query = @"UPDATE Libros SET Stock=@stock WHERE Id=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                // id del libro que se va actualizar
                cmd.Parameters.AddWithValue("@id", id);
                //nuevo valor de stock
                cmd.Parameters.AddWithValue("@stock", L.Stock);
                conn.Open();
                cmd.ExecuteNonQuery();//se ejecuta la actualizacion
            }
        }

        // DELETE
        public void Delete(int id)//metodo para eliminar un libro
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Consulta para eliminar un registro
                string query = "DELETE FROM Libros WHERE Id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                // id del libro que se va eliminar
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}