using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ArsTest.Models
{
    public class ars_Planes
    {
        public int plan_id { get; set; }

        public string descripcion { get; set; }

        public int ramos_id { get; set; }

        public List<ars_Planes> GetPlanes(int _plan_id)
        {
            List<ars_Planes> listaPlanes = new List<ars_Planes>();
            Conexion.Resultados result = Conexion.ClConexion.FGeneral($"exec sp_GetPlanes {_plan_id}");

            foreach (DataRow row in result.dtResult.Rows)
            {
                Models.ars_Planes plan = new Models.ars_Planes();
                plan.plan_id = int.Parse(row["plan_id"].ToString());
                plan.descripcion = row["descripcion"].ToString();
                listaPlanes.Add(plan);

            }

            return listaPlanes;
        }
    }
}