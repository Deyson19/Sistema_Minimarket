using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Sol_Minimarket.Entidades;
using Sol_Minimarket.Datos;

namespace Sol_Minimarket.Negocio
{
    public class N_Categorias
    {
        public N_Categorias() { }
        public static DataTable Listado_ca(string cTexto)
        {
            D_Categorias datos = new D_Categorias();
            return datos.Listado_ca(cTexto);
        }

    }
}
