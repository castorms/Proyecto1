using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acceso;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace EmpresaDominio
{
    public class Usuario:ActiveRecord
    {
        private string email;
        private string pasword;
        private string tipo;

        #region PROPIEDADES
        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public string Pasword
        {
            get
            {
                return pasword;
            }

            set
            {
                pasword = value;
            }
        }

        public string Tipo
        {
            get
            {
                return tipo;
            }

            set
            {
                tipo = value;
            }
        }
        #endregion

        #region VALIDACIONES
        public bool Validar()
        {
            return  this.email.Length > 8
                && this.pasword.Length > 5
                && this.tipo == "administrador" || this.tipo == "evaluador" || this.tipo == "postulante";
        }
        #endregion

        #region ACTIVE RECORD
        public bool Insertar()
        {
            {
                if (!this.Validar()) return false;

                SqlConnection cn = Conexion.CrearConexion();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Usuario
                                VALUES(@email,@pasword,@tipo)";
                cmd.Parameters.AddWithValue("@email", this.Email);
                cmd.Parameters.AddWithValue("@pasword", this.Pasword);
                cmd.Parameters.AddWithValue("@tipo", this.Tipo);

                cmd.Connection = cn;
                try
                {

                    Conexion.AbrirConexion(cn);
                    int filas = cmd.ExecuteNonQuery();

                    return filas == 1;
                }
                catch (SqlException ex)
                {
                    System.Diagnostics.Debug.Assert(false, ex.Message);
                    return false;

                }
                finally
                {
                    Conexion.CerrarConexion(cn);
                }
            }

        }

        public bool Modificar()
        {
            if (!this.Validar()) return false;

            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"UPDATE Usuario SET  US_CLAVE=@pasword,
                                                    US_TIPO=@tipo,
                                WHERE US_EMAIL=@email";
            cmd.Parameters.AddWithValue("@email", this.Email);
            cmd.Parameters.AddWithValue("@pasword", this.Pasword);
            cmd.Parameters.AddWithValue("@tipo", this.Tipo);

            cmd.Connection = cn;
            try
            {

                Conexion.AbrirConexion(cn);
                cmd.ExecuteNonQuery();

                return true;

            }
            catch (SqlException ex)
            {
                //
                System.Diagnostics.Debug.Assert(false, ex.Message);
                return false;
            }
            finally
            {
                Conexion.CerrarConexion(cn);
            }
        }

        public bool Eliminar()
        {
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"DELETE Usuario WHERE US_EMAIL=@email";
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@email", this.Email);
            try
            {
                cn.Open();
                int filas = cmd.ExecuteNonQuery();

                return filas == 1;
            }
            catch (SqlException ex)
            {
                //
                System.Diagnostics.Debug.Assert(false, ex.Message);
                return false;
            }
            finally
            {
                Conexion.CerrarConexion(cn);
            }
        }

        #endregion

        #region BUSCAR
        public static Usuario FindByEmail(string email, string pasword)
        {

            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT * FROM Usuario WHERE US_EMAIL=@email and US_CLAVE=@pas";
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@pas", pasword);
            cmd.Connection = cn;
            try
            {
                Conexion.AbrirConexion(cn);
                Usuario u = null;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    u = CargarDatosDesdeReader(dr);
                }
                return u;
            }
            catch (SqlException ex)
            {
                
                System.Diagnostics.Debug.Assert(false, ex.Message);
                return null;
            }
            finally
            {
                Conexion.CerrarConexion(cn);
            }
        }


        protected static Usuario CargarDatosDesdeReader(IDataRecord fila)
        {
            Usuario u = null;
            if (fila != null)
            {
                u = new Usuario
                {
                    Email = fila.GetString(fila.GetOrdinal("US_EMAIL")),
                    Pasword = fila.GetString(fila.GetOrdinal("US_CLAVE")),
                    Tipo = fila.GetString(fila.GetOrdinal("US_TIPO")),
                    
                };
            }
            return u;
        }
        #endregion
    }
}

