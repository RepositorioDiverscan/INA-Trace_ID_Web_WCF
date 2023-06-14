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
using Diverscan.MJP.Negocio.MotorDecisiones;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.Negocio.LogicaWMS;
using Diverscan.MJP.Utilidades;
using Diverscan.Visitas.Utilidades;
using TRACEID.WCF;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Telerik.Web.UI.PersistenceFramework;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Drawing;
using System.Threading;


namespace Diverscan.MJP.UI.Operaciones.Consultas
{
    public partial class wf_Consultas : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        public int ToleranciaAgregar = 110;
        string Pagina = "";
        string Ruta = "~/Images/Colores/";  // Ruta para el objeto Imagen.
        string Rutapp = "D:\\Desarrollo\\TRACE-ID\\Diverscan.MJP.UI\\Images\\Colores\\";  // Ruta para el Objeto Bitmap.

        protected void Page_Load(object sender, EventArgs e)
        {
            Pagina = Page.AppRelativeVirtualPath.ToString();
            UsrLogged = (e_Usuario)Session["USUARIO"];
            CargarAccionesPagina(Pagina);
            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {
                CargarDDLS();
            }
        
        }

        private void CargarDDLS()
        {
            try
            {
                string[] Msj = n_SmartMaintenance.CargarDDL(ddlIDUSUARIO, e_TablasBaseDatos.VistaUsuariosSinAdmin(), UsrLogged.IdUsuario, true); // ddlIDUSUARIO
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-CON-000001" + ex.Message, "");
            }
          
        }

