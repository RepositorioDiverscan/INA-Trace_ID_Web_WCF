using Diverscan.MJP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Diverscan.MJP.AccesoDatos.Reportes.ReportePedidoSinOla;
using Diverscan.MJP.AccesoDatos.Reportes;
using System.Reflection;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.AccesoDatos.Reportes.KardexXSkuTIDSAP.Entidad;
using Diverscan.MJP.Utilidades;
using System.ComponentModel;

namespace Diverscan.MJP.UI.Reportes.KardexXSkuTIDSAP
{
    public partial class wf_KardexXSkuTIDSAP : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        IReportes _reportes;

        public wf_KardexXSkuTIDSAP()
        {
          //  _reportes = new NReporte();
        }

        private int _idBodega
        {
            get
            {
                var objeto = ViewState["IdBodega"];
                var data = 0;

                if (objeto == null)
                {
                    data = 0;

                }
                else
                {
                    data = Convert.ToInt32(objeto);
                }
                ViewState["IdBodega"] = data;
                return data;
            }
            set
            {
                ViewState["IdBodega"] = value;
            }
        }
        private List<EListKardexSkuTID> _listKardexTID
        {
            get
            {
                var data = ViewState["listKardexTID"] as List<EListKardexSkuTID>;
                if (data == null)
                {
                    data = new List<EListKardexSkuTID>();
                    ViewState["listKardexTID"] = data;
                }
                return data;
            }
            set
            {
                ViewState["listKardexTID"] = value;
            }
        }
        private List<EListKardexSkuSAP> _listKardexSAP
        {
            get
            {
                var data = ViewState["listKardexSAP"] as List<EListKardexSkuSAP>;
                if (data == null)
                {
                    data = new List<EListKardexSkuSAP>();
                    ViewState["listKardexSAP"] = data;
                }
                return data;
            }
            set
            {
                ViewState["listKardexSAP"] = value;
            }
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
            this.UpdatePanel1.Unload += new EventHandler(UpdatePanel1_Unload);
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
        private void SetDatetime()
        {
            DateTime datetime = DateTime.Now;

            if (datetime.DayOfWeek == DayOfWeek.Monday || datetime.DayOfWeek == DayOfWeek.Tuesday || datetime.DayOfWeek == DayOfWeek.Wednesday || datetime.DayOfWeek == DayOfWeek.Thursday || datetime.DayOfWeek == DayOfWeek.Friday)
            {
                RDPFechaFinal.SelectedDate = datetime.AddDays(1).Date;
            }
            else if (datetime.DayOfWeek == DayOfWeek.Saturday)
            {
                RDPFechaFinal.SelectedDate = datetime.AddDays(2).Date;
            }
            else
            {
                RDPFechaFinal.SelectedDate = datetime.AddDays(1).Date;
            }

            RDPFechaInicio.SelectedDate = datetime;
            RDPFechaFinal.SelectedDate = datetime;

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

        private bool validarCampos()
        {
            if (ddBodega.SelectedIndex < 0 || txtSearch.Text.ToString().Trim() == "")
            {
                Mensaje("info", "Debe llenar todos los espacios", "");
                return false;
            }
            return true;
        }

        private void ObtenerKardexSkuTID(EKardexSkuTIDSAP eKardexSkuTID)
        {
            try
            {
                _listKardexTID = _reportes.ObtenerKardexSkuTID(eKardexSkuTID);
                RGAKardexTID.DataSource = _listKardexTID;
                RGAKardexTID.DataBind();
            } catch (Exception ex)
            {
                Mensaje("error", "Ha ocurrido el error: " + ex.Message, "");
            }
        }

        private void ObtenerKardexSkuSAP(EKardexSkuTIDSAP eKardexxSkuSAP)
        {
            try
            {
                _listKardexSAP = _reportes.ObtenerKardexSkuSAP(eKardexxSkuSAP);
                RGAKardexSAP.DataSource = _listKardexSAP;
                RGAKardexSAP.DataBind();
            } catch (Exception ex)
            {
                Mensaje("error", "Ha ocurrido el error: " + ex.Message, "");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                _idBodega = Convert.ToInt32(ddBodega.SelectedValue.ToString());
                DateTime fechaInicial = Convert.ToDateTime(RDPFechaInicio.SelectedDate);
                DateTime fechaFinal = Convert.ToDateTime(RDPFechaFinal.SelectedDate);
                string sku = txtSearch.Text.Trim();

                EKardexSkuTIDSAP eKardexSkuTID = new EKardexSkuTIDSAP()
                {
                    IdBodega = _idBodega
                    , FechaInicio = fechaInicial
                    , FechaFin = fechaFinal
                    , Sku = sku

                };

                ObtenerKardexSkuTID(eKardexSkuTID);

                ObtenerKardexSkuSAP(eKardexSkuTID);
            }
        }

        protected void RGAKardexTID_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGAKardexTID.DataSource = _listKardexTID;
        }

        protected void RGAKardexSAP_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGAKardexSAP.DataSource = _listKardexSAP;
        }

