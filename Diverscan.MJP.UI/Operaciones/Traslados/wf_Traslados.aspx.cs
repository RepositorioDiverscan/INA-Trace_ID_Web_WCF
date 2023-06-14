using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.Negocio.LogicaWMS;
using Diverscan.MJP.Utilidades;
using Diverscan.Visitas.Utilidades;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Telerik.Web.UI.PersistenceFramework;
using System.Linq;
using System.Data;
using Diverscan.MJP.Negocio.MotorDecisiones;
using System.Data.SqlClient;
using System.Reflection;

namespace Diverscan.MJP.UI.Operaciones.Traslados
{
    public partial class wf_Traslados : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        public int ToleranciaAgregar = 110;
        string Pagina;


        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];
            Pagina = Page.AppRelativeVirtualPath.ToString();

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }

            if (!IsPostBack)
            {
                txtUbicacionActual.Attributes.Add("onchange", "DisplayLoadingImage1()");
                txtUbicacionMover.Attributes.Add("onchange", "DisplayLoadingImage1()");
                txtCODBARRAS.Attributes.Add("onchange", "DisplayLoadingImage1()");
            }
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Panel1.Unload += new EventHandler(UpdatePanel1_Unload);
            this.Panel2.Unload += new EventHandler(UpdatePanel2_Unload);
            this.Panel3.Unload += new EventHandler(UpdatePanel3_Unload);
        }

        void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel(sender as UpdatePanel);
        }

        void UpdatePanel2_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel2(sender as UpdatePanel);
        }

        void UpdatePanel3_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel3(sender as UpdatePanel);
            Limpiar(sender);
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

        public void RegisterUpdatePanel2(UpdatePanel panel)
        {
            foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel2 });
                }
            }
        }
        public void RegisterUpdatePanel3(UpdatePanel panel)
        {
            foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel3 });
                }
            }
        }


        #region TabsControl

        #region EventosFrontEnd

        /// <summary>
        /// Para que esto funcione el boton debe estar contenido en una Panel (Parent), 
        /// luego en un update Panel (Parent), y luego en el Panel (Parent) contenedor, la idea es usar el mismo boton
        /// para cualquier accion, segun el Patron de Programacion 1 / Diverscan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void BtnAgregar3_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblDestinoRestriccionHorario(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void BtnEditar3_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblDestinoRestriccionHorario(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnAgregar2_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblDetalleSolicitud(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnEditar2_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblDetalleSolicitud(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.CargarDatos(Panel, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnAccion11_Click(object sender, EventArgs e)
        {
            string Pagina = "~/HH/Operaciones/Salidas/wf_CrearAlisto.aspx";
            string CodLeido = txtCODBARRAS.Text;
            string Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "btnAccion11");

            txtCODBARRAS.Text = Respuesta;
        }


        #endregion //EventosFrontEnd

        #endregion //TabsControl

        protected void btnTrasladar_Click(object sender, EventArgs e)
        {
            // si ambas ubicaciones son validas, se procede al traslado.
            string Pagina = "~/HH/Operaciones/Traslados/wf_Traslados.aspx";
            string CodLeido = txtCODBARRAS.Text + ";" + txtUbicacionActual.Text + ";" + txtUbicacionMover.Text + ";1";
            string Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido,UsrLogged.IdUsuario,"btnTrasladar");
            Mensaje("ok", Respuesta, "");
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar(sender);
        }

        protected void txtUbicacionActual_TextChanged(object sender, EventArgs e)
        {
            return;
            // aquí se valida: si la ubicación existe y si el artículos está en la ubicación.
            string Pagina = "~/HH/Operaciones/Ingresos/wf_UbicarArticulo.aspx";
            string resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, txtUbicacionActual.Text, UsrLogged.IdUsuario, "btnArticulosSegunUbicacion");
            if (!resultado.Contains(TxtIdarticulo.Text))
            {
                Mensaje("ok", "Artículo con esa fecha de vencimiento y ese lote no encontrado en la ubicación... o la ubicación no es válida.", "");
                txtUbicacionActual.Text = "";
            }
            else
            {
                int indbodega = 0;
                int indbodegaf = 0;
                indbodega = resultado.IndexOf("-");
                indbodegaf = resultado.IndexOf("-", indbodega + 1);
                TxtBodega.Text = resultado.Substring(indbodega, (indbodegaf - indbodega));
            }
        }

        protected void txtUbicacionMover_TextChanged(object sender, EventArgs e)
        {
           // valida si la ubicación a mover existe y está en la misma bodega que el producto a trasladar.
            string Pagina = "~/HH/Operaciones/Ingresos/wf_UbicarArticulo.aspx";
            string resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, txtUbicacionMover.Text, UsrLogged.IdUsuario, "btnArticulosSegunUbicacion");
            if (resultado.Contains("Ningún artículo encontrado"))
            {
                Mensaje("ok", resultado, "");
                txtUbicacionMover.Text = "";
            }
            else
            {
                int indbodega = 0;
                int indbodegaf = 0;
                indbodega = resultado.IndexOf("-");
                indbodegaf = resultado.IndexOf("-", indbodega + 1);
                if (!resultado.Substring(indbodega, (indbodegaf - indbodega)).Contains(TxtBodega.Text))
                {
                    Mensaje("ok", "Bodega de ubicación a mover(" + resultado.Substring(indbodega, (indbodegaf - indbodega)) + ") no correspende con la bodega de la ubicación actual(" + TxtBodega.Text + ").", "");
                    txtUbicacionMover.Text = "";
                }

            } 
        }

        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            if (txtCODBARRAS.Text == "")
            {
                txtCODBARRAS.Focus();
                return;
            }

            if (txtUbicacionActual.Text == "")
            {
                txtUbicacionActual.Focus();
                return;
            }


            if (txtUbicacionMover.Text == "")
            {
                txtUbicacionMover.Focus();
                return;
            }
        }

        protected void txtCODBARRAS_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string Pagina = "~/Operaciones/Salidas/wf_CrearAlisto.aspx";
                Control Ctr = (Control)sender;
                var Panel = Ctr.Parent.Parent.Parent;
                string resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, Panel, UsrLogged.IdUsuario, "btnObtenerAlisto");

                string[] Elementos = resultado.Split(';');
                if (Elementos[0].Contains("EXITOSAMENTE"))
                {
                    DateTime fv = DateTime.Parse(Elementos[5]);
                    TxtNombrearticulo.Text = Elementos[2] + "-" + Elementos[3] + "-" + Elementos[1] + "(" + Elementos[4] + ")-" + fv.ToShortDateString().Trim() + "-" + Elementos[6];
                    TxtIdarticulo.Text = "id Artículo......:" + Elementos[1] + "\n" + "Fecha Vencimiento:" + fv.ToShortDateString().Trim() + "\n" + "Lote.............:" + Elementos[6];  // idarticulo TraceId.
                }
                else
                {
                    txtCODBARRAS.Text = "";
                    TxtNombrearticulo.Text = "";
                    TxtIdarticulo.Text = "";
                    Mensaje("ok", Elementos[0], "");
                }
            }
            catch (Exception ex)
            {
                Mensaje("ok", ex.Message, "");
            }
        }

        private void Limpiar(object limpia)
        {
            Control Ctr = (Control)limpia;
            var Panel = Ctr.Parent.Parent.Parent;
            n_SmartMaintenance.LimpiarForm(Panel);
        }

    }
}