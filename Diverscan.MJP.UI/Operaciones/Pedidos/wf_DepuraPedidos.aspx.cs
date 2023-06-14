using System;
using System.Data;
using System.Web.UI;
using Diverscan.MJP.Entidades;

namespace Diverscan.MJP.UI.Operaciones.Pedidos
{
    public partial class wf_DepuraPedidos : Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        public static DataTable DTDetalleSol = new DataTable();
        public static DataTable DTMaestroSol = new DataTable();
        private string Pagina;

        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];
            Pagina = Page.AppRelativeVirtualPath.ToString();

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
        }

        //private void LlenaSolicitud(string idusuario)
        //{
        //    try
        //    {
        //        DataTable Presolicitud = new DataTable();
        //        n_OPESALPreDetalleSolicitud PreMaestroSolicitud = new n_OPESALPreDetalleSolicitud();

        //        Presolicitud = PreMaestroSolicitud.ObtenerPreSolicitudes(idusuario);
        //        PreSolicitudGridView.DataSource = Presolicitud;
        //        PreSolicitudGridView.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        var cl = new clErrores();
        //        cl.escribirError(ex.Message, ex.StackTrace);
        //        Mensaje("error", ex.Message, "");
        //    }
        //}

        //private void Mensaje(string sTipo, string sMensaje, string sLLenado)
        //{
        //    string javaScript = "";
        //    sMensaje = sMensaje.Replace((char)39, (char)32);

        //    switch (sTipo)
        //    {
        //        case "error":
        //            javaScript = "mensajeError('" + sMensaje + "');";
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
        //            break;
        //        case "info":
        //            javaScript = "mensajeInfo('" + sMensaje + "');";
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
        //            break;
        //        case "ok":
        //            javaScript = "mensajeExito('" + sMensaje + "');";
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
        //            break;
        //    }
        //}

        //private void LlenaSolicitudDetalle(string idMaestroSolicitud)
        //{
        //    try
        //    {
        //        DataTable Presolicitud = new DataTable();
        //        n_OPESALPreDetalleSolicitud PreMaestroSolicitudDetalle = new n_OPESALPreDetalleSolicitud();

        //        Presolicitud = PreMaestroSolicitudDetalle.ObtenerPreSolicitudDetalle(idMaestroSolicitud);
        //        PreSolicitudDetalleGridView.DataSource = Presolicitud;
        //        PreSolicitudDetalleGridView.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        var cl = new clErrores();
        //        cl.escribirError(ex.Message, ex.StackTrace);
        //        Mensaje("error", ex.Message, "");
        //    }
        //}

        //protected void Grid1_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        int indc = PreSolicitudGridView.SelectedIndex;
        //        string idMaestroSolicitud = PreSolicitudGridView.Rows[indc].Cells[0].Text;
        //        PreSolicitudGridView.Rows[indc].BackColor = System.Drawing.Color.WhiteSmoke;

        //        LlenaSolicitudDetalle(idMaestroSolicitud);
        //        PreSolicitudDetalleGridView.Visible = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        var cl = new clErrores();
        //        cl.escribirError(ex.Message, ex.StackTrace);
        //        Mensaje("error", ex.Message, "");
        //    }
        //}

        //protected void btnLimpia_onClick(object sender, EventArgs e)
        //{
        //    Limpia();
        //}

        //protected void Grid2_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        String idPreDetalleSolicitud = "";
        //        // obtener la cantidad original y marcar línea, con otro color.
        //        int indc = PreSolicitudDetalleGridView.SelectedIndex;
        //        Single cantOriginal = Convert.ToSingle(PreSolicitudDetalleGridView.DataKeys[indc].Values[0]);
        //        idPreDetalleSolicitud = PreSolicitudDetalleGridView.DataKeys[indc].Values[1].ToString();

        //        PreSolicitudDetalleGridView.Rows[indc].BackColor = System.Drawing.Color.MistyRose;
        //        TxtIdMaestro.Text = idPreDetalleSolicitud + "," + cantOriginal.ToString();
        //        txtCantidadActualiza.Text = cantOriginal.ToString();
        //        TblCantidad.Visible = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        var cl = new clErrores();
        //        cl.escribirError(ex.Message, ex.StackTrace);
        //        Mensaje("error", ex.Message, "");
        //    }

        //}

        //protected void btnCambiar_onClick(object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        // modificar la cantidad.
        //        string[] resulta = TxtIdMaestro.Text.Split(',');
        //        string resultado = "";
        //        Single co = Single.Parse(resulta[1]);
        //        if (Single.Parse(txtCantidadActualiza.Text) <= co && Single.Parse(txtCantidadActualiza.Text) > 0)
        //        {
        //            n_OPESALPreDetalleSolicitud PreMaestroSolicitudDetalle = new n_OPESALPreDetalleSolicitud();
        //            resultado = PreMaestroSolicitudDetalle.ActualizaCantidadPreSolicitudDetalle(resulta[0], Single.Parse(txtCantidadActualiza.Text));
        //            Mensaje("ok", resultado, "");
        //            Limpia();
        //        }
        //        else
        //        {
        //            resultado = "Cantidad a modificar, superior/igual a la original o menor a Cero 00";
        //            Mensaje("error", resultado, "");
        //            txtCantidadActualiza.Text = co.ToString();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        var cl = new clErrores();
        //        cl.escribirError(ex.Message, ex.StackTrace);
        //        Mensaje("error", ex.Message, "");
        //        txtCantidadActualiza.Text = "";
        //    }

        //}

        //private void Limpia()
        //{
        //    PreSolicitudGridView.DataSource = "";

        //    PreSolicitudDetalleGridView.DataSource = "";
        //    PreSolicitudDetalleGridView.Visible = false;
        //    TblCantidad.Visible = false;

        //    LlenaSolicitud(UsrLogged.IdUsuario);
        //}
    }
}

