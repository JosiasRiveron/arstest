using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArsTest.Models
{
    public class ars_Cotizacion
    {
        
        public int  cotizacion_id { get; set; }
        public decimal estimacion_cliente {get; set;}
        public SelectList listaClientes { get; set; }
        public string cliente_id { get; set; }
        public string Nombre { get; set; }
        [DisplayName("descripcion")]
        public int ramo_id { get; set; }
        public string Ramo { get; set; }
        public SelectList listaRamos { get; set; }
        [DisplayName("descripcion")]
        public int plan_id { get; set; }
        public string plan { get; set; }
        public SelectList listaPlanes{ get; set; }
        public decimal total_cotizacion {get; set;}
        public DateTime fecha_cotizacion { get; set; }

      


    }
}