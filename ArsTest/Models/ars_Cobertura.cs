using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArsTest.Models
{
    public class ars_Cobertura
    {
        public int cobertura_id { get; set; }
        public string descripcion { get; set; }
        public decimal tasa_cobertura { get; set; }
        public decimal prima { get; set; }
    }
}