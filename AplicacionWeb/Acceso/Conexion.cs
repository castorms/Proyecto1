﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Acceso
{
    public class Conexion
    {
        #region Manejo de la conexión.
        //Cambiarla si se utiliza otro servicio de SQLServer.
        private static string cadenaConexion = @"SERVER=DESKTOP-7J9CGTO\CASTORMS;
                                                 DATABASE=Empresa;
                                                 INTEGRATED SECURITY=TRUE";
        public static SqlConnection CrearConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
        public static void AbrirConexion(SqlConnection cn)
        {

            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

            }
            catch (Exception ex)
            {

                Debug.Assert(false, ex.Message);

            }
        }
        public static void CerrarConexion(SqlConnection cn)
        {

            try
            {
                if (cn.State != ConnectionState.Closed)
                {
                    cn.Close();
                    cn.Dispose();
                }

            }
            catch (Exception ex)
            {

                Debug.Assert(false, ex.Message);

            }
        }
        #endregion
    }
}
