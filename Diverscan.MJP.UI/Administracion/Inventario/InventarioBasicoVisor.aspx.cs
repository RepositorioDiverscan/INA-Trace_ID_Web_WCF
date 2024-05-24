using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.InventarioBasico;
using Diverscan.MJP.Negocio.InventarioBasico;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Administracion.Inventario
{
    public partial class InventarioBasicoVisor : Page
    {
        //Instancia de la variable global de los usuario
        e_Usuario UsrLogged = new e_Usuario();

        DataTable dt;

        //Método de carga de la página
        protected void Page_Load(object sender, EventArgs e)
        {
            //Obtener el usuario que inicio sesión en el proyecto
            UsrLogged = (e_Usuario)Session["USUARIO"];

            //En caso de que sea nulo o se expire la sesión le pide las credenciales
            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }

            //Agregar la fecha actual a los 3 calendarios de la página
            if (!IsPostBack)
            {
                _rdpFechaPorAplicar.SelectedDate = DateTime.Now;
                _rdpFechaInicio.SelectedDate = DateTime.Now;
                _rdpFechaFin.SelectedDate = DateTime.Now;
                cargarCombos();
            }
        }


        //Método para iniciar el panel
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel1 });
                }
            }
        }


        //Método del botón de agregar, para crear un nuevo Inventario
        protected void _btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                //Instanciar variables a utilizar
                int idBodega = UsrLogged.IdBodega; //Obtener la bodega de la sesión del usuario
                var today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day); //Obtener la fecha de hoy
                Agregar(idBodega); //Invocar al método de agregar inventario
                buscar(today, today, idBodega); //Actualizar la tabla con las fechas de hoy
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ha ocurrido un error. Mensaje del error: " + ex.Message, "");
                return;
            }
        }


        //Método de agregar un inventario a la BD
        private void Agregar(int idBodega)
        {
            //Comprobar que los campos no estén vacios
            if (string.IsNullOrEmpty(txtFamilia.Text)) //Nombre
            {
                Mensaje("info", "Debe ingresar el numero de Familia", "");
                return;
            }
            else if (lstTipoInventario.SelectedIndex == 0) //TipoInventario
            {
                Mensaje("info", "Debe seleccionar un Tipo de inventario", "");
                return;
            }
            else if (lstUsarios.SelectedIndex == 0) //TipoInventario
            {
                Mensaje("info", "Debe seleccionar un Usuario", "");
                return;
            }
            else if (txtFamilia.Text.StartsWith("5") && !(lstTipoInventario.SelectedValue.Equals("2")))
            {
                Mensaje("info", "No existe esta categoría para esta familia", "");
                return;
            }
            else
            {
                //Comprobar que exista una fecha por aplicar, en caso contrario
                DateTime fechaAplicar = _rdpFechaPorAplicar.SelectedDate ?? DateTime.Now;

                //Invocar la capa lógica para insertar el inventario
                String resultado = N_InventarioBasico.InsertLogAjusteDeInventario(new InventarioBasicoRecord(txtFamilia.Text, Convert.ToInt16(lstTipoInventario.SelectedValue), Convert.ToInt32(lstUsarios.SelectedValue), fechaAplicar), idBodega);

                //Comprobar el valor del resultado 
                if (resultado == "Insertado")
                {
                    Mensaje("ok", "Inventario agregado correctamente", "");

                    //Limpiar los campos del formulario
                    txtFamilia.Text = "";
                    lstTipoInventario.SelectedIndex = 0;
                    lstUsarios.SelectedIndex = 0;
                }
                else
                {
                    Mensaje("error", resultado, "");
                }
            }
        }


        //Método del botón de buscar, para buscar Inventarios entre fechas
        protected void _btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //Obtener la bodega del usuario y las fechas que seleccionó en los campos de consulta
                int idBodega = UsrLogged.IdBodega;
                DateTime fechaInicio = _rdpFechaInicio.SelectedDate ?? DateTime.Now;
                DateTime fechaFin = _rdpFechaFin.SelectedDate ?? DateTime.Now;
                buscar(fechaInicio, fechaFin, idBodega); //Invocar el método de busqueda 
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ha ocurrido un error, descripción del error: " + ex.Message, "");
                return;
            }
        }

        protected void txtFamilia_TextChanged(object sender, EventArgs e)
        {
            lstTipoInventario.Items.Clear();

            lstTipoInventario.Enabled = true;
            cargalstTipoInventario();
        }


        //Método para buscar los inventarios entre fechas seleccionadas
        private void buscar(DateTime fechaInicio, DateTime fechaFin, int idbodega)
        {
            //Obtener la lista de inventarios de la BD
            var inventarios = N_InventarioBasico.ObtenerTodosInventarioBasicoRecords(fechaInicio, fechaFin, idbodega);
            _inventarioBasicoRecords = inventarios; //Enviar los datos al método de extraer la lista
            //Agregar a la Tabla la información y que aparezca en pantalla
            _rgInventarioBasicos.DataSource = _inventarioBasicoRecords;
            _rgInventarioBasicos.DataBind();
            Mensaje("ok", "Se han encontrado registros", "");
        }


        //Método para obtener los inventarios de la lista del método _inventarioBasicoRecords
        protected void _rgInventarioBasicos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            _rgInventarioBasicos.DataSource = _inventarioBasicoRecords;
        }


        //Método para obtener una vista de la lista que se obtiene de los datos de los Inventarios
        private List<InventarioBasicoRecord> _inventarioBasicoRecords
        {
            get
            {
                //Obtener una vista de los inventarios
                var data = ViewState["InventarioBasicoRecords"] as List<InventarioBasicoRecord>;

                //En caso de que la data sea nula se crea una lista vacia
                if (data == null)
                {
                    data = new List<InventarioBasicoRecord>();
                    ViewState["InventarioBasicoRecords"] = data;
                }

                return data; //Retornar los datos
            }
            set
            {
                ViewState["InventarioBasicoRecords"] = value; //Agregar los valores 
            }
        }


        public void cargarCombos()
        {
            int idBodega = UsrLogged.IdBodega;

            dt = N_InventarioBasico.ObtenerUsuarios(idBodega);

            lstUsarios.DataSource = dt;
            lstUsarios.DataTextField = "Nombre"; // El nombre de la columna que será mostrada en el ComboBox
            lstUsarios.DataValueField = "IdUsuario"; // El valor asociado a cada elemento del ComboBox
            lstUsarios.DataBind();

            lstTipoInventario.Items.Insert(0, new ListItem("Selecciona una opción", ""));

            lstUsarios.Items.Insert(0, new ListItem("Selecciona una opción", ""));
        }


        //Método que crea las notificaciones de la pantalla, sea error, información o éxito
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

        private void cargalstTipoInventario()
        {
            lstTipoInventario.Items.Clear();

            lstTipoInventario.Items.Insert(0, new ListItem("Selecciona una opción", ""));

            if (txtFamilia.Text.StartsWith("5"))
            {
                lstTipoInventario.Items.Add(new ListItem("EQUIPO", "2"));
                lstTipoInventario.SelectedValue = "2";
                lstTipoInventario.Enabled = false;
            }
            else
            {
                lstTipoInventario.Items.Add(new ListItem("MATERIALES", "1"));
                lstTipoInventario.Items.Add(new ListItem("MATERIAL DEVOLUTIVO", "3"));
                lstTipoInventario.Items.Add(new ListItem("MATERIAL PERECEDERO", "4"));
            }
        }
    }
}