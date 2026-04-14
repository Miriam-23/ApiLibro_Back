using ApiLibro.Data;
using ApiLibro.Models;
using ApiLibro.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;



namespace ApiLibro.Controllers
{
    //habilita cors y permite que el frontend consuma la api
    [EnableCors(origins: "https://localhost:44367", headers: "*", methods: "*")]
    [CustomAuthorize]
    public class LibroController : ApiController
    {
        // Se crea una instancia del DAO para acceder a la base de datos
        LibroDAO dao = new LibroDAO();

        //Obtener todo GET api/<controller>
        public IEnumerable<Libro> Get()
        {
            // Devuelve una lista de libros
            return dao.GetAll();
        }

        //Obtener por ID GET api/<controller>/5
        public Libro Get (int id)
        {
            //llama al metodo GetById del DAO y devuelve un libro 
            return dao.GetById(id);
        }

        //Insertar POST api/<controller>
        public void Post(Libro libro)
        {
           //recibe un objeto libro y lo envía al DAO para insertarlo en la base de datos
            dao.Insert(libro);
        }

        //Actualizar PUT api/<controller>/5
        public void Put(int id, Libro libro)
        {
            //recibe el id y el objeto para actualizarlo
            dao.Update(id, libro);
        }

        //Eliminar DELETE api/<controller>/5
        public void Delete(int id)
        {
            //llama al DAO para eliminar el registro
            dao.Delete(id);
        }
    }
}