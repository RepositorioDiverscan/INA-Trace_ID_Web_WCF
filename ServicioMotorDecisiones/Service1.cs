using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.Utilidades;
using Timer = System.Timers.Timer;
using System.Security.Cryptography;
using System.Reflection;
using System.Web;


namespace ServicioMotorDecisiones
{
    public partial class Service1 : ServiceBase
    {
        private const string ConnName = "MJPConnectionString";

        private string idUsuario = ConfigurationManager.AppSettings["idUsuario"];
        private string CollateBD = ConfigurationManager.AppSettings["CollateBD"];
        private string GLOidCompania = ConfigurationManager.AppSettings["idCompania"];
        private string RutaArchivos = ConfigurationManager.AppSettings["RutaArchivos"];

        private string LogIniciar = ConfigurationManager.AppSettings["LogIniciar"];
        private string LogInsert = ConfigurationManager.AppSettings["LogInsert"];
        private string LogUpdate = ConfigurationManager.AppSettings["LogUpdate"];
        private string LogTransaccion = ConfigurationManager.AppSettings["LogTransaccion"];

        private volatile Timer _timerInsertar;
        private volatile Timer _timerActualizar;
        private volatile Timer _timerTransaccional;

        //TraceID.(2016). ServicioMotorDecisiones/Service1.En Trace ID Codigos documentados(29).Costa Rica:Grupo Diverscan. 
        

        public Service1()
        {
            var clLog = new clErrores();
            try
            {
                InitializeComponent();
                ServiceName = ConfigurationManager.AppSettings["ServiceName"];
                EventLog.Source = "LOG_" + ServiceName;
                EventLog.Log = "Application";

                // Estos Indicadores fijados o no para manejar ese tipo concreto de evento. 
                // Se establece en true si usted lo necesita, false en caso contrario.
                CanHandlePowerEvent = true;
                CanHandleSessionChangeEvent = true;
                CanPauseAndContinue = true;
                CanShutdown = true;
                CanStop = true;

                var cl = new clErrores();
                //cl.escribirError("Method: Service1 -- ", "Se inicio el servicio correctamente.");
                cl.escribirLogSincro(LogIniciar, "Method: Service1: ", "Se inicio el servicio correctamente.");

            }
            catch (Exception ex)
            {
                clLog.escribirError("No se inicio el servicio correctamente. Codigo:TID-SMD-SV1-000001 ", ex.Message);
            }
        }

        /// Descripcion: La acción se dispara cuando el servicio windows es iniciado.
        protected override void OnStart(string[] args)
        {
            var clE = new clEncriptar();
            var cl = new clErrores();
            try
            {
                //TraceID.(2016). ServicioMotorDecisiones/Service1.En Trace ID Codigos documentados(30).Costa Rica:Grupo Diverscan. 

                var timeInsertar = Convert.ToDouble(ConfigurationManager.AppSettings["TimerInsertar"]);
                _timerInsertar = new Timer(timeInsertar) { AutoReset = true };
                _timerInsertar.Elapsed += TimerInsertar;
                _timerInsertar.Start();

                //cl.escribirError("INICIO TimerInsertar", "");

                //var timeActualizar = Convert.ToDouble(ConfigurationManager.AppSettings["TimerActualizar"]);
                //_timerActualizar = new Timer(timeActualizar) { AutoReset = true };
                //_timerActualizar.Elapsed += TimerActualizar;
                //_timerActualizar.Start();

                //cl.escribirError("INICIO TimerActualizar", "");

                //var timeTransaccional = Convert.ToDouble(ConfigurationManager.AppSettings["TimerTransaccional"]);
                //_timerTransaccional = new Timer(timeTransaccional) { AutoReset = true };
                //_timerTransaccional.Elapsed += TimerTransaccional;
                //_timerTransaccional.Start();

                //cl.escribirError("INICIO TimerTransaccional", "");

                base.OnStart(args);
            }
            catch (Exception ex)
            {
                
                cl.escribirError("Method: OnStart -- , Codigo:TID-SMD-SV1-000002" + ex.Message, ex.StackTrace);
            }
        }

        /// Descripcion: La acción se dispara cuando el servicio windows es detenido.
        protected override void OnStop()
        {
            var clE = new clEncriptar();
            try
            {
                //TraceID.(2016). ServicioMotorDecisiones/Service1.En Trace ID Codigos documentados(31).Costa Rica:Grupo Diverscan. 

                _timerInsertar.Stop();
                _timerInsertar = null;

                _timerActualizar.Stop();
                _timerActualizar = null;

                //_timerTransaccional.Stop();
                //_timerTransaccional = null;

                base.OnStop();
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError("Method: OnStop -- " + ex.Message, ex.StackTrace);
            }
            finally
            {
                _timerInsertar = null;
                _timerActualizar = null;
                //_timerTransaccional = null;
            }
        }

        #region Timer

        private void TimerInsertar(object sender, ElapsedEventArgs e)
        {
            var cl = new clErrores();
            try
            {
                _timerInsertar.Stop();
                _timerInsertar = null;

                Task tareaInsertar = Task.Factory.StartNew(() => Insertar());

            }
            catch (Exception ex)
            {
                cl.escribirError("Method: TimerInsertar --,Codigo:TID-SMD-SV1-000003 " + ex.Message, ex.StackTrace);
                if (ex is OutOfMemoryException)
                {
                    cl.escribirError("Method: TimerInsertar --,Codigo:TID-SMD-SV1-000004 " + ex.Message, ex.StackTrace);
                }
            }
        }

        private void TimerActualizar(object sender, ElapsedEventArgs e)
        {
            var cl = new clErrores();
            try
            {
                _timerActualizar.Stop();
                _timerActualizar = null;

                Task tareaActualizar = Task.Factory.StartNew(() => Actualizar());

            }
            catch (Exception ex)
            {
                cl.escribirError("Method: TimerActualizar --,Codigo:TID-SMD-SV1-000005 " + ex.Message, ex.StackTrace);
                if (ex is OutOfMemoryException)
                {
                    cl.escribirError("Method: TimerActualizar --,Codigo:TID-SMD-SV1-000006 " + ex.Message, ex.StackTrace);
                }
            }
        }

        private void TimerTransaccional(object sender, ElapsedEventArgs e)
        {
            var cl = new clErrores();
            try
            {
                _timerTransaccional.Stop();
                _timerTransaccional = null;

                Task tareaMonitoreo = Task.Factory.StartNew(() => Transaccional());

                //TraceID.(2016). ServicioMotorDecisiones/Service1.En Trace ID Codigos documentados(32).Costa Rica:Grupo Diverscan. 
            }
            catch (Exception ex)
            {
                cl.escribirError("Method: TimerTransaccional --,Codigo:TID-SMD-SV1-000007 " + ex.Message, ex.StackTrace);
                if (ex is OutOfMemoryException)
                {
                    cl.escribirError("Method: TimerTransaccional --,Codigo:TID-SMD-SV1-000008  " + ex.Message, ex.StackTrace);
                }
            }
        }

        #endregion

        #region Metodos

        /// <summary>
        /// INSERTA LOS NUEVOS REGISTROS DEL ERP AL WMS QUE SE ENCUENTREN EN LA TABLA DE TRACEID.dbo.MDReferencias CON ANTERIORIDAD
        /// </summary>

