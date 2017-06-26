using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArsTest.Controllers
{
    public class CotizacionController : Controller
    {
        // GET: Cotizacion
        public ActionResult Index()
        {
            ViewData["Cobertura"] = null;
           Models.ars_Ramos ramos = new Models.ars_Ramos();
            Models.ars_Clientes cliente = new Models.ars_Clientes();
            Models.ars_Cotizacion co = new Models.ars_Cotizacion()
            {

                listaRamos = new SelectList(ramos.GetRamos(), "ramo_id", "descripcion"),
                listaClientes = new SelectList(cliente.GetClientes(),"cliente_id","Nombre"),
                listaPlanes = new SelectList (new List<string> {""}) 
                
            };
            return View("Index",co);
        }

        [HttpPost]
        public ActionResult Index( Models.ars_Cotizacion cotizacion)
        {
            ViewData["Cobertura"] = null;
            Models.ars_Ramos ramos = new Models.ars_Ramos();
            Models.ars_Planes planes = new Models.ars_Planes();
            Models.ars_Clientes cliente = new Models.ars_Clientes();
            Models.ars_Cotizacion co = null;
            if (cotizacion.plan_id == 0)
            {
                co = new Models.ars_Cotizacion()
                {
                    listaClientes = new SelectList(cliente.GetClientes(), "cliente_id", "Nombre"),
                    cliente_id = cotizacion.cliente_id,
                    listaRamos = new SelectList(ramos.GetRamos(), "ramo_id", "descripcion"),
                    ramo_id = cotizacion.ramo_id,
                    estimacion_cliente = cotizacion.estimacion_cliente,
                    listaPlanes = new SelectList(planes.GetPlanes(cotizacion.ramo_id), "plan_id", "descripcion")
                };
            }
            else
            {
               co = new Models.ars_Cotizacion()
               {
                   listaClientes = new SelectList(cliente.GetClientes(), "cliente_id", "Nombre"),
                   cliente_id = cotizacion.cliente_id,
                   listaRamos = new SelectList(ramos.GetRamos(), "ramo_id", "descripcion"),
                    ramo_id = cotizacion.ramo_id,
                    estimacion_cliente = cotizacion.estimacion_cliente,
                    listaPlanes = new SelectList(planes.GetPlanes(cotizacion.ramo_id), "plan_id", "descripcion"),
                    plan_id = cotizacion.plan_id
                    
                };
                Conexion.Resultados cobertura = new Conexion.Resultados();
               cobertura = Conexion.ClConexion.FGeneral($"exec sp_GetCobertura {cotizacion.plan_id}");
                ViewData["Cobertura"] = cobertura;
               co.total_cotizacion = ((decimal.Parse(cobertura.dtResult.Compute("sum(prima)","").ToString()) * cotizacion.estimacion_cliente)/100);
            }



            return View("Index", co);
        }
        [HttpPost]
        public ActionResult RegistraCotizacion(Models.ars_Cotizacion cotizacion)
        {
            ViewData["Cobertura"] = null;
           
            Conexion.Resultados resultado = new Conexion.Resultados();
            resultado = Conexion.ClConexion.FGeneral($"exec sp_InsertCotizacion {cotizacion.estimacion_cliente},{cotizacion.cliente_id},{cotizacion.ramo_id},{cotizacion.plan_id},{cotizacion.total_cotizacion}");


                
                Conexion.Resultados cobertura = new Conexion.Resultados();
                cobertura = Conexion.ClConexion.FGeneral($"exec sp_GetCobertura {cotizacion.plan_id}");
                ViewData["Cobertura"] = cobertura;

            cotizacion.cotizacion_id = int.Parse(resultado.dtResult.Rows[0]["cotizacion_id"].ToString());
            cotizacion.Nombre = resultado.dtResult.Rows[0]["Nombre"].ToString();
            cotizacion.plan = resultado.dtResult.Rows[0]["plan"].ToString();
            cotizacion.Ramo = resultado.dtResult.Rows[0]["ramo"].ToString();
            cotizacion.fecha_cotizacion = (DateTime) resultado.dtResult.Rows[0]["fecha_cotizacion"];




            return View("GetCotizacion", cotizacion);
        }

        public ActionResult GetCotizacion(Models.ars_Cotizacion cotizacion)
        {
            ViewData["Cobertura"] = null;
           
                Conexion.Resultados cobertura = new Conexion.Resultados();
                cobertura = Conexion.ClConexion.FGeneral($"exec sp_GetCobertura {cotizacion.plan_id}");
                ViewData["Cobertura"] = cobertura;
            
              

            return View("GetCotizacion", cotizacion);
        }
    }
}