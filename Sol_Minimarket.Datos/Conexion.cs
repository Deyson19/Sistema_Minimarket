using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Sol_Minimarket.Datos
{
    public class Conexion
    {
        private string Dbase;
        private string Server;
        private string User;
        private string Password;
        private bool Security;
        private static Conexion Conn = null;

        private Conexion() 
        {
            this.Dbase = "DB_Minimarket";
            this.Server = "localhost";
            this.User = "ANDRES";
            this.Password = "Deyson.developer";
            this.Security = false;
        }
        public SqlConnection CrearConexion()
        {
            SqlConnection Cadena = new SqlConnection();
            try
            {
                Cadena.ConnectionString = "Server=" + this.Server + "; Database=" + this.Dbase + ";";
                if (Security)
                {
                    Cadena.ConnectionString = Cadena.ConnectionString + "Integrated Security=SSPI";
                }
                else
                {
                    Cadena.ConnectionString = Cadena.ConnectionString+"User Id="+this.User+ "; Password="+this.Password;
                }
            }
            catch (SqlException)
            {
                Cadena = null;
                throw;
            }
            return Cadena;
        }

        public static Conexion getInstance()
        {
            if (Conn==null)
            {
                Conn = new Conexion();
            }
            return Conn;
        }
    }
}
