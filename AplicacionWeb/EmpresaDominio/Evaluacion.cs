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
    public class Evaluacion:ActiveRecord
    {
        private int puntaje;
        private string justificacion;
        private DateTime fechaRealizacion;
        private bool estado;
        private string identificador;
        private string cedula;
        private string email;

        #region Propiedades
        public int Puntaje
        {
            get
            {
                return puntaje;
            }

            set
            {
                puntaje = value;
            }
        }

        public string Justificacion
        {
            get
            {
                return justificacion;
            }

            set
            {
                justificacion = value;
            }
        }

        public DateTime FechaRealizacion
        {
            get
            {
                return fechaRealizacion;
            }

            set
            {
                fechaRealizacion = value;
            }
        }
        public string Identificador
        {
            get
            {
                return identificador;
            }

            set
            {
                identificador = value;
            }
        }

        public string Cedula
        {
            get
            {
                return cedula;
            }

            set
            {
                cedula = value;
            }
        }

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

        public bool Estado
        {
            get
            {
                return estado;
            }

            set
            {
                estado = value;
            }
        }
        #endregion

        #region Validacion
        public bool Validar()
        {
            return this.puntaje >= 0
                && this.puntaje <= 4
                && this.justificacion.Length >= 0 && this.justificacion.Length <= 500
                && this.identificador.Length >= 1
                && this.cedula.Length <= 8
                ;
        }

        public bool Disponible()
        {
            Emprendimiento emp = Emprendimiento.FindByIdentificador(this.identificador);
            bool bandera = true;
            if (emp != null)
            {
                if (emp.Evaluaciones.Count < 3)
                {
                    foreach (Evaluacion e in emp.Evaluaciones)
                    {
                        if (e.Cedula == this.cedula)
                        {
                            bandera = false;
                        }
                    }
                }
                else bandera = false;
            }


            if (bandera) { 
            Evaluacion ev = new Evaluacion
            {
                Puntaje = 0,
                Justificacion = "",
                FechaRealizacion = DateTime.Now,
                Identificador = this.identificador,
                Cedula = this.cedula,
                Email = this.email,

            };
                emp.Evaluaciones.Add(ev);
            }
            
            return bandera;
        
        }

      
        #endregion

        #region Active Record
        public bool Insertar()
        {
            {
                if (!this.Validar()) return false;
                if (!this.Disponible()) return false;

                SqlConnection cn = Conexion.CrearConexion();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Evaluacion
                                VALUES(@puntaje,@justificacion,@fechaRealizacion,@estado,@identificador,@cedula,@email)";
                cmd.Parameters.AddWithValue("@puntaje", this.Puntaje);
                cmd.Parameters.AddWithValue("@justificacion", this.Justificacion);
                cmd.Parameters.AddWithValue("@fechaRealizacion", this.FechaRealizacion);
                cmd.Parameters.AddWithValue("@estado", this.Estado);
                cmd.Parameters.AddWithValue("@identificador", this.identificador);
                cmd.Parameters.AddWithValue("@cedula", this.cedula);
                cmd.Parameters.AddWithValue("@email", this.email);



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
            cmd.CommandText = @"UPDATE Evaluacion SET  Puntaje_ev=@puntaje,
                                                    Justificacion_ev=@justificacion,
                                                    FechaRealizacion_ev=@fechaRealizacion,
                                                    Estado_ev=@estado,    
                                WHERE Emp_id=@identificador 
                                AND EV_CEDULA=@cedula 
                                AND FechaRealizacion_ev=@fechaRealizacion 
                                AND EV_EMAIL=@email";
            cmd.Parameters.AddWithValue("@puntaje", this.Puntaje);
            cmd.Parameters.AddWithValue("@justificacion", this.Justificacion);
            cmd.Parameters.AddWithValue("@fechaRealizacion", this.FechaRealizacion);
            cmd.Parameters.AddWithValue("@estado", this.Estado);
            cmd.Parameters.AddWithValue("@identificador", this.identificador);
            cmd.Parameters.AddWithValue("@cedula", this.cedula);
            cmd.Parameters.AddWithValue("@email", this.email);


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
            cmd.CommandText = @"DELETE Evaluacion WHERE Emp_id=@identificador 
                                                  AND EV_CEDULA=@cedula 
                                                  AND FechaRealizacion_ev=@fechaRealizacion 
                                                  AND EV_EMAIL=@email ";
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@identificador", this.Identificador);
            cmd.Parameters.AddWithValue("@cedula", this.Cedula);
            cmd.Parameters.AddWithValue("@fechaRealizacion", this.FechaRealizacion);
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

        #region Metodos Buscar
        public static List<Evaluacion> FindAll()
        {

            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT * FROM Evaluacion";

            cmd.Connection = cn;
            List<Evaluacion> listaEvaluaciones = null;
            try
            {
                Conexion.AbrirConexion(cn);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    listaEvaluaciones = new List<Evaluacion>();
                    while (dr.Read())
                    {

                        Evaluacion e = CargarDatosDesdeReader(dr);
                        listaEvaluaciones.Add(e);
                    }
                }
                return listaEvaluaciones;
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
        protected static Evaluacion CargarDatosDesdeReader(IDataRecord fila)
        {
            Evaluacion o = null;
            if (fila != null)
            {
                o = new Evaluacion
                {

                    Puntaje = fila.IsDBNull((int)fila["puntaje_ev"]) ? 0 : fila.GetInt32(fila.GetOrdinal("Puntaje_ev")),
                    Justificacion = fila.IsDBNull(fila.GetOrdinal("Justificacion_ev")) ? "" : fila.GetString(fila.GetOrdinal("Justificacion_ev")),
                    Estado = fila.IsDBNull((Byte)fila["Estado_ev"]) ? false : fila.GetBoolean(fila.GetOrdinal("Estado_ev")),
                    Identificador = fila.IsDBNull(fila.GetOrdinal("Emp_id")) ? "" : fila.GetString(fila.GetOrdinal("Emp_id")),
                    Cedula = fila.IsDBNull(fila.GetOrdinal("EV_CEDULA")) ? "" : fila.GetString(fila.GetOrdinal("EV_CEDULA")),
                    Email = fila.IsDBNull(fila.GetOrdinal("EV_EMAIL")) ? "" : fila.GetString(fila.GetOrdinal("EV_EMAIL")),

                };
            }
            return o;
        }
        #endregion
    }
}
 