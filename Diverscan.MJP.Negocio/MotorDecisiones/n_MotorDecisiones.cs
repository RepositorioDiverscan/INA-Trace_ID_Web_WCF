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
using Diverscan.MJP.AccesoDatos.MotorDecision;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.Negocio.Programa;
using Diverscan.MJP.Negocio.LogicaWMS;
using Diverscan.MJP.Utilidades;
using Diverscan.Visitas.Utilidades;
using Telerik.Web.UI;
using Telerik.Web.UI.Diagram;
using Telerik.Web.UI.PersistenceFramework;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.IO;
using System.Reflection.Emit;


namespace Diverscan.MJP.Negocio.MotorDecisiones
{
    public class n_MotorDecisiones
    {
        public class Metodos
        {
            public static string ObtenerCompaniaXUsuario(string idUsuario)
            {
                return n_WMS.ObtenerCompaniaXUsuario(idUsuario);
            }

            public static string ObtenerCompaniaXArticulo(string idArticulo, string idUsuario)
            {
                return n_WMS.ObtenerCompaniaXArticulo(idArticulo, idUsuario);
            }

            public string AgregarFormularioCalidad(string CodLeido, string idUsuario)
            {
                return n_WMS.AgregarFormularioCalidad(CodLeido, idUsuario);
            }

            public string ImprimirCodigo(string CodLeido, string idUsuario)
            {
                return n_WMS.ImprimirCodigo(CodLeido, idUsuario);
            }

            public string ObtenerVarableIndependienteYDependiente(string EnunciadoPregunta, string idUsuario, string idMetodoAccion)
            {
                return da_MotorDecision.ObtenerVarableIndependienteYDependiente(EnunciadoPregunta, idMetodoAccion, idUsuario);
            }

            public string ObtenerAlistosPendientes(string CodLeido, string idUsuario, string idMetodoAccion)
            {
                //return "";
                return ObtenerVarableIndependienteYDependiente(CodLeido, idUsuario, idMetodoAccion);
            }

            public string ObtieneIdTablaEvento(string BaseDatos, string Tabla, string Evento, string CampoBuscado, string ValorDevolver, string idUsuario)
            {
                string respuesta = "";
                try
                {
                    DataSet DSTran = new DataSet();
                    string SQL = "SELECT TipoTrn, PKNombre, PKId, " + ValorDevolver + " FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + "'LogTransacciones WHERE Procesado = 0 AND Tabla = '" + BaseDatos + "." + Tabla + "' AND TipoTrn = '" + Evento + "' AND Campo = '" + CampoBuscado + "' GROUP BY TipoTrn, Tabla, PKNombre, PKId, " + ValorDevolver + "";
                    DSTran = n_ConsultaDummy.GetDataSet(SQL, idUsuario);
                    respuesta = DSTran.Tables[0].Rows[0][ValorDevolver].ToString();


                    return respuesta;
                }
                catch (Exception)
                {
                    respuesta = "error, Ops! Ha ocurrido un Error, Codigo: TID-NE-MDS-000003";
                    return respuesta;
                }
            }

            public static string ObtenerValorTextBox(Control Contenedor, string NombreTXB)
            {
                return n_ManejadorControlesASPX.ObtenerValorTextBox(Contenedor, NombreTXB);
            }

            public string ObtieneCampoTablaEvento(string BaseDatos, string Tabla, string Evento, string CampoBuscado, string ValorDevolver, string idUsuario)
            {
                string respuesta = "";
                try
                {
                    DataSet DSTran = new DataSet();
                    string SQL = "SELECT TipoTrn, PKNombre, PKId, " + ValorDevolver + " FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + "LogTransacciones WHERE Procesado = 0 AND Tabla = '" + BaseDatos + "." + Tabla + "' AND TipoTrn = '" + Evento + "' AND Campo = '" + CampoBuscado + "' GROUP BY TipoTrn, Tabla, PKNombre, PKId, " + ValorDevolver + "";
                    DSTran = n_ConsultaDummy.GetDataSet(SQL, idUsuario);
                    respuesta = DSTran.Tables[0].Rows[0][ValorDevolver].ToString();


                    return respuesta;
                }
                catch (Exception)
                {
                    respuesta = "error, Ops! Ha ocurrido un Error, Codigo: TID-NE-MDS-000004";
                    return respuesta;
                }
            }

