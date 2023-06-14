using Diverscan.MJP.AccesoDatos.Recaudo;
using Diverscan.MJP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Diverscan.MJP.UI.Recaudo
{
    public partial class Recaudo : System.Web.UI.Page
    {
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

        protected void Page_Load(object sender, EventArgs e)
        {
            e_Usuario UsrLogged = new e_Usuario();
            UsrLogged = (e_Usuario)Session["USUARIO"];
            
            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {
                SetDatetime();
            }
        }

        private List<EJornadaRecaudo> _jornadasRecaudo
        {
            get
            {
                var data = ViewState["JornadasRecaudo"] as List<EJornadaRecaudo>;
                if (data == null)
                {
                    data = new List<EJornadaRecaudo>();
                    ViewState["JornadasRecaudo"] = data;
                }
                return data;
            }
            set
            {
                ViewState["JornadasRecaudo"] = value;
            }
        }

        private List<ERecaudoEncabezado> _recaudoEncabezado
        {
            get
            {
                var data = ViewState["RecaudoEncabezado"] as List<ERecaudoEncabezado>;
                if (data == null)
                {
                    data = new List<ERecaudoEncabezado>();
                    ViewState["RecaudoEncabezado"] = data;
                }
                return data;
            }
            set
            {
                ViewState["RecaudoEncabezado"] = value;
            }
        }
        
        private EJornadaRecaudo _jornadaRecaudo
        {
            get
            {
                return ViewState["JornadaRecaudo"] as EJornadaRecaudo;
            }
            set
            {
                ViewState["JornadaRecaudo"] = value;
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


        protected void RadGridArticulos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGridJornadas.DataSource = _jornadasRecaudo;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void buscar()
        {
            try
            {
                DJornadaRecaudo dJornadaRecaudo = new DJornadaRecaudo();
                if (!string.IsNullOrEmpty(txtIdJornada.Text))
                {
                    int idJornada = Convert.ToInt32(txtIdJornada.Text);
                    EJornadaRecaudo jornada = dJornadaRecaudo.GetJornadaXIdJornada(idJornada);
                    List<EJornadaRecaudo> jornadasRecaudo = new List<EJornadaRecaudo>();
                    jornadasRecaudo.Add(jornada);
                    _jornadasRecaudo = jornadasRecaudo;
                }
                else
                {
                    if(!string.IsNullOrEmpty(txtMail.Text))
                    {                   
                        DateTime fechaInicio = RDPFechaInicio.SelectedDate.Value;
                        DateTime fechaFin = RDPFechaFinal.SelectedDate.Value;
                        _jornadasRecaudo = dJornadaRecaudo.GetJornadaRecaudosCorreo(txtMail.Text, fechaInicio, fechaFin);
                    }
                    else
                        Mensaje("error", "Debe ingresar un correo", "");
                }

                RadGridJornadas.DataSource = _jornadasRecaudo;
                RadGridJornadas.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("error", "Se presente un error " + ex.Message, "");
            }
        }    

        protected void RadGridJornadas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            CheckBox cb = new CheckBox();
            switch (e.CommandName)
            {
                case "RowClick":
                    {
                        var jornadaRecaudo = _jornadasRecaudo[e.Item.DataSetIndex];
                        _jornadaRecaudo = jornadaRecaudo;
                        if (jornadaRecaudo != null)
                        {
                            DRecaudoEncabezado dRecaudoEncabezado = new DRecaudoEncabezado();
                            _recaudoEncabezado = dRecaudoEncabezado.GetRecaudoEncabezado(jornadaRecaudo.IdJornada);
                            RadGrid1.DataSource = _recaudoEncabezado;
                            RadGrid1.DataBind();
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = _recaudoEncabezado;
        }

        protected void RadGrid1_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "Detalle":
                    {
                        DRecaudoDetalle dRecaudoDetalle = new DRecaudoDetalle();
                        int idRecaudo = Convert.ToInt32(dataItem.GetDataKeyValue("IdRecaudo").ToString());
                        e.DetailTableView.DataSource = dRecaudoDetalle.GetRecaudoDetalle(idRecaudo);
                        break;
                    }            
            }
        }

        protected void btnCerrarJornada_Click(object sender, EventArgs e)
        {
            if(_jornadaRecaudo!=null)
            {
                DJornadaCerrar dJornadaCerrar = new DJornadaCerrar();
                dJornadaCerrar.CerrarJornada(_jornadaRecaudo.IdJornada);
                Mensaje("info", "Se ha cerrrado corectamente", "");
                buscar();
            }          
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
    }
}