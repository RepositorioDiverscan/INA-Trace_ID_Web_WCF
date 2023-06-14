using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Diverscan.MJP.UI.Inventario
{
    public partial class CategoriaProductoVisor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showE_CategoriaProductos();
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

        protected void _btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(_txtNombre.Text))
                {
                    var nombre = _txtNombre.Text;
                    var descripcion = _txtDescripcion.Text;
                    e_CategoriaArticulo e_categoriaProducto = new e_CategoriaArticulo(nombre);
                    N_CategoriaArticulo.InsertCategoriaArticulo(e_categoriaProducto);
                    showE_CategoriaProductos();
                }
                else
                {
                    Mensaje("info", "Debe de ingresar un nombre.", "");
                }
            }
            catch(Exception ex)
            {
                Mensaje("error", "No se puede ingresar el dato. " + ex.Message, "");
            }
        }

        private void showE_CategoriaProductos()
        {
            _e_CategoriaProductoData = N_CategoriaArticulo.GetCategoriaArticulo();
            RGCategoriaProducto.DataSource = _e_CategoriaProductoData;
            RGCategoriaProducto.DataBind();
        }

        private List<e_CategoriaArticulo> _e_CategoriaProductoData
        {
            get
            {
                var data = ViewState["e_CategoriaProductoData"] as List<e_CategoriaArticulo>;
                if (data == null)
                {
                    data = new List<e_CategoriaArticulo>();
                    ViewState["e_CategoriaProductoData"] = data;
                }
                return data;
            }
            set
            {
                ViewState["e_CategoriaProductoData"] = value;
            }
        }

        private e_CategoriaArticulo _e_CategoriaProductoSelected
        {
            get
            {
                var data = ViewState["_e_CategoriaProductoSelected"] as e_CategoriaArticulo;
                if (data == null)
                {
                    data = new e_CategoriaArticulo("");
                    ViewState["_e_CategoriaProductoSelected"] = data;
                }
                return data;
            }
            set
            {
                ViewState["_e_CategoriaProductoSelected"] = value;
            }
        }

        protected void RGCategoriaProducto_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGCategoriaProducto.DataSource = _e_CategoriaProductoData;
        }


        protected void RadGrid_ItemCommand(object source, GridCommandEventArgs e)
        {
            CheckBox cb = new CheckBox();
            switch (e.CommandName)
            {
                case "RowClick":
                    {
                        var logAjusteInventarioRecord = _e_CategoriaProductoData[e.Item.ItemIndex];
                        if (logAjusteInventarioRecord != null)
                        {
                            _e_CategoriaProductoSelected = logAjusteInventarioRecord;
                            _txtNombre.Text = logAjusteInventarioRecord.Nombre;                            
                        }

                        break;
                    }
                default:
                    break;
            }

        }

        protected void _btnActualizar_Click(object sender, EventArgs e)
        {
            if (_e_CategoriaProductoSelected.IdCategoriaArticulo > 0)
            {
                _e_CategoriaProductoSelected.Nombre = _txtNombre.Text;                
                N_CategoriaArticulo.UpdateCategoriaArticulo(_e_CategoriaProductoSelected);
                showE_CategoriaProductos();
            }
        }
    }
}