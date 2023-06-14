using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;

namespace Diverscan.MJP.UI.Administracion.Inventario
{
    public partial class InventarioCiclicoVisor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadCategoriaArticulo();
                _rdpFechaInicio.SelectedDate = DateTime.Now;
                _rdpFechaFinal.SelectedDate = DateTime.Now;
                _rdpDiaEspecifico.SelectedDate = DateTime.Now;
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

        private void loadCategoriaArticulo()
        {
            var _e_CategoriaProductoData = N_CategoriaArticulo.GetCategoriaArticulo();
            _ddlCategoriaArticulo.DataTextField = "Nombre";
            _ddlCategoriaArticulo.DataValueField = "IdCategoriaArticulo";
            _ddlCategoriaArticulo.DataSource = _e_CategoriaProductoData;
            _ddlCategoriaArticulo.DataBind();
        }

        protected void _btnDiasSemanaAgregar_Click(object sender, EventArgs e)
        {
            int _idCategoriaArticulo = int.Parse(_ddlCategoriaArticulo.SelectedValue);

            var fechaInicio = _rdpFechaInicio.SelectedDate ?? DateTime.Now;
            var fechaFinal = _rdpFechaFinal.SelectedDate ?? DateTime.Now;
            List<e_InventarioCiclicoRecord> listHorarios = new List<e_InventarioCiclicoRecord>();

            for (DateTime date = fechaInicio; date.CompareTo(fechaFinal) < 1; date = date.AddDays(1.0))
            {
                System.DayOfWeek day = date.DayOfWeek;
                if (_cbLunes.Checked && day == DayOfWeek.Monday)
                {
                    listHorarios.Add(new e_InventarioCiclicoRecord(_idCategoriaArticulo, date));
                }
                if (_cbMartes.Checked && day == DayOfWeek.Tuesday)
                {
                    listHorarios.Add(new e_InventarioCiclicoRecord(_idCategoriaArticulo, date));
                }
                if (_cbMiercoles.Checked && day == DayOfWeek.Wednesday)
                {
                    listHorarios.Add(new e_InventarioCiclicoRecord(_idCategoriaArticulo, date));
                }
                if (_cbJueves.Checked && day == DayOfWeek.Thursday)
                {
                    listHorarios.Add(new e_InventarioCiclicoRecord(_idCategoriaArticulo, date));
                }
                if (_cbViernes.Checked && day == DayOfWeek.Friday)
                {
                    listHorarios.Add(new e_InventarioCiclicoRecord(_idCategoriaArticulo, date));
                }
                if (_cbSabado.Checked && day == DayOfWeek.Saturday)
                {
                    listHorarios.Add(new e_InventarioCiclicoRecord(_idCategoriaArticulo, date));
                }
                if (_cbDomingo.Checked && day == DayOfWeek.Sunday)
                {
                    listHorarios.Add(new e_InventarioCiclicoRecord(_idCategoriaArticulo, date));
                }
            }
            if(listHorarios.Count>0)
                N_InventarioCiclico.InsertInventarioCiclico(listHorarios);
            buscarInventariosCiclicos(fechaInicio, fechaFinal);
        }

        protected void _btnDiaEspecificoAgregar_Click(object sender, EventArgs e)
        {
            int _idCategoriaArticulo = int.Parse(_ddlCategoriaArticulo.SelectedValue);
            var fechaDiaEspecifico = _rdpDiaEspecifico.SelectedDate ?? DateTime.Now;

            var inventario = new e_InventarioCiclicoRecord(_idCategoriaArticulo, fechaDiaEspecifico);
            N_InventarioCiclico.InsertInventarioCiclico(inventario);
            buscarInventariosCiclicos(fechaDiaEspecifico, fechaDiaEspecifico);
        }

        protected void _rblPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = _rblPanel.SelectedValue;
            if (value == "Dias Semana")
            {
                PanelDiaEspecifico.Visible = false;
                PanelDiasSemana.Visible = true;
            }
            if (value == "Dia Especifico")
            {
                PanelDiaEspecifico.Visible = true;
                PanelDiasSemana.Visible = false;
            }                     
        }

        private void buscarInventariosCiclicos(DateTime fechaInicio, DateTime fechaFin)
        {
            var inventarios = N_InventarioCiclico.GetInventariosCiclicos(fechaInicio, fechaFin);
           _inventarioCiclicoData = inventarios;
           RGInventariosCiclicos.DataSource = _inventarioCiclicoData;
           RGInventariosCiclicos.DataBind();
        }

        protected void RGInventariosCiclicos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGInventariosCiclicos.DataSource = _inventarioCiclicoData;
        }

        private List<e_InventarioCiclicoRecord> _inventarioCiclicoData
        {
            get
            {
                var data = ViewState["InventarioCiclicoData"] as List<e_InventarioCiclicoRecord>;
                if (data == null)
                {
                    data = new List<e_InventarioCiclicoRecord>();
                    ViewState["InventarioCiclicoData"] = data;
                }
                return data;
            }
            set
            {
                ViewState["InventarioCiclicoData"] = value;
            }
        }

        protected void _btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime fechaInicio = _rdpB_FechaInicio.SelectedDate ?? DateTime.Now;
            DateTime fechaFin = _rdpB_FechaFinal.SelectedDate ?? DateTime.Now;
            buscarInventariosCiclicos(fechaInicio, fechaFin);
        }
    }

    public class HorarioInventarioFisico
    {
        int _idCategoriaArticulo;
        DateTime _fechaAplicar;

        public HorarioInventarioFisico(int iDCategoriaArticulo, DateTime fechaAplicar)
        {
            _idCategoriaArticulo = iDCategoriaArticulo;
            _fechaAplicar = fechaAplicar;
        }
        public int IdCategoriaArticulo()
        {
            return _idCategoriaArticulo;
        }
        public DateTime FechaAplicar()
        {
            return _fechaAplicar;
        }
    }
}