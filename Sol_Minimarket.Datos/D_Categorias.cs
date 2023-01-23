﻿using System;
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

        public string Guardar_ca(int nOpcion, E_Categorias oCa)
        {
            string respuesta = string.Empty;
            SqlConnection sqlConnection= new SqlConnection();
            try
            {
                sqlConnection = Conexion.getInstance().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_Guardar_ca", sqlConnection);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nOpcion",SqlDbType.Int).Value= nOpcion;
                comando.Parameters.Add("@nCodigo_ca", SqlDbType.Int).Value= oCa.Codigo_ca;
                comando.Parameters.Add("@cDescripcion_ca", SqlDbType.VarChar).Value= oCa.Descripcion_ca;
                sqlConnection.Open();
                respuesta= comando.ExecuteNonQuery()==1?"Ok":"No se pudo realizar el registro.";
            }
            catch (Exception e)
            {

                respuesta = e.Message;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return respuesta;
        }

        public string Eliminar_ca(int codigo_ca)
        {
            string respuesta = string.Empty;
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                sqlConnection = Conexion.getInstance().CrearConexion();
                SqlCommand comando = new SqlCommand("USP_Eliminar_ca", sqlConnection);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nCodigo_ca", SqlDbType.Int).Value = codigo_ca;
                sqlConnection.Open();
                respuesta = comando.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo realizar la eliminación.";
            }
            catch (Exception e)
            {
                respuesta = e.Message;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return respuesta;
        }
    }
}