            public string EditableTextBox(Control Contenedor, string NombreTXB, bool Value)
            {
                return n_ManejadorControlesASPX.EditableTextBox(Contenedor, NombreTXB, Value);
            }

            public string VisibilidadTextBox(Control Contenedor, string NombreTXB, bool Value)
            {
                return n_ManejadorControlesASPX.VisibilidadTextBox(Contenedor, NombreTXB, Value);
            }

            public string CargarTextBox(Control Contenedor, string NombreTXB, string ValorTextBox)
            {
                return n_ManejadorControlesASPX.CargarTextBox(Contenedor, NombreTXB, ValorTextBox);
            }

            public string LeerCodigoParaUbicar(Control Contenedor, string CodLeido, string txtidArticulo, string txtNombre,
                string txtFechaVencimiento, string txtInfoCod, string txtLote, string txtUbicacionSugerida, string txtCantidad, string idUsuario, string TxtidarticuloERP)
            {
                return n_WMS.LeerCodigoParaUbicar(Contenedor, CodLeido, txtidArticulo, txtNombre, txtFechaVencimiento, txtInfoCod, txtLote, txtUbicacionSugerida, txtCantidad, idUsuario, TxtidarticuloERP);
            }

            public static string InsertDetalleOrdenCompra(string CodLeido, string idUsuario, string idMetodoAccion)
            {
                return n_WMS.InsertDetalleOrdenCompra(CodLeido, idUsuario, idMetodoAccion);
            }

            public static string InsertDetalleSolicitud(string CodLeido, string idUsuario, string idMetodoAccion)
            {
                return n_WMS.InsertDetalleSolicitud(CodLeido, idUsuario, idMetodoAccion);
            }

            public string ObtenerIdArticuloCodigoLeido_GS1128(string CodLeido, string idUsuario)
            {
                return n_WMS.ObtenerIdArticuloCodigoLeido_GS1128(CodLeido, idUsuario);
            }

            public string LeerCodigoParaUbicarHH(string CodLeido, string idUsuario, string idMetodoAccion)
            {
                return n_WMS.LeerCodigoParaUbicarHH(CodLeido, idUsuario, idMetodoAccion);
            }

            public string CodigoParaSolicitarHH(string idArticulo, string cantidad, string idUsuario)
            {
                return n_WMS.CodigoParaSolicitarHH(idArticulo, cantidad, idUsuario);
            }

            public static string SiguienteTarea(string CodLeido)
            {
                //Cambios LuisR
                return n_WMS.SiguienteTarea(CodLeido);
            }

            public static string IngresoEvento(string CodLeido)
            {
                return n_WMS.IngresoEvento(CodLeido);
            }

            public string CrearSSCC(string CodLeido, string idUsuario)     // public static string CrearSSCC(string CodLeido, string idUsuario)
            {
                return n_WMS.CrearSSCC(CodLeido, idUsuario);
            }

            public string AsociarSSCC(string CodLeido, string idUsuario)
            {
                return n_WMS.AsociarSSCC(CodLeido, idUsuario);
            }

            public string ProcesarDespacho(string CodLeido, string idUsuario, string idMetodoAccion)
            {
                return n_WMS.ProcesarDespacho(CodLeido, idUsuario, idMetodoAccion);
            }

            public string ObtenerUbicacionXcodigoLeido(string CodLeido, string idUsuario, string idMetodoAccion)
            {
                return n_WMS.ObtenerUbicacionXcodigoLeido(CodLeido, idUsuario, idMetodoAccion);
            }

            public List<e_Rutas> ObtenerRutas(string idUsuario, string stridRuta)
            {
                return n_WMS.ObtenerRutasFiltro(idUsuario, stridRuta);
            }

