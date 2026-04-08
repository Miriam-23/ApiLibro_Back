using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiLibro.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Imagen { get; set; }
    }
}