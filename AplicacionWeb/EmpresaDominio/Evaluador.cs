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
    public class Evaluador : ActiveRecord
    {
        private string cedula;
        private string nombre;
        private string email;
        private string telefono;
        private int calificacion;

        #region PROPIEDADES
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

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
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

        public string Telefono
        {
            get
            {
                return telefono;
            }

            set
            {
                telefono = value;
            }
        }

        public int Calificaccion
        {
            get
            {
                return calificacion;
            }

            set
            {
                calificacion = value;
            }
        }
        #endregion

        #region VALIDACIONES
        public bool Validar()
        {
            return this.Cedula.Length <= 8
                && this.Nombre.Length > 4
                && this.email.Length > 6
                && this.telefono.Length <= 9
                && this.calificacion <= 4
                && this.calificacion >= 0;
        }

        
        #endregion

        #region ACTIVERECORD

        public bool Insertar()
        {

            if (!this.Validar()) return false;
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"INSERT INTO Evaluador
                                VALUES(@cedu,@nom,@email,@telefono,@calificacion)";
            cmd.Parameters.AddWithValue("@cedu", this.Cedula);
            cmd.Parameters.AddWithValue("@nom", this.Nombre);
            cmd.Parameters.AddWithValue("@email", this.Email);
            cmd.Parameters.AddWithValue("@telefono", this.Telefono);
            cmd.Parameters.AddWithValue("@calificacion", this.Calificaccion);

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

        public bool Modificar()
        {
            if (!this.Validar()) return false;

            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"UPDATE Evaluador SET  EV_NOMBRE=@nombre,
                                                      EV_EMAIL=@email, 
                                                      EV_TELEFONO=@telefono,
                                                      EV_CALIFICACION=@calificacion
                                WHERE EV_CEDULA=@cedu";
            cmd.Parameters.AddWithValue("@cedu", this.Cedula);
            cmd.Parameters.AddWithValue("@nombre", this.Nombre);
            cmd.Parameters.AddWithValue("@email", this.Email);
            cmd.Parameters.AddWithValue("@telefono", this.Telefono);
            cmd.Parameters.AddWithValue("@calificacion", this.Calificaccion);

            cmd.Connection = cn;
            try
            {

                Conexion.AbrirConexion(cn);
                cmd.ExecuteNonQuery();

                return true;

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

        public bool Eliminar()
        {
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"DELETE Evaluador WHERE EV_CEDULA=@cedu";
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@cedu", this.Cedula);
            try
            {
                cn.Open();
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
        #endregion

        #region TOSTRING
        public override string ToString()
        {
            return "Nombre: " + nombre + "//  Cedula: " + cedula + "//  Email: " + email;
        }
        #endregion

        #region Metodos de busqueda
        public static List<Evaluador> FindAll()
        {

            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT * FROM EVALUADOR";

            cmd.Connection = cn;
            List<Evaluador> listaEvaluadores = null;
            try
            {
                Conexion.AbrirConexion(cn);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    listaEvaluadores = new List<Evaluador>();
                    while (dr.Read())
                    {

                        Evaluador e = CargarDatosDesdeReader(dr);
                        listaEvaluadores.Add(e);
                    }
                }
                return listaEvaluadores;
            }
            catch (SqlException ex)
            {
                //
                System.Diagnostics.Debug.Assert(false, ex.Message);
                return null;
            }
            finally
            {
                Conexion.CerrarConexion(cn);
            }
        }
        protected static Evaluador CargarDatosDesdeReader(IDataRecord fila)
        {
            Evaluador o = null;
            if (fila != null)
            {
                o = new Evaluador
                {
                    
                    Nombre = fila.IsDBNull(fila.GetOrdinal("EV_NOMBRE")) ? "" : fila.GetString(fila.GetOrdinal("EV_NOMBRE")),
                    Cedula = fila.IsDBNull(fila.GetOrdinal("EV_CEDULA")) ? "" : fila.GetString(fila.GetOrdinal("EV_CEDULA")),
                    Email = fila.IsDBNull(fila.GetOrdinal("EV_EMAIL")) ? "" : fila.GetString(fila.GetOrdinal("EV_EMAIL")),

                };
            }
            return o;
        }

        private static void MostrarEvaluadores(List<Evaluador> listaEvaluadores)
        {

            if (listaEvaluadores == null || listaEvaluadores.Count == 0)
                Console.WriteLine("No hay personas");
            else
            {
                foreach (Evaluador e in listaEvaluadores)
                    Console.WriteLine(e);
            }
        }
        private static void MostrarTodosLosEvaluadores()
        {
            Console.WriteLine("Lista de todas las personas:");

            MostrarEvaluadores(Evaluador.FindAll());
        }
        #endregion
    }
}
