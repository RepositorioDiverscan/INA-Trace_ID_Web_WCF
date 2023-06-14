using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Diverscan.MJP.AccesoDatos.Articulos;
using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.AccesoDatos.Operacion.DespachoPedidos;
using Diverscan.MJP.AccesoDatos.Operacion.DespachoPedidos.Entidades;
using Diverscan.MJP.AccesoDatos.Operaciones;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.MaestroArticulo;
using Diverscan.MJP.Negocio.SectorWarehouse;
using Diverscan.MJP.UI.CrystalReportes.PedidosFacturadosProducto;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Reportes.PedidosFacturadosProducto {
    public partial class wfReporteProductoPedidosFacturados : System.Web.UI.Page
    {
        private readonly IDespachoDePedidos despachoDePedidos;
        private readonly DAArticulos dAArticulos;
        private e_Usuario UsrLogged = new e_Usuario();

        public wfReporteProductoPedidosFacturados()
        {
            despachoDePedidos = new DepachoDePedidosDBA();
            dAArticulos = new DAArticulos();
        }

     
        private long _idWarehouse
        {
            get
            {
                long result = -1;
                var data = ViewState["_idWarehouse"];
                if (data != null)
                {
                    result = long.Parse(data.ToString());
                }
                return result;
            }
            set
            {
                ViewState["_idWarehouse"] = value;
            }
        }

        private string _lote
        {
            get
            {
                string result = "";
                var data = ViewState["_lote"];
                if (data != null)
                {
                    result = data.ToString();
                }
                return result;
            }
            set
            {
                ViewState["_lote"] = value;
            }
        }

        private string _fechaExp
        {
            get
            {
                string result = "";
                var data = ViewState["_fechaExp"];
                if (data != null)
                {
                    result = data.ToString();
                }
                return result;
            }
            set
            {
                ViewState["_fechaExp"] = value;
            }
        }

        private List<EMaestroFacturadoProducto> _listaMaestroFacturados
        {
            get
            {
                var data = ViewState["_listaMaestroFacturados"] as List<EMaestroFacturadoProducto>;
                if (data == null)
                {
                    data = new List<EMaestroFacturadoProducto>();
                    ViewState["_listaMaestroFacturados"] = data;
                }
                return data;
            }
            set { ViewState["_listaMaestroFacturados"] = value; }
        }

        private e_MaestroArticulo _articulo
        {
            get
            {
                var data = ViewState["_articulo"] as e_MaestroArticulo;
                if (data == null)
                {
                    data = new e_MaestroArticulo();
                    ViewState["_articulo"] = data;
                }
                return data;
            }
            set { ViewState["_articulo"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {

                SetDatetime();
                FillDDBodega();
            }
        }

        #region SCRIPTSMANEGER
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
        #endregion

        #region METODOS PRIVADOS

        private void SetDatetime()
        {
            DateTime datetime = DateTime.Now;           
            RDPFechaExp.SelectedDate = datetime;           
        }

        private void FillDDBodega()
        {
            NConsultas nConsultas = new NConsultas();
            List<EBodega> ListBodegas = nConsultas.GETBODEGAS();
            ddBodega.DataSource = ListBodegas;
            ddBodega.DataTextField = "Nombre";
            ddBodega.DataValueField = "IdBodega";
            ddBodega.DataBind();
            ddBodega.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            ddBodega.Items[0].Attributes.Add("disabled", "disabled");
        }

        public void Mensaje(string sTipo, string sMensaje, string sLLenado)
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

        #endregion

        #region EVENTOS
        protected void ddBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddBodega.SelectedIndex > 0)
                {
                    _idWarehouse = Convert.ToInt32(ddBodega.SelectedValue);
                    FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                    NSectorWareHouse nSectorWare = new NSectorWareHouse(fileExceptionWriter);
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Se presente un error " + ex.Message, "");
            }

        }      

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string idInternoArticulo = txtIdInterno.Text;
                _lote = txtLote.Text;
                DateTime fechaExp = RDPFechaExp.SelectedDate.Value;
                _fechaExp = fechaExp.ToShortDateString();
                if (String.IsNullOrEmpty(idInternoArticulo))
                {
                    Mensaje("error", "Debe de ingresar un SKU", "");
                    return;
                }
                _articulo = 
                 dAArticulos.ObtenerArticulo(idInternoArticulo);

                if (_articulo == null)
                {
                    Mensaje("error", "Articulo no existente!!!", "");
                    return;
                }
                else
                {
                    txtNombreArticulo.Text = _articulo.Nombre;
                }               

                if (ddBodega.SelectedIndex == 0)
                {
                    Mensaje("error", "Debe seleccionar una bodega", "");
                    return;
                }
                else
                    _idWarehouse = Convert.ToInt32(ddBodega.SelectedIndex.ToString());

                if (!_articulo.Trazable)
                {
                    _lote = "NA";
                    _fechaExp = "NA";
                    fechaExp = DateTime.Parse("Jan 1, 1900");
                }

                _listaMaestroFacturados = despachoDePedidos.
                    ObtenerPreMaestrosXArticulo(_idWarehouse, _lote, fechaExp, _articulo.IdArticulo);
                RGDMaestrosFacturados.DataSource = _listaMaestroFacturados;
                RGDMaestrosFacturados.DataBind();
                PedidosFacturados.Visible = true;

                //_listaMaestroFacturados.

            }
            catch (Exception ex)
            {
                Mensaje("error", "Se presento un error " + ex.Message, "");
            }
        }

        protected void RGDMaestrosFacturados_NeedDataSource(object sender, 
            Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGDMaestrosFacturados.DataSource = _listaMaestroFacturados;
        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            if (_articulo != null)
            {
                GenerarReporte();
            }
        }

        private void GenerarReporte()
        {
            try
            {
                if (_articulo == null)
                {
                    Mensaje("error", "Debe buscar un articulo primero!!!", "");
                    return;
                }

                ReportDocument report = new CrystalReportes.PedidosFacturadosProducto.PedidosFacturasProducto();               

                List<CREMaestroArticulo> listaArticulos = new List<CREMaestroArticulo>();

                CREMaestroArticulo articulo = new CREMaestroArticulo();
                articulo.IdArticulo = _articulo.IdArticulo;
                articulo.Nombre = _articulo.Nombre;
                articulo.FechaExp = _fechaExp;
                articulo.Lote = _lote;
                articulo.IdInterno = _articulo.IdInterno;            

                listaArticulos.Add(articulo);

                var tablename = report.Database.Tables[0];
                report.Database.Tables[0].SetDataSource(_listaMaestroFacturados);

                var tablename1 = report.Database.Tables[1];

                report.Database.Tables[1].SetDataSource(listaArticulos);

                var fileName = "Facturas por Articulo " + _articulo.Nombre + ".pdf";
                var path = HttpRuntime.AppDomainAppPath + @"CrystalReportes" + @"\PedidosFacturadosProducto\" + fileName;

                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = path;
                CrExportOptions = report.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                report.Export();
                report.Close();
                report.Dispose();
                GC.Collect();

                Response.AddHeader("Content-Type", "application/octet-stream");
                Response.AddHeader("Content-Transfer-Encoding", "Binary");
                Response.AddHeader("Content-disposition", "attachment; filename=\"" + fileName + "\"");
                Response.WriteFile(path);
                Response.End();
            }
            catch (Exception ex)
            {
                Mensaje("error", "Se presente un error " + ex.Message, "");
            }
        }
        #endregion
    }
}