        private void GenerarReporteKardexTID()
        {
            FileExceptionWriter exceptionWriter = new FileExceptionWriter();
            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(EListKardexSkuTID));
                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[5];
                propertiesSelected[0] = properties.Find("Tipo", false);
                propertiesSelected[1] = properties.Find("Socio", false);
                propertiesSelected[2] = properties.Find("Fecha", false);
                propertiesSelected[3] = properties.Find("Cantidad", false);
                propertiesSelected[4] = properties.Find("Saldo", false);
                var propertySelected = new PropertyDescriptorCollection(propertiesSelected);
                var rutaVirtual = "~/temp/" + string.Format("KardexXSkuTID-" + txtSearch.Text.Trim() + ".xlsx");
                var fileName = Server.MapPath(rutaVirtual);
                List<string> headers = new List<string>() { "Transacción", "Socio", "Fecha", "Cantidad", "Saldo" };
                ExcelExporter.ExportData(_listKardexTID, fileName, propertySelected, headers);
                Response.Redirect(rutaVirtual, false);
            }
            catch (Exception ex)
            {
                exceptionWriter.WriteException(ex, PathFileConfig.INVENTORYFILEPATHEXCEPTION);
                Mensaje("error", "Ha ocurrido un error, vuelva a intentar.", "");
            }
        }

        private void GenerarReporteKardexSAP()
        {
            FileExceptionWriter exceptionWriter = new FileExceptionWriter();
            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(EListKardexSkuSAP));
                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[5];
                propertiesSelected[0] = properties.Find("Tipo", false);
                propertiesSelected[1] = properties.Find("Socio", false);
                propertiesSelected[2] = properties.Find("Fecha", false);
                propertiesSelected[3] = properties.Find("Cantidad", false);
                propertiesSelected[4] = properties.Find("Saldo", false);
                var propertySelected = new PropertyDescriptorCollection(propertiesSelected);
                var rutaVirtual = "~/temp/" + string.Format("KardexXSkuSAP-"+txtSearch.Text.Trim()+".xlsx");
                var fileName = Server.MapPath(rutaVirtual);
                List<string> headers = new List<string>() { "Transacción", "Socio", "Fecha", "Cantidad", "Saldo" };
                ExcelExporter.ExportData(_listKardexSAP, fileName, propertySelected, headers);
                Response.Redirect(rutaVirtual, false);
            }
            catch (Exception ex)
            {
                exceptionWriter.WriteException(ex, PathFileConfig.INVENTORYFILEPATHEXCEPTION);
                Mensaje("error", "Ha ocurrido un error, vuelva a intentar.", "");
            }
        }

        protected void btnGenerarReporteTID_Click(object sender, EventArgs e)
        {
            GenerarReporteKardexTID();
        }

        protected void btnGenerarReporteSAP_Click(object sender, EventArgs e)
        {
            GenerarReporteKardexSAP();
        }
    }
}