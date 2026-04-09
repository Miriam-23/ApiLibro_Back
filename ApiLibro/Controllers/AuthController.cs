using ApiLibro.Data;
using ApiLibro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Mvc;

namespace ApiLibro.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        /*
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }*/

        //Instancia para acceder a la base de datos
        UsuariosDAO dao = new UsuariosDAO();

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginDTO login)
        {
            // Busca el usuario en la base de datos
            var user = dao.GetByUsuario(login.Usuario);

            // Si no existe el usuario marcara error
            if (user == null)
            {
                return Unauthorized();
            }

            // ⚠️ comparación simple (luego mejoramos con hash)
            if (user.Password != login.Password)
            {
                return Unauthorized();
            }

            // Si las credenciales son correctas, devuelve los datos del usuario
            return Ok(new
            {
                id = user.Id,
                usuario = user.Usuario,
                rol = user.Rol
            });
        }
    }
}
