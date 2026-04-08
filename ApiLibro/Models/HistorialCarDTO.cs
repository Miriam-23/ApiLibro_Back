using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiLibro.Models
{
    public class HistorialCarDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string Titulo { get; set; }
        public decimal Total { get; set; }
    }
}