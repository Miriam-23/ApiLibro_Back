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
    //Conexion con el frontend
    [EnableCors(origins: "https://localhost:44367", headers: "*", methods: "*")]
    [RoutePrefix("api/historialcar")]
    public class HistorialCarController : ApiController
    {
        HistorialCarDAO dao = new HistorialCarDAO();

        // GET api/<controller>
        public IEnumerable<HistorialCar> Get()
        {
            //Se obtiene todo el historial
            return dao.GetAll(); 
        }

        // GET api/<controller>/5
        public HistorialCar Get(int id)
        {
            //Se obtiene el historial de carrito por id
            return dao.GetById(id); 
        }

        // POST api/<controller>
        public void Post(HistorialCar historial)
        {
            // Se inserta manualmente los datos recibidos en la base
            dao.Insert(historial);
        }

        //OBTENER DATOS POR ID DE USUARIO GET api/<controller>/5
        [HttpGet]
        [Route("usuario/{userId}")]
        public IHttpActionResult GetCarrito(int userId)
        {
            //Consulta dao para obtener los registros del usuario
            var lista = dao.GetByUserId(userId); 
            return Ok(lista);
        }

        // PUT api/<controller>/5
        public void Put(int id, HistorialCar historial) //Se actualiza el historial
        {
            dao.Update(id, historial); 
        }

        // DELETE api/<controller>/5
        public void Delete(int id) //Elimina los registro 
        {
            dao.Delete(id);
        }

    }
}