            public List<string> VariablesDeLaClass(Type Tipo)
            {
                return da_MotorDecision.VariablesDeLaClass(Tipo);
            }

            public List<string> NombresObjeto(Type Tipo)
            {
                return da_MotorDecision.NombresObjeto(Tipo);
            }

            public string RetonarIdValorDDL(Control Contenedor, string NombreDDL)
            {
                return n_ManejadorControlesASPX.RetonarIdValorDDL(Contenedor, NombreDDL);
            }

            public string ObtenerDisponibilidadArticulo(string idArticulo, string idUsuario)
            {
                return n_WMS.ObtenerDisponibilidadArticulo(idArticulo, idUsuario);
            }

            public string TransaccionMD(string idArticulo, string cantidad, string Tabla, string IdCampo, string ValorCampo, string NombreEvento, string IdEstado, string SumUno_RestaCero, string idUsuario, string idMetodoAccion, string FechaVencimiento, string Lote, string idUbicacion)
            {
                return n_WMS.TransaccionMD(idArticulo, cantidad, Tabla, IdCampo, ValorCampo, NombreEvento, IdEstado, SumUno_RestaCero, idUsuario, idMetodoAccion, FechaVencimiento, Lote, idUbicacion);
            }

            public string ObtenerIdEstadoTransaccion(string idMetodoAccion, string idUsuario)
            {
                return n_WMS.ObtenerIdEstadoTransaccion(idMetodoAccion, idUsuario);
            }

            public string ObtenerSumUno_RestaCeroTransaccion(string idMetodoAccion, string idUsuario)
            {
                return n_WMS.ObtenerSumUno_RestaCeroTransaccion(idMetodoAccion, idUsuario);
            }

            public string ObteneridUbicacionXCodUbicacion(string CodUbicacion, string idUsuario)
            {
                return n_WMS.ObteneridUbicacionXCodUbicacion(CodUbicacion, idUsuario);
            }

            public string ObtenerValorIdCampo(string CantidadCampo1, string CantidadCampo2, string TablaCompleta, string idMetodoAccion, string idEstado, string idUsuario)
            {
                return n_WMS.ObtenerValorIdCampo(CantidadCampo1, CantidadCampo2, TablaCompleta, idMetodoAccion, idEstado, idUsuario);
            }

            public string ObtenerUbicacionSugerida(string idArticulo, int CantidadUndsInventario, string idUsuario, string idZona)
            {
                return n_WMS.ObtenerUbicacionSugerida(idArticulo, CantidadUndsInventario, idUsuario, idZona);
            }

            public List<e_Ubicacion> ObtenerUbicaciones(string idUsuario, string stridArticulo)
            {
                return n_WMS.ObtenerUbicaciones(idUsuario, stridArticulo);
            }

            public string EditarDatosFormulario(Control Contenedor, string TablaBaseDatos, string idUsuario)
            {
                string[] respuesta = n_SmartMaintenance.EditarDatos(Contenedor, TablaBaseDatos, idUsuario);
                return respuesta[0] + ";" + respuesta[1];
            }

            public string AgregarDatosFormulario(Control Contenedor, string TablaBaseDatos, int Tolerancia, string idUsuario)
            {
                string[] respuesta = n_SmartMaintenance.AgregarDatos(Contenedor, TablaBaseDatos, Tolerancia, idUsuario);
                return respuesta[0] + ";" + respuesta[1];
            }

            public string BuscarRegistroFormulario(Control Contenedor, string idUsuario)
            {
                string[] respuesta = n_SmartMaintenance.CargarDatos(Contenedor, idUsuario);
                return respuesta[0] + ";" + respuesta[1];
            }

            public List<e_AccionFlujo> ObtenerAccionObjeto(string pagina, string objetoAccion)
            {
                return da_MotorDecision.ObtenerAccionObjeto(pagina, objetoAccion);
            }

