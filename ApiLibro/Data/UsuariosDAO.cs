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
        string connectionString =
            ConfigurationManager.ConnectionStrings["LibreriaDBConnection"].ConnectionString;
        // GET ALL
        public List<Usuarios> GetAll()
        {
            List<Usuarios> lista = new List<Usuarios>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Usuarios";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Usuarios()
                    {
                        Id = (int)reader["Id"],
                        Usuario = reader["Usuario"].ToString(),
                        Password = reader["Password"].ToString(),
                        Rol = reader["Rol"].ToString()

                    });
                }
            }
            return lista;
        }

        // GET BY ID
        public Usuarios GetByUsuario(string usuario)
        {
            Usuarios U = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Usuarios WHERE Usuario=@usuario";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    U = new Usuarios()
                    {

                        Id = (int)reader["Id"],
                        Usuario = reader["Usuario"].ToString(),
                        Password = reader["Password"].ToString(),
                        Rol = reader["Rol"].ToString()

                    };
                }
            }
            return U;
        }

        // INSERT
        /*public void Insert(Usuarios U)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query =
                "INSERT INTO Usuarios (Usuario,Password,Rol) VALUES(@usuario,@password,@rol)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@usuario", U.Usuario);
                cmd.Parameters.AddWithValue("@password", U.Password);
                cmd.Parameters.AddWithValue("@rol", U.Rol);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }*/

        // UPDATE
        /*public void Update(int id, Usuarios U)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Usuarios SET Usuario=@usuario, Password=@password, Rol=@rol WHERE Id=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@usuario", U.Usuario);
                cmd.Parameters.AddWithValue("@password", U.Password);
                cmd.Parameters.AddWithValue("@rol", U.Rol);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }*/

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