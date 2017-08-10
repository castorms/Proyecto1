using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EmpresaDominio;

namespace AplicacionWeb
{
    public partial class AsignarEvaluador : System.Web.UI.Page
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

        protected void BtNListarEvaluadores_Click(object sender, EventArgs e)
        {
            LsBEvaluadores.DataSource = EmpresaDominio.Evaluador.FindAll();
            LsBEvaluadores.DataBind();
        }

        protected void BtNListarEmprendimiento_Click(object sender, EventArgs e)
        {
            LsBEmprendimiento.DataSource = EmpresaDominio.Emprendimiento.FindAll();
            LsBEmprendimiento.DataBind();
        }

        protected void BtnEvaluadorEmprendimeinto_Click(object sender, EventArgs e)
        {
            try
            {

                string identificador = TxtIdEmprendimiento.Text;
                string cedula = TxtCedulaEvaluador.Text;
                string email = TxtEmailEvaluador.Text;

                Evaluacion ev = new Evaluacion
                {
                    Puntaje = 0,
                    Justificacion = "",
                    FechaRealizacion = DateTime.Now,
                    Estado = false,
                    Identificador = identificador,
                    Cedula = cedula,
                    Email = email,

                };
                if (ev.Insertar())
                {

                    {
                        LbLMensaje.Text = "Alta realizada con exito";
                    }
                }
                else
                {
                    LbLMensaje.Text = "No se pudo dar de Alta de la Evaluacion";
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