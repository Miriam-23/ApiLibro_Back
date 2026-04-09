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
        //conexion del servidor  
        string connectionString =
            ConfigurationManager.ConnectionStrings["LibreriaDBConnection"].ConnectionString;
        
        // GET ALL
        public List<Carrito> GetAll() //metodo para obtener todos los elementos de carrito
        {
            //se guardan los resultados en la lista  
            List<Carrito> lista = new List<Carrito>();

            //se hace conexion con la base de datos
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //consulta de SQL
                string query = "SELECT * FROM Carrito";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                ////se crea un objeto carrito por cada fila y se agrega a la lista
                while (reader.Read())
                {
                    lista.Add(new Carrito()
                    {

                        Id = (int)reader["Id"], //se obtiene id
                        UserId = (int)reader["UserId"], //id del usuario
                        LibroId = (int)reader["LibroId"],//id del libro
                        Cantidad = (int)reader["Cantidad"],//cantidad de producto
                        Precio = (decimal)reader["Precio"] //precio unitario 

                    });
                }
            }
            return lista; //regresa la lista completa 
        }

        // GET BY USERID
        public Carrito GetById(int Id) //metodo que busca un elemento por su id 
        {
            Carrito C = null; //objeto a retornar
            List<CarritoDTO> lista = new List<CarritoDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //consulta SQL con parametros
                string query = "SELECT * FROM Carrito WHERE Id = @id;";
                SqlCommand cmd = new SqlCommand(query, conn);
                //evita ataques de Inyección SQL
                cmd.Parameters.AddWithValue("@id", Id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    C = new Carrito()
                    {
                        Id = (int)(reader["Id"]),//se obtiene id
                        UserId = (int)reader["UserId"], //id del usuario
                        LibroId = (int)reader["LibroId"],//id del libro
                        Cantidad = (int)reader["Cantidad"],//cantidad de producto
                        Precio = (decimal)reader["Precio"]//precio unitario

                    };
                }
            }
            return C;//retorna el objeto encontrado
        }


        // GET BY USERID
        public List<CarritoDTO> GetByUserId(int userId)//metodo que obtiene el carrito de un usuario
        {
            List<CarritoDTO> lista = new List<CarritoDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //consulta para traer la informacion del libro
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
                //se recorren los resultados
                while (reader.Read())
                {
                    lista.Add( new CarritoDTO()
                    {
                        Id= (int)(reader["Id"]),//se obtiene id
                        UserId = (int)reader["UserId"],//id del usuario
                        LibroId = (int)reader["LibroId"],//id del libro
                        Cantidad = (int)reader["Cantidad"],//cantidad de producto
                        Precio = (decimal)reader["Precio"],//precio unitario
                        Titulo = reader["Titulo"].ToString(),//nombre del libro
                        Imagen = reader["Imagen"].ToString()//imagen del libro

                    });
                }
            }
            return lista; //devuleve la lista del carrito del usuario 
        }

        // INSERT
        public void Insert(Carrito C)// metodo para insertar un nuevo producto al carrito
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //se inserta un nuevo registro en la tabla carrito
                string query =
                "INSERT INTO Carrito (UserId,LibroId,Cantidad,Precio) VALUES(@userid,@libroid,@cantidad,@precio)";
                //se envian valores como parametros
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userid", C.UserId);
                cmd.Parameters.AddWithValue("@libroid", C.LibroId);
                cmd.Parameters.AddWithValue("@cantidad", C.Cantidad);
                cmd.Parameters.AddWithValue("@precio", C.Precio);
                conn.Open();
                cmd.ExecuteNonQuery();//se ejecuta la insercion
            }
        }

        // UPDATE
        public void Update(int id, Carrito C)//metodo para actualizar la cantidad de item en le carrito
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //se permite modificar la cantidad de un producto basandose en el id
                string query = "UPDATE Carrito SET Cantidad=@cantidad WHERE Id=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@cantidad", C.Cantidad);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        
        // Vaciar carrito tras compra (marca CompraRealizada y borra items)
        public void VaciarCarrito(int userId)//metodo para finalizar la compra
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
        public void Delete(int id) //metodo para eliminar el registro
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //se elimina por id
                string query = "DELETE FROM Carrito WHERE Id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();//ejecuta la eliminacion
            }
        }
    }
}