            public List<e_AccionFlujo> ObtenerAccionObjetoMD(string basedatos, string tabla)
            {
                return da_MotorDecision.ObtenerAccionObjetoMD(basedatos, tabla);
            }

            public List<e_AccionFlujo> ObtenerAcciones(string Pagina)
            {
                return da_MotorDecision.ObtenerAcciones(Pagina);
            }

            public string Obtener_IdRadListBox(System.Web.UI.Control Panel, string RadListName)
            {
                Control RLA = new Control();
                RLA = n_ManejadorControlesASPX.RetornaControlContendor(Panel, RadListName);
                return n_ManejadorControlesASPX.ObtenerIDsRadListBox(RLA);
            }

            public string Obtener_CantRadListBox(System.Web.UI.Control Panel, string RadListName, string TxtNumberName)
            {
                Control RLA = new Control();
                RLA = n_ManejadorControlesASPX.RetornaControlContendor(Panel, RadListName);
                return n_ManejadorControlesASPX.ObtenerCantidadesRadListBox(RLA, TxtNumberName);
            }

            private e_Metodo ObtenerMetodo(double idMetodoAccion)
            {
                List<e_AccionFlujo> Acciones = new List<e_AccionFlujo>();
                e_Metodo MetodoEncontrado = new e_Metodo();
                Acciones = da_MotorDecision.ObtenerAcciones(null);
                //var fd = Acciones.ForEach(x => x.Metodos.Find(y => y.idMetodoAccion == idMetodoAccion));
                foreach (e_AccionFlujo Accion in Acciones)
                {
                    foreach (e_Metodo Metodo in Accion.Metodos)
                    {
                        if (Metodo.idMetodoAccion == idMetodoAccion)
                        {
                            MetodoEncontrado = Metodo;
                        }
                    }
                }
                return MetodoEncontrado;
            }

            public int sum(int a, int b)
            {
                return a + b;
            }

            public string CompararIgualdad(string a, string b)
            {
                string Resultado = "False";

                if (a == b)
                {
                    Resultado = "True";
                }

                return Resultado;
            }

            public void ActualizarValorParametro(string idParametro, string Valor, string idUsuario)
            {
                n_ConsultaDummy.PushData("Update " + e_TablasBaseDatos.TblParametrosMetodo() + " set " + e_TblParametrosFields.Valor() + " = '" + Valor + "' where " + e_TblParametrosFields.idParametroAccion() + " = '" + idParametro + "'", idUsuario);
            }

            public string ProcesarDetalleOC(string CantidadRecibida, string idDetalleOrdenCompra, string idUsuario)
            {
                return n_WMS.ProcesarDetalleOC(CantidadRecibida, idDetalleOrdenCompra, idUsuario);
            }

            public List<string> ConsultaMetodos()
            {
                List<string> ListaMetodos = new List<string>();
                try
                {
                    MethodInfo[] methodInfo = this.GetType().GetMethods();

                    foreach (MethodInfo mi in methodInfo)
                    {
                        if (mi.Name != "ConsultaMetodos" && mi.Name != "ToString" && mi.Name != "GetType" && mi.Name != "ConsultaParametros"
                            && mi.Name != "ConsultaTipoParametro" && mi.Name != "Accionar" && mi.Name != "Equals" && mi.Name != "GetHashCode")
                        {
                            ListaMetodos.Add(mi.Name);
                        }
                    }
                    return ListaMetodos;
                }
                catch (Exception)
                {
                    return ListaMetodos;
                }
            }

            public List<string> ConsultaParametros(string NombreMetodo)
            {
                List<string> ListaParametros = new List<string>();
                try
                {

                    MethodInfo methodInfo = this.GetType().GetMethod(NombreMetodo);
                    if (methodInfo != null)
                    {
                        ParameterInfo[] parameters = methodInfo.GetParameters();

                        foreach (ParameterInfo pi in parameters)
                        {
                            ListaParametros.Add(pi.Name);
                        }
                    }

                    return ListaParametros;
                }
                catch (Exception)
                {
                    return ListaParametros;
                }
            }

