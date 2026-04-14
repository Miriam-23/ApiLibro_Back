using ApiLibro.Data;
using ApiLibro.Models;
using ApiLibro.Handlers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiLibro.Controllers
{
    // Se habilita CORS para permitir conexión con el frontend
    [EnableCors(origins: "https://localhost:44367", headers: "*", methods: "*")]
    [CustomAuthorize]
    [RoutePrefix("api/carrito")]
    public class CarritoController : ApiController
    {
        CarritoDAO dao = new CarritoDAO();

        //OBTENER TODOS LOS DATOS GET api/<controller>
        public IEnumerable<Carrito> Get() 
        {
            return dao.GetAll();//Se obtiene todos los registros del carrito
        }

        //OBTENER POR ID GET api/<controller>/5
        public Carrito Get(int Id)
        {
            return dao.GetById(Id);
        }

        //OBTENER DATOS POR ID DE USUARIO GET api/<controller>/5
        [HttpGet]
        [Route("usuario/{userId}")]
        public IHttpActionResult GetCarrito(int userId) //Se obtiene carrito de un usuario
        {
            var lista = dao.GetByUserId(userId);
            return Ok(lista);
        }

        //INSERTAR POST api/<controller>
        public void Post(Carrito  carrito) 
        {
            dao.Insert(carrito); //Agrega producto al carrito
        }

        //ACTUALIZAR PUT api/<controller>/5
        public void Put(int id, Carrito carrito)
        {
            dao.Update(id, carrito); //Actualiza un producto del carrito
        }

        // DELETE api/carrito/usuario/{id} - vaciar carrito tras compra
        [HttpDelete]
        [Route("usuario/{userId}")]
        public IHttpActionResult VaciarCarrito(int userId)
        {
            dao.VaciarCarrito(userId);
            return Ok(new { mensaje = "Carrito vaciado y movido al historial" });
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            dao.Delete(id); //Elimina un producto en especifico
        }
    }
}