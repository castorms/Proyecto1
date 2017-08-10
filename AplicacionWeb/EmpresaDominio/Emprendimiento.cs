using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acceso;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;

namespace EmpresaDominio
{
    public class Emprendimiento : ActiveRecord
    {
        private string identificador;
        private string titulo;
        private string descripcion;
        private double costo;
        private int tiempo;
        private List<Integrante> integrantes = new List<Integrante>();
        private List<Evaluacion> evaluaciones = new List<Evaluacion>();

        #region PROPIEDADES
        public string Titulo
        {
            get
            {
                return titulo;
            }

            set
            {
                titulo = value;
            }
        }


        public string Descripcion
        {
            get
            {
                return descripcion;
            }

            set
            {
                descripcion = value;
            }
        }

        public double Costo
        {
            get
            {
                return costo;
            }

            set
            {
                costo = value;
            }
        }

        public int Tiempo
        {
            get
            {
                return tiempo;
            }

            set
            {
                tiempo = value;
            }
        }

        public List<Integrante> Integrantes
        {
            get
            {
                return integrantes;
            }

            set
            {
                integrantes = value;
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

        

         public List<Evaluacion> Evaluaciones
         {
             get
             {
                 return evaluaciones;
             }

             set
             {
                 evaluaciones = value;
             }
         }
        #endregion

        #region VALIDACIONES
        public bool Validar()
        {
            return this.titulo.Length >= 1
                && this.titulo.Length <= 20
                && this.descripcion.Length >= 1
                && this.descripcion.Length <= 50
                && this.costo >= 0
                && this.tiempo >= 0 
                && this.Identificador.Length > 1
                && this.integrantes != null;
        }
        #endregion

        #region ACTIVERECORD
        public bool Eliminar()
        {
            throw new NotImplementedException();
        }

        public bool Insertar()
        {
            if (!this.Validar()) return false;

            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"INSERT INTO Emprendimiento
                                VALUES(@id,@titulo,@descripcion,@costo,@tiempo)";
            SqlTransaction trn = null;
            cmd.Parameters.AddWithValue("@id", this.Identificador);
            cmd.Parameters.AddWithValue("@titulo", this.Titulo);
            cmd.Parameters.AddWithValue("@descripcion", this.Descripcion);
            cmd.Parameters.AddWithValue("@costo", this.Costo);
            cmd.Parameters.AddWithValue("@tiempo", this.Tiempo);
            
            cmd.Connection = cn;
            try
            {

                Conexion.AbrirConexion(cn);
                trn = cn.BeginTransaction();
                cmd.Transaction = trn;
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();                

                if(this.Integrantes !=null && this.Integrantes.Count >0)
                {
                    cmd.CommandText = @"INSERT INTO Integrante
                                VALUES(@id,@cedula,@nombre,@email)";
                    foreach(Integrante i in this.Integrantes)
                    {
                        if (i.ValidarIntegrante())
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@id", this.Identificador);
                            cmd.Parameters.AddWithValue("@cedula", i.Cedula);
                            cmd.Parameters.AddWithValue("@nombre", i.Nombre);
                            cmd.Parameters.AddWithValue("@email", i.Email);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    trn.Commit();
                    return true;
                }
                return false;
            }
            catch (SqlException ex)
            {
                trn.Rollback();
                System.Diagnostics.Debug.Assert(false, ex.Message);
                foreach (Integrante i in this.Integrantes) { 
                    Usuario u = new Usuario
                {
                    Email = i.Email,
                    Pasword = "aaa",
                    Tipo = "postulante",
                };
                    u.Eliminar();
                }
                return false;

            }
            finally
            {
                Conexion.CerrarConexion(cn);
            }
        }

        public bool Modificar()
        {
            throw new NotImplementedException();
        }


        #endregion

        #region To String
        public override string ToString()
        {
            return "Titulo: " + titulo + "//  Id: " + Identificador;
        }
        #endregion

        #region Metodos de busqueda

        public static List<Emprendimiento> FindAll()
        {

            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT * FROM EMPRENDIMIENTO";

            cmd.Connection = cn;
            List<Emprendimiento> listaEmprendimiento = null;
            try
            {
                Conexion.AbrirConexion(cn);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    listaEmprendimiento = new List<Emprendimiento>();
                    while (dr.Read())
                    {

                        Emprendimiento e = CargarDatosDesdeReader(dr);
                        listaEmprendimiento.Add(e);
                    }
                }
                return listaEmprendimiento;
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
        protected static Emprendimiento CargarDatosDesdeReader(IDataRecord fila)
        {
            Emprendimiento m = null;
            if (fila != null)
            {
                m = new Emprendimiento
                {

                   Titulo = fila.IsDBNull(fila.GetOrdinal("Emp_Titulo")) ? "" : fila.GetString(fila.GetOrdinal("Emp_Titulo")),
                   Identificador = fila.IsDBNull(fila.GetOrdinal("Emp_id")) ? "" : fila.GetString(fila.GetOrdinal("Emp_id")),

                };
            }
            return m;
        }

        private static void MostrarEmprendimiento(List<Emprendimiento> listaEmprendimiento)
        {

            if (listaEmprendimiento == null || listaEmprendimiento.Count == 0)
                Console.WriteLine("No hay personas");
            else
            {
                foreach (Emprendimiento e in listaEmprendimiento)
                    Console.WriteLine(e);
            }
        }
        private static void MostrarTodosLosEmprendimiento()
        {
            Console.WriteLine("Lista de todas las personas:");

            MostrarEmprendimiento(Emprendimiento.FindAll());
        }


        public static Emprendimiento FindByIdentificador(string identificador)
        {

            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT * FROM EMPRENDIMIENTO 
                                         WHERE Emp_id=@IDENTIFICADOR";
            cmd.Parameters.AddWithValue("@IDENTIFICADOR", identificador);
            cmd.Connection = cn;
            Emprendimiento Empre = null;
            try
            {
                Conexion.AbrirConexion(cn);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    Empre = new Emprendimiento();
                    while (dr.Read())
                    {

                        Emprendimiento m = CargarDatosDesdeReader2(dr);

                    }
                }
                return Empre;
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
        protected static Emprendimiento CargarDatosDesdeReader2(IDataRecord fila)
        {
            Emprendimiento o = null;
            if (fila != null)
            {
                o = new Emprendimiento
                {

                    Identificador = fila.IsDBNull(fila.GetOrdinal("Emp_id")) ? "" : fila.GetString(fila.GetOrdinal("Emp_id")),

                };
            }
            return o;
        }


        public static List<Emprendimiento> BuscarPorEvaluador(string cedula, string email)
        {
            List<Emprendimiento> empEv = Emprendimiento.FindAll();
            List<Evaluacion> eva = Evaluacion.FindAll();
            foreach (Evaluacion e in eva)
            {
                if(cedula == e.Cedula && email== e.Email && e.Estado==false)
                {
                    Emprendimiento emp = FindByIdentificador(e.Identificador);
                    empEv.Add(emp);
                }
            }
            return empEv;

        }
        #endregion

        #region Metodos Servicio

        public static string constr
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
            }
        }

        protected static SqlDataReader select(List<SqlParameter> parameters, string StoreProcedureoQuery, CommandType tipo)
        {

            SqlConnection con = new SqlConnection();
            con.ConnectionString = constr;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = StoreProcedureoQuery;
            cmd.CommandType = tipo;

            if (parameters != null)
            {
                foreach (SqlParameter p in parameters)
                {
                    cmd.Parameters.Add(p);
                }
            }
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }


        public static SqlDataReader select_All()
        {
            string StoredProcedure = "SELECT_ALL_EMPRENDIMIENTO";
            SqlDataReader dr = select(null, StoredProcedure, CommandType.StoredProcedure);
            return dr;
        }


        public static SqlDataReader select_byId(string identificador)
        {

            SqlParameter param = new SqlParameter();
            param.Value = identificador;
            param.ParameterName = "Identificador";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(param);
            string StoreProcedure = "SELECT_EMPRENDIMIENTOById";
            SqlDataReader dr = select(parameters, StoreProcedure, CommandType.StoredProcedure);
            return dr;
        }


        #endregion
    }
}