            public List<string> ConsultaIdMetodosAccion(string idAccion)
            {
                List<string> ListaIdMetodos = new List<string>();
                try
                {
                    string SQL = "";
                    DataSet DS = new DataSet();
                    SQL = "select " + e_TblMetodoAccionFields.idMetodoAccion() + " from " + e_TablasBaseDatos.TblMetodoAccion();
                    SQL += " where " + e_TblMetodoAccionFields.idAccion() + " = '" + idAccion + "'";
                    DS = n_ConsultaDummy.GetDataSet(SQL, "0");
                    foreach (DataRow DR in DS.Tables[0].Rows)
                    {
                        ListaIdMetodos.Add(DR[e_TblMetodoAccionFields.idMetodoAccion()].ToString());
                    }

                    return ListaIdMetodos;
                }
                catch (Exception)
                {
                    return ListaIdMetodos;
                }
            }

            public List<string> ConsultaNombresMetodosAccion(string idAccion)
            {
                List<string> ListaIdMetodos = new List<string>();
                try
                {
                    string SQL = "";
                    DataSet DS = new DataSet();
                    SQL = "select " + e_TblMetodoAccionFields.Nombre() + " from " + e_TablasBaseDatos.TblMetodoAccion();
                    SQL += " where " + e_TblMetodoAccionFields.idAccion() + " = '" + idAccion + "'";
                    DS = n_ConsultaDummy.GetDataSet(SQL, "0");
                    foreach (DataRow DR in DS.Tables[0].Rows)
                    {
                        ListaIdMetodos.Add(DR[e_TblMetodoAccionFields.Nombre()].ToString());
                    }

                    return ListaIdMetodos;
                }
                catch (Exception)
                {
                    return ListaIdMetodos;
                }
            }

            public string ConsultaTipoParametro(string NombreMetodo, string ParametroName)
            {
                string TipoParametro = "";
                try
                {


                    MethodInfo methodInfo = this.GetType().GetMethod(NombreMetodo);
                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    foreach (ParameterInfo pi in parameters)
                    {
                        if (pi.Name == ParametroName)
                        {
                            TipoParametro = pi.ParameterType.FullName;
                        }
                    }
                    return TipoParametro;
                }
                catch (Exception)
                {
                    return TipoParametro;
                }
            }

            public static string AgregarTRAIngresoSalidaArticulos(string trama)
            {
                return da_MotorDecision.AgregarTRAIngresoSalidaArticulos(trama);
            }

            public static string AgregarTRAIngresoSalidaArticulos(string SumUno_RestaCero, string idArticulo, string FechaVencimiento, string Lote, string idUsuario, string idAccion, string idTablaCampoDocumentoAccion, string idUbicacion, string idCampoDocumentoAccion, string NumDocumentoAccion, string Cantidad, string Procesado, string idEstado)
            {
                return da_MotorDecision.AgregarTRAIngresoSalidaArticulos(SumUno_RestaCero, idArticulo, FechaVencimiento, Lote, idUsuario, idAccion, idTablaCampoDocumentoAccion, idUbicacion, idCampoDocumentoAccion, NumDocumentoAccion, Cantidad, Procesado, idEstado);
            }

