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
    public class VentasController : ApiController
    {
        VentasDAO dao = new VentasDAO();
        // GET api/<controller>
        public IEnumerable<Ventas> Get()
        {
            return dao.GetAll();
        }

        // GET api/<controller>/5
        public Ventas Get(int id)
        {
            return dao.GetById(id);
        }

        // POST api/<controller>
        /*public void Post(Ventas ventas)
        {
            dao.Insert(ventas);
            return;
        }*/
        [HttpPost]
        public IHttpActionResult Post(Ventas ventas)
        {
            dao.Insert(ventas);
            return Ok(ventas);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}