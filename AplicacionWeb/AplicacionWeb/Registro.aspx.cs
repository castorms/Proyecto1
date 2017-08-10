using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EmpresaDominio;

namespace AplicacionWeb
{
    public partial class Registro : System.Web.UI.Page
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

        protected void BtNAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string cedula = TxTCedula.Text;
                string nombre = TxTNombre.Text;
                string email = TxTEmail.Text;
                string telefono = TxTTelefono.Text;
                int calificacion = 0;
                int.TryParse(TxTCalificacion.Text, out calificacion);
                string pasword = TxTPasword.Text;
                Evaluador ev = new Evaluador
                {
                    Cedula = cedula,
                    Nombre = nombre,
                    Email = email,
                    Telefono = telefono,
                    Calificaccion = calificacion,
                };

                Usuario us = new Usuario
                {
                    Email = email,
                    Pasword = pasword,
                    Tipo = "evaluador",
                };
                if (us.Insertar())
                {
                    if (ev.Insertar())
                    {
                        LbLMensaje.Text = "Alta realizada con exito";
                    } 
                }
                else
                {
                    LbLMensaje.Text = "No se pudo dar de Alta del Evaluador";
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