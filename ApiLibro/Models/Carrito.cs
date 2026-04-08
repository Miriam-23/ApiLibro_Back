using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiLibro.Models
{
    public class Carrito
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LibroId { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio {  get; set; }
        public int CompraRealizada { get; set; }
    }
}