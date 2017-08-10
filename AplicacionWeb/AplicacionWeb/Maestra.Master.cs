using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacionWeb
{
    public partial class Maestra : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["rol"] == null)
            {
                MenuAdmin.Visible = false;
                MenuVisitante.Visible = true;
                MenuEvaluador.Visible = false;

            }
            else if (Session["rol"].ToString() == "administrador")
            {
                MenuAdmin.Visible = true;
                MenuVisitante.Visible = false;
                MenuEvaluador.Visible = false;

            }
            else if (Session["rol"].ToString() == "evaluador")
            {
                MenuAdmin.Visible = false;
                MenuVisitante.Visible = false;
                MenuEvaluador.Visible = true;

            }

        }
    }
}