            public string Accionar(e_Metodo Metodo, Control Panel, string IdUsuario, string idAccion, string idMetodoAccion)
            {
                string respuesta = "";
                string NombreMetodo = Metodo.Nombre;
                try
                {
                    MethodInfo methodInfo = this.GetType().GetMethod(NombreMetodo);
                    if (methodInfo != null)
                    {
                        ParameterInfo[] parameters = methodInfo.GetParameters();
                        e_ParametrosWF param = new e_ParametrosWF();
                        object[] parametersArray = new object[parameters.Length];

                        for (int i = 0; i < parameters.Length; i++)
                        {
                            param = Metodo.Parametros.Find((x) => x.Nombre == parameters[i].Name);
                            if (param != null)
                            {
                                if (param.TipoParametro.Nombre == "System.Web.UI.Control")
                                {
                                    parametersArray[i] = Panel;
                                }
                                else
                                {
                                    switch (param.Nombre)
                                    {
                                        case "idUsuario":
                                            parametersArray[i] = Convert.ChangeType(IdUsuario, Type.GetType(param.TipoParametro.Nombre));
                                            break;
                                        case "idAccion":
                                            parametersArray[i] = Convert.ChangeType(idAccion, Type.GetType(param.TipoParametro.Nombre));
                                            break;
                                        case "idMetodoAccion":
                                            parametersArray[i] = Convert.ChangeType(idMetodoAccion, Type.GetType(param.TipoParametro.Nombre));
                                            break;
                                        default:
                                            parametersArray[i] = Convert.ChangeType(param.Valor, Type.GetType(param.TipoParametro.Nombre));
                                            break;
                                    }
                                }

                            }
                            else
                            {
                                respuesta = "error; Al parecer algun parametro no fue encontrado!, Codigo: TID-NE-MDS-000005";

                                return respuesta;
                            }
                        }
                        var svc = Activator.CreateInstance(typeof(n_MotorDecisiones.Metodos));
                        Object ret = typeof(n_MotorDecisiones.Metodos).InvokeMember(NombreMetodo, BindingFlags.InvokeMethod, Type.DefaultBinder, svc, parametersArray);
                        respuesta = ret.ToString();
                    }


                    return respuesta;
                }
                catch (Exception ex)
                {
                    respuesta = "error,Ops! Ha ocurrido un Error, Codigo: TID-NE-MDS-000006" + ex.Message;
                    return respuesta;
                }
            }

            public string Accionar(e_Metodo Metodo, string CodLeido, string IdUsuario, string idAccion, string idMetodoAccion)
            {
                string respuesta = "";
                string NombreMetodo = Metodo.Nombre;
                string Paso = "";
                Object ret = new Object();
                try
                {
                    string SQL = String.Empty;
                    List<e_ParametrosWF> Parametros = new List<e_ParametrosWF>();
                    MethodInfo methodInfo = this.GetType().GetMethod(NombreMetodo);
                    if (methodInfo != null)
                    {
                        ParameterInfo[] parameters = methodInfo.GetParameters();
                        e_ParametrosWF param = new e_ParametrosWF();
                        object[] parametersArray = new object[parameters.Length];

                        Paso = "paso1";

                        for (int i = 0; i < parameters.Length; i++)
                        {
                            param = Metodo.Parametros.Find((x) => x.Nombre == parameters[i].Name);
                            if (param != null)
                            {
                                Paso = "paso2";
                                switch (param.Nombre)
                                {
                                    case "idUsuario":
                                        parametersArray[i] = Convert.ChangeType(IdUsuario, Type.GetType(param.TipoParametro.Nombre));
                                        param.Valor = IdUsuario;
                                        break;
                                    case "CodLeido":
                                        parametersArray[i] = Convert.ChangeType(CodLeido, Type.GetType(param.TipoParametro.Nombre));
                                        param.Valor = CodLeido;
                                        break;
                                    case "idAccion":
                                        parametersArray[i] = Convert.ChangeType(idAccion, Type.GetType(param.TipoParametro.Nombre));
                                        param.Valor = idAccion;
                                        break;
                                    case "idMetodoAccion":
                                        parametersArray[i] = Convert.ChangeType(idMetodoAccion, Type.GetType(param.TipoParametro.Nombre));
                                        param.Valor = idMetodoAccion;
                                        break;
                                    default:
                                        parametersArray[i] = Convert.ChangeType(param.Valor, Type.GetType(param.TipoParametro.Nombre));
                                        param.Valor = param.Valor.ToString();
                                        break;
                                }

                                Parametros.Add(param);
                                Paso = "paso3";
                            }
                            else
                            {
                                respuesta = "error; Al parecer algun parametro no fue encontrado! Codigo: TID-NE-MDS-000007";
                                Paso = "paso4";
                                return respuesta;
                            }
                        }

                        Paso = "paso5";
                        var svc = Activator.CreateInstance(typeof(n_MotorDecisiones.Metodos));
                        Paso = "paso5.1";
                        ret = typeof(n_MotorDecisiones.Metodos).InvokeMember(NombreMetodo, BindingFlags.InvokeMethod, Type.DefaultBinder, svc, parametersArray);
                        Paso = "paso5.2";
                        respuesta = ret.ToString();
                        Paso = "paso6";

                        foreach (e_ParametrosWF Parametro in Parametros)
                        {
                            SQL = "insert into MDObjetoHistoria(Dependiente, Independiente,Valor)";
                            SQL += " values ('" + Metodo.Nombre.ToUpper() + "', '" + Parametro.Nombre.ToUpper() + "','" + Parametro.Valor.ToUpper().ToString() + "')";
                            n_ConsultaDummy.PushData(SQL, IdUsuario);
                        }
                        Paso = "paso7";
                        SQL = "insert into MDObjetoHistoria(Dependiente, Independiente,Valor)";
                        SQL += " values ('" + Metodo.Nombre.ToUpper() + "', 'Respuesta','" + respuesta.ToUpper() + "')";
                        n_ConsultaDummy.PushData(SQL, IdUsuario);
                        Paso = "paso8";
                    }

                    return respuesta;
                }
                catch (Exception ex)
                {
                    respuesta = "error, Ops! Ha ocurrido un Error, Codigo:TID-NE-MDS-000008-" + ex.Message + "-" + Paso + "-" + ret.ToString();
                    return respuesta;
                }
            }

