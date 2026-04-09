using ApiLibro.Data;
using ApiLibro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiLibro.Controllers
{
    [EnableCors(origins: "https://localhost:44367", headers: "*", methods: "*")]
    public class UsuariosController : ApiController
    {
        UsuariosDAO dao = new UsuariosDAO();

        // GET api/<controller>
        public IEnumerable<Usuarios> Get()//metodo para obtener la lista completa de registros
        {
            //devuelve todos los usuarios existentes
            return dao.GetAll();
        }

        // GET api/<controller>/5
        public Usuarios Get(string usuario)// se obtiene usuario por nombre
        {
            // devulve el usuario que coincida con el nombre 
            return dao.GetByUsuario(usuario);
        }

        /*
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
        }
        */
    }
}