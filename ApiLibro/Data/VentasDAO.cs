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
        //conexion con la base de datos
        string connectionString =
            ConfigurationManager.ConnectionStrings["LibreriaDBConnection"].ConnectionString;

        // GET ALL
        public List<Ventas> GetAll()//metodo para obtener todas las ventas registradas
        {
            //lista donde se guardaran los resultados
            List<Ventas> lista = new List<Ventas>();
            //se hace la conexión a la base de datos
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Consulta SQL para obtener todas las ventas
                string query = "SELECT * FROM Ventas";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                // Se ejecuta la consulta y se obtiene un lector de datos
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // Se crea un objeto Ventas por cada fila
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
            return lista;//se retorna la lista completa de ventas
        }

        // GET BY ID
        public Ventas GetById(int id)//metodo para obtener una venta especifica por id
        {
            // Variable donde se almacenara el resultado
            Ventas V = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Consulta SQL con parametro
                string query = "SELECT * FROM Ventas WHERE Id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                // Se asigna el valor al parametro 
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                // Se ejecuta la consulta
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Se crea el objeto
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
            return V; // Retorna la venta encontrada o null si no existe
        }

        // INSERT
        public void Insert(Ventas V)//metodo que registra una nueva venta
        {            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Consulta SQL para insertar una venta
                string query =
                "INSERT INTO Ventas (CarritoId,UserId,Total) VALUES(@CarritoId,@userId,@total)";
                // Se crea el comando SQL
                SqlCommand cmd = new SqlCommand(query, conn);
                // Se asignan los valores desde el objeto Ventas
                cmd.Parameters.AddWithValue("@carritoId", V.CarritoId);
                cmd.Parameters.AddWithValue("@userId", V.UserId);
                cmd.Parameters.AddWithValue("@total", V.Total);
                // Se abre la conexion
                conn.Open();
                // se ejecuta la inserción pero no devuelve datos
                cmd.ExecuteNonQuery();
            }
        }


    }
}