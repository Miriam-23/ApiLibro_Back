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
        //conexion a la base de datos
        string connectionString =
           ConfigurationManager.ConnectionStrings["LibreriaDBConnection"].ConnectionString;
        
        // GET ALL
        public List<HistorialCar> GetAll()//metodo para obtener el historial 
        {
            //Lista de resultados
            List<HistorialCar> lista = new List<HistorialCar>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //consulta SQL que nos muestra el historial
                string query = "SELECT * FROM HistorialCar";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();// Abre la conexion
                SqlDataReader reader = cmd.ExecuteReader();// Ejecuta la consulta
                //recorre todas las filas
                while (reader.Read())
                {
                    // Convierte cada fila en un objeto HistorialCar
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
            return lista;//devuelve la lista
        }

        // GET BY ID
        public HistorialCar GetById(int Id)//metodo para obtener le registro po id
        {
            HistorialCar H = null;// se almacena el resultado en la variable
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Consulta con parametro
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
            return H;//retorna el objeto o null si no existe
        }


        // GET BY USERID
        public List<HistorialCarDTO> GetByUserId(int userId)//metodo para obtener el historial de compras de un usuario específico
        {
            // Lista que almacenará resultados combinados (DTO)
            List<HistorialCarDTO> lista = new List<HistorialCarDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //consulta SQL con JOINs para obtener informacion
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
                // Se crea el comando
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                //recorre los resultados 
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
            return lista; // devuelve la lista de historial del usuario
        }

        // INSERT
        public void Insert(HistorialCar H)//metodo que inserta un nuevo registro e el historial
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Consulta SQL para insertar datos
                string query =
                "INSERT INTO HistorialCar (UserId,LibroId,Cantidad,Precio) VALUES(@userid,@libroid,@cantidad,@precio)";
                SqlCommand cmd = new SqlCommand(query, conn);
                // Se le asignan los valores del objeto a los parametros
                cmd.Parameters.AddWithValue("@userid", H.UserId);
                cmd.Parameters.AddWithValue("@libroid", H.LibroId);
                cmd.Parameters.AddWithValue("@cantidad", H.Cantidad);
                cmd.Parameters.AddWithValue("@precio", H.Precio);
                conn.Open();
                // Ejecuta la consulta
                cmd.ExecuteNonQuery();
            }
        }

        // UPDATE
        public void Update(int id, HistorialCar H)
        {
            
        }

        // DELETE
        public void Delete(int id)//metodo que eliminna un registro por id
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Consulta para eliminar un registro
                string query = "DELETE FROM HistorialCar WHERE Id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                // Se asigna el ID a eliminar
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
