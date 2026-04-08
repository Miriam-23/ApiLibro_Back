using ApiLibro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiLibro.Data
{
    [EnableCors(origins: "https://localhost:44367", headers: "*", methods: "*")]
    [RoutePrefix("api/historialcar")]
    public class HistorialCarController : ApiController
    {
        HistorialCarDAO dao = new HistorialCarDAO();
        // GET api/<controller>
        public IEnumerable<HistorialCar> Get()
        {
            return dao.GetAll();
        }

        // GET api/<controller>/5
        public HistorialCar Get(int id)
        {
            return dao.GetById(id);
        }

        // POST api/<controller>
        public void Post(HistorialCar historial)
        {
            dao.Insert(historial);
        }

        //OBTENER DATOS POR ID DE USUARIO GET api/<controller>/5
        [HttpGet]
        [Route("usuario/{userId}")]
        public IHttpActionResult GetCarrito(int userId)
        {
            var lista = dao.GetByUserId(userId);
            return Ok(lista);
        }

        // PUT api/<controller>/5
        public void Put(int id, HistorialCar historial)
        {
            dao.Update(id, historial);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            dao.Delete(id);
        }

    }
}