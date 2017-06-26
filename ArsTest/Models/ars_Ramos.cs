using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ArsTest.Models
{
    public class ars_Ramos
    {
        public int ramo_id { get; set; }
        public string descripcion { get; set; }

        public List<ars_Ramos> GetRamos()
        {
            List<ars_Ramos> listaramos = new List<ars_Ramos>();
            Conexion.Resultados result = Conexion.ClConexion.FGeneral("exec sp_GetRamos");

            foreach (DataRow row in result.dtResult.Rows)
            {
                Models.ars_Ramos ramo = new Models.ars_Ramos();
                ramo.ramo_id = int.Parse(row["ramo_id"].ToString());
                ramo.descripcion = row["descripcion"].ToString();
                listaramos.Add(ramo);

            }

            return listaramos;
        }
    }
}