            public string Accionar(e_Metodo Metodo, string IdUsuario)
            {
                string respuesta = "";
                string NombreMetodo = Metodo.Nombre;
                try
                {
                    MethodInfo methodInfo = this.GetType().GetMethod(NombreMetodo);
                    if (methodInfo != null)
                    {
                        ParameterInfo[] parameters = methodInfo.GetParameters();
                        e_ParametrosWF param = new e_ParametrosWF();
                        object[] parametersArray = new object[parameters.Length];

                        for (int i = 0; i < parameters.Length; i++)
                        {
                            param = Metodo.Parametros.Find((x) => x.Nombre == parameters[i].Name);
                            if (param != null)
                            {

                                if (param.Nombre == "idUsuario")
                                {
                                    parametersArray[i] = Convert.ChangeType(IdUsuario, Type.GetType(param.TipoParametro.Nombre));
                                }
                                else
                                {
                                    parametersArray[i] = Convert.ChangeType(param.Valor, Type.GetType(param.TipoParametro.Nombre));

                                }
                            }
                            else
                            {
                                respuesta = "error; Al parecer algun parametro no fue encontrado!, Codigo: TID-NE-MDS-000009";
                                return respuesta;
                            }
                        }
                        var svc = Activator.CreateInstance(typeof(n_MotorDecisiones.Metodos));
                        Object ret = typeof(n_MotorDecisiones.Metodos).InvokeMember(NombreMetodo, BindingFlags.InvokeMethod, Type.DefaultBinder, svc, parametersArray);
                        respuesta = ret.ToString();
                    }


                    return respuesta;
                }
                catch (Exception ex)
                {
                    respuesta = "error, Ops! Ha ocurrido un Error, Codigo: TID-NE-MDS-000010" + ex.Message;
                    return respuesta;
                }
            }

            public static string ValidaDevolucionArticuloSSCC(string CodLeido, string idUsuario)
            {
                return n_WMS.ValidaDevolucionArticuloSSCC(CodLeido, idUsuario);
            }

            public void CargarDrop(DropDownList ddl, string CodLeido, string idUsuario)
            {
                //n_WMS.CargarDrop(ddl, CodLeido, idUsuario);
            }

            public string CargarDropStr(string CodLeido, string idUsuario)
            {
                return n_WMS.CargarDropStr(CodLeido, idUsuario);
            }

