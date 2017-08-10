using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EmpresaDominio;

namespace AplicacionWeb
{
    public partial class Login1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login10_Authenticate(object sender, AuthenticateEventArgs e)
        {
            Usuario User = Usuario.FindByEmail(Login10.UserName, Login10.Password);
            if (User != null)
            {
                if (User.Tipo == "administrador")
                {
                    Session["rol"] = "administrador";
                    Response.Redirect("Registro.aspx");
                }
                
            }
        }
    }
}