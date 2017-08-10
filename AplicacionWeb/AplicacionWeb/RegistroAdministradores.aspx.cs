using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EmpresaDominio;

namespace AplicacionWeb
{
    public partial class RegistroAdministradores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["rol"] != null)
            {
                if (Session["rol"].ToString() != "administrador")
                {

                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
               
                string email = TxTEmail.Text;
                string pasword = TxTPasword.Text;
                
                Usuario us = new Usuario
                {
                    Email = email,
                    Pasword = pasword,
                    Tipo = "administrador",
                };
                if (us.Insertar())
                {
                    
                    {
                        LbLMensaje.Text = "Alta realizada con exito";
                    }
                }
                else
                {
                    LbLMensaje.Text = "No se pudo dar de Alta del Administrador";
                }
            }
            catch (Exception ex)
            {
                LbLMensaje.Text = ex.Message;
            }
            finally
            {
                //LblMensaje.Text="cierro conexiones, estoy en finally";
            }
        }
    }
}