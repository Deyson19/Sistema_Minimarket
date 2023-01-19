using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Sol_Minimarket.Entidades;

namespace Sol_Minimarket.Datos
{
    public class D_Categorias
    {
        public D_Categorias() { }

        public DataTable Listado_ca (string cTexto)
        {
            SqlDataReader resultado;
            DataTable dt = new DataTable();
            SqlConnection SqlConn = new SqlConnection();

            try
            {
                SqlConn = Conexion.getInstance().CrearConexion();
                SqlCommand comando= new SqlCommand("USP_Listado_ca", SqlConn);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@cTexto",SqlDbType.VarChar).Value= cTexto;
                SqlConn.Open();
                resultado = comando.ExecuteReader();
                dt.Load(resultado);
                return dt;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                if (SqlConn.State==ConnectionState.Open) { SqlConn.Close();}
            }
        }
    }
}
