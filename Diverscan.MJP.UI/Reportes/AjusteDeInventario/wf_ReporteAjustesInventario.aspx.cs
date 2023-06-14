using Diverscan.MJP.AccesoDatos.Reportes.Reporte_Ajuste_de_Inventario;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Diverscan.MJP.UI.Reportes.AjusteDeInventario
{
    public partial class wf_ReporteAjustesInventario : System.Web.UI.Page
    {
        private readonly IObtenerPromociones ObtenerEncabezadoPromocion;
        private e_Usuario UsrLogged = new e_Usuario();
        public wf_ReporteAjustesInventario()
        {
            ObtenerEncabezadoPromocion = new ObtenerPromociones();
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

                ObtieneEncabezadoPromocion();
                SetDatetime();
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


        private void ObtieneEncabezadoPromocion()
        {
            List<NObtenerEncabezadoPromocion> ListaPromocion = ObtenerEncabezadoPromocion.ObtenerPromociones();
            ddlPromociones.DataSource = ListaPromocion;
            ddlPromociones.DataTextField = "Nombre";
            ddlPromociones.DataValueField = "idMaestroPromocion";
            ddlPromociones.DataBind();
            ddlPromociones.Items.Insert(0, new ListItem("--Seleccione--", "0"));

        }

        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            PanelAjustes.Visible = true;

            DateTime FechaInicio = Convert.ToDateTime(RDPFechaInicial.SelectedDate.ToString());
            DateTime FechaFinal = Convert.ToDateTime(RDPFechaFinal.SelectedDate.ToString());
            var v1 = RDPFechaFinal.SelectedDate;
            List<AjusteSolicitudRecord> ListaAjustesPorFechas = ObtenerEncabezadoPromocion.ObtenesAjustesPorFechas(Convert.ToDateTime(RDPFechaInicial.SelectedDate.ToString()), Convert.ToDateTime(RDPFechaFinal.SelectedDate.ToString()));
            RGAjustes.DataSource = ListaAjustesPorFechas;
            RGAjustes.DataBind();


        }

        protected void ddlPromociones_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PanelPromoc.Visible = true;
                int idMaestro = int.Parse(ddlPromociones.SelectedValue);
                List<EObtenerArticulosPromocion> ListaArticulosPromocion = ObtenerEncabezadoPromocion.ObtenerArticulosDePromocion(idMaestro);
                RGArticulosPromocion.DataSource = ListaArticulosPromocion;
                RGArticulosPromocion.DataBind();
                // AjusteSolicitudRecord
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
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

            RDPFechaInicial.SelectedDate = datetime;
            RDPFechaFinal.SelectedDate = datetime;

        }

        protected void RGAjustes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;

                int idSolicitud = Convert.ToInt32(item["IdSolicitudAjusteInventario"].Text.Replace("&nbsp;", ""));

                if (e.CommandName == "btnVerArticulos")
                {
                    PanelArticulosSolicitud.Visible = true;

                    List<EObtieneArticulosSolicitud> ListaArticulosSolicitud = ObtenerEncabezadoPromocion.ObtenerArticulosPorAjuste(idSolicitud);
                    RGArticulosSolicitud.DataSource = ListaArticulosSolicitud;
                    RGArticulosSolicitud.DataBind();
                }
                else if (e.CommandName == "btnGeneraReporte")
                {
                    int idMaestro = int.Parse(ddlPromociones.SelectedValue);
                    DateTime FechaInicio = Convert.ToDateTime(RDPFechaInicial.SelectedDate.ToString());
                    DateTime FechaFinal = Convert.ToDateTime(RDPFechaFinal.SelectedDate.ToString());
                    List<EObtieneArticulosSolicitud> ListaArticulosSolicitud = ObtenerEncabezadoPromocion.ObtenerArticulosPorAjuste(idSolicitud);
                    List<EObtenerArticulosPromocion> ListaArticulosPromocion = ObtenerEncabezadoPromocion.ObtenerArticulosDePromocion(idMaestro);
                    List<AjusteSolicitudRecord> ListaAjustesPorFechas = ObtenerEncabezadoPromocion.ObtenesAjustesPorFechas(FechaInicio, FechaFinal);

                    foreach (var a in ListaArticulosPromocion)
                    {
                        var valores = a.IdInternoArticulo;

                        List<int> lista = new List<int>();
                        lista.Add(valores);
                        var resultado = lista;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Se presento el error." + ex.Message, "");
            }
        }

        protected void btInformacion_Click(object sender, EventArgs e)
        {

            List<AjusteSolicitudRecord> ListaAjustesPorFechas = new List<AjusteSolicitudRecord>();

            List<EObtieneArticulosSolicitud> ListaArticulosSolicitud = new List<EObtieneArticulosSolicitud>();
            List<EObtieneArticulosSolicitud> ListaArticulosSolicitudInfo = new List<EObtieneArticulosSolicitud>();

            List<EObtieneArticulosSolicitud> ListaArticulosSolicitudFiltradaEntrada = new List<EObtieneArticulosSolicitud>();
            List<EObtieneArticulosSolicitud> ListaArticulosSolicitudFiltradaSalida = new List<EObtieneArticulosSolicitud>();

            DateTime FechaInicio = Convert.ToDateTime(RDPFechaInicial.SelectedDate.ToString());
            DateTime FechaFinal = Convert.ToDateTime(RDPFechaFinal.SelectedDate.ToString());

            int idMaestro = int.Parse(ddlPromociones.SelectedValue);
            List<EObtenerArticulosPromocion> ListaArticulosPromocion = ObtenerEncabezadoPromocion.ObtenerArticulosDePromocion(idMaestro);
            List<E_EntidadBase> ListaBase = new List<E_EntidadBase>();
            List<E_EntidadBase> ListaBaseUtilizar = new List<E_EntidadBase>();
            List<NObtenerEncabezadoPromocion> ListaPromocion = ObtenerEncabezadoPromocion.ObtenerPromociones();
            ListaPromocion = ListaPromocion.FindAll(x => x.idMaestroPromocion == idMaestro);

            for (int i = 0; i < RGAjustes.Items.Count; i++)
            {
                var item = RGAjustes.Items[i];
                var checkbox = item["checkDetalle"].Controls[0] as CheckBox;
                if (checkbox != null && checkbox.Checked)
                {
                    int idSolicitud = Convert.ToInt32(item["IdSolicitudAjusteInventario"].Text.Replace("&nbsp;", ""));

                    ListaArticulosSolicitud = ObtenerEncabezadoPromocion.ObtenerArticulosPorAjuste(idSolicitud);

                    foreach (var item2 in ListaArticulosSolicitud)
                    {
                        ListaArticulosSolicitudInfo.Add(new EObtieneArticulosSolicitud(item2.IdSolicitudAjusteInventario, item2.IdArticulo, item2.IdInterno, item2.Nombre, item2.Cantidad));
                    }

                }
            }
            ListaAjustesPorFechas = ObtenerEncabezadoPromocion.ObtenesAjustesPorFechas(FechaInicio, FechaFinal);
            foreach (var item in ListaAjustesPorFechas)
            {
                foreach (var item2 in ListaAjustesPorFechas.FindAll(x => x.IdSolicitudAjusteInventario == item.IdSolicitudAjusteInventario && x.TipoMotivo == "Salida"))
                {

                    foreach (var item3 in ListaArticulosSolicitudInfo.FindAll(x => x.IdSolicitudAjusteInventario == item2.IdSolicitudAjusteInventario))
                    {
                        ListaArticulosSolicitudFiltradaSalida.Add(new EObtieneArticulosSolicitud(item3.IdSolicitudAjusteInventario, item3.IdArticulo, item3.IdInterno, item3.Nombre, item3.Cantidad));
                    }
                }
            }
            foreach (var item in ListaAjustesPorFechas)
            {
                foreach (var item2 in ListaAjustesPorFechas.FindAll(x => x.IdSolicitudAjusteInventario == item.IdSolicitudAjusteInventario && x.TipoMotivo == "Entrada"))
                {

                    foreach (var item3 in ListaArticulosSolicitudInfo.FindAll(x => x.IdSolicitudAjusteInventario == item2.IdSolicitudAjusteInventario))
                    {
                        ListaArticulosSolicitudFiltradaEntrada.Add(new EObtieneArticulosSolicitud(item3.IdSolicitudAjusteInventario, item3.IdArticulo, item3.IdInterno, item3.Nombre, item3.Cantidad));
                    }
                }
            }
            decimal CantidadEntrada = 0;
            decimal CantidadArticulo = 0;

            foreach (var item in ListaPromocion.FindAll(x => x.idMaestroPromocion == Convert.ToInt32(ddlPromociones.SelectedValue)))
            {
                foreach (var item2 in ListaArticulosPromocion.FindAll(x => x.IdMaestroPromocion == item.idMaestroPromocion))
                {
                    decimal CantidadSalida = 0;
                    foreach (var item3 in ListaArticulosSolicitudFiltradaSalida.FindAll(x => x.IdArticulo == Convert.ToInt32(item2.IdInternoArticulo)))
                    {
                        CantidadSalida = CantidadSalida + item3.Cantidad;
                    }

                    ListaBase.Add(new E_EntidadBase(item2.IdInternoArticulo, item2.IdInternoPANAL, item2.Nombre, CantidadSalida, CantidadEntrada, CantidadArticulo));

                    foreach (var item4 in ListaBase.FindAll(x => x.IdArticulo == item2.IdInternoArticulo))
                    {
                        foreach (var item5 in ListaArticulosSolicitudFiltradaEntrada.FindAll(x => x.IdArticulo == item2.IdArticuloNuevo && item2.IdInternoArticulo == item4.IdArticulo))
                        {
                            /*CantidadEntrada = item5.Cantidad;
                            CantidadArticulo = CantidadSalida - CantidadEntrada;*/

                            /*
                             * CantidadEntrada = item5.Cantidad;
                            decimal CantidadVisualizar = 0;
                            CantidadVisualizar = CantidadSalida / item2.Cantidad;
                            CantidadArticulo = CantidadVisualizar - CantidadEntrada;
                            */
                            CantidadEntrada = item5.Cantidad;
                            decimal CantidadVisualizar = 0;
                            CantidadVisualizar = CantidadEntrada * item2.Cantidad;
                            CantidadArticulo = CantidadSalida - CantidadVisualizar;


                        }
                        ListaBaseUtilizar.Add(new E_EntidadBase(item2.IdInternoArticulo, item2.IdInternoPANAL, item2.Nombre, CantidadSalida, CantidadEntrada, CantidadArticulo));

                    }
                }
            }
            PanelVisualizacionInformacion.Visible = true;
            RadGrid1.DataSource = ListaBaseUtilizar;
            RadGrid1.DataBind();

        }

    }

    public class E_EntidadBase
    {


        public E_EntidadBase(int idInternoArticulo, string idInterno, string nombre, decimal cantidadSalida, decimal cantidadEntrada, decimal sobrante)
        {
            this.IdArticulo = idInternoArticulo;
            CantidadSalida = cantidadSalida;
            CantidadEntrada = cantidadEntrada;
            this.Sobrante = sobrante;
            this.IdInterno = idInterno;
            this.Nombre = nombre;
        }

        public int IdArticulo { get; set; }
        public string IdInterno { get; set; }
        public string Nombre { get; set; }
        public decimal CantidadSalida { get; set; }
        public decimal CantidadEntrada { get; set; }
        public decimal Sobrante { get; set; }

    }
}