            public static string HHRetornarInfoSSCC(string CodLeido, string idUsuario)
            {
                return n_WMS.HHRetornarInfoSSCC(CodLeido, idUsuario);
            }

            public static string AsociarVehiculoSSCC(string CodLeido, string idUsuario)
            {
                return n_WMS.AsociarVehiculoSSCC(CodLeido, idUsuario);
            }

            public static string VerInfoSSCC(string CodLeido, string idUsuario)
            {
                return n_WMS.VerInfoSSCC(CodLeido, idUsuario);
            }

            public static string ObtenerArticuloGranel(string CodLeido, string idUsuario)
            {
                return n_WMS.ObtenerArticuloGranel(CodLeido, idUsuario);
            }

            public static string ProcesarCodigoGS1ArticuloGranel(string CodLeido, string idUsuario)
            {
                return n_WMS.ProcesarCodigoGS1ArticuloGranel(CodLeido, idUsuario);
            }

            public static string ObtenerArticulosSegunUbicacion(string CodLeido, string idUsuario)
            {
                return n_WMS.ObtenerArticulosSegunUbicacion(CodLeido, idUsuario);
            }

            public static string ValidaAccesosHH(string idUsuario)
            {
                return n_WMS.ValidaAccesosHH(idUsuario);
            }

            #region Eventos

            public static string EventoObtenerOpcionesXCedula(string CodLeido, string idUsuario)
            {
                return n_WMS.EventoObtenerOpcionesXCedula(CodLeido, idUsuario);
            }

            public static string EventoObtenerRegaliaXCedula(string CodLeido, string idUsuario)
            {
                return n_WMS.EventoObtenerRegaliaXCedula(CodLeido, idUsuario);
            }

            public static bool EventoEnviarOpcionesDemo(string CodLeido, string idUsuario)
            {
                return n_WMS.EventoEnviarOpcionesDemo(CodLeido, idUsuario);
            }

            public static bool ProcesaCantidadGrandeArticulo(string idArticulo, string cantidad, string idUsuario, string Idmetodoaccion)
            {
                return n_WMS.ProcesaCantidadGrandeArticulo(idArticulo, cantidad, idUsuario, Idmetodoaccion);
            }

            public static string EjecutaSQLHH(string CodLeido, string idUsuario)
            {
                return n_WMS.EjecutaSQLHH(CodLeido, idUsuario);
            }

            public static string DetalleOrdenCompra(string CodLeido, string idUsuario)
            {
                return n_WMS.DetalleOrdenCompra(CodLeido, idUsuario);
            }

            public static string DevolverIdUbicacion(string CodLeido, string idUsuario)
            {
                return n_WMS.DevolverIdUbicacion(CodLeido, idUsuario);
            }

            public static string ArmaDatsetAsociarSSCC(string CodLeido, string idUsuario)
            {
                return n_WMS.ArmaDatsetAsociarSSCC(CodLeido, idUsuario);
            }

            public static string CierraSolicitud(string CodLeido, string idUsuario)
            {
                return n_WMS.CierraSolicitud(CodLeido, idUsuario);
            }

            public static string CierraSSCC(string CodLeido, string idUsuario)
            {
                return n_WMS.CierraSSCC(CodLeido, idUsuario);
            }

            public static string ValidaCierreSSCC(string CodLeido, string idUsuario)
            {
                return n_WMS.ValidaCierreSSCC(CodLeido, idUsuario);
            }

            public static string CierraAlisto(string CodLeido, string idUsuario)
            {
                return n_WMS.CierraAlisto(CodLeido, idUsuario);
            }

            public static string SaltarSiguienteTarea(string CodLeido, string idUsuario)
            {
                return n_WMS.SaltarSiguienteTarea(CodLeido, idUsuario);
            }

            public static string MuestraInfoArticuloHH(string CodLeido, string idUsuario)
            {
                return n_WMS.MuestraInfoArticuloHH(CodLeido, idUsuario);
            }

        }
              #endregion Eventos
    }
}