        private void CargarAccionesPagina(string Pagina)
        {
            try
            {
                n_MotorDecisiones.Metodos MD = new n_MotorDecisiones.Metodos();
                List<e_AccionFlujo> Acciones = MD.ObtenerAcciones(Pagina);
                foreach (Control c in Panel1.Controls)
                {
                    if (c.GetType().ToString().Equals("System.Web.UI.UpdatePanel"))
                    {
                        foreach (Control cc in c.Controls)
                        {
                            foreach (Control ccc in cc.Controls)
                            {
                                if (ccc is Button)
                                {
                                    if (Acciones.Exists(x => x.ObjetoFuente == ccc.ID))
                                    {
                                        Button Btn = (Button)ccc;
                                        Btn.Visible = true;
                                        Btn.Text = Acciones.Find((x) => x.ObjetoFuente == ccc.ID).Nombre;
                                        Btn.Click += new EventHandler(Accion);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-CON-000002" + ex.Message, "");
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

        #region TabsControl

        #endregion //ControlRadGrid

        public void Accion(object sender, EventArgs e)
        {
            string Pagina = "~/HH/Operaciones/Salidas/wf_CrearAlisto.aspx";
            string CodLeido = txtConsulta.Text;

           //TraceID.(2016). Operaciones/Consultas/wf_Consultas.En Trace ID Codigos documentados(5).Costa Rica:Grupo Diverscan. 

            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, Ctr.ID.ToString());
            txmRespuesta.Text = resultado;
            txmRespuesta.Text = txmRespuesta.Text.Replace("\n", Environment.NewLine);
        }


        protected void btnConsulta_Click(object sender, EventArgs e)
        {
           if (FUpld.HasFile) 
              ImgCargarColor.ImageUrl = Ruta + FUpld.FileName;

            //string Pagina = "~/HH/Operaciones/Salidas/wf_CrearAlisto.aspx";
            //string CodLeido = txtConsulta.Text;

            HacerConsulta();
        }

        private void CargarHoras()
        {
            string idMetodoAccion = "28";
            e_TareaUsuario TareaUsuario = new e_TareaUsuario();
            e_UsuarioProductivo UsuarioProductivo = new e_UsuarioProductivo();
            List<e_UsuarioProductivo> UsuariosProductivosOcupados = new List<e_UsuarioProductivo>();
            List<e_TareaUsuario> TareasUsuarios = new List<e_TareaUsuario>();
            TareasUsuarios = n_WMS.ObtenerTareasUsuarios(UsrLogged.IdUsuario, idMetodoAccion);
            e_UsuarioProductivo UP = new e_UsuarioProductivo();
            List<e_TareaUsuario> TareasAsignadas = new List<e_TareaUsuario>();
            e_TareaUsuario TareaAsignada = new e_TareaUsuario();
            e_UsuarioProductivo UPR = new e_UsuarioProductivo();
            List<string> usuarios = new List<string>();
            List<e_TareaUsuario> TareasUsuariosAsignada = new List<e_TareaUsuario>();
            foreach (e_TareaUsuario TU in TareasUsuarios)
            {
                foreach (e_UsuarioProductivo _UP in TU.UsuariosProductivos)
                {
                    bool Ocupado = false;
                    bool EstaLaTareaAsignada = false;
                    foreach (e_UsuarioProductivo up in UsuariosProductivosOcupados)
                    {
                        if (up != null)
                        {
                            if(up.TareasAsignadas != null)
                            foreach (e_TareaUsuario ta in up.TareasAsignadas)
                            {
                                if (TU.idTarea == ta.idTarea)
                                {
                                    EstaLaTareaAsignada = true;
                                }
                            }
                        }
                    }
                    if (EstaLaTareaAsignada == false)
                    {
                        UP = new e_UsuarioProductivo();
                        UP = UsuariosProductivosOcupados.FindLast(x => x.idUsuario == _UP.idUsuario);
                        if (UP != null)
                        {
                            Ocupado = UP.Ocupado;
                        }
                        else
                        {
                            UP = _UP;
                        }
                        /// Si las horas Disponibles es mayor al Porcentaje de Desecho la tarea debe hacerse aunque se paguen horas extra.
                        if (/*Esta*/Ocupado == false && UP.idUsuario == _UP.idUsuario)
                        {
                            decimal HorasDisponibles = UP.HHDisponiblesParaTarea;   // 2
                            double PorcentajeDesperdicio = 0.05;                    // 5%
                            string idUsuario = UP.idUsuario;                        // 0
                            string idRegistro = TU.idRegistro;                      // 3114
                            string idTarea = TU.idTarea;                            // Alisto-3114
                            decimal TiempoNecesario = TU.TiempoEstimado;            // 2.56

                            if (double.Parse(HorasDisponibles.ToString()) > double.Parse(HorasDisponibles.ToString()) * PorcentajeDesperdicio)
                            {
                                decimal HorasATrabajar = TiempoNecesario;
                                decimal HorasExtraNecesarias = HorasATrabajar - HorasDisponibles;
                                if (HorasExtraNecesarias < 0 /*HorasExtraNecesarias es Negativo*/)
                                {
                                    HorasExtraNecesarias = 0;
                                }
                                else
                                {
                                    Ocupado = true;
                                }

                                HorasDisponibles = HorasDisponibles + HorasExtraNecesarias - HorasATrabajar;
                                /*Fin de los calculos */
                                UsuarioProductivo = new e_UsuarioProductivo();
                                TareaAsignada = new e_TareaUsuario();
                                TareasAsignadas = new List<e_TareaUsuario>();
                                UsuarioProductivo.idUsuario = idUsuario;
                                UsuarioProductivo.Ocupado = Ocupado;
                                UsuarioProductivo.HHDisponiblesParaTarea = HorasDisponibles;
                                UsuarioProductivo.HorasExtraNecesarias = HorasExtraNecesarias;
                                TareaAsignada = TU;

                                //TraceID.(2016). Operaciones/Consultas/wf_Consultas.En Trace ID Codigos documentados(6).Costa Rica:Grupo Diverscan.       
                         
                                TareasAsignadas.Add(TareaAsignada);
                                UsuarioProductivo.TareasAsignadas = TareasAsignadas;
                                UsuariosProductivosOcupados.Add(UsuarioProductivo);
                            }
                            else
                            {
                                Ocupado = true;
                            }
                        }
                    }
                    else
                    {
                        //break; // saltar siguiente tarea;
                    }
                }
            }
            string up_idusuario = String.Empty;
            List<e_TareaUsuario> TareasParaUsuarios = new List<e_TareaUsuario>();
            e_TareaUsuario TareaParaUsuario = new e_TareaUsuario();
            e_UsuarioProductivo usr_Prod = new e_UsuarioProductivo();
            List<e_UsuarioProductivo> usr_Prods = new List<e_UsuarioProductivo>();
            foreach (e_UsuarioProductivo UsrProdOcupado in UsuariosProductivosOcupados)
            {
                foreach (e_TareaUsuario TareaUsrProdOcupado in UsrProdOcupado.TareasAsignadas)
                {
                    if (up_idusuario != UsrProdOcupado.idUsuario) // Cambio de usuarios
                    {
                        TareasParaUsuarios.Add(TareaParaUsuario);
                        TareasParaUsuarios = new List<e_TareaUsuario>();
                        TareaParaUsuario = new e_TareaUsuario();
                        up_idusuario = UsrProdOcupado.idUsuario;
                        TareaParaUsuario = TareaUsrProdOcupado;
                        TareasParaUsuarios.Add(TareaParaUsuario);
                        usr_Prod = UsrProdOcupado;
                        usr_Prod.TareasAsignadas = TareasParaUsuarios;
                        usr_Prods.Add(usr_Prod);
                    }
                    else
                    {
                        usr_Prod = UsrProdOcupado;
                        foreach (e_UsuarioProductivo _usrprod_ in usr_Prods)
                        {
                            if (_usrprod_.idUsuario == usr_Prod.idUsuario)
                            {
                                _usrprod_.HorasExtraNecesarias = usr_Prod.HorasExtraNecesarias;
                                _usrprod_.HHDisponiblesParaTarea = usr_Prod.HHDisponiblesParaTarea;
                            }
                        }
                        TareaParaUsuario = TareaUsrProdOcupado;
                        TareasParaUsuarios.Add(TareaParaUsuario);
                    }
                }
            }
            foreach (e_UsuarioProductivo usrProd in usr_Prods)
            {
                DataSet DS = new DataSet();
                string SQL = "select idtarea from MDTareasAsignadasUsuarios order by idtarea";
                DS = n_ConsultaDummy.GetDataSet(SQL, usrProd.idUsuario);
                List<e_Tarea> TaReAs = new List<e_Tarea>();
                e_Tarea TaReA = new e_Tarea();
                foreach(DataRow DR in DS.Tables[0].Rows)
                {
                    TaReA = new e_Tarea();
                    TaReA.idTarea = DR["idtarea"].ToString();
                    TaReAs.Add(TaReA);
                }
                foreach (e_TareaUsuario tareaUsr in usrProd.TareasAsignadas)
                {
                    var _id_tarea = String.Empty;
                    if(TaReAs.Count > 0)
                        try{ 
                            var _id_tarea_ = TaReAs.Find(x => x.idTarea == tareaUsr.idTarea);
                            if (_id_tarea_ != null) _id_tarea = _id_tarea_.idTarea;
                        } catch (Exception){}
                    if (tareaUsr.idTarea != _id_tarea)
                    {
                        string _idMetodoAccion = tareaUsr.idMetodoAccion;
                        string _idUsuario = usrProd.idUsuario;
                        string _HorasExtra_ = usrProd.HorasExtraNecesarias.ToString().Replace(',','.');
                        string _HorasDisponibles_ = usrProd.HHDisponiblesParaTarea.ToString().Replace(',', '.'); 
                        string _idTarea_ = tareaUsr.idTarea;
                        string _TiempoEstimadoHora_ = tareaUsr.TiempoEstimado.ToString().Replace(',', '.'); 
                        SQL = "insert into MDTareasAsignadasUsuarios ";
                        SQL += "(idMetodoAccion, idUsuario, HorasExtra, HorasDisponibles, idTarea,TiempoEstimadoHora ) values ";
                        SQL += "('" + _idMetodoAccion + "','" + _idUsuario + "','" + _HorasExtra_;
                        SQL += "','" + _HorasDisponibles_ + "','" + _idTarea_ + "','" + _TiempoEstimadoHora_ + "')";
                        n_ConsultaDummy.PushData(SQL, _idUsuario);
                    }
                }
            }
        }

        private void HacerConsulta()
        {
            if (txtConsulta.Text.Trim() != String.Empty)
            {
                string Pagina = "~/HH/Operaciones/Salidas/wf_CrearAlisto.aspx";
                string CodLeido = txtConsulta.Text;
                string resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "btnObtenerAlisto");
                txmRespuesta.Text += "\n";
                txmRespuesta.Text += "Usted: " + txtConsulta.Text + "\n";
                txmRespuesta.Text += "Alisia: " + resultado.ToString();
                txmRespuesta.Text = txmRespuesta.Text.Replace("\n", Environment.NewLine);
                try
                {
                    if (txtConsulta.Text.ToUpper().Substring(0, 6) == "PINTAR")
                    {
                    if (txtConsulta.Text.ToUpper().Substring(13) == "IMAGEN")
                    {
                        string Rutacolor = "";
                        if (FUpld.HasFile)
                        {
                            Rutacolor = Rutapp + FUpld.FileName;
                            Bitmap BmpPixel = new Bitmap(Rutacolor);     // se crea un objeto Bitmap a partir de un archivo de imagen o control imagen.
                            Color pixelColor = BmpPixel.GetPixel(0, 0); // obtengo el color del primer pixel del objeto Bitmap.
                            txmRespuesta.BackColor = pixelColor;       // al textbox de respuesta le pongo el color de fondo del pixel.
                        }
                        else
                          txmRespuesta.BackColor = txtConsulta.BackColor;
                    }
                    else
                    {
                        string[] respuestas = resultado.Split(' ');
                       string ColorHex = "#" + respuestas[9];  //[9]-> blanco; [156]->azul
                        txmRespuesta.BackColor = System.Drawing.ColorTranslator.FromHtml(ColorHex);
                    }
                        if (txtConsulta.Text.ToUpper().Substring(0, 6) == "PINTAR") txmRespuesta.Text = String.Empty;
                    }
                    txtConsulta.Text = String.Empty;
                }
            catch (Exception ex)
                {
                txmRespuesta.Text = ex.Message;
                    Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-CON-000003" + ex.Message, "");
                }
            }
        }
    }
}