using ApiLibro.Data;
using ApiLibro.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Mvc;

namespace ApiLibro.Controllers
{

    namespace ApiLibro.Controllers
    {
        /*[RoutePrefix("api/auth")]
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
        /*
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
        }*/

        //[Authorize]
        [RoutePrefix("api/auth")]
        public class AuthController : ApiController
        {
            UsuariosDAO dao = new UsuariosDAO();

            private string claveSecreta = "LIBREIRAJ4ZM1N+K4R3N+M1RI4M8C2026"; // clave

            [HttpPost]
            [Route("login")]
            public IHttpActionResult Login(LoginDTO login)
            {
                var user = dao.GetByUsuario(login.Usuario);

                if (user == null)
                    return Unauthorized();

                // Si ya tienes hash, cambia esto por BCrypt.Verify
                if (user.Password != login.Password)
                    return Unauthorized();

                // GENERAR TOKEN
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(claveSecreta);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, user.Usuario),
                        new Claim("id", user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Rol)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(15), // Expiración
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature
                    )
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // CONTROL DE SESIÓN ÚNICA
                user.TokenActivo = tokenString;
                dao.Update(user.Id, user);

                return Ok(new
                {
                    token = tokenString,
                    usuario = new
                    {
                        id = user.Id,
                        usuario = user.Usuario,
                        rol = user.Rol
                    }
                });
            }
        }

    }
}
