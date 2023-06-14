using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Compilation;
using System.Xml;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.Negocio.Programa;
using Diverscan.MJP.Negocio.LogicaWMS;
using Diverscan.MJP.Negocio.OrdenCompa;
using Diverscan.MJP.UI.ServiceMH;
using Diverscan.MJP.Utilidades;
using Diverscan.Visitas.Utilidades;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Telerik.Web.UI.Diagram;
using Telerik.Web.UI.PersistenceFramework;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Globalization;
using Diverscan.MJP.Utilidades.general;
using System.Text;

namespace Diverscan.MJP.UI.Administracion.Ingreso
{
    public partial class wf_MaestroOrdenCompra : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        public static DataTable DTDetalleOC = new DataTable();
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        public int ToleranciaAgregar = 80;
        RadGridProperties radGridProperties = new RadGridProperties();

        protected void Page_Load(object sender, EventArgs e)
        {

            UsrLogged = (e_Usuario)Session["USUARIO"];

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {
                CargarDDLS();
                CrearDSDetalleOC();
                RadGridOPEINGDetalleOrdenCompra.GroupingSettings.CaseSensitive = false;
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
        private void CargarDDLS()
        {
            try
            {
                
                txtIdCompania.Text = ConfigurationManager.AppSettings["Compania"].ToString();


                string[] Msj = n_SmartMaintenance.CargarDDL(ddlIdProveedor, e_TablasBaseDatos.TblProveedores(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-ADM-ING-000001" + ex.Message, "");
            }
        }
        protected void ddlIdProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // carga el combo de artículos según el proveedor elejido.
            string SQL = "SELECT idArticuloInterno, Nombre " +
                          "  FROM Vista_ArticulosProveedor " +
                          "  WHERE idproveedor = " + ddlIdProveedor.SelectedValue.ToString() +
                          "  ORDER BY Nombre";
                          

            //string SQL = "select idArticulo, cast(idArticulo as nvarchar(max)) + '-' + Nombre as Nombre from ADMMaestroArticulo ";

            ddlidArticuloInterno.DataTextField = "Nombre";
            ddlidArticuloInterno.DataSource = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);
            ddlidArticuloInterno.DataValueField = "idArticuloInterno";
            ddlidArticuloInterno.DataBind();
            ddlidArticuloInterno.Items.Insert(0, new ListItem("--Seleccionar--"));
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

        private void CrearDSDetalleOC()
        {
            try
            {
                if (!DTDetalleOC.Columns.Contains("IdDetalleOC"))
                {
                    DTDetalleOC.Columns.Add("IdDetalleOC", typeof(string));
                    DTDetalleOC.Columns.Add("idArticuloInterno", typeof(string));
                    DTDetalleOC.Columns.Add("NombreArticuloInterno", typeof(string));
                    DTDetalleOC.Columns.Add("Cantidad", typeof(string));
                    //DTDetalleOC.Columns.Add("UnidadMedida", typeof(string));
                    //DTDetalleOC.Columns.Add("UnidadesAlisto", typeof(string));

                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridOPEINGDetalleordencompra_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid DG = (RadGrid)sender;
            Control Parent = DG.Parent;
            n_SmartMaintenance.CargarGrid(Parent, UsrLogged.IdUsuario);
        }

        protected void ToggleRowSelection(object sender, EventArgs e)
        {
            try
            {
                RadGrid RG = (RadGrid)sender;
                if (RG.ID == "RadGrid1")
                {
                    ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
                    bool checkHeader = true;
                    foreach (GridDataItem dataItem in RG.MasterTableView.Items)
                    {
                        if (!(dataItem.FindControl("CheckBox1") as CheckBox).Checked)
                        {
                            checkHeader = false;
                            break;
                        }
                    }
                    GridHeaderItem headerItem = RG.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
                    (headerItem.FindControl("headerChkbox") as CheckBox).Checked = checkHeader;
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-ADM-ING-000002" + ex.Message, "");
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            try
            {
                RadGrid RG = (RadGrid)sender;
                if (RG.ID == "RadGridOPEINGDetalleOrdenCompra")
                {
                    CheckBox headerCheckBox = (sender as CheckBox);
                    foreach (GridDataItem dataItem in RG.MasterTableView.Items)
                    {
                        (dataItem.FindControl("CheckBox1") as CheckBox).Checked = headerCheckBox.Checked;
                        dataItem.Selected = headerCheckBox.Checked;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-ADM-ING-000003" + ex.Message, "");
            }
        }

        
        protected void RadGridOPEINGDetalleOrdenCompra_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    ddlidArticuloInterno.SelectedValue = item["idArticuloInterno"].Text.Replace("&nbsp;", "");
                    txtCantidadxRecibir.Text = item["Cantidad"].Text.Replace("&nbsp;", "");
                    TxtNumFilaGrid.Text = item["IdDetalleOC"].Text.Replace("&nbsp;", "");

                    Button3.Visible = false;  //--> botón agregar.
                    Button4.Visible = true;  //--> botón editar.
                    BtnElimina.Visible = true;
                    Label30.Visible = true;  //--> separador.
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void btnEditar3_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblAcciones(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnAgregar3_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblAcciones(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnEditar2_Click(object sender, EventArgs e)
        {
            // editar detalle orden de compra.
            try
            {
                string idArticuloInterno = ddlidArticuloInterno.SelectedValue;
                string NombreArticuloInterno = ddlidArticuloInterno.SelectedItem.Text;
                string Cantidad = txtCantidadxRecibir.Text.ToString().Trim().Replace(",", ".");


                if (EsValidaCantidadArticulo(Cantidad))//Valida que la cantidad editada sea válida
                {
                    foreach (DataRow DRDetalleOC in DTDetalleOC.Rows)
                    {
                        if (DRDetalleOC["IdDetalleOC"].ToString() == TxtNumFilaGrid.Text)
                        {
                            decimal cantidadIngresada = decimal.Parse(Cantidad, CultureInfo.InvariantCulture.NumberFormat);

                            DRDetalleOC["NombreArticuloInterno"] = NombreArticuloInterno;
                            DRDetalleOC["Cantidad"] = Cantidad;
                            DRDetalleOC["idArticuloInterno"] = idArticuloInterno;
                            break;

                        }
                    }

                    LimpiarDetalleOC();
                    CargarDetalleOC("", true);
                    Mensaje("ok", "El artículo se editó correctamente", "");
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void btnAgregar2_Click(object sender, EventArgs e)
        {
            // Agregar detalle orden de compra.
            try
            {
                string idArticuloInterno = ddlidArticuloInterno.SelectedValue;
                string NombreArticuloInterno = ddlidArticuloInterno.SelectedItem.Text;
                string Cantidad = txtCantidadxRecibir.Text.ToString().Trim().Replace(",", ".");

                if (ddlidArticuloInterno.SelectedIndex == 0)   //Valida si se seleccionó un artículo
                {
                    Mensaje("info", "Debe seleccionar un artículo", "");
                    txtCantidadxRecibir.Text = "0";
                }
                else if (EsValidaCantidadArticulo(Cantidad))//Se valida la cantidad ingresada sea valida               
                {

                    bool existeArticulo = false;

                    foreach (DataRow DRPDetalleOC in DTDetalleOC.Rows)
                    {
                        if (DRPDetalleOC["idArticuloInterno"].ToString() == idArticuloInterno)
                        {
                            existeArticulo = true;
                        }
                    }
                    if (!existeArticulo)
                    {
                        int NumeroLinea = 1 + (DTDetalleOC.Rows.Count);
                        decimal cantidadIngresada = decimal.Parse(Cantidad, CultureInfo.InvariantCulture.NumberFormat);
                        //e_OPESALPreDetalleSolicitudArticulo detalleArticulo = n_OPESALPreDetalleSolicitud.GetDetallesArticuloPorIdInterno(wms.getIdCompania(UsrLogged.IdUsuario), idArticuloInterno, cantidadIngresada);//Se obtienen los detalles del idInterno articulo seleleccionado
                        //if (EsValidoElArticuloAInsertarEnDetalleSolicitud(cantidadIngresada, detalleArticulo))//Valida que el artículo a agregar sea correcto 
                        DTDetalleOC.Rows.Add(NumeroLinea, idArticuloInterno, NombreArticuloInterno, Cantidad);   //, detalleArticulo.Unidad_Medida, detalleArticulo.UnidadesAlistoDetalle);
                        LimpiarDetalleOC();
                        CargarDetalleOC("", true);
                        Mensaje("ok", "El artículo se ingresó correctamente", "");
                        btnAgregar.Enabled = true;
                        //lbUnidadMedida.Text = "";

                    }
                    else
                    {
                        Mensaje("error", "El artículo ya existe", "");
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", ex.Message, "");
            }

        }

        private void LimpiarDetalleOC()
        {
            //txtIdPreLineaDetalleSolicitud.Text = "";
            ddlidArticuloInterno.SelectedValue = "--Seleccionar--";
            txtCantidadxRecibir.Text = "";

            Button3.Visible = true;
            Button4.Visible = false;
            //btnEliminarArticulo.Visible = false;
            //Label30.Visible = false;
        }

        private void CargarDetalleOC(string buscar, bool pestana)
        {
            try
            {
                RadGridOPEINGDetalleOrdenCompra.DataSource = DTDetalleOC;
                if (pestana)
                {
                    RadGridOPEINGDetalleOrdenCompra.DataBind();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private bool EsValidaCantidadArticulo(string cantidadIngresada)
        {
            try
            {
                float cantidad = float.Parse(cantidadIngresada, CultureInfo.InvariantCulture.NumberFormat);
                if (cantidad > 0)
                {
                    txtCantidadxRecibir.Text = "";
                    return true;
                }
                else
                {
                    txtCantidadxRecibir.Text = "";
                    Mensaje("info", "Por favor ingrese una cantidad mayor a cero", "");
                    return false;
                }
            }
            catch (Exception)
            {
                Mensaje("info", "Por favor ingrese una cantidad válida, la misma no debe contener caracteres especiales, ni letras", "");
                txtCantidadxRecibir.Text = "";
                return false;
            }
        } //Verifica si la cantidad ingresada es posible convertirla a decimal

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.CargarDatos(Panel, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            StringBuilder ValuesSQL = new StringBuilder();
            string mensaje = "0";

            n_DetalleOrdenCompra ProcesaOC = new n_DetalleOrdenCompra();
            mensaje = ProcesaOC.InsertarDetalleOC(DTDetalleOC,
                                                  Int64.Parse(ddlIdProveedor.SelectedValue),
                                                  dtpFechaCreacion.SelectedDate.Value,
                                                  txtFechaDespacho.SelectedDate.Value,
                                                  txtNombre.Text,
                                                  txmComentario.Text,
                                                  txtIdCompania.Text,
                                                  int.Parse(UsrLogged.IdUsuario));

            Mensaje("ok", mensaje, "");
            limpia();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblMaestroOrdenesCompra(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

            if (Msj[0] == "ok")
            {
                CargarDDLS();
            }
        }


        protected void BtnEditar4_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblCalendarioAnden(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void Btnlimpiar1_Click(object sender, EventArgs e)
        {
            limpia();
        }

        protected void limpia()
        {
            //ddlIdProveedor.SelectedIndex = 0;
            //dtpFechaCreacion.Clear();
            //txtFechaDespacho.Clear();
            //txmComentario.Text = "";
            //txtNombre.Text = "";
            //ddlidArticuloInterno.SelectedIndex = 0;
            //txtCantidadxRecibir.Text = "";
            //DTDetalleOC.Clear();
            //RadGridOPEINGDetalleOrdenCompra.DataSource = "";
            //RadGridOPEINGDetalleOrdenCompra.Rebind();
            //Button3.Visible = true;  //--> botón agregar.
            //Button4.Visible = false;  //--> botón editar.
            //BtnElimina.Visible = false;
            //Label30.Visible = false;  //--> separador.
        }
        protected void Btnlimpiar2_Click(object sender, EventArgs e)
        {
            ddlidArticuloInterno.SelectedIndex = 0;
            txtCantidadxRecibir.Text = "";
        }

        protected void Btnlimpiar3_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            n_SmartMaintenance.LimpiarForm(Panel);
        }

        protected void Btnlimpiar4_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            n_SmartMaintenance.LimpiarForm(Panel);
        }


        protected void BtnElimina_Click(object sender, EventArgs e)
        {
            try
            {
                string IdDetalleOC = TxtNumFilaGrid.Text.ToString().Trim();

                foreach (DataRow DRDetalleOC in DTDetalleOC.Rows)
                {
                    if (DRDetalleOC["IdDetalleOC"].ToString() == IdDetalleOC)
                    {
                        DRDetalleOC.Delete();
                        break;
                    }
                }

                int NumeroLinea = 1;

                foreach (DataRow DRDetalleOC in DTDetalleOC.Rows)
                {
                    DRDetalleOC["IdDetalleOC"] = NumeroLinea;
                    NumeroLinea++;
                }

                if (DTDetalleOC.Rows.Count == 0)
                {
                    btnAgregar.Enabled = false;
                }

                LimpiarDetalleOC();
                CargarDetalleOC("", true);
                Mensaje("ok", "El artículo se eliminó correctamente", "");
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }
    }
}