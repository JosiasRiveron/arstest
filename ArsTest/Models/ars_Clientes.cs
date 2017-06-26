using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ArsTest.Models
{
    public class ars_Clientes
    {
        public int cliente_id { get; set; }
        public string Nombre { get; set;  }

        public List<ars_Clientes> GetClientes()
        {
            List<ars_Clientes> listaclientes = new List<ars_Clientes>();
            Conexion.Resultados result = Conexion.ClConexion.FGeneral("exec sp_GetClientes");

            foreach (DataRow row in result.dtResult.Rows)
            {
                ars_Clientes cliente = new ars_Clientes();
                cliente.cliente_id = int.Parse(row["cliente_id"].ToString());
                cliente.Nombre = row["Nombre"].ToString();
                listaclientes.Add(cliente);

            }

            return listaclientes;
        }
    }
}