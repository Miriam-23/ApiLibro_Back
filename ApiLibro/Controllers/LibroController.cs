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
    public class LibroController : ApiController
    {
        LibroDAO dao = new LibroDAO();
        //Obtener todo GET api/<controller>
        public IEnumerable<Libro> Get()
        {
            return dao.GetAll();
        }

        //Obtener por ID GET api/<controller>/5
        public Libro Get (int id)
        {
            return dao.GetById(id);
        }

        //Insertar POST api/<controller>
        public void Post(Libro libro)
        {
            dao.Insert(libro);
        }

        //Actualizar PUT api/<controller>/5
        public void Put(int id, Libro libro)
        {
            dao.Update(id, libro);
        }

        //Eliminar DELETE api/<controller>/5
        public void Delete(int id)
        {
            dao.Delete(id);
        }
    }
}