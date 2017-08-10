using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EmpresaDominio;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WcfEmprendimiento
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ListaEmprendimiento : IListaEmprendimiento
    {
        public List<NuevoEmprendimiento> GetAll()
        {
            List<NuevoEmprendimiento> lst = new List<NuevoEmprendimiento>();
            SqlDataReader dr = Emprendimiento.select_All();
            while (dr.Read())
            {

                lst.Add(cargarEmprendimiento(dr));
            }

            return lst;
        }

        public NuevoEmprendimiento GetEmprendimiento(string identificador)
        {
            SqlDataReader dr = Emprendimiento.select_byId(identificador);
            NuevoEmprendimiento p = null;
            if (dr != null && dr.Read())
            {
                p = cargarEmprendimiento(dr);
            }
            return p;
        }
        NuevoEmprendimiento cargarEmprendimiento(SqlDataReader dr)
        {

            NuevoEmprendimiento p = new NuevoEmprendimiento();
            p.Descripcion = dr["Emp_Descripcion"].ToString();
            p.Titulo = dr["Emp_Titulo"].ToString();
            p.Identificador = dr["Emp_id"].ToString();
            return p;

        }
    }
}
