using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Conexion
{
    public static class ClConexion
    {
        
        public static Resultados FGeneral(string query)
        {
            Resultados result = new Resultados();
            result.dtResult = new DataTable();
            string strcon = ConfigurationManager.ConnectionStrings["conx"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter adap = new SqlDataAdapter(query, con);

            try {
                con.Open();
                adap.Fill(result.dtResult);
                con.Close();
                result.mensaje = "OK";
            } catch(Exception ex) { result.mensaje = ex.Message; }

            return result;
        }

        public static Resultados FGeneral(string query, params SqlParameter[] parametros )
        {
            Resultados result = new Resultados();
            result.dtResult = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string strcon = ConfigurationManager.ConnectionStrings["conx"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter adap = new SqlDataAdapter();
            cmd.Connection = con;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.StoredProcedure;
            adap.SelectCommand = cmd;

            foreach (var parametro in parametros)
            {
                cmd.Parameters.Add(parametro);
            }
            try
            {
                con.Open();
                adap.Fill(result.dtResult);
                con.Close();
                result.mensaje = "OK";
            }
            catch (Exception ex) { result.mensaje = ex.Message; }

            return result;
        }

    }

    public class Resultados
    {
        public DataTable dtResult { get; set; } 
       public string mensaje { get; set; }
    }
}
