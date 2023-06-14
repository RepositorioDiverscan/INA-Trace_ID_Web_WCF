using System;
using System.ComponentModel;
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
using Diverscan.MJP.UI.ServiceMH;
using Diverscan.MJP.Utilidades;
using Diverscan.Visitas.Utilidades;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Telerik.Web.UI.PersistenceFramework;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Diverscan.MJP.Negocio.LogicaWMS;
using System.IO;
using OfficeOpenXml;

namespace Diverscan.MJP.UI.Consultas.Salidas
{
    public partial class wf_ConsultarSolicitud : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        public int ToleranciaAgregar = 110;

        static string idMaestroSolicitud = "";

        static DataSet DSDatosExport = new DataSet();

        static string _idInterno = "";
        static string _idInternoSAP = "";
        static string _Destino = "";
        static string _FechaRegistro = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }  
            if (!IsPostBack)
            {
                RDPFechaInicio.SelectedDate = DateTime.Now;
                RDPFechaFinal.SelectedDate = DateTime.Now;
                RadGridSolicitud.Rebind();
                RadGridDetalleSolicitud.Rebind();
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

        #region Solicitud

        private void CargarSolicitud(string buscar, bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);
                DateTime DTfechaInicio = RDPFechaInicio.SelectedDate ?? DateTime.Now;
                DateTime DTfechaFin = RDPFechaFinal.SelectedDate ?? DateTime.Now;

                string fechaInicio = DTfechaInicio.ToString("yyyyMMdd") + " 00:00:00";
                string fechaFin = DTfechaFin.ToString("yyyyMMdd") + " 23:59:59";

                SQL = "EXEC SP_BuscarMaestroSolicitud '" + idCompania + "', '" + buscar + "', '" + fechaInicio + "', '" + fechaFin + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridSolicitud.DataSource = DSDatos;
                if (pestana)
                {
                    RadGridSolicitud.DataBind();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private void LimpiarSolicitud()
        {
            CargarDetalleSolicitud("", true);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarSolicitud();
                CargarSolicitud(txtSearch.Text.ToString().Trim(), true);
                //txtSearch.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {
                btnExportar.Enabled = false;
                LimpiarSolicitud();
                CargarSolicitud("", true);
                txtSearch.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> encabezadosMaestro = new List<string>() { "Id Interno: ", "N.Pedido Tienda: ", "Destino: ", "Fecha Solicitud: " };

                List<string> encabezadosDatosMaestro = new List<string>() { _idInterno, _idInternoSAP, _Destino, _FechaRegistro };


                List<string> encabezados = new List<string>() { "Id DS", "Id SOL", "Nombre", "Codigo Articulo", "Descripcion", 
                "Cantidad", "Cantidad Despachada", "Cantidad Pendiente", "Unidad Medida"};

                var rutaVirtual = "~/temp/" + string.Format("Solicitud Unidad Inventario.xlsx");
                var fileName = Server.MapPath(rutaVirtual);
                Console.WriteLine(fileName);
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                var newFile = new FileInfo(fileName);
                using (ExcelPackage aplicacion = new ExcelPackage(newFile))
                {
                    aplicacion.Workbook.Worksheets.Add("Hoja 1");
                    ExcelWorksheet hoja_trabajo = aplicacion.Workbook.Worksheets[1];

                    for (int x = 0; x < encabezadosMaestro.Count; x++)
                    {
                        hoja_trabajo.Cells[x + 1, 1].Value = encabezadosMaestro[x];
                    }

                    for (int x = 0; x < encabezadosDatosMaestro.Count; x++)
                    {
                        hoja_trabajo.Cells[x + 1, 2].Value = encabezadosDatosMaestro[x];
                    }

                    for (int x = 0; x < encabezados.Count; x++)
                    {
                        hoja_trabajo.Cells[6, x + 1].Value = encabezados[x];
                    }

                    for (int i = 0; i < DSDatosExport.Tables[0].Rows.Count; i++)
                    {
                        for (int j = 0; j < DSDatosExport.Tables[0].Columns.Count; j++)
                        {
                            hoja_trabajo.Cells[i + 7, j + 1].Value = DSDatosExport.Tables[0].Rows[i][j].ToString();
                        }
                    }

                    hoja_trabajo.Cells["A1:Z10000"].AutoFitColumns();

                    aplicacion.Save();
                }

                Response.Redirect(rutaVirtual, false);
            }
            catch (Exception ex)
            {
                new clErrores().escribirError(ex.Message, ex.StackTrace);
            }
        }

        protected void RadGridSolicitud_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarSolicitud(txtSearch.Text.ToString().Trim(), false);
                //CargarSolicitud("", false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridSolicitud_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    idMaestroSolicitud = item["idMaestroSolicitud"].Text.Replace("&nbsp;", "");

                    //_idInterno = item["idInterno"].Text.Replace("&nbsp;", "");
                    //_idInternoSAP = item["idInternoSAP"].Text.Replace("&nbsp;", "");
                    _Destino = item["NombreDestino"].Text.Replace("&nbsp;", "");
                    _FechaRegistro = item["FechaCreacion"].Text.Replace("&nbsp;", "");

                    btnExportar.Enabled = true;

                    CargarDetalleSolicitud(idMaestroSolicitud, true);
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        #endregion Solicitud

        #region DetalleSolicitud

        private void CargarDetalleSolicitud(string buscar, bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                SQL = "EXEC SP_BuscarDetalleSolicitud '" + idCompania + "', '" + buscar + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                if (DSDatos.Tables[0].Rows.Count > 0)
                {
                    RadGridDetalleSolicitud.Visible = true;

                    RadGridDetalleSolicitud.DataSource = DSDatos;
                    if (pestana)
                    {
                        RadGridDetalleSolicitud.DataBind();
                    }
                }
                else
                {
                    RadGridDetalleSolicitud.Visible = false;
                }

                DSDatosExport = DSDatos;
                
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridDetalleSolicitud_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarDetalleSolicitud(idMaestroSolicitud, false);

                //n_WMS wms = new n_WMS();
                //DataSet DSDatos = new DataSet();
                //string SQL = "";
                //string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                //DSDatos.Clear();
                //SQL = "EXEC SP_BuscarDetalleSolicitud '" + idCompania + "', ''";
                //DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                //RadGridDetalleSolicitud.DataSource = new string[] { };
                //RadGridDetalleSolicitud.DataSource = DSDatos;
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridDetalleSolicitud_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    //string idMaestroSolicitud = item["idMaestroSolicitud"].Text.Replace("&nbsp;", "");
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        #endregion DetalleSolicitud

    }
}