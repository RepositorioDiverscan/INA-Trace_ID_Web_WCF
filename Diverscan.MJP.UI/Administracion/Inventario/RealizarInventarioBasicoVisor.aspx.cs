using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.Invertario;
using Diverscan.MJP.Negocio.AjusteInventario;
using Diverscan.MJP.Negocio.GS1;
using Diverscan.MJP.Negocio.InventarioBasico;
using Diverscan.MJP.Negocio.LogicaWMS;
using Diverscan.MJP.Negocio.UsoGeneral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Administracion.Inventario
{
    public partial class RealizarInventarioBasicoVisor : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }

            if (!IsPostBack)
            {
                var today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                loadInventarios(today, today);
            }
        }

        void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel(sender as UpdatePanel);
        }

        public void RegisterUpdatePanel(UpdatePanel panel)
        {
            foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel1 });
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Panel1.Unload += new EventHandler(UpdatePanel1_Unload);
        }

        protected void _btnEnviar_Click(object sender, EventArgs e)
        {

            //string Pagina = "~/HH/Operaciones/Ingresos/wf_UbicarArticulo.aspx";
            //string CodLeido = "0107445239009173310200487610ABC;OC13";
            //string Resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "btnAccion32");
            //string Pagina = "~/HH/Operaciones/Ingresos/wf_UbicarArticulo.aspx";
            //string CodLeido = "01074452390091731718101710ABC;91SEC-PIC-00E-002-001-001";
            //string Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "bntAccion22");



            if (string.IsNullOrEmpty(_txtS_CodigoBarras.Text))
            {
                Mensaje("info", "Debe de ingresar el codigo de barras", "");
                return;
            }
            if (string.IsNullOrEmpty(_txtS_UbicacionActual.Text))
            {
                Mensaje("info", "Debe de ingresar la ublicacion", "");
                return;
            }
            try
            {
                long idInventario = long.Parse(_ddlInventariosDisponibles.SelectedValue);
                if (idInventario > 0)
                {
                    var ubicacion = _txtS_UbicacionActual.Text.Trim();
                    var newEtiqueta = String.Format("({0}){1}", ubicacion.Substring(0, 2), ubicacion.Substring(2));
                    var idUbicacionActual = UbicacionEtiquetaLoader.OtenerIdUbicacion(newEtiqueta);
                    if (idUbicacionActual > 0)
                    {
                        var codigoBarrar = _txtS_CodigoBarras.Text.Trim();
                        string codLeido = codigoBarrar + ";" + ubicacion + ";1";
                        var gs1Data = GS1Extractor.ExtraerGS1(codigoBarrar, UsrLogged.IdUsuario);   // codLeido
                        if (!string.IsNullOrEmpty(gs1Data.IdArticulo) || !string.IsNullOrEmpty(gs1Data.IdUbicacion) ||
                            !string.IsNullOrEmpty(gs1Data.Lote) || !string.IsNullOrEmpty(gs1Data.Cantidad) || !string.IsNullOrEmpty(gs1Data.FechaVencimiento))
                        {
                            long idArticulo = int.Parse(gs1Data.IdArticulo);
                            string lote = gs1Data.Lote;
                            DateTime fechaVencimiento = DateTime.Parse(gs1Data.FechaVencimiento);
                            int cantidad = Convert.ToInt32(Single.Parse(gs1Data.Cantidad));

                            var extraInfoArticulo = N_DetalleArticulo.ObtenerArticuloPorIdArticulo(idArticulo);
                            if (extraInfoArticulo.EsGranel)
                                cantidad = gs1Data.Peso;

                            TomaFisicaInventario tomaFisicaInventario = new TomaFisicaInventario(idInventario, idArticulo, idUbicacionActual, lote, fechaVencimiento, cantidad, int.Parse(UsrLogged.IdUsuario));
                            N_ArticulosInventarioBasico.InsertarTomaFisicaInventario(tomaFisicaInventario);
                            Mensaje("info", "Enviado con exito", "");
                        }
                    }
                    else
                        Mensaje("info", "La ubicación "+ newEtiqueta+ " no existe", "");
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", ex.Message, "");
            }
            finally
            {
                Limpiar();
            }
        }

        private void loadInventarios(DateTime fechaInicio, DateTime fechaFin)
        {
            var inventarios = N_InventarioBasico.ObtenerInventarioBasicoRecords(fechaInicio, fechaFin);
            _ddlInventariosDisponibles.DataTextField = "Nombre";
            _ddlInventariosDisponibles.DataValueField = "IdInventarioBasico";
            _ddlInventariosDisponibles.DataSource = inventarios;
            _ddlInventariosDisponibles.DataBind();
        }

        private void Mensaje(string sTipo, string sMensaje, string sLLenado)
        {
            switch (sTipo)
            {
                case "error":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "error('" + sMensaje + "');", true);
                    break;
                case "info":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "notificacion('" + sMensaje + "');", true);
                    break;
                case "ok":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "ok('" + sMensaje + "');", true);
                    break;
            }
        }

        private void Limpiar()
        {
            _txtS_CodigoBarras.Text = "";
            _txtS_UbicacionActual.Text = "";
        }
    }
}