using ApiLibro.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ApiLibro.Data
{
    public class UsuariosDAO
    {
        //conexion con la base de datos
        string connectionString =
            ConfigurationManager.ConnectionStrings["LibreriaDBConnection"].ConnectionString;
        
        // GET ALL
        public List<Usuarios> GetAll()//metodos para obtener los usuarios de la base de datos
        {
            //se crea una lista vacía donde se guardarán los usuarios
            List<Usuarios> lista = new List<Usuarios>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //consulta SQL para obtener todos los registros
                string query = "SELECT * FROM Usuarios";
                //se crea el comando SQL asociado a la conexion
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                // Se ejecuta la consulta y se obtiene un lector de datos
                SqlDataReader reader = cmd.ExecuteReader();
                //se recorren todas las filas del resultado
                while (reader.Read())
                {
                    //se crea un objeto Usuarios por cada fila
                    lista.Add(new Usuarios()
                    {
                        Id = (int)reader["Id"],
                        Usuario = reader["Usuario"].ToString(),
                        Password = reader["Password"].ToString(),
                        Rol = reader["Rol"].ToString()

                    });
                }
            }
            return lista; //se devuelve la lista completa de usuarios
        }

        // GET BY ID
        public Usuarios GetByUsuario(string usuario)//metodo que busca un usuario por su nombre
        {
            //variable donde se guardará el usuario encontrado
            Usuarios U = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Consulta SQL con parametros
                string query = "SELECT * FROM Usuarios WHERE Usuario=@usuario";
                SqlCommand cmd = new SqlCommand(query, conn);
                // Se asigna el valor al parámetro
                cmd.Parameters.AddWithValue("@usuario", usuario);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Se instancia el objeto
                    U = new Usuarios()
                    {

                        Id = (int)reader["Id"],
                        Usuario = reader["Usuario"].ToString(),
                        Password = reader["Password"].ToString(),
                        Rol = reader["Rol"].ToString()

                    };
                }
            }
            return U;// Retorna el usuario
        }

        //GET BY ID TOKEN
        public Usuarios GetByToken(string token)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Usuarios WHERE TokenActivo = @Token";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Token", token);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuarios
                            {
                                Id = (int)reader["Id"],
                                Usuario = reader["Usuario"].ToString(),
                                Password = reader["Password"].ToString(),
                                Rol = reader["Rol"].ToString(),
                                Email = reader["Email"].ToString(),
                                TokenActivo = reader["TokenActivo"]?.ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        // INSERT
        public string Insert(Usuarios U)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                //Verificar usuario
                string checkUsuario = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = @usuario";
                SqlCommand cmdCheckUsuario = new SqlCommand(checkUsuario, conn);
                cmdCheckUsuario.Parameters.AddWithValue("@usuario", U.Usuario);

                int existeUsuario = (int)cmdCheckUsuario.ExecuteScalar();
                if (existeUsuario > 0)
                    return "usuario_existe";

                //Verificar email
                string checkEmail = "SELECT COUNT(*) FROM Usuarios WHERE Email = @email";
                SqlCommand cmdCheckEmail = new SqlCommand(checkEmail, conn);
                cmdCheckEmail.Parameters.AddWithValue("@email", U.Email);

                int existeEmail = (int)cmdCheckEmail.ExecuteScalar();
                if (existeEmail > 0)
                    return "email_existe";

                //Insertar
                string query = "INSERT INTO Usuarios (Usuario,Password,Rol,Email) VALUES(@usuario,@password,@rol,@email)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@usuario", U.Usuario);
                cmd.Parameters.AddWithValue("@password", U.Password);
                cmd.Parameters.AddWithValue("@rol", U.Rol);
                cmd.Parameters.AddWithValue("@email", U.Email);

                cmd.ExecuteNonQuery();

                return "ok";
            }
        }

        // UPDATE
        public void Update(int id, Usuarios U)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {              

                string query = "UPDATE Usuarios SET TokenActivo = @Token WHERE Id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Token", U.TokenActivo);
                cmd.Parameters.AddWithValue("@id", U.Id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            /*{
                string query = @"UPDATE Usuarios SET Usuario=@usuario, Password=@password, Rol=@rol WHERE Id=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@usuario", U.Usuario);
                cmd.Parameters.AddWithValue("@password", U.Password);
                cmd.Parameters.AddWithValue("@rol", U.Rol);
                conn.Open();
                cmd.ExecuteNonQuery();
            }*/
        }

        // DELETE
        /*public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Usuarios WHERE Id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }*/

    }
}