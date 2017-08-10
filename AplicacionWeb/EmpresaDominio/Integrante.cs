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
    public class Integrante
    {
        private string cedula;
        private string nombre;
        private string email;

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
        #endregion

        #region VALIDACIONES
        public  bool ValidarIntegrante()
        {
            return this.Cedula.Length <= 8
                && this.Nombre.Length > 4
                && this.email.Length > 8;
        }
        #endregion


    }
}
