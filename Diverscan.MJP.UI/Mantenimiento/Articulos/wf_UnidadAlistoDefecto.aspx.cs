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
using Diverscan.MJP.AccesoDatos.MaestroArticulo;

namespace Diverscan.MJP.UI.Mantenimiento.Articulos
{
    public partial class wf_UnidadAlistoDefecto : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        public int ToleranciaAgregar = 110;
        da_MaestroArticulo da_MaestroArticulo = new da_MaestroArticulo();

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    UsrLogged = (e_Usuario)Session["USUARIO"];

        //    if (UsrLogged == null)
        //    {
        //        Response.Redirect("~/Administracion/wf_Credenciales.aspx");
        //    }
        //    if (!IsPostBack)
        //    {
        //        CargarDDLS();
        //        cargarGridArticulosSinUnidadAlistoDefecto();
        //    }
        //}
        #region Web Form

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

        #endregion
        #region TabsControl

        #region ControlRadGrid

        /// <summary>
        /// Evento llamado desde el grida para cargar los datos.
        /// Esta opcion se coloca cuando se crea el grid en ASPX --> OnNeedDataSource="RadGrid1_NeedDataSource"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid DG = (RadGrid)sender;
            Control Parent = DG.Parent;
            n_SmartMaintenance.CargarGrid(Parent, UsrLogged.IdUsuario);
        }

        private void CargarDDLS()
        {
            try
            {
                string[] Msj = n_SmartMaintenance.CargarDDL(ddlidArticuloInterno, e_TablasBaseDatos.VistaArticulosInternos(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-MAN-DES-000004" + ex.Message, "");
            }
        }

        protected void ddlidArticuloInterno_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtGTINDefecto.Text = "";
            CargarUnidadAlistoDefecto("", true);
            CargarArticulo();
            CargarGTIN13();
        }

        protected void ddlGTIN13_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtGTINDefecto.Text = "";
            CargarUnidadAlistoDefecto("", true);
            CargarGTIN13();
        }

        /// <summary>
        /// Evento que se dispara cuando se hace click sobre el checkbox. El checbox se crea desde el inicio
        /// antes de asignarle el datasource al RadGrid. Tiene que estar dentro de le etiqueta columns, y
        /// el AutoPostBack debe ser True, de lo contrario no dispara el evento.
        ///     <ItemTemplate>
        ///         <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="ToggleRowSelection"
        ///         AutoPostBack="True" />
        ///     </ItemTemplate>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-MAN-DES-000005" + ex.Message, "");
            }
        }

        /// <summary>
        /// Evento que se dispara cuando se hace click sobre el encabezado de la columna tipo checkbox. 
        /// El checbox se crea desde el inicio en conjunto con la etiqueta antetior.
        /// Antes de asignarle el datasource al RadGrid. Tiene que estar dentro de le etiqueta columns, y
        /// el AutoPostBack debe ser True, de lo contrario no dispara el evento.
        ///     <HeaderTemplate>
        ///         <asp:CheckBox ID="headerChkbox" runat="server" OnCheckedChanged="ToggleSelectedState"
        ///         AutoPostBack="True" />
        ///     </HeaderTemplate>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            try
            {
                RadGrid RG = (RadGrid)sender;
                if (RG.ID == "RadGrid1")
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
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-MAN-DES-000006" + ex.Message, "");
            }
        }

        /// <summary>
        /// Este metodo se ejecuta cada vez que se presiona una fila.
        /// Para que funcion tiene que estar declarado cuando se crea el RadGrid 
        /// asi -->  onitemcommand="RadGrid1_ItemCommand" y PostaBack activo 
        /// asi -->  EnablePostBackOnRowClick="true"
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void RadGrid_ItemCommand(object source, GridCommandEventArgs e)
        {
            CheckBox cb = new CheckBox();
            switch (e.CommandName)
            {
                case "RowClick":
                    break;
                default:
                    break;
            }
        }

        #endregion //ControlRadGrid

        #region EventosFrontEnd

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            EnviarArticuloDefecto();
            CargarDDLS();
            LimpiarUnidadAlistosDefecto();
            CargarArticulo();
            CargarGTIN13();
            CargarUnidadAlistoDefecto("", true);
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            CargarDDLS();
            LimpiarUnidadAlistosDefecto();
            CargarArticulo();
            CargarGTIN13();
            CargarUnidadAlistoDefecto("", true);
        }

        #endregion //EventosFrontEnd

        #endregion //TabsControl

        #region UnidadAlistoDefecto

        private void EnviarArticuloDefecto()
        {
            try
            {
                bool esGTIN13 = true;
                string GTIN14 = "";

                string idArticulo = ddlGTIN13.SelectedValue;
                string _GTIN = txtGTINDefecto.Text;
                //int CaracterGTIN = _GTIN.Length;

                if (_GTIN.Length >= 14)
                {
                    esGTIN13 = false;
                    GTIN14 = _GTIN;
                }

                string SQL = "EXEC SP_ActualizarListaUnidadAlistoDefecto '" + idArticulo + "', '" + GTIN14 + "', '" + esGTIN13 + "'";
                bool Resultado = n_ConsultaDummy.PushData(SQL, UsrLogged.IdUsuario);
                if (Resultado)
                {
                    Mensaje("ok", "Transacción Exitosa", "");
                }
                else
                {
                    Mensaje("error", "Error en la transacción", "");
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ha ocurrido un Error, " + ex.Message, "");
            }
        }

        private void CargarArticulo()
        {
            try
            {
                string idInterno = ddlidArticuloInterno.SelectedValue;

                if (!idInterno.Equals("--Seleccionar--"))
                {
                    string SQL = "EXEC SP_ObtenerGTINXidInterno '" + idInterno + "'";
                    DataSet DSDatos = new DataSet();

                    DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);
                    ddlGTIN13.DataSource = DSDatos;
                    ddlGTIN13.DataTextField = "GTIN";
                    ddlGTIN13.DataValueField = "idArticulo";
                    ddlGTIN13.DataBind();
                    ddlGTIN13.Items.Insert(0, new ListItem("--Seleccionar--"));

                    ddlGTIN13.Enabled = true;
                }
                else
                {
                    ddlGTIN13.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ha ocurrido un Error, " + ex.Message, "");
            }
        }

        private void CargarGTIN13()
        {
            try
            {
                string idArticulo = ddlGTIN13.SelectedValue;

                if (!idArticulo.Equals("--Seleccionar--"))
                {
                    CargarUnidadAlistoDefecto(idArticulo, true);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ha ocurrido un Error, " + ex.Message, "");
            }
        }

        private void CargarUnidadAlistoDefecto(string idArticulo, bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";

                SQL = "EXEC SP_ObtenerListaUnidadAlistoDefecto '" + idArticulo + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                string GTIN = "";

                if (DSDatos != null)
                {
                    if (DSDatos.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dssRow in DSDatos.Tables[0].Rows)
                        {
                            string Defecto = dssRow["Defecto"].ToString();

                            if (Defecto.Equals("1"))
                            {
                                GTIN = dssRow["GTIN"].ToString();
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(GTIN))
                {
                    txtGTINDefecto.Text = GTIN;
                }
                else
                {
                    txtGTINDefecto.Text = "";
                    btnAgregar.Enabled = false;
                }

                RadGridUnidadAlistoDefecto.DataSource = DSDatos;
                if (pestana)
                {
                    RadGridUnidadAlistoDefecto.DataBind();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private void LimpiarUnidadAlistosDefecto()
        {
            txtGTINDefecto.Text = "";
            ddlidArticuloInterno.SelectedValue = "--Seleccionar--";
            ddlGTIN13.SelectedValue = "--Seleccionar--";
        }

        protected void RadGridUnidadAlistoDefecto_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                //CargarUnidadAlistoDefecto(txtSearch.Text.ToString().Trim(), false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridUnidadAlistoDefecto_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    txtGTINDefecto.Text = item["GTIN"].Text.Replace("&nbsp;", "");

                    btnAgregar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        #endregion UnidadAlistoDefecto

        #region Grid Artículos Sin Unidad Alisto Defecto
        //private void cargarGridArticulosSinUnidadAlistoDefecto()
        //{
        //    try
        //    {
        //        var listaDatos = da_MaestroArticulo.GetArticuloSinUnidadAlistoDefecto();
        //        RadGridArticulosSinUnidadAlisto.DataSource = listaDatos;
        //        RadGridArticulosSinUnidadAlisto.DataBind();
        //    }
        //    catch (Exception)
        //    {
        //        Mensaje("error", "Problema al cargar GridArtículos Sin UAD", "");
        //    }
        //}

        //protected void RadGridArticulosSinUnidadAlisto_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    try
        //    {
        //        var listaDatos = da_MaestroArticulo.GetArticuloSinUnidadAlistoDefecto();
        //        RadGridArticulosSinUnidadAlisto.DataSource = listaDatos;
        //    }
        //    catch (Exception)
        //    {
        //        Mensaje("error", "Problema al páginar GridArtículos Sin UAD", "");
        //    }
        //}

        #endregion


    }
}