        private void Insertar()
        {
            var cl = new clErrores();
            string SQL = "";
            try
            {
                DataSet DSKey = new DataSet();
                //SQL = "SELECT * FROM " + e_BaseDatos.LinkServer() + "." + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMDReferencias() + " WHERE " + e_TblMDReferenciasFields.IdCompania() + " = '" + GLOidCompania + "' AND " + e_TblMDReferenciasFields.PrimaryKey() + " = 1";
                //SQL = "SELECT * FROM " + e_VistasSINC.VistaSINCReferencias() + " WHERE " + e_TblMDReferenciasFields.IdCompania() + " = '" + GLOidCompania + "' AND " + e_TblMDReferenciasFields.PrimaryKey() + " = '1'";
                SQL = "SELECT * FROM " + e_VistasSINC.VistaSINCReferencias() + " WHERE " + e_TblMDReferenciasFields.IdCompania() + " = '" + GLOidCompania + "' AND " + e_TblMDReferenciasFields.PrimaryKey() + " = '1' ORDER BY IdReferencia ASC";
                DSKey = n_ConsultaDummy.GetDataSet(SQL, idUsuario);

                DataSet DSnotKey = new DataSet();
                //SQL = "SELECT * FROM " + e_BaseDatos.LinkServer() + "." + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMDReferencias() + " WHERE " + e_TblMDReferenciasFields.IdCompania() + " = '" + GLOidCompania + "' AND " + e_TblMDReferenciasFields.PrimaryKey() + " = 0";
                SQL = "SELECT * FROM " + e_VistasSINC.VistaSINCReferencias() +
                      "  WHERE " + e_TblMDReferenciasFields.IdCompania() + " = '" + GLOidCompania + "'" +
                               " AND " + e_TblMDReferenciasFields.PrimaryKey() + " = '0'";
                DSnotKey = n_ConsultaDummy.GetDataSet(SQL, idUsuario);

                if (DSKey.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dsRow in DSKey.Tables[0].Rows)
                    {
                        string TablaDestino = dsRow[e_TblMDReferenciasFields.TablaDestino()].ToString();
                        string IdCampoDestino = dsRow[e_TblMDReferenciasFields.CampoDestino()].ToString();
                        string TablaOrigen = dsRow[e_TblMDReferenciasFields.TablaOrigen()].ToString();
                        string IdCampoOrigen = dsRow[e_TblMDReferenciasFields.CampoOrigen()].ToString();
                        string PrimaryKey = dsRow[e_TblMDReferenciasFields.PrimaryKey()].ToString();
                        string Envia = dsRow[e_TblMDReferenciasFields.Envia()].ToString();

                        DataSet DSDatos = new DataSet();

                        //OLD//SQL = "SELECT * FROM " + TablaOrigen + " WHERE CONVERT(NVARCHAR(50), " + IdCampoOrigen + ") NOT IN (SELECT " + e_TblMDRELkeysFields.IdOrigen() + " COLLATE " + CollateBD + " FROM " + e_BaseDatos.LinkServer() + "." + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMDRELkeys() + " WHERE " + e_TblMDRELkeysFields.CampoDestino() + " = '" + TablaDestino + "." + IdCampoDestino + "' AND " + e_TblMDRELkeysFields.CampoOrigen() + " = '" + TablaOrigen + "." + IdCampoOrigen + "')";
                        //TEST//SQL = "SELECT * FROM " + TablaOrigen + " WHERE CONVERT(NVARCHAR(50), " + IdCampoOrigen + ") NOT IN (SELECT " + e_TblMDRELkeysFields.IdOrigen() + " COLLATE " + CollateBD + " FROM " + e_VistasSINC.VistaSINCRELkeys() + " WHERE " + e_TblMDRELkeysFields.CampoDestino() + " = '" + TablaDestino + "." + IdCampoDestino + "' AND " + e_TblMDRELkeysFields.CampoOrigen() + " = '" + TablaOrigen + "." + IdCampoOrigen + "')";

                        SQL = "SELECT * FROM " + TablaOrigen + " WHERE CONVERT(NVARCHAR(50), " + IdCampoOrigen + ") NOT IN (SELECT " + e_TblMDRELkeysFields.IdOrigen() + " COLLATE " + CollateBD + " FROM " + e_VistasSINC.VistaSINCRELkeys() + " WHERE " + e_TblMDRELkeysFields.CampoOrigen() + " = '" + TablaOrigen + "." + IdCampoOrigen + "')";
                        

                        DSDatos = n_ConsultaDummy.GetDataSet(SQL, idUsuario);
                        if (DSDatos != null)
                        {
                            if (DSDatos.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dssRow in DSDatos.Tables[0].Rows)
                                {
                                    string IdOrigen = dssRow[IdCampoOrigen].ToString();

                                    DataRow[] foundRows = DSnotKey.Tables[0].Select(e_TblMDReferenciasFields.TablaDestino() + " = '" + TablaDestino + "' AND " + e_TblMDReferenciasFields.TablaOrigen() + " = '" + TablaOrigen + "' AND " + e_TblMDReferenciasFields.PrimaryKey() + " = 0");

                                    //string columnas = e_TblMDReferenciasFields.IdCompania() + "," + "idUsuario" + ",";
                                    //string datos = "'" + GLOidCompania + "','" + idUsuario + "',";

                                    string columnas = "";
                                    string datos = "";

                                    foreach (DataRow dssRowFiltro in foundRows)
                                    {
                                        string CampoDestino = dssRowFiltro[e_TblMDReferenciasFields.CampoDestino()].ToString();
                                        string CampoOrigen = dssRowFiltro[e_TblMDReferenciasFields.CampoOrigen()].ToString();

                                        columnas = columnas + CampoDestino + ",";

                                        DataRow[] DRDatos = DSDatos.Tables[0].Select(IdCampoOrigen + " = '" + IdOrigen + "'");
                                        foreach (DataRow DRDatosFiltro in DRDatos)
                                        {
                                            string valor = DRDatosFiltro[CampoOrigen].ToString();
                                            //valor = valor.Replace("'", "");
                                            valor = clUtilities.ConvertCaracteresInvalidos(valor);

                                            Double number = 0;
                                            DateTime Fecha = new DateTime();

                                            if (!TablaOrigen.Equals("Vista_ERP_PIZZA_Destinos"))
                                            {
                                                if (Double.TryParse(valor, out number))
                                                {
                                                    valor = number.ToString().Replace(",", ".");
                                                }
                                                else if (DateTime.TryParse(valor, out Fecha))
                                                {
                                                    valor = Fecha.ToString("yyyyMMdd HH:mm:ss");
                                                }
                                            }
                                            else
                                            {
                                                if (DateTime.TryParse(valor, out Fecha))
                                                {
                                                    valor = Fecha.ToString("yyyyMMdd HH:mm:ss");
                                                }
                                            }

                                            datos = datos + "'" + valor + "',";
                                        }//foreach (DataRow DRDatosFiltro in DRDatos)
                                    }// foreach (DataRow dssRowFiltro in foundRows)

                                    columnas = columnas.Substring(0, columnas.Length - 1);
                                    datos = datos.Substring(0, datos.Length - 1);

                                    //SQL = "INSERT INTO " + e_BaseDatos.LinkServer() + "." + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + TablaDestino + "(" + columnas + ") VALUES (" + datos + ")";
                                    SQL = "INSERT INTO " + TablaDestino + "(" + columnas + ") VALUES (" + datos + ")";

                                    if (n_ConsultaDummy.PushData(SQL, idUsuario))
                                    {
                                        cl.escribirLogSincro(LogInsert, "Method: Insertar: ", "OK - " + SQL);

                                        //SQL = "SELECT TOP 1 " + IdCampoDestino + " FROM " + e_BaseDatos.LinkServer() + "." + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + TablaDestino + " WHERE " + IdCampoDestino + " NOT IN (SELECT " + e_TblMDRELkeysFields.IdDestino() + " FROM " + e_BaseDatos.LinkServer() + "." + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMDRELkeys() + " WHERE " + e_TblMDRELkeysFields.CampoDestino() + " = '" + TablaDestino + "." + IdCampoDestino + "' AND " + e_TblMDRELkeysFields.CampoOrigen() + " = '" + TablaOrigen + "." + IdCampoOrigen + "') ORDER BY " + IdCampoDestino + " DESC";
                                        SQL = "SELECT TOP 1 " + IdCampoDestino + " FROM " + TablaDestino + " WHERE " + IdCampoDestino + " NOT IN (SELECT " + e_TblMDRELkeysFields.IdDestino() + " FROM " + e_VistasSINC.VistaSINCRELkeys() + " WHERE " + e_TblMDRELkeysFields.CampoDestino() + " = '" + TablaDestino + "." + IdCampoDestino + "' AND " + e_TblMDRELkeysFields.CampoOrigen() + " = '" + TablaOrigen + "." + IdCampoOrigen + "') ORDER BY " + IdCampoDestino + " DESC";

                                        string identity = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                                        //SQL = "INSERT INTO " + e_BaseDatos.LinkServer() + "." + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMDRELkeys() + " (" + e_TblMDRELkeysFields.IdCompania() + "," + e_TblMDRELkeysFields.CampoDestino() + ", " + e_TblMDRELkeysFields.IdDestino() + ", " + e_TblMDRELkeysFields.CampoOrigen() + ", " + e_TblMDRELkeysFields.IdOrigen() + ") VALUES ('" + GLOidCompania + "', '" + TablaDestino + "." + IdCampoDestino + "', '" + identity + "', '" + TablaOrigen + "." + IdCampoOrigen + "', '" + IdOrigen + "')";
                                        SQL = "INSERT INTO " + e_VistasSINC.VistaSINCRELkeys() + " (" + e_TblMDRELkeysFields.IdCompania() + "," + e_TblMDRELkeysFields.CampoDestino() + ", " + e_TblMDRELkeysFields.IdDestino() + ", " + e_TblMDRELkeysFields.CampoOrigen() + ", " + e_TblMDRELkeysFields.IdOrigen() + ") VALUES ('" + GLOidCompania + "', '" + TablaDestino + "." + IdCampoDestino + "', '" + identity + "', '" + TablaOrigen + "." + IdCampoOrigen + "', '" + IdOrigen + "')";

                                        if (n_ConsultaDummy.PushData(SQL, idUsuario))
                                        {
                                            cl.escribirLogSincro(LogInsert, "Method: Insertar: ", "OK - " + SQL);
                                            SQL = "UPDATE " + TablaOrigen + " SET Sincronizado = GETDATE() WHERE " + IdCampoOrigen + " = '" + IdOrigen + "'";
                                            n_ConsultaDummy.PushData(SQL, idUsuario);
                                        }
                                    }//(n_ConsultaDummy.PushData(SQL, idUsuario))
                                } // foreach (DataRow dssRow in DS0.Tables[0].Rows)
                            }//(DS0.Tables[0].Rows.Count > 0)
                        }//(DSDatos != null)
                    }//foreach (DataRow dsRow in DSKey.Tables[0].Rows)
                }
                else //if (DSKey.Tables[0].Rows.Count > 0)
                {

                }
            }
            catch (Exception ex)
            {
                cl.escribirError("Method: Insertar ERROR:Codigo:TID-SMD-SV1-000009 ", ex.Message + " - ultimo query: " + SQL);
            }

            var timeActualizar = Convert.ToDouble(ConfigurationManager.AppSettings["TimerActualizar"]);
            _timerActualizar = new Timer(timeActualizar) { AutoReset = true };
            _timerActualizar.Elapsed += TimerActualizar;
            _timerActualizar.Start();

            //var timeInsertar = Convert.ToDouble(ConfigurationManager.AppSettings["TimerInsertar"]);
            //_timerInsertar = new Timer(timeInsertar) { AutoReset = true };
            //_timerInsertar.Elapsed += TimerInsertar;
            //_timerInsertar.Start();

            //cl.escribirError("INICIO TimerInsertar", "");
        }/// 

        /// <summary>
        /// ACTUALIZA LOS CAMPOS QUE CAMBIARON QUE SE ENCUENTREN EN LA TABLA DE TRACEID.dbo.MDReferencias
        /// </summary>
        private void Actualizar()
        {
            var cl = new clErrores();
            string SQL = "";
            try
            {
                DataSet DSKey_Actu = new DataSet();
                //SQL = "SELECT * FROM " + e_BaseDatos.LinkServer() + "." + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMDReferencias() + " WHERE " + e_TblMDReferenciasFields.IdCompania() + " = '" + GLOidCompania + "' AND " + e_TblMDReferenciasFields.PrimaryKey() + " = 1";
                //SQL = "SELECT * FROM " + e_VistasSINC.VistaSINCReferencias() + " WHERE " + e_TblMDReferenciasFields.IdCompania() + " = '" + GLOidCompania + "' AND " + e_TblMDReferenciasFields.PrimaryKey() + " = '1'";
                SQL = "SELECT * FROM " + e_VistasSINC.VistaSINCReferencias() + " WHERE " + e_TblMDReferenciasFields.IdCompania() + " = '" + GLOidCompania + "' AND " + e_TblMDReferenciasFields.PrimaryKey() + " = '1' ORDER BY IdReferencia ASC";
                DSKey_Actu = n_ConsultaDummy.GetDataSet2(SQL, idUsuario);

                DataSet DSnotKey_Actu = new DataSet();
                //SQL = "SELECT * FROM " + e_BaseDatos.LinkServer() + "." + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMDReferencias() + " WHERE " + e_TblMDReferenciasFields.IdCompania() + " = '" + GLOidCompania + "' AND " + e_TblMDReferenciasFields.PrimaryKey() + " = 0";
                SQL = "SELECT * FROM " + e_VistasSINC.VistaSINCReferencias() + " WHERE " + e_TblMDReferenciasFields.IdCompania() + " = '" + GLOidCompania + "' AND " + e_TblMDReferenciasFields.PrimaryKey() + " = '0'";
                DSnotKey_Actu = n_ConsultaDummy.GetDataSet2(SQL, idUsuario);

                if (DSnotKey_Actu != null)
                {
                    if (DSKey_Actu.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dsRow in DSKey_Actu.Tables[0].Rows)
                        {
                            string TablaDestino = dsRow[e_TblMDReferenciasFields.TablaDestino()].ToString();
                            string IdCampoDestino = dsRow[e_TblMDReferenciasFields.CampoDestino()].ToString();
                            string TablaOrigen = dsRow[e_TblMDReferenciasFields.TablaOrigen()].ToString();
                            string IdCampoOrigen = dsRow[e_TblMDReferenciasFields.CampoOrigen()].ToString();
                            string PrimaryKey = dsRow[e_TblMDReferenciasFields.PrimaryKey()].ToString();
                            string Envia = dsRow[e_TblMDReferenciasFields.Envia()].ToString();

                            DataSet DSIds = new DataSet();
                            //SQL = "SELECT b." + e_TblMDRELkeysFields.IdDestino() + ", b." + e_TblMDRELkeysFields.IdOrigen() + " FROM " + e_BaseDatos.LinkServer() + "." + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + TablaDestino + " a INNER JOIN " + e_BaseDatos.LinkServer() + "." + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMDRELkeys() + " b ON CONVERT(VARCHAR(50), a." + IdCampoDestino + ") = CONVERT(VARCHAR(50), b." + e_TblMDRELkeysFields.IdDestino() + ") INNER JOIN " + TablaOrigen + " c ON CONVERT(VARCHAR(50), c." + IdCampoOrigen + ") COLLATE " + CollateBD + " = CONVERT(VARCHAR(50), b." + e_TblMDRELkeysFields.IdOrigen() + ") WHERE a.idCompania = '" + GLOidCompania + "' AND b." + e_TblMDRELkeysFields.CampoDestino() + " = '" + TablaDestino + "." + IdCampoDestino + "' AND b." + e_TblMDRELkeysFields.CampoOrigen() + " = '" + TablaOrigen + "." + IdCampoOrigen + "' AND b." + e_TblMDRELkeysFields.IdCompania() + " = '" + GLOidCompania + "'";

                            if (Envia.Equals("False"))
                            {
                                SQL = "SELECT b." + e_TblMDRELkeysFields.IdDestino() + ", b." + e_TblMDRELkeysFields.IdOrigen() + " FROM " + TablaDestino + " a INNER JOIN " + e_VistasSINC.VistaSINCRELkeys() + " b ON CONVERT(VARCHAR(50), a." + IdCampoDestino + ") COLLATE " + CollateBD + " = CONVERT(VARCHAR(50), b." + e_TblMDRELkeysFields.IdDestino() + ") INNER JOIN " + TablaOrigen + " c ON CONVERT(VARCHAR(50), c." + IdCampoOrigen + ") COLLATE " + CollateBD + " = CONVERT(VARCHAR(50), b." + e_TblMDRELkeysFields.IdOrigen() + ") WHERE a.idCompania = '" + GLOidCompania + "' AND b." + e_TblMDRELkeysFields.CampoDestino() + " = '" + TablaDestino + "." + IdCampoDestino + "' AND b." + e_TblMDRELkeysFields.CampoOrigen() + " = '" + TablaOrigen + "." + IdCampoOrigen + "' AND b." + e_TblMDRELkeysFields.IdCompania() + " = '" + GLOidCompania + "'";
                            }
                            else
                            {
                                //SQL = "SELECT b." + e_TblMDRELkeysFields.IdOrigen() + ", b." + e_TblMDRELkeysFields.IdDestino() + " FROM " + TablaDestino + " a INNER JOIN " + e_VistasSINC.VistaSINCRELkeys() + " b ON CONVERT(VARCHAR(50), a." + IdCampoDestino + ") COLLATE " + CollateBD + " = CONVERT(VARCHAR(50), b." + e_TblMDRELkeysFields.IdOrigen() + ") INNER JOIN " + TablaOrigen + " c ON CONVERT(VARCHAR(50), c." + IdCampoOrigen + ") COLLATE " + CollateBD + " = CONVERT(VARCHAR(50), b." + e_TblMDRELkeysFields.IdDestino() + ") WHERE a.idCompania = '" + GLOidCompania + "' AND b." + e_TblMDRELkeysFields.CampoOrigen() + " = '" + TablaDestino + "." + IdCampoDestino + "' AND b." + e_TblMDRELkeysFields.IdCompania() + " = '" + GLOidCompania + "'";
                                SQL = "SELECT CONVERT(VARCHAR(50),b." + e_TblMDRELkeysFields.IdOrigen() + ") AS 'IdOrigen', CONVERT(VARCHAR(50), b." + e_TblMDRELkeysFields.IdDestino() + ") AS 'IdDestino' FROM " + TablaDestino + " a INNER JOIN " + e_VistasSINC.VistaSINCRELkeys() + " b ON CONVERT(VARCHAR(50), a." + IdCampoDestino + ") COLLATE " + CollateBD + " = CONVERT(VARCHAR(50), b." + e_TblMDRELkeysFields.IdOrigen() + ") INNER JOIN " + TablaOrigen + " c ON CONVERT(VARCHAR(50), c." + IdCampoOrigen + ") COLLATE " + CollateBD + " = CONVERT(VARCHAR(50), b." + e_TblMDRELkeysFields.IdDestino() + ") WHERE a.idCompania = '" + GLOidCompania + "' AND b." + e_TblMDRELkeysFields.CampoOrigen() + " = '" + TablaDestino + "." + IdCampoDestino + "' AND b." + e_TblMDRELkeysFields.IdCompania() + " = '" + GLOidCompania + "'";
                            }

                            DSIds = n_ConsultaDummy.GetDataSet2(SQL, idUsuario);

                            DataSet DSDatosDestino = new DataSet();
                            DSDatosDestino = null;
                            //SQL = "SELECT * FROM " + TablaDestino;
                            SQL = "SELECT * FROM " + TablaDestino + " WHERE idCompania = '" + GLOidCompania + "'";
                            DSDatosDestino = n_ConsultaDummy.GetDataSet2(SQL, idUsuario);

                            DataSet DSDatosOrigen = new DataSet();
                            DSDatosOrigen = null;
                            //SQL = "SELECT * FROM " + TablaOrigen;
                            SQL = "SELECT * FROM " + TablaOrigen + " WHERE idCompania = '" + GLOidCompania + "'";
                            DSDatosOrigen = n_ConsultaDummy.GetDataSet2(SQL, idUsuario);

                            if (DSIds != null)
                            {
                                if (DSIds.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow dssRow in DSIds.Tables[0].Rows)
                                    {
                                        string IdDestino = dssRow[0].ToString();
                                        string IdOrigen = dssRow[1].ToString();

                                        DataRow[] foundRows = DSnotKey_Actu.Tables[0].Select(e_TblMDReferenciasFields.TablaDestino() + " = '" + TablaDestino + "' AND " + e_TblMDReferenciasFields.TablaOrigen() + " = '" + TablaOrigen + "' AND " + e_TblMDReferenciasFields.PrimaryKey() + " = 0");

                                        foreach (DataRow dssRowFiltro in foundRows)
                                        {
                                            string CampoDestino = dssRowFiltro[e_TblMDReferenciasFields.CampoDestino()].ToString();
                                            string CampoOrigen = dssRowFiltro[e_TblMDReferenciasFields.CampoOrigen()].ToString();

                                            string StrCampoDestino = "";
                                            string StrCampoOrigen = "";

                                            #region StrCampoDestino

                                            if (DSDatosDestino.Tables[0].Rows.Count > 0)
                                            {
                                                DataRow[] DRDatosDestino = DSDatosDestino.Tables[0].Select(IdCampoDestino + " = '" + IdDestino + "'");
                                                foreach (DataRow DRDatosFiltro in DRDatosDestino)
                                                {
                                                    StrCampoDestino = DRDatosFiltro[CampoDestino].ToString();
                                                    //StrCampoDestino = StrCampoDestino.Replace("'", "");
                                                    StrCampoDestino = clUtilities.ConvertCaracteresInvalidos(StrCampoDestino);

                                                    if (!TablaOrigen.Equals("Vista_ERP_PIZZA_Destinos"))
                                                    {
                                                        Double NumeroA = 0;
                                                        if (Double.TryParse(StrCampoDestino, out NumeroA))
                                                        {
                                                            NumeroA = Math.Round(NumeroA, 2);
                                                            StrCampoDestino = NumeroA.ToString().Replace(",", ".");
                                                        }
                                                    }

                                                    DateTime FechaOrigen = new DateTime();
                                                    if (DateTime.TryParse(StrCampoDestino, out FechaOrigen))
                                                    {
                                                        StrCampoDestino = FechaOrigen.ToString("yyyyMMdd HH:mm:ss");
                                                    }

                                                    if (StrCampoDestino.Equals("True"))
                                                    {
                                                        StrCampoDestino = "1";
                                                    }

                                                    if (StrCampoDestino.Equals("False"))
                                                    {
                                                        StrCampoDestino = "0";
                                                    }
                                                }//foreach (DataRow DRDatosFiltro in DRDatosDestino)
                                            }//(DSDatosDestino.Tables[0].Rows.Count > 0)

                                            #endregion StrCampoDestino

                                            #region StrCampoOrigen

                                            if (DSDatosOrigen.Tables[0].Rows.Count > 0)
                                            {
                                                DataRow[] DRDatosOrigen = DSDatosOrigen.Tables[0].Select(IdCampoOrigen + " = '" + IdOrigen + "'");
                                                foreach (DataRow DRDatosFiltro in DRDatosOrigen)
                                                {
                                                    StrCampoOrigen = DRDatosFiltro[CampoOrigen].ToString();
                                                    //StrCampoOrigen = StrCampoOrigen.Replace("'", "");
                                                    StrCampoOrigen = clUtilities.ConvertCaracteresInvalidos(StrCampoOrigen);

                                                    if (!TablaOrigen.Equals("Vista_ERP_PIZZA_Destinos"))
                                                    {
                                                        Double NumeroB = 0;
                                                        if (Double.TryParse(StrCampoOrigen, out NumeroB))
                                                        {
                                                            NumeroB = Math.Round(NumeroB, 2);
                                                            StrCampoOrigen = NumeroB.ToString().Replace(",", ".");
                                                        }
                                                    }

                                                    DateTime FechaOrigen = new DateTime();
                                                    if (DateTime.TryParse(StrCampoOrigen, out FechaOrigen))
                                                    {
                                                        StrCampoOrigen = FechaOrigen.ToString("yyyyMMdd HH:mm:ss");
                                                    }

                                                    if (StrCampoOrigen.Equals("True"))
                                                    {
                                                        StrCampoOrigen = "1";
                                                    }

                                                    if (StrCampoOrigen.Equals("False"))
                                                    {
                                                        StrCampoOrigen = "0";
                                                    }
                                                }//foreach (DataRow DRDatosFiltro in DRDatosOrigen)
                                            }//(DSDatosOrigen.Tables[0].Rows.Count > 0)

                                            #endregion StrCampoOrigen

                                            #region UPDATE

                                            if (StrCampoDestino != StrCampoOrigen)
                                            {

                                                cl.escribirLogSincro(LogUpdate, "Method: Actualizar: ", "CampoDestino: " + StrCampoDestino + " | CampoOrigen: " + StrCampoOrigen);
                                                SQL = "UPDATE " + TablaDestino + " SET " + CampoDestino + " = '" + StrCampoOrigen + "' WHERE " + IdCampoDestino + " = '" + IdDestino + "'";
                                                if (n_ConsultaDummy.PushData(SQL, idUsuario))
                                                {
                                                    cl.escribirLogSincro(LogUpdate, "Method: Actualizar: ", "OK - " + SQL);
                                                    SQL = "UPDATE " + TablaOrigen + " SET Sincronizado = GETDATE() WHERE " + IdCampoOrigen + " = '" + IdOrigen + "'";
                                                    n_ConsultaDummy.PushData(SQL, idUsuario);
                                                }
                                                else
                                                {
                                                    //cl.escribirLogSincro(LogUpdate, "Method: Actualizar: ", "NO - SQL);
                                                }
                                            }
                                            else
                                            {
                                                //cl.escribirLogSincro(LogUpdate, "Method: Actualizar: ", "NO - IGUALES - campoA:" + campoA + "/campoB:" + campoB);
                                            }

                                            #endregion UPDATE

                                        }// foreach (DataRow dssRowFiltro in foundRows)
                                    } // foreach (DataRow dssRow in DS0.Tables[0].Rows)
                                }//(DS0.Tables[0].Rows.Count > 0)
                            }
                        }//foreach (DataRow dsRow in DSKey_Actu.Tables[0].Rows)
                    }
                }
            }
            catch (Exception ex)
            {
                cl.escribirError("Method: Actualizar ERROR:Codigo:TID-SMD-SV1-000010 ", ex.Message + " - ultimo query: " + SQL);
            }

            //var timeTransaccional = Convert.ToDouble(ConfigurationManager.AppSettings["TimerTransaccional"]);
            //_timerTransaccional = new Timer(timeTransaccional) { AutoReset = true };
            //_timerTransaccional.Elapsed += TimerTransaccional;
            //_timerTransaccional.Start();

            var timeInsertar = Convert.ToDouble(ConfigurationManager.AppSettings["TimerInsertar"]);
            _timerInsertar = new Timer(timeInsertar) { AutoReset = true };
            _timerInsertar.Elapsed += TimerInsertar;
            _timerInsertar.Start();

            //var timeActualizar = Convert.ToDouble(ConfigurationManager.AppSettings["TimerActualizar"]);
            //_timerActualizar = new Timer(timeActualizar) { AutoReset = true };
            //_timerActualizar.Elapsed += TimerActualizar;
            //_timerActualizar.Start();

            //cl.escribirError("INICIO TimerActualizar", "");
        }

        private void DatosActivos()
        {
            var cl = new clErrores();
            string SQL = "";
            try
            {
                DataSet DSKey = new DataSet();
                SQL = "SELECT * FROM " + e_VistasSINC.VistaSINCReferencias() + " WHERE " + e_TblMDReferenciasFields.IdCompania() + " = '" + GLOidCompania + "' AND " + e_TblMDReferenciasFields.PrimaryKey() + " = '1' AND " + e_TblMDReferenciasFields.Envia() + " = '0' ORDER BY IdReferencia ASC";
                DSKey = n_ConsultaDummy.GetDataSet(SQL, idUsuario);

                DataSet DSnotKey = new DataSet();
                SQL = "SELECT * FROM " + e_VistasSINC.VistaSINCReferencias() + " WHERE " + e_TblMDReferenciasFields.IdCompania() + " = '" + GLOidCompania + "' AND " + e_TblMDReferenciasFields.PrimaryKey() + " = '0' AND " + e_TblMDReferenciasFields.Envia() + " = '0'";
                DSnotKey = n_ConsultaDummy.GetDataSet(SQL, idUsuario);

                if (DSKey.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dsRow in DSKey.Tables[0].Rows)
                    {
                        string TablaDestino = dsRow[e_TblMDReferenciasFields.TablaDestino()].ToString();
                        string IdCampoDestino = dsRow[e_TblMDReferenciasFields.CampoDestino()].ToString();
                        string TablaOrigen = dsRow[e_TblMDReferenciasFields.TablaOrigen()].ToString();
                        string IdCampoOrigen = dsRow[e_TblMDReferenciasFields.CampoOrigen()].ToString();
                        string PrimaryKey = dsRow[e_TblMDReferenciasFields.PrimaryKey()].ToString();
                        string Envia = dsRow[e_TblMDReferenciasFields.Envia()].ToString();

                        DataSet DSIds = new DataSet();
                        //SQL = "SELECT b." + e_TblMDRELkeysFields.IdDestino() + ", b." + e_TblMDRELkeysFields.IdOrigen() + " FROM " + TablaDestino + " a INNER JOIN " + e_VistasSINC.VistaSINCRELkeys() + " b ON CONVERT(VARCHAR(50), a." + IdCampoDestino + ") COLLATE " + CollateBD + " = CONVERT(VARCHAR(50), b." + e_TblMDRELkeysFields.IdDestino() + ") INNER JOIN " + TablaOrigen + " c ON CONVERT(VARCHAR(50), c." + IdCampoOrigen + ") COLLATE " + CollateBD + " = CONVERT(VARCHAR(50), b." + e_TblMDRELkeysFields.IdOrigen() + ") WHERE a.idCompania = '" + GLOidCompania + "' AND b." + e_TblMDRELkeysFields.CampoDestino() + " = '" + TablaDestino + "." + IdCampoDestino + "' AND b." + e_TblMDRELkeysFields.CampoOrigen() + " = '" + TablaOrigen + "." + IdCampoOrigen + "' AND b." + e_TblMDRELkeysFields.IdCompania() + " = '" + GLOidCompania + "'";
                        SQL = "SELECT b." + e_TblMDRELkeysFields.IdDestino() + ", b." + e_TblMDRELkeysFields.IdOrigen() + " FROM " + TablaDestino + " a INNER JOIN " + e_VistasSINC.VistaSINCRELkeys() + " b ON CONVERT(VARCHAR(50), a." + IdCampoDestino + ") COLLATE " + CollateBD + " = CONVERT(VARCHAR(50), b." + e_TblMDRELkeysFields.IdDestino() + " WHERE a." + IdCampoDestino + " NOT IN (SELECT " + IdCampoOrigen + " FROM " + TablaOrigen + ")";

                        DSIds = n_ConsultaDummy.GetDataSet2(SQL, idUsuario);

                        DataSet DSDatosDestino = new DataSet();
                        DSDatosDestino = null;
                        SQL = "SELECT * FROM " + TablaDestino + " WHERE idCompania = '" + GLOidCompania + "'";
                        DSDatosDestino = n_ConsultaDummy.GetDataSet2(SQL, idUsuario);

                        DataSet DSDatosOrigen = new DataSet();
                        DSDatosOrigen = null;
                        SQL = "SELECT * FROM " + TablaOrigen + " WHERE idCompania = '" + GLOidCompania + "'";
                        DSDatosOrigen = n_ConsultaDummy.GetDataSet2(SQL, idUsuario);

                        if (DSIds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dssRow in DSIds.Tables[0].Rows)
                            {
                                string IdDestino = dssRow[0].ToString();
                                string IdOrigen = dssRow[1].ToString();

                                DataRow[] foundRows = DSnotKey.Tables[0].Select(e_TblMDReferenciasFields.TablaDestino() + " = '" + TablaDestino + "' AND " + e_TblMDReferenciasFields.TablaOrigen() + " = '" + TablaOrigen + "' AND " + e_TblMDReferenciasFields.PrimaryKey() + " = 0");

                                foreach (DataRow dssRowFiltro in foundRows)
                                {
                                    string CampoDestino = dssRowFiltro[e_TblMDReferenciasFields.CampoDestino()].ToString();
                                    string CampoOrigen = dssRowFiltro[e_TblMDReferenciasFields.CampoOrigen()].ToString();

                                    string StrCampoDestino = "";
                                    string StrCampoOrigen = "";

                                    #region StrCampoDestino

                                    if (DSDatosDestino.Tables[0].Rows.Count > 0)
                                    {
                                        DataRow[] DRDatosDestino = DSDatosDestino.Tables[0].Select(IdCampoDestino + " = '" + IdDestino + "'");
                                        foreach (DataRow DRDatosFiltro in DRDatosDestino)
                                        {
                                            StrCampoDestino = DRDatosFiltro[CampoDestino].ToString();
                                            //StrCampoDestino = StrCampoDestino.Replace("'", "");
                                            StrCampoDestino = clUtilities.ConvertCaracteresInvalidos(StrCampoDestino);

                                            Double NumeroA = 0;
                                            if (Double.TryParse(StrCampoDestino, out NumeroA))
                                            {
                                                StrCampoDestino = NumeroA.ToString().Replace(",", ".");
                                            }

                                            DateTime FechaOrigen = new DateTime();
                                            if (DateTime.TryParse(StrCampoDestino, out FechaOrigen))
                                            {
                                                StrCampoDestino = FechaOrigen.ToString("yyyyMMdd HH:mm:ss");
                                            }

                                            if (StrCampoDestino.Equals("True"))
                                            {
                                                StrCampoDestino = "1";
                                            }

                                            if (StrCampoDestino.Equals("False"))
                                            {
                                                StrCampoDestino = "0";
                                            }
                                        }//foreach (DataRow DRDatosFiltro in DRDatosDestino)
                                    }//(DSDatosDestino.Tables[0].Rows.Count > 0)

                                    #endregion StrCampoDestino

                                    #region StrCampoOrigen

                                    if (DSDatosOrigen.Tables[0].Rows.Count > 0)
                                    {
                                        DataRow[] DRDatosOrigen = DSDatosOrigen.Tables[0].Select(IdCampoOrigen + " = '" + IdOrigen + "'");
                                        foreach (DataRow DRDatosFiltro in DRDatosOrigen)
                                        {
                                            StrCampoOrigen = DRDatosFiltro[CampoOrigen].ToString();
                                            //StrCampoOrigen = StrCampoOrigen.Replace("'", "");
                                            StrCampoOrigen = clUtilities.ConvertCaracteresInvalidos(StrCampoOrigen);

                                            Double NumeroB = 0;
                                            if (Double.TryParse(StrCampoOrigen, out NumeroB))
                                            {
                                                StrCampoOrigen = NumeroB.ToString().Replace(",", ".");
                                            }

                                            DateTime FechaOrigen = new DateTime();
                                            if (DateTime.TryParse(StrCampoOrigen, out FechaOrigen))
                                            {
                                                StrCampoOrigen = FechaOrigen.ToString("yyyyMMdd HH:mm:ss");
                                            }

                                            if (StrCampoOrigen.Equals("True"))
                                            {
                                                StrCampoOrigen = "1";
                                            }

                                            if (StrCampoOrigen.Equals("False"))
                                            {
                                                StrCampoOrigen = "0";
                                            }
                                        }//foreach (DataRow DRDatosFiltro in DRDatosOrigen)
                                    }//(DSDatosOrigen.Tables[0].Rows.Count > 0)

                                    #endregion StrCampoOrigen

                                    #region UPDATE

                                    if (StrCampoDestino != StrCampoOrigen)
                                    {

                                        cl.escribirLogSincro(LogUpdate, "Method: Actualizar: ", "CampoDestino: " + StrCampoDestino + " | CampoOrigen: " + StrCampoOrigen);
                                        SQL = "UPDATE " + TablaDestino + " SET " + CampoDestino + " = '" + StrCampoOrigen + "' WHERE " + IdCampoDestino + " = '" + IdDestino + "'";
                                        if (n_ConsultaDummy.PushData(SQL, idUsuario))
                                        {
                                            cl.escribirLogSincro(LogUpdate, "Method: Actualizar: ", "OK - " + SQL);
                                            SQL = "UPDATE " + TablaOrigen + " SET Sincronizado = GETDATE() WHERE " + IdCampoOrigen + " = '" + IdOrigen + "'";
                                            n_ConsultaDummy.PushData(SQL, idUsuario);
                                        }
                                        else
                                        {
                                            //cl.escribirLogSincro(LogUpdate, "Method: Actualizar: ", "NO - SQL);
                                        }
                                    }
                                    else
                                    {
                                        //cl.escribirLogSincro(LogUpdate, "Method: Actualizar: ", "NO - IGUALES - campoA:" + campoA + "/campoB:" + campoB);
                                    }

                                    #endregion UPDATE

                                }// foreach (DataRow dssRowFiltro in foundRows)
                            } // foreach (DataRow dssRow in DS0.Tables[0].Rows)
                        }//(DS0.Tables[0].Rows.Count > 0)
                    }//foreach (DataRow dsRow in DSKey.Tables[0].Rows)
                }
                else //if (DSKey.Tables[0].Rows.Count > 0)
                {

                }
            }
            catch (Exception ex)
            {
                cl.escribirError("Method: Insertar ERROR:Codigo:TID-SMD-SV1-000009 ", ex.Message + " - ultimo query: " + SQL);
            }

            var timeActualizar = Convert.ToDouble(ConfigurationManager.AppSettings["TimerActualizar"]);
            _timerActualizar = new Timer(timeActualizar) { AutoReset = true };
            _timerActualizar.Elapsed += TimerActualizar;
            _timerActualizar.Start();

            //var timeInsertar = Convert.ToDouble(ConfigurationManager.AppSettings["TimerInsertar"]);
            //_timerInsertar = new Timer(timeInsertar) { AutoReset = true };
            //_timerInsertar.Elapsed += TimerInsertar;
            //_timerInsertar.Start();

            //cl.escribirError("INICIO TimerInsertar", "");
        }/// 

        //TraceID.(2016). ServicioMotorDecisiones/Service1.En Trace ID Codigos documentados(32).Costa Rica:Grupo Diverscan. 

        private void AccionEjecutarHilo(string Fuente, string CodLeido, string idUsuario, string ObjetoFuente)
        {
            var cl = new clErrores();
            string resultado = n_SmartMaintenance.CargarEjecutarAccion(Fuente, CodLeido, idUsuario, ObjetoFuente);
            //cl.escribirError("Method: AccionEjecutarHilo: ", resultado);
        }

        /// <summary>
        /// Monitorea la tablas tablas para que cuando realice un Insert, delete o update se ejecute una accion
        /// </summary>
        private void Transaccional()
        {
            var cl = new clErrores();
            string SQL = "";
            try
            {
                DataSet DSEvento = new DataSet();
                //SQL = "SELECT " + e_VistaAccionesFields.Fuente() + ", " + e_VistaAccionesFields.ObjetoFuente() + " FROM " + e_BaseDatos.LinkServer() + "." + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaAcciones() + " WHERE " + e_VistaAccionesFields.Fuente() + " NOT LIKE '~%' GROUP BY " + e_VistaAccionesFields.Fuente() + ", " + e_VistaAccionesFields.ObjetoFuente() + "";
                SQL = "SELECT " + e_TblAccionesFields.Fuente() + ", " + e_TblAccionesFields.ObjetoFuente() + " FROM " + e_VistasSINC.VistaSINCAcciones() + " WHERE " + e_TblAccionesFields.Fuente() + " NOT LIKE '~%' GROUP BY " + e_TblAccionesFields.Fuente() + ", " + e_TblAccionesFields.ObjetoFuente() + "";
                DSEvento = n_ConsultaDummy.GetDataSet3(SQL, idUsuario);
                if (DSEvento.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dsRowEvento in DSEvento.Tables[0].Rows)
                    {
                        //string Fuente = dsRowEvento[e_VistaAccionesFields.Fuente()].ToString();
                        //string ObjetoFuente = dsRowEvento[e_VistaAccionesFields.ObjetoFuente()].ToString();
                        string Fuente = dsRowEvento[e_TblAccionesFields.Fuente()].ToString();
                        string ObjetoFuente = dsRowEvento[e_TblAccionesFields.ObjetoFuente()].ToString();

                        string Tabla = Fuente + "." + ObjetoFuente;

                        string[] split = ObjetoFuente.Split('.');

                        string CHANGE_BASEDATOS = Fuente;
                        string CHANGE_ESQUEMA = split[0];
                        string CHANGE_TABLA = split[1];

                        #region Crear Trigger

                        SQL = "IF EXISTS (SELECT * FROM " + e_BaseDatos.LinkServer() + "." + e_BaseDatos.NombreBD() + "." + "sys.triggers WHERE object_id = OBJECT_ID(N'dbo." + CHANGE_TABLA + "_LogTransaccion')) BEGIN SELECT 1 AS 'Result' END ELSE BEGIN SELECT 0 AS 'Result' END";
                        string ExisteTrigger = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
                        if (ExisteTrigger == "0")
                        {
                            string trigger = System.IO.File.ReadAllText(RutaArchivos + "TriggerLogTransaccion.txt");
                            trigger = trigger.Replace("CHANGE_BASEDATOS", CHANGE_BASEDATOS);
                            trigger = trigger.Replace("CHANGE_ESQUEMA", CHANGE_ESQUEMA);
                            trigger = trigger.Replace("CHANGE_TABLA", CHANGE_TABLA);
                            trigger = trigger.Replace("\n", " ");
                            trigger = trigger.Replace("\r", " ");

                            SQL = trigger;
                            if (n_ConsultaDummy.PushData(SQL, idUsuario))
                            {
                                cl.escribirLogSincro(LogTransaccion, "Method: Transaccional: ", "OK - CREATE TRIGGER " + Tabla);
                            }
                        }
                        else
                        {
                            //cl.escribirLogSincro(LogTransaccion, "Method: Transaccional: ", "NO - " + SQL);
                        }

                        #endregion
                        DataSet DSEventoAccion = new DataSet();
                        //SQL = "SELECT * FROM " + e_BaseDatos.LinkServer() + "." + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaAcciones() + " WHERE " + e_VistaAccionesFields.Fuente() + " = '" + Fuente + "' AND " + e_VistaAccionesFields.ObjetoFuente() + " = '" + ObjetoFuente + "'";
                        SQL = "SELECT * FROM " + e_VistasSINC.VistaSINCAcciones() + " WHERE " + e_TblAccionesFields.Fuente() + " = '" + Fuente + "' AND " + e_TblAccionesFields.ObjetoFuente() + " = '" + ObjetoFuente + "'";
                        DSEventoAccion = n_ConsultaDummy.GetDataSet3(SQL, idUsuario);
                        if (DSEventoAccion.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dsRowEA in DSEventoAccion.Tables[0].Rows)
                            {
                                //string idAccion = dsRowEA[e_VistaAccionesFields.idAccion()].ToString();
                                //string NombreEvento = dsRowEA[e_VistaAccionesFields.idEvento()].ToString();
                                string idAccion = dsRowEA[e_TblAccionesFields.idAccion()].ToString();
                                string NombreEvento = dsRowEA[e_TblAccionesFields.idEvento()].ToString();

                                DataSet DSTran = new DataSet();
                                //SQL = "SELECT " + e_TblLogTransaccionesFields.TipoTrn() + ", " + e_TblLogTransaccionesFields.PKNombre() + ", " + e_TblLogTransaccionesFields.PKId() + " FROM " + e_BaseDatos.LinkServer() + "." + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblLogTransacciones() + " WHERE " + e_TblLogTransaccionesFields.Procesado() + " = 0 AND " + e_TblLogTransaccionesFields.Tabla() + " = '" + Tabla + "' AND " + e_TblLogTransaccionesFields.TipoTrn() + " = '" + NombreEvento + "' GROUP BY " + e_TblLogTransaccionesFields.TipoTrn() + ", " + e_TblLogTransaccionesFields.Tabla() + ", " + e_TblLogTransaccionesFields.PKNombre() + ", " + e_TblLogTransaccionesFields.PKId();
                                SQL = "SELECT " + e_TblLogTransaccionesFields.TipoTrn() + ", " + e_TblLogTransaccionesFields.PKNombre() + ", " + e_TblLogTransaccionesFields.PKId() + " FROM " + e_VistasSINC.VistaSINCLogTransacciones() + " WHERE " + e_TblLogTransaccionesFields.Procesado() + " = 0 AND " + e_TblLogTransaccionesFields.Tabla() + " = '" + Tabla + "' AND " + e_TblLogTransaccionesFields.TipoTrn() + " = '" + NombreEvento + "' GROUP BY " + e_TblLogTransaccionesFields.TipoTrn() + ", " + e_TblLogTransaccionesFields.Tabla() + ", " + e_TblLogTransaccionesFields.PKNombre() + ", " + e_TblLogTransaccionesFields.PKId();
                                DSTran = n_ConsultaDummy.GetDataSet3(SQL, idUsuario);
                                if (DSTran.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow dsRowTran in DSTran.Tables[0].Rows)
                                    {
                                        string TipoTrn = dsRowTran[e_TblLogTransaccionesFields.TipoTrn()].ToString();
                                        string PKNombre = dsRowTran[e_TblLogTransaccionesFields.PKNombre()].ToString();
                                        string PKId = dsRowTran[e_TblLogTransaccionesFields.PKId()].ToString();

                                        AccionEjecutarHilo(Fuente, PKId, idUsuario, ObjetoFuente);

                                        //SQL = "UPDATE " + e_BaseDatos.LinkServer() + "." + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblLogTransacciones() + " SET " + e_TblLogTransaccionesFields.Procesado() + " = '1' WHERE " + e_TblLogTransaccionesFields.Procesado() + " = '0' AND " + e_TblLogTransaccionesFields.TipoTrn() + " = '" + TipoTrn + "' AND " + e_TblLogTransaccionesFields.Tabla() + " = '" + Tabla + "' AND " + e_TblLogTransaccionesFields.PKNombre() + " = '" + PKNombre + "' AND " + e_TblLogTransaccionesFields.PKId() + " = '" + PKId + "'";
                                        SQL = "UPDATE " + e_VistasSINC.VistaSINCLogTransacciones() + " SET " + e_TblLogTransaccionesFields.Procesado() + " = '1' WHERE " + e_TblLogTransaccionesFields.Procesado() + " = '0' AND " + e_TblLogTransaccionesFields.TipoTrn() + " = '" + TipoTrn + "' AND " + e_TblLogTransaccionesFields.Tabla() + " = '" + Tabla + "' AND " + e_TblLogTransaccionesFields.PKNombre() + " = '" + PKNombre + "' AND " + e_TblLogTransaccionesFields.PKId() + " = '" + PKId + "'";
                                        if (n_ConsultaDummy.PushData(SQL, idUsuario))
                                        {
                                            cl.escribirLogSincro(LogTransaccion, "Method: Transaccional: ", "OK - " + SQL);

                                            //SQL = "DELETE FROM TRACEID.dbo.LogTransacciones WHERE Procesado = '0' AND TipoTrn = '" + TipoTrn + "' AND Tabla = '" + Tabla + "' AND PKNombre = '" + PKNombre + "' AND PKId = '" + PKId + "'";
                                            //if (n_ConsultaDummy.PushData(SQL, idUsuario))
                                            //{
                                            //    cl.escribirLogSincro(LogTransaccion, "Method: Transaccional: ", "OK - " + SQL);
                                            //}
                                        }
                                    }//foreach (DataRow dsRowTran in DSTran.Tables[0].Rows)
                                }//(DSTran.Tables[0].Rows.Count > 0)
                            }//foreach (DataRow dsRowEA in DSEventoAccion.Tables[0].Rows)
                        }//(DSEventoAccion.Tables[0].Rows.Count > 0)
                    }//foreach (DataRow dsRowEvento in DSEvento.Tables[0].Rows)
                }//if (DSEvento.Tables[0].Rows.Count > 0)
            }
            catch (Exception ex)
            {
                cl.escribirError("Method: Transaccional ERROR:Codigo:TID-SMD-SV1-000011 ", ex.Message + " - ultimo query: " + SQL);
            }

            //var timeInsertar = Convert.ToDouble(ConfigurationManager.AppSettings["TimerInsertar"]);
            //_timerInsertar = new Timer(timeInsertar) { AutoReset = true };
            //_timerInsertar.Elapsed += TimerInsertar;
            //_timerInsertar.Start();

            var timeTransaccional = Convert.ToDouble(ConfigurationManager.AppSettings["TimerTransaccional"]);
            _timerTransaccional = new Timer(timeTransaccional) { AutoReset = true };
            _timerTransaccional.Elapsed += TimerTransaccional;
            _timerTransaccional.Start();

            //cl.escribirError("INICIO TimerElapsedTransaccional", "");

        }

        #endregion
    }
}
