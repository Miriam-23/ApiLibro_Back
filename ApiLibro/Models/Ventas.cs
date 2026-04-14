using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiLibro.Models
{
    public class Ventas
    {
        public int Id { get; set; }
        public int CompraId { get; set; }
        public int UserId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total {get; set;}
    }
}