using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.Utilidades;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.AccesoDatos.UsoGeneral;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Reflection;

namespace Diverscan.MJP.AccesoDatos.MotorDecision
{
    public class da_MotorDecision
    {
        public static List<string> NombresObjeto(Type Tipo)
        {
            List<string> Variables = new List<string>();
            try
            {
                foreach (PropertyInfo campos in Tipo.GetProperties())
                {
                    Variables.Add(campos.Name);
                }

                return Variables;
            }
            catch (Exception)
            {
                return Variables;
            }
        }

        public static List<string> VariablesDeLaClass(Type Tipo)
        {
            List<string> Variables = new List<string>();
            try
            {
                const BindingFlags flags = BindingFlags.Public | BindingFlags.Static;

                foreach (MemberInfo campos in Tipo.GetMembers(flags))
                {
                    Variables.Add(campos.Name);
                }

                return Variables;
            }
            catch (Exception)
            {
                return Variables;
            }
        }

        #region Acciones


        //LuisR


        /// <summary>
        /// Button=btn, TextBox=txt o txm, CheckBox=chk, DropDownList=ddl
        /// </summary>
        /// <param name="formulario"></param>
        /// <param name="usuario"></param>
        /// <param name="IncialesControl"></param>
        /// <returns></returns>
        public static List<string> BotonesAccion(string formulario, string usuario, string IncialesControl)
        {
            List<string> ObjetoAccion = new List<string>();
            try
            {
                DataSet DS = new DataSet();
                string NombreControl = "";

                string SQL = "select " + e_VistaCamposAccion.ObjetoFuente() + " from " + e_TablasBaseDatos.VistaDetalleMetodosImplementados();
                DS = da_ConsultaDummy.GetDataSet(SQL, usuario);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        NombreControl = DS.Tables[0].Rows[i][0].ToString();
                        if (NombreControl.Substring(0, 3).ToUpper() == IncialesControl.ToUpper())
                        {
                            ObjetoAccion.Add(NombreControl);
                        }
                    }
                }
                return ObjetoAccion;
            }
            catch (Exception)
            {
                return ObjetoAccion;
            }
        }

        private static e_AccionFlujo ObtenerAccion(DataRow DR)
        {
            bool EsNumero = false;
            double numero = 0;
            e_Evento Evento = new e_Evento();


            e_AccionFlujo Accion = new e_AccionFlujo();
            Accion.Nombre = DR[e_VistaCamposAccion.NombreAccion()].ToString();
            Accion.Fuente = DR[e_VistaCamposAccion.Fuente()].ToString();
            Accion.ObjetoFuente = DR[e_VistaCamposAccion.ObjetoFuente()].ToString();
            Evento.idEvento = int.Parse(DR[e_VistaCamposAccion.idEvento()].ToString());
            Evento.Nombre = DR[e_VistaCamposAccion.NombreEvento()].ToString();
            Accion.Envento = Evento;
            EsNumero = double.TryParse(DR[e_VistaCamposAccion.idAccion()].ToString(), out numero);
            if (EsNumero) Accion.IdAccion = numero; else { Accion.IdAccion = 0; }
            return Accion;
        }

        private static e_Metodo ObtenerMetodo(DataRow DR)
        {
            bool EsNumero = false;
            double numero = 0;
            int numeroint = 1;

            e_Metodo Metodo = new e_Metodo();
            Metodo.IdMetodo = DR[e_VistaCamposAccion.NombreMetodo()].ToString();
            Metodo.Nombre = DR[e_VistaCamposAccion.NombreMetodo()].ToString();
            EsNumero = double.TryParse(DR[e_VistaCamposAccion.idTipoMetodo()].ToString(), out numero);
            if (EsNumero) Metodo.idTipoMetodo = numero; else { Metodo.idTipoMetodo = 0; }
            Metodo.NombreTipoMetodo = DR[e_VistaCamposAccion.NombreTipoMetodo()].ToString();
            Metodo.idParametroAccionSalida = DR[e_VistaCamposAccion.idParametroAccionSalida()].ToString();
            Metodo.AcumulaSalida = bool.Parse(DR[e_VistaCamposAccion.AcumulaSalidaMetodo()].ToString());
            EsNumero = double.TryParse(DR[e_VistaCamposAccion.idMetodoAccion()].ToString(), out numero);
            if (EsNumero) Metodo.idMetodoAccion = numero; else { Metodo.idMetodoAccion = 0; }
            EsNumero = int.TryParse(DR[e_VistaCamposAccion.SecuenciaMetodo()].ToString(), out numeroint);
            if (EsNumero) Metodo.Secuencia = numeroint; else { Metodo.Secuencia = 1; }
            EsNumero = double.TryParse(DR[e_VistaCamposAccion.IdParametroDestino()].ToString(), out numero);
            if (EsNumero) Metodo.IdParametroAccion = numero; else { Metodo.IdParametroAccion = 0; }

            return Metodo;
         
        }

        private static void ActualizarMultipleValor(bool Estado, string IdParametroAccion)
        {
            string SQL = "update " + e_TablasBaseDatos.TblParametrosMetodo() + " set ";
            SQL += e_TblParametrosFields.MultipleValor() + " = '" + Estado + "' where ";
            SQL += e_TblParametrosFields.idParametroAccion() + " = '" + IdParametroAccion + "'";
            da_ConsultaDummy.PushData(SQL, "MotorDecision");
        }

        private static e_ParametrosWF ObtenerParametro(DataRow DR)
        {
            bool EsNumero = false;
            double numero = 0;
            int numeroint = 0;
            e_ParametrosWF Parametro = new e_ParametrosWF();
            e_TipoParametro TipoParametro = new e_TipoParametro();
            EsNumero = double.TryParse(DR[e_VistaCamposAccion.idParametroAccion()].ToString(), out numero);
            if (EsNumero) Parametro.IdParametroAccion = numero; else { Parametro.IdParametroAccion = 0; }
            EsNumero = double.TryParse(DR[e_VistaCamposAccion.idMetodoAccion()].ToString(), out numero);
            if (EsNumero) Parametro.IdMetodoAccion = numero; else { Parametro.IdMetodoAccion = 0; }
            Parametro.Nombre = DR[e_VistaCamposAccion.NombreParametroAccion()].ToString();
            Parametro.Valor = DR[e_VistaCamposAccion.ValorParametro()].ToString();
            EsNumero = double.TryParse(DR[e_VistaCamposAccion.NumeroParametro()].ToString(), out numero);
            if (EsNumero) Parametro.Numero = numero; else { Parametro.Numero = 0; }
            TipoParametro.Nombre = DR[e_VistaCamposAccion.NombreTipoParametro()].ToString();
            EsNumero = int.TryParse(DR[e_VistaCamposAccion.idParametroAccion()].ToString(), out numeroint);
            if (EsNumero) TipoParametro.IdTipoParametro = numeroint; else { TipoParametro.IdTipoParametro = 0; }
            Parametro.TipoParametro = TipoParametro;
            Parametro.MultipleValor = bool.Parse(DR[e_VistaCamposAccion.MultipleValor()].ToString());
            string[] valores = Parametro.Valor.Split(';');
            if (valores.Length > 1)
            {
                if (Parametro.MultipleValor == false)
                {
                    Parametro.MultipleValor = true;
                    ActualizarMultipleValor(Parametro.MultipleValor, Parametro.IdParametroAccion.ToString());
                }
            }
            else
            {
                if (Parametro.MultipleValor == true)
                {
                    Parametro.MultipleValor = false;
                    ActualizarMultipleValor(Parametro.MultipleValor, Parametro.IdParametroAccion.ToString());
                }
            }
            List<e_ValorMultipleValor> VMS = new List<e_ValorMultipleValor>();
            foreach (string str in valores)
            {
                e_ValorMultipleValor VM = new e_ValorMultipleValor();
                VM.TipoParametro = Parametro.TipoParametro;
                VM.ValorMulti = str;
                VMS.Add(VM);
            }
            Parametro.ValorM = VMS;
            return Parametro;
        }

        public static DataSet ObtenerDataSetAcciones(string And)
        {
            try
            {
                DataSet DS = new DataSet();
                List<string> Campos = new List<string>();
                Type type = typeof(e_VistaCamposAccion);
                Campos = VariablesDeLaClass(type);
                string SQL = "SELECT ";
                foreach (string str in Campos)
                {
                    SQL += str + ",";
                }
                SQL = SQL.Substring(0, SQL.Length - 1);  // +",b.idRol";
                SQL += " FROM " + e_TablasBaseDatos.VistaDetalleMetodosImplementados();
                //SQL += "   INNER JOIN traceid.dbo.RELRolMetodoAccion        AS b on (a.idMetodoAccion = b.idMetodoAccion)";
                SQL += " WHERE " + e_VistaCamposAccion.idParametroAccion() + " <> '0' ";
                if (!string.IsNullOrEmpty(And.Trim()))
                {
                    SQL += " " + And;
                }
                SQL += " order by " + e_VistaCamposAccion.idAccion() + "," + e_VistaCamposAccion.SecuenciaMetodo() + " ";
                DS = da_ConsultaDummy.GetDataSet(SQL, "MD");
                return DS;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static List<e_AccionFlujo> ObtenerAccionObjeto(string Pagina, string objetoAccion)
        {
            List<e_AccionFlujo> Acciones = new List<e_AccionFlujo>();
            try
            {
                DataSet DS = new DataSet();
                List<string> Campos = new List<string>();
                Type type = typeof(e_VistaCamposAccion);
                string and = "";
                Campos = VariablesDeLaClass(type);
                if (!string.IsNullOrEmpty(Pagina.Trim()))
                {
                    and = " and " + e_VistaCamposAccion.Fuente() + " = '" + Pagina + "' and " + e_VistaCamposAccion.ObjetoFuente() + " = '" + objetoAccion + "'";
                }

                DS = ObtenerDataSetAcciones(and);
                e_AccionFlujo Accion = new e_AccionFlujo();
                List<e_ParametrosWF> Parametros = new List<e_ParametrosWF>();
                e_Metodo Metodo = new e_Metodo();
                List<e_Metodo> Metodos = new List<e_Metodo>();
                ///Carga los parametros 
                ///
                int contadorMetodo = 0;
                string idMetodoAccion = "";
                foreach (DataRow DR in DS.Tables[0].Rows)
                {
                    if (DR.Table.Rows.IndexOf(DR) == 0 )
                    {
                        ///si se ingresa aqui o cambio de metodo o es la primea vez y se tiene que cargar todo... Accion, Metodo y Parametro
                      
                        Accion = ObtenerAccion(DR);
                        Acciones.Add(Accion);
                        idMetodoAccion = ObtenerMetodo(DR).idMetodoAccion.ToString(); //esto sirve solo para identidicar el metodo cuando se valide en cual metodo se esta
                        Metodo = ObtenerMetodo(DR);
                        Parametros.Add(ObtenerParametro(DR));
                        if (DR.Table.Rows.IndexOf(DR) == DR.Table.Rows.Count - 1)
                        {
                            Parametros.Add(ObtenerParametro(DR));
                            Metodo.Parametros = Parametros;
                            Metodos.Add(Metodo); //se crea nuevo metodo
                            Acciones[0].Metodos = Metodos;
                        }

                    }
                    else// si no es la primera
                    {

                        if (DR[e_VistaCamposAccion.idMetodoAccion()].ToString() == idMetodoAccion)//si el metodo a cargar es el mismo se vuelve a cargar parametro
                        {
                            if (DR.Table.Rows.IndexOf(DR) == DR.Table.Rows.Count - 1) //Ultima vez que se ejecuta el ciclo
                            {
                                Parametros.Add(ObtenerParametro(DR));
                                Metodo.Parametros = Parametros;
                                Metodos.Add(Metodo); //se crea nuevo metodo
                                Acciones[0].Metodos = Metodos;
                            }
                            else
                            {
                                 Parametros.Add(ObtenerParametro(DR));
                            }
                           
                        }
                        else //si el metodo a cargar NO es el mismo se asignan los parametros al metodo anterior y se cargan nuevoa paramtros
                        {
                            contadorMetodo++;
                            Metodo.Parametros = Parametros; //Se cargan los parametros al metodo finalizado
                            Metodos.Add(Metodo); //se crea nuevo metodo

                            Metodo = new e_Metodo();
                            Parametros = new List<e_ParametrosWF>();
                            Metodo = ObtenerMetodo(DR);
                            idMetodoAccion = ObtenerMetodo(DR).idMetodoAccion.ToString(); //esto sirve solo para identidicar el metodo cuando se valide en cual metodo se esta
                            Parametros.Add(ObtenerParametro(DR));

                        }
                     }
                }

                return Acciones;
            }
            catch (Exception)
            {
                return Acciones;
            }
        }

        public static List<e_AccionFlujo> ObtenerAcciones(string Pagina)
        {
            List<e_AccionFlujo> Acciones = new List<e_AccionFlujo>();
            try
            {
                DataSet DS = new DataSet();
                string and = "";
                e_AccionFlujo Accion = new e_AccionFlujo();
                List<e_ParametrosWF> Parametros = new List<e_ParametrosWF>();
                e_Metodo Metodo = new e_Metodo();
                List<e_Metodo> Metodos = new List<e_Metodo>();
                if (!string.IsNullOrEmpty(Pagina.Trim()))
                {
                    and = " and " + e_VistaCamposAccion.Fuente() + " = '" + Pagina + "' ";
                }
                DS = ObtenerDataSetAcciones(and);
                ///Carga los parametros 
                ///
                string idMetodoAccion = "";
                string NombreAccion = "";
                foreach (DataRow DR in DS.Tables[0].Rows)
                {
                    if (DR.Table.Rows.IndexOf(DR) == 0)
                    {
                        ///si se ingresa aqui o cambio de metodo o es la primea vez y se tiene que cargar todo... Accion, Metodo y Parametro

                        Accion = ObtenerAccion(DR);
                        NombreAccion = Accion.Nombre;
                        Metodo = ObtenerMetodo(DR);
                        idMetodoAccion = Metodo.idMetodoAccion.ToString(); //esto sirve solo para identidicar el metodo cuando se valide en cual metodo se esta
                        Parametros.Add(ObtenerParametro(DR));
                    }
                    else// si no es la primera
                    {
                        if (DR[e_VistaCamposAccion.NombreAccion()].ToString() == NombreAccion)
                        {
                            if (DR[e_VistaCamposAccion.idMetodoAccion()].ToString() == idMetodoAccion)//si el metodo a cargar es el mismo se vuelve a cargar parametro
                            {
                                if (DR.Table.Rows.IndexOf(DR) == DR.Table.Rows.Count - 1) //Ultima vez que se ejecuta el ciclo
                                {
                                    Parametros.Add(ObtenerParametro(DR));
                                    Metodo.Parametros = Parametros;
                                    Metodos.Add(Metodo); //se crea nuevo metodo
                                    //Acciones[0].Metodos = Metodos;
                                    Accion.Metodos = Metodos;
                                    Acciones.Add(Accion);
                                }
                                else
                                {
                                    Parametros.Add(ObtenerParametro(DR));
                                }

                            }
                            else //si el metodo a cargar NO es el mismo se asignan los parametros al metodo anterior y se cargan nuevoa paramtros
                            {
                                Metodo.Parametros = Parametros; //Se cargan los parametros al metodo finalizado
                                Metodos.Add(Metodo); //se crea nuevo metodo

                                Metodo = new e_Metodo();
                                Parametros = new List<e_ParametrosWF>();
                                Metodo = ObtenerMetodo(DR);
                                idMetodoAccion = ObtenerMetodo(DR).idMetodoAccion.ToString(); //esto sirve solo para identidicar el metodo cuando se valide en cual metodo se esta
                                Parametros.Add(ObtenerParametro(DR));
                            }
                        }
                        else // Cambio la accion.
                        {
                            Metodo.Parametros = Parametros; //Se cargan los parametros al metodo finalizado
                            Metodos.Add(Metodo); //se crea nuevo metodo
                            Accion.Metodos = Metodos;
                            Acciones.Add(Accion);
                            Accion = new e_AccionFlujo();
                            Metodo = new e_Metodo();
                            Parametros = new List<e_ParametrosWF>();
                            Accion = ObtenerAccion(DR);
                            NombreAccion = Accion.Nombre;
                            Metodo = ObtenerMetodo(DR);
                            idMetodoAccion = Metodo.idMetodoAccion.ToString(); ; //esto sirve solo para identidicar el metodo cuando se valide en cual metodo se esta
                            Parametros.Add(ObtenerParametro(DR));
                        }
                    }
                }

                return Acciones;
            }
            catch (Exception)
            {
                return Acciones;
            }
        }

        #endregion //Acciones

        #region ManejoPalabras

        private static Diccionario ObtenerDiccionario(DataRow DR)
        {
            Diccionario DICC = new Diccionario();
            Palabra PLB = new Palabra();
            PLB.Nombre = DR[e_VistaPalabrasFields.DICCIONARIO()].ToString().ToUpper();
            PLB.Tipo = "Diccionario";
            PLB.Valor = "Diccionario";
            PLB.TieneNumeros = false;
            PLB.LargoMin = 1;
            PLB.LargoMax = 200;
            DICC._Diccionaro = PLB;
            return DICC;
        }

        private static Dependiente ObtenerDependiente(DataSet DataSetSinonimos, DataRow DR, string idMetodoAccion, string idUsuario)
        {
            Dependiente DEP = new Dependiente();
            Palabra PLB = new Palabra();
            List<Palabra> PLBs = new List<Palabra>();
            PLB.Nombre = DR[e_VistaPalabrasFields.Dependiente()].ToString().ToUpper();
            PLB.Tipo = "Dependiente";
            PLB.Valor = "Dependiente";
            PLB.TieneNumeros = false;
            PLB.LargoMin = 1;
            PLB.LargoMax = 200;
            DEP._Dependiente = PLB;
            DEP.Sinonimos = CargarSinonimos(DataSetSinonimos, PLB, idMetodoAccion, idUsuario);
            return DEP;
        }

        private static List<Palabra> CargarSinonimos(DataSet DS, Palabra PLB, string idMetodoAccion, string idUsuario)
        {
            List<Palabra> Sinonimos = new List<Palabra>();
            try
            {
                foreach (DataRow DR in DS.Tables[0].Rows)
                {
                    if (DR["Palabra"].ToString().ToUpper() == PLB.Nombre.ToUpper())
                    {
                        Palabra PLB2 = new Palabra();
                        PLB2.Nombre = DR["Sinonimo"].ToString().ToUpper();
                        PLB2.Tipo = "Sinonimo";
                        PLB2.Valor = PLB.Nombre;
                        PLB2.TieneNumeros = false;
                        PLB2.LargoMin = 1;
                        PLB2.LargoMax = 200;
                        Sinonimos.Add(PLB2);
                    }
                }
                return Sinonimos;
            }
            catch (Exception)
            {
                return Sinonimos;
            }
        }

        private static Independiente ObtenerIndependiente(DataSet DataSetSinonimos, DataRow DR, string idMetodoAccion, string idUsuario)
        {
            Independiente IND = new Independiente();
            Palabra PLB = new Palabra();
            List<string> Valores = new List<string>();
            PLB.Nombre = DR[e_VistaPalabrasFields.Independiente()].ToString().ToUpper();
            PLB.Tipo = "Independiente";
            try
            {
                PLB.Valor = DR["Valor"].ToString().ToUpper();
            }
            catch (Exception)
            {
                PLB.Valor = "Independiente";
            }
           
            PLB.Precision = int.Parse(DR[e_VistaPalabrasFields.Precision()].ToString());
            PLB.TieneNumeros = (PLB.Precision > 0);
            PLB.LargoMin = 1;
            PLB.LargoMax = int.Parse(DR[e_VistaPalabrasFields.max_length()].ToString());
            PLB.is_identity = bool.Parse(DR[e_VistaPalabrasFields.is_identity()].ToString());
            PLB.Valores = Valores;
            IND._Independiente = PLB;
            IND.Sinonimos = CargarSinonimos(DataSetSinonimos, PLB, idMetodoAccion, idUsuario);
            return IND;
        }

        private bool ObtenerValores(DataSet DataSetObjetos, Palabra PLB)
        {
            bool respuesta = false;
            try
            {
                return respuesta;
            }
            catch (Exception)
            {
                return respuesta;
            }
        }

        public static DataSet ObtenerDataSetPalabras(string And)
        {
            try
            {
                DataSet DS = new DataSet();
                List<string> Campos = new List<string>();
                Type type = typeof(e_VistaPalabrasFields);
                Campos = VariablesDeLaClass(type);
                string SQL = "select ";
                foreach (string str in Campos)
                {
                    SQL += str.ToUpper() + ",";
                }
                SQL = SQL.Substring(0, SQL.Length - 1);
                SQL += " from " + e_TablasBaseDatos.VistaPalabras();

                if (!string.IsNullOrEmpty(And.Trim()))
                {
                    SQL += " " + And;
                }
                SQL += " order by " + e_VistaPalabrasFields.DICCIONARIO() + "," + e_VistaPalabrasFields.Dependiente() + ", " + e_VistaPalabrasFields.Independiente();
                DS = da_ConsultaDummy.GetDataSet(SQL, "MD");
                return DS;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static DataSet ObtenerDataSetRespuestas(string And)
        {
            try
            {
                DataSet DS = new DataSet();
                List<string> Campos = new List<string>();
                Type type = typeof(e_TablaObjetoHistoria);
                Campos = VariablesDeLaClass(type);
                string SQL = "select ";
                foreach (string str in Campos)
                {
                    SQL += str.ToUpper() + ",";
                }
                SQL = SQL.Substring(0, SQL.Length - 1);
                SQL += " from " + e_TablasBaseDatos.TBLMDObjetoHistoria();

                if (!string.IsNullOrEmpty(And.Trim()))
                {
                    SQL += " " + And;
                }
                SQL += " order by " + e_TablaObjetoHistoria.DICCIONARIO() + "," + e_TablaObjetoHistoria.Dependiente() + ", " + e_TablaObjetoHistoria.Independiente();
                DS = da_ConsultaDummy.GetDataSet(SQL, "MD");
                return DS;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static List<Diccionario> ObtenerDiccionarios(string idUsuario, string BaseDatos, string idMetodoAccion)
        {
            List<Diccionario> Diccionarios = new List<Diccionario>();
            double Interacion = 0;
            DataSet DS = new DataSet();
            string where = String.Empty;
            if (!string.IsNullOrEmpty(BaseDatos.Trim()))
            {
               where = " where " + e_VistaPalabrasFields.DICCIONARIO().ToUpper() + " = '" + BaseDatos + "' ";
            }
            try
            {
                DS = ObtenerDataSetPalabras(where);
                Diccionario _Diccionario = new Diccionario();
                
                List<Dependiente> Dependientes = new List<Dependiente>();
                Dependiente Dependiente = new Dependiente();
                List<Independiente> Independientes = new List<Independiente>();
                /// Carga los parametros 
                /// Primero Nivel = Diccionario   //Ubicacion
                /// Segundo Nivel = Dependiente   //Articulo
                /// Tercer  Nivel = Independiente //CantEstados
                string NombreDiccionario = "";
                string NombreDependiente = "";
                DataSet DataSetSinonimos = new DataSet();
                DataSet DataSetIndependientes = new DataSet();
                string SQL = "select Palabra, Sinonimo from Sinonimos where idMetodoAccion = '" + idMetodoAccion + "'";
                DataSetSinonimos = da_ConsultaDummy.GetDataSet(SQL, idUsuario);
                foreach (DataRow DR in DS.Tables[0].Rows)
                {
                    Interacion++;
                    if (DR.Table.Rows.IndexOf(DR) == 0)
                    {
                        /// si se ingresa aqui o cambio de metodo o es la primea vez y se tiene que cargar todo... 
                        /// Accion, Metodo y Parametro
                        _Diccionario = ObtenerDiccionario(DR);
                        NombreDiccionario = _Diccionario._Diccionaro.Nombre.ToUpper();
                        Dependiente = ObtenerDependiente(DataSetSinonimos, DR, idMetodoAccion, idUsuario);
                        NombreDependiente = Dependiente._Dependiente.Nombre.ToUpper();
                        Independientes.Add(ObtenerIndependiente(DataSetSinonimos, DR, idMetodoAccion, idUsuario));
                        if (DR.Table.Rows.IndexOf(DR) == DR.Table.Rows.Count - 1)
                        {
                            //Independientes.Add(ObtenerIndependiente(DataSetSinonimos, DataSetIndependientes, DR, idMetodoAccion, idUsuario));
                            Dependiente.Independientes = Independientes;
                            Dependientes.Add(Dependiente);
                            _Diccionario.Dependientes = Dependientes;
                            Diccionarios.Add(_Diccionario);
                        }
                    }
                    else// si no es la primera
                    {
                        if (DR[e_VistaPalabrasFields.DICCIONARIO()].ToString().ToUpper() == NombreDiccionario.ToUpper())
                        {
                            if (DR[e_VistaPalabrasFields.Dependiente()].ToString().ToUpper() == NombreDependiente.ToUpper())//si el metodo a cargar es el mismo se vuelve a cargar parametro
                            {
                                if (DR.Table.Rows.IndexOf(DR) == DR.Table.Rows.Count - 1) //Ultima vez que se ejecuta el ciclo
                                {
                                    NombreDependiente = Dependiente._Dependiente.Nombre.ToUpper();  //esto sirve solo para identidicar el metodo cuando se valide en cual metodo se esta
                                    Independientes.Add(ObtenerIndependiente(DataSetSinonimos, DR, idMetodoAccion, idUsuario));
                                    Dependiente.Independientes = Independientes;
                                    Dependientes.Add(Dependiente); //se crea nuevo articulo
                                    _Diccionario.Dependientes = Dependientes;
                                    Diccionarios.Add(_Diccionario);
                                }
                                else
                                {
                                    Independientes.Add(ObtenerIndependiente(DataSetSinonimos, DR, idMetodoAccion, idUsuario));
                                }

                            }
                            else //si el articulo a cargar NO es el mismo se asignan la cantEstado al articulo anterior y se cargan nuevos CantEstado
                            {
                                Dependiente.Independientes = Independientes; //Se cargan las CantEstado al articulo finalizado
                                Dependientes.Add(Dependiente); //se crea nuevo metodo
                                Dependiente = new Dependiente();
                                Independientes = new List<Independiente>();
                                Dependiente = ObtenerDependiente(DataSetSinonimos, DR, idMetodoAccion, idUsuario);
                                NombreDependiente = Dependiente._Dependiente.Nombre.ToUpper();  //esto sirve solo para identidicar el metodo cuando se valide en cual metodo se esta
                                Independientes.Add(ObtenerIndependiente(DataSetSinonimos, DR, idMetodoAccion, idUsuario));
                            }
                        }
                        else // Cambio la ubicacion.
                        {
                            Dependiente.Independientes = Independientes; //Se cargan los parametros al metodo finalizado
                            Dependientes.Add(Dependiente);//se crea nuevo metodo
                            _Diccionario.Dependientes = Dependientes;
                            Diccionarios.Add(_Diccionario);
                            Dependientes = new List<Dependiente>();
                            _Diccionario = new Diccionario();
                            Dependiente = new Dependiente();
                            Independientes = new List<Independiente>();
                            _Diccionario = ObtenerDiccionario(DR);
                            NombreDiccionario = _Diccionario._Diccionaro.Nombre.ToUpper();
                            Dependiente = ObtenerDependiente(DataSetSinonimos, DR, idMetodoAccion, idUsuario);
                            NombreDependiente = Dependiente._Dependiente.Nombre.ToUpper();//esto sirve solo para identidicar el metodo cuando se valide en cual metodo se esta
                            // esto pasa cuando cambia de ubicacion en el utlimo registro
                            if (DR.Table.Rows.IndexOf(DR) == DR.Table.Rows.Count - 1)
                            {
                                /// Primero Nivel = Diccionario   //Ubicacion
                                /// Segundo Nivel = Dependiente   //Articulo
                                /// Tercer  Nivel = Independiente //CantEstados
                                Independientes.Add(ObtenerIndependiente(DataSetSinonimos, DR, idMetodoAccion, idUsuario));
                                Dependiente.Independientes = Independientes;
                                Dependientes.Add(Dependiente);
                                _Diccionario.Dependientes = Dependientes;
                                Diccionarios.Add(_Diccionario);
                            }
                        }
                    }
                }
                return Diccionarios;
            }
            catch (Exception)
            {
                return Diccionarios;
            }
        }

        public static List<Diccionario> ObtenerDiccionariosRespuestas(string idUsuario, string BaseDatos, string idMetodoAccion)
        {
            List<Diccionario> Diccionarios = new List<Diccionario>();
            double Interacion = 0;
            DataSet DS = new DataSet();
            string where = String.Empty;
            if (!string.IsNullOrEmpty(BaseDatos.Trim()))
            {
                where = " where " + e_VistaPalabrasFields.DICCIONARIO().ToUpper() + " = '" + BaseDatos + "' ";
            }
            try
            {
                DS = ObtenerDataSetRespuestas(where);
                Diccionario _Diccionario = new Diccionario();

                List<Dependiente> Dependientes = new List<Dependiente>();
                Dependiente Dependiente = new Dependiente();
                List<Independiente> Independientes = new List<Independiente>();
                /// Carga los parametros 
                /// Primero Nivel = Diccionario   //Ubicacion
                /// Segundo Nivel = Dependiente   //Articulo
                /// Tercer  Nivel = Independiente //CantEstados
                string NombreDiccionario = "";
                string NombreDependiente = "";
                DataSet DataSetSinonimos = new DataSet();
                DataSet DataSetIndependientes = new DataSet();
                string SQL = "select Palabra, Sinonimo from Sinonimos where idMetodoAccion = '" + idMetodoAccion + "'";
                DataSetSinonimos = da_ConsultaDummy.GetDataSet(SQL, idUsuario);
                foreach (DataRow DR in DS.Tables[0].Rows)
                {
                    Interacion++;
                    if (DR.Table.Rows.IndexOf(DR) == 0)
                    {
                        /// si se ingresa aqui o cambio de metodo o es la primea vez y se tiene que cargar todo... 
                        /// Accion, Metodo y Parametro
                        _Diccionario = ObtenerDiccionario(DR);
                        NombreDiccionario = _Diccionario._Diccionaro.Nombre.ToUpper();
                        Dependiente = ObtenerDependiente(DataSetSinonimos, DR, idMetodoAccion, idUsuario);
                        NombreDependiente = Dependiente._Dependiente.Nombre.ToUpper();
                        Independientes.Add(ObtenerIndependiente(DataSetSinonimos, DR, idMetodoAccion, idUsuario));
                        if (DR.Table.Rows.IndexOf(DR) == DR.Table.Rows.Count - 1)
                        {
                            //Independientes.Add(ObtenerIndependiente(DataSetSinonimos, DataSetIndependientes, DR, idMetodoAccion, idUsuario));
                            Dependiente.Independientes = Independientes;
                            Dependientes.Add(Dependiente);
                            _Diccionario.Dependientes = Dependientes;
                            Diccionarios.Add(_Diccionario);
                        }
                    }
                    else// si no es la primera
                    {
                        if (DR[e_VistaPalabrasFields.DICCIONARIO()].ToString().ToUpper() == NombreDiccionario.ToUpper())
                        {
                            if (DR[e_VistaPalabrasFields.Dependiente()].ToString().ToUpper() == NombreDependiente.ToUpper())//si el metodo a cargar es el mismo se vuelve a cargar parametro
                            {
                                if (DR.Table.Rows.IndexOf(DR) == DR.Table.Rows.Count - 1) //Ultima vez que se ejecuta el ciclo
                                {
                                    NombreDependiente = Dependiente._Dependiente.Nombre.ToUpper();  //esto sirve solo para identidicar el metodo cuando se valide en cual metodo se esta
                                    Independientes.Add(ObtenerIndependiente(DataSetSinonimos, DR, idMetodoAccion, idUsuario));
                                    Dependiente.Independientes = Independientes;
                                    Dependientes.Add(Dependiente); //se crea nuevo articulo
                                    _Diccionario.Dependientes = Dependientes;
                                    Diccionarios.Add(_Diccionario);
                                }
                                else
                                {
                                    Independientes.Add(ObtenerIndependiente(DataSetSinonimos, DR, idMetodoAccion, idUsuario));
                                }

                            }
                            else //si el articulo a cargar NO es el mismo se asignan la cantEstado al articulo anterior y se cargan nuevos CantEstado
                            {
                                Dependiente.Independientes = Independientes; //Se cargan las CantEstado al articulo finalizado
                                Dependientes.Add(Dependiente); //se crea nuevo metodo
                                Dependiente = new Dependiente();
                                Independientes = new List<Independiente>();
                                Dependiente = ObtenerDependiente(DataSetSinonimos, DR, idMetodoAccion, idUsuario);
                                NombreDependiente = Dependiente._Dependiente.Nombre.ToUpper();  //esto sirve solo para identidicar el metodo cuando se valide en cual metodo se esta
                                Independientes.Add(ObtenerIndependiente(DataSetSinonimos, DR, idMetodoAccion, idUsuario));
                                if (DR.Table.Rows.IndexOf(DR) == DR.Table.Rows.Count - 1)
                                {
                                    /// Primero Nivel = Diccionario   //Ubicacion
                                    /// Segundo Nivel = Dependiente   //Articulo
                                    /// Tercer  Nivel = Independiente //CantEstados
                                    Independientes.Add(ObtenerIndependiente(DataSetSinonimos, DR, idMetodoAccion, idUsuario));
                                    Dependiente.Independientes = Independientes;
                                    Dependientes.Add(Dependiente);
                                    _Diccionario.Dependientes = Dependientes;
                                    Diccionarios.Add(_Diccionario);
                                }
                            }
                        }
                        else // Cambio la ubicacion.
                        {
                            Dependiente.Independientes = Independientes; //Se cargan los parametros al metodo finalizado
                            Dependientes.Add(Dependiente);//se crea nuevo metodo
                            _Diccionario.Dependientes = Dependientes;
                            Diccionarios.Add(_Diccionario);
                            Dependientes = new List<Dependiente>();
                            _Diccionario = new Diccionario();
                            Dependiente = new Dependiente();
                            Independientes = new List<Independiente>();
                            _Diccionario = ObtenerDiccionario(DR);
                            NombreDiccionario = _Diccionario._Diccionaro.Nombre.ToUpper();
                            Dependiente = ObtenerDependiente(DataSetSinonimos, DR, idMetodoAccion, idUsuario);
                            NombreDependiente = Dependiente._Dependiente.Nombre.ToUpper();//esto sirve solo para identidicar el metodo cuando se valide en cual metodo se esta
                            // esto pasa cuando cambia de ubicacion en el utlimo registro
                            if (DR.Table.Rows.IndexOf(DR) == DR.Table.Rows.Count - 1)
                            {
                                /// Primero Nivel = Diccionario   //Ubicacion
                                /// Segundo Nivel = Dependiente   //Articulo
                                /// Tercer  Nivel = Independiente //CantEstados
                                Independientes.Add(ObtenerIndependiente(DataSetSinonimos, DR, idMetodoAccion, idUsuario));
                                Dependiente.Independientes = Independientes;
                                Dependientes.Add(Dependiente);
                                _Diccionario.Dependientes = Dependientes;
                                Diccionarios.Add(_Diccionario);
                            }
                        }
                    }
                }
              
                return Diccionarios;
            }
            catch (Exception)
            {
                return Diccionarios;
            }
        }
       
        ///////////////////////////////////////////////////////////////////////////////////////
        public static string DevolverRespuestaText(string Consulta, string Dependiente, string Independiente, string idUsuario)
        {
            string respuesta = "Ops no se pudo configurar la respuesta correctamente! intente mas tarde...Codigo:TID-AD-MDS-000001";
            try
            {
                // D = Query;
                // x = Preguntas
                // D = x
                // D = A + B + C ...
                // se ingresa la pregunta
                DataSet DS = new DataSet();
                List<Respuesta> _Respuestas = new List<Respuesta>();
                List<Pregunta> _Preguntas = new List<Pregunta>();
                List<Palabra> _Palabras = new List<Palabra>();
                Respuesta _Respuesta = new Respuesta();
                Pregunta _Pregunta = new Pregunta();
                Palabra _Palabra = new Palabra();
                Palabra PalabraFinal = new Palabra();
                int largoresp = respuesta.Length;
                string SiPalabraEsNumero = " ";
                string SiPalabraEsTexto = " ";
                int VecesRepetir = 0;
                string Espacios = "";
                decimal DecNum;
                bool EsNumero = false;

                PalabraFinal.Nombre = "Final de carril";
                PalabraFinal.Valor = "\n";
                DS = da_ConsultaDummy.GetDataSet(Consulta.ToUpper(), idUsuario);

                if (DS != null)
                {
                    // Busca los resultados de la pregunta original o Query que es consulta                   
                    foreach (DataRow DR in DS.Tables[0].Rows)// esta sea la cantidad de preguntas que tuvieron respuesta correcta
                    {
                        _Palabras = new List<Palabra>();
                        _Pregunta = new Pregunta();
                        _Respuesta = new Respuesta();
                        #region BuscarRespuestaPorCada Palabra
                        foreach (DataColumn COLUMNA in DS.Tables[0].Columns)
                        {
                            string PalabraNombre = COLUMNA.ColumnName.ToString();
                            _Palabra = new Palabra();
                            _Palabra.Nombre = PalabraNombre;
                            _Palabra.Valor = DR[_Palabra.Nombre].ToString();
                            EsNumero = decimal.TryParse(_Palabra.Valor.ToString(), out DecNum);
                            if (EsNumero)
                            {
                                _Palabra.NumeroDecimal = DecNum;
                                int LargoMax = 6;
                                if (_Palabra.Valor.Length > LargoMax) LargoMax = _Palabra.Valor.Length;
                                _Palabra.LargoMax = LargoMax;
                                _Palabra.TieneNumeros = EsNumero;
                            }
                            else
                            {
                                int LargoMax = 30;
                                _Palabra.LargoMax = LargoMax;
                                if (_Palabra.Valor.Length > LargoMax) LargoMax = _Palabra.Valor.Length;
                                _Palabra.LargoMax = LargoMax;
                                _Palabra.TieneNumeros = EsNumero;
                            }
                            VecesRepetir = _Palabra.LargoMax - _Palabra.Valor.Length;
                            Espacios = "";
                            if (_Palabra.TieneNumeros == true)
                            {
                                Espacios = string.Concat(Enumerable.Repeat(SiPalabraEsNumero, VecesRepetir));
                                _Palabra.Valor = Espacios + _Palabra.Valor;
                            }
                            else
                            {
                                Espacios = string.Concat(Enumerable.Repeat(SiPalabraEsTexto, VecesRepetir));
                                _Palabra.Valor = _Palabra.Valor + Espacios;
                            }

                            _Palabras.Add(_Palabra);
                            _Respuesta.Enunciado = "El Valor para = " + _Palabra.Nombre + " es: " + _Palabra.Valor;
                            _Pregunta.Enunciado = "Cual es el valor para " + _Palabra.Nombre;
                        }
                        #endregion //BuscarRespuestaPorCada Palabra
                        _Pregunta.Palabras = _Palabras;
                        _Respuesta.Palabras = _Palabras;
                        _Respuestas.Add(_Respuesta);
                        _Preguntas.Add(_Pregunta);
                    }

                }
                respuesta = "";
                if (_Pregunta.Palabras.Count > 1)
                {
                    foreach (Palabra Word in _Pregunta.Palabras)
                    {
                        respuesta += Word.Nombre + "|";
                    }
                    respuesta = respuesta.TrimEnd('|');
                    respuesta += PalabraFinal.Valor;
                }

                /// El sistema debe buscar las respuestas mas semejantes a la pregunta,
                /// para sugerirle preguntas correctas que si tengan respuesta.
                /// 
                
                // D = A + B + C ...
                // Creacion de D1, D2, D3...
                // D1 = A1 + B1 + C1 .                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ..
                // D2 = A2 + B2 + C2 ...
                foreach (Respuesta Answer in _Respuestas)
                {
                    foreach (Palabra Word in Answer.Palabras)
                    {
                        respuesta += Word.Valor + "|";
                    }
                    respuesta = respuesta.TrimEnd('|');
                    respuesta += PalabraFinal.Valor;
                }

                if (respuesta != String.Empty)
                {
                    string SQL = "insert into MDObjetoHistoria(Dependiente, Independiente,Valor)";
                    SQL += " values ('" + Dependiente + "', '" + Independiente + "','" + respuesta + "')";
                    da_ConsultaDummy.PushData(SQL, idUsuario);
                }
                return respuesta;

            }
            catch (Exception)
            {
                return respuesta;
            }

        }

        public static bool BuscarRespuestaEnMemoria(string Dependiente, bool RefrescarMemoria, string idUsuario, string BaseDatos, string idMetodoAccion, out string Respuesta)
        {
            bool resultado = false;
            string respuesta = String.Empty;
            string independiente = String.Empty;
            List<Diccionario> Diccionarios = new List<Diccionario>();
            if (RefrescarMemoria)
            { independiente = "SQL"; }
            else
            { independiente = "TEXTO"; }
            Diccionarios = ObtenerDiccionariosRespuestas(idUsuario, BaseDatos, idMetodoAccion);
            try
            {
                foreach (Diccionario DIC in Diccionarios)  //2
                {
                    foreach (Dependiente DEP in DIC.Dependientes)  //3
                    {
                        if (DEP._Dependiente.Nombre.ToUpper() == Dependiente.ToUpper())
                        {
                            foreach (Independiente IDE in DEP.Independientes)
                            {
                                if (IDE._Independiente.Nombre.ToUpper() == independiente.ToUpper())
                                {
                                    if (IDE._Independiente.Valor != String.Empty)
                                    {
                                        respuesta = IDE._Independiente.Valor;
                                        resultado = true;
                                        break; 
                                    }
                                }
                            }
                           
                        }
                    }
                }
               

                Respuesta = respuesta;
                return resultado;
            }
            catch (Exception)
            {
                Respuesta = respuesta;
                return resultado;

            }
        }

        private static string EnseñarAlMotor(string[] Leccion, string idMetodoAccion, string idUsuario)
        {
            // ENSEÑAR; SINONIMO ADMMAESTROUBICACION = ubicaciones
            /// Leccion[1] = "SINONIMO"
            /// Leccion[2] = "ADMMAESTROUBICACION"
            /// Leccion[3] = "="
            /// Leccion[4] = "ubicaciones"
            string respuesta = "Ummm por alguna razon no le entendi, podria explicarse mejor...Codigo:TID-AD-MDS-000002";
            string BD = "TRACEID";
            try
            {
                if (Leccion[1].ToUpper() == "SINONIMO")
                {
                    if (Leccion[3] == "=")
                    {
                        string SQL = "";
                        DataSet DS = new DataSet();
                        SQL = "select top 1 * from MDSinonimo where idMetodoAccion = '" + idMetodoAccion + "'";
                        SQL += " and Palabra = '" + Leccion[2] + "' and Sinonimo = '" + Leccion[4] + "'";
                        DS = da_ConsultaDummy.GetDataSet(SQL, idUsuario);
                        if (DS.Tables[0].Rows.Count == 0)
                        {
                            SQL = "insert into MDSinonimo(idMDTipoPalabra, idMDContexto, idMetodoAccion, ";
                            SQL += "Palabra, Sinonimo)";
                            SQL += " values(1,1," + idMetodoAccion + ",'" + Leccion[2] + "','" + Leccion[4] + "')";
                            da_ConsultaDummy.PushData(SQL, idUsuario);
                            respuesta = "Eureka! Me enseñaste algo nuevo, tienes 15 puntos";
                        }
                        else
                        {
                            respuesta = "Gracias, esto ya lo sabia, gracias por reforzarlo, tienes 10 pts";
                        }
                    }
 
                }
                 
                 if (Leccion[1].ToUpper() == "AGREGAR")
                 {  
                    List<Diccionario> Diccio = new List<Diccionario>();
                    Diccio = ObtenerDiccionarios(idUsuario ,BD ,idMetodoAccion) ;
                    string SinonimoPalabraIndependiente = "";
                    string Tabla = "";
                    string Campo = "";
                    string SQL = "";
                    string elemento = "";
                    string Palabra = Leccion[2].ToString();
                     
                    //busca la palabra dependiente(Tabla)
                    foreach (Diccionario DIC in Diccio) 
                    {  
                      foreach (Dependiente DEP in DIC.Dependientes) 
                      {  
                        foreach (Palabra PLB in DEP.Sinonimos) 
                        {  
                          if (PLB.Nombre.ToUpper() == Palabra.ToUpper())
                          {  
                             Tabla  = DEP._Dependiente.Nombre;
                             break;
                          }
                        }
                      }
                    } 
                    // con este bucle se arma el insert into... para poder insertar varios campos a la vez
                    int elementos = Leccion.Count();
                    for (int x = 3; x <= (elementos - 1); x++)
                    {
                        string[] CampoElemento = Leccion[x].Split(':', ' ');  // divide el nombre del campo y el elemento a agregar
                        // devuelve el campo
                        if (VerificarSiPalabraIndependienteTieneSinonimo(Diccio, Tabla.ToUpper(), CampoElemento[0].ToUpper(), out SinonimoPalabraIndependiente))
                        {
                            Campo += SinonimoPalabraIndependiente;
                            elemento += "'" + CampoElemento[1] + "'";
                        }
                        else  // en caso de que la función devuelva false
                        {
                            Campo += CampoElemento[0];
                            elemento += "'" + CampoElemento[1] + "'";
                    }
 
                        if (x < (elementos -1))  // se concatena para igualar la sintaxis de SQL para varios campos
                        {
                            Campo += ',';
                            elemento += ',';
                        }
                }
                    SQL = "insert into " + Tabla + "(" + Campo + ")     values (" + elemento + ")";  // arma la consulta SQl para insertar elemento.
                    if (da_ConsultaDummy.PushData(SQL, idUsuario))
                        respuesta = "Eureka! Me enseñaste algo nuevo, tienes 15 puntos";
                    else
                        respuesta = "!! No se logró completar la operación, revise la consulta !! Codigo:TID-AD-MDS-000003";

                }

                return respuesta;
            }
            catch (Exception)
            {
                return respuesta;
            }
        }

        private static string Memorizar(string EnunciadoMemorizar, string idMetodoAccion, string idUsuario)
        {
            string respuesta = String.Empty;
            string Pregunta =  String.Empty;
            string Respuesta = String.Empty;
            string NombrePregunta = String.Empty;
            bool EsPregunta = true;
            try
            {
                List<string> Palabras = new List<string>();
                Palabras = EnunciadoMemorizar.ToUpper().Split(' ').ToList();
                foreach (string PLB in Palabras)
                {
                    if (PLB.Trim() == "ES")
                    {
                        EsPregunta = false;
                    }
                    else
                    {
                        if (EsPregunta)
                        {
                            Pregunta += PLB + " ";
                        }
                        else
                        {
                            Respuesta += PLB + " ";
                        }
                    }

                }
                if (Palabras[0].ToUpper() == "COMO" || Palabras[0].ToUpper() == "QUE")
                {
                    NombrePregunta = String.Empty;
                }
                else
                {
                    NombrePregunta = "CUAL ES ";
                }
                Pregunta  = NombrePregunta  + Pregunta.TrimEnd(' ');
                Respuesta = Respuesta.TrimEnd(' ');

                string SQL = "insert into MDObjetoHistoria(Dependiente, Independiente,Valor)";
                SQL += " values ('" + Pregunta + "', 'TEXTO','" + Respuesta.ToUpper() + "')";
                da_ConsultaDummy.PushData(SQL, idUsuario);
                respuesta = Pregunta;
                return respuesta;
            }
            catch (Exception)
            {
                respuesta = "... no pude memorizar, intentarlo esbriendolo diferente, Codigo:TID-AD-MDS-000004";
                return respuesta;
            }
        }

        /// <summary>
        /// En una oracion la ultima palabra importante es la que debe contener la respuesta a las diferentes palabras de la oracion,
        /// pues por cada palabra antes de la posible fuente existe esa cantidad de posibles respuestas
        /// </summary>
        /// <param name="EnunciadoPregunta"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public static string ObtenerVarableIndependienteYDependiente(string EnunciadoPregunta, string idMetodoAccion, string idUsuario)
        {
            string respuesta = String.Empty;
            try
            {
                string Contexto1 = "";
                string DependienteContextoAnterior = "";
                string BaseDatos = "TRACEID";
                string PalabrasContexto = "";
                string Agrupar = "";
                string Condicion = string.Empty;
                string UnirContextos = " IN ";
                string Consulta = "";
                string PalabrasIndependientes = "";
                string PalabraDependiente = "";
                string PalabraAnalizar = "";
                string PalabraAntes, PalabraDespues;

                int FinalPalabraDependiente = 0;
                int PalabraIndependientesContexto = 0;

                decimal EsDecimal1 = 0;
                decimal EsDecimal2 = 0;

                bool HayPalabraUniversal = false;
                bool EsNumero1 = false;
                bool EsNumero2 = false;
                bool AprenderSQL = false;
                bool Pintar = false;
                bool RefrescarMemoria = false;
                bool CargarEnMemoria = false;

                List<string> PalabrasUnicas = new List<string>();
                List<Palabra> Palabras = new List<Palabra>();
                List<string> Contextos = new List<string>();
                List<Diccionario> Diccionarios = new List<Diccionario>();
                List<string> Condiciones = new List<string>();
                string PalabraRefrescar = "Refrescar";
                string PalabraMemorizar = "Memorizar";

                if (EnunciadoPregunta.Length > PalabraRefrescar.Length)
                {
                    if (EnunciadoPregunta.ToUpper().Substring(0, PalabraRefrescar.Length) == PalabraRefrescar.ToUpper())
                    {
                        EnunciadoPregunta = EnunciadoPregunta.Substring(PalabraRefrescar.Length + 1);
                        RefrescarMemoria = true;
                    }
                }

                if (EnunciadoPregunta.Length > PalabraMemorizar.Length)
                {
                    if (EnunciadoPregunta.ToUpper().Substring(0, PalabraMemorizar.Length) == PalabraMemorizar.ToUpper())
                    {
                        EnunciadoPregunta = EnunciadoPregunta.Substring(PalabraMemorizar.Length + 1);
                        CargarEnMemoria = true;
                    }
                }

                if (CargarEnMemoria)
                {
                    EnunciadoPregunta = Memorizar(EnunciadoPregunta, idMetodoAccion, idUsuario);
                }


                /*----------- FIN DECLARACION DE VARIABLES   ----------------*/
                if (!BuscarRespuestaEnMemoria(EnunciadoPregunta, RefrescarMemoria, idUsuario, BaseDatos, idMetodoAccion, out respuesta))
                {
                #region EnCasoNuevaPregunta
                    Diccionarios = ObtenerDiccionarios(idUsuario, BaseDatos, idMetodoAccion);
                    EnunciadoPregunta = EnunciadoPregunta.ToUpper();
                    Contextos = EnunciadoPregunta.Split(' ').ToList();                    
                    foreach (string Ctx in Contextos)
                    {
                        string[] NombrePalabras = Ctx.Split(' ');
                        if (Pintar == true)
                        {
                            EnunciadoPregunta = "ver de todos los colores cuando el color sea " + Ctx.TrimStart(' ').ToUpper();
                            EnunciadoPregunta = EnunciadoPregunta.ToUpper();
                            EnunciadoPregunta = EnunciadoPregunta.Replace(',', ' ');
                            EnunciadoPregunta = EnunciadoPregunta.TrimStart(' ');
                            NombrePalabras = EnunciadoPregunta.ToUpper().Split(' ');
                        }
                        else
                        {
                            NombrePalabras = EnunciadoPregunta.ToUpper().Split(' ');
                        }

                        if (Ctx.ToUpper().Trim() == "PINTAR")
                        {
                            Pintar = true;
                        }
                        if (Ctx.ToUpper().Trim() == "SQL")
                        {
                            AprenderSQL = true;
                        }
                        else
                        {
                        if (EnunciadoPregunta.ToUpper().Trim() == "ENSEÑAR")
                        {
                            // ENSEÑAR; SINONIMO ADMMAESTROUBICACION = ubicaciones
                            string[] Leccion = Contextos[1].Split(' ');
                            respuesta = EnseñarAlMotor(Leccion, idMetodoAccion, idUsuario);
                            break;
                        }
                        else
                        {
                       
                            #region Consultar
                            // Palabras Independientes(x) = A,B,C
                            // Palabra Dependiente = D
                            // n*D = n*(A + B + C... n)
                            //   D =   (A + B + C... n)
                            //   D = Sum(A...n)
                            //   D = A + Sum(n)
                            // x*n = A
                            //   D = x*n + Sum(n)
                            //   D = x*n(1+sum(1)
                            // D/x = 2(n)
                            // Cov(D/x) = 0
                            // 2(n) = 0
                            //  D = x
                            // Covarianza(D/A) = 0 No significa que el reciproco sea cierto o sea, Conv(H/B)= 0 no ser independientes

                            foreach (string Palabra in NombrePalabras.ToList())  //1
                            {
                                foreach (Diccionario DIC in Diccionarios)  //2
                                {
                                    foreach (Dependiente DEP in DIC.Dependientes)  //3
                                    {
                                        foreach (Palabra PLB in DEP.Sinonimos)  //4
                                        {
                                            if (PLB.Nombre.ToUpper() == Palabra.ToUpper())
                                            {
                                                PalabraDependiente = DEP._Dependiente.Nombre;
                                            }
                                        }
                                    }
                                }
                            }

                            #region PalabrasCondicionales
                            List<Palabra> PalabrasCondicionales = new List<Palabra>();
                            Palabra PalabraCondicional = new Palabra();
                            PalabraCondicional.Nombre = "IGUAL";
                            PalabraCondicional.Valor = "=";
                            PalabrasCondicionales.Add(PalabraCondicional);
                            PalabraCondicional = new Palabra();
                            PalabraCondicional.Nombre = "ES";
                            PalabraCondicional.Valor = "=";
                            PalabrasCondicionales.Add(PalabraCondicional);
                            PalabraCondicional = new Palabra();
                            PalabraCondicional.Nombre = "SEA";
                            PalabraCondicional.Valor = "=";
                            PalabrasCondicionales.Add(PalabraCondicional);
                            PalabraCondicional = new Palabra();
                            PalabraCondicional.Nombre = "TENGA";
                            PalabraCondicional.Valor = "LIKE";
                            PalabrasCondicionales.Add(PalabraCondicional);
                            PalabraCondicional = new Palabra();
                            PalabraCondicional.Nombre = "MENOR";
                            PalabraCondicional.Valor = "<";
                            PalabrasCondicionales.Add(PalabraCondicional);
                            PalabraCondicional = new Palabra();
                            PalabraCondicional.Nombre = "MAYOR";
                            PalabraCondicional.Valor = ">";
                            PalabrasCondicionales.Add(PalabraCondicional);
                            PalabraCondicional = new Palabra();
                            PalabraCondicional.Nombre = "MENORIGUAL";
                            PalabraCondicional.Valor = "<=";
                            PalabrasCondicionales.Add(PalabraCondicional);
                            PalabraCondicional = new Palabra();
                            PalabraCondicional.Nombre = "MAYORIGUAL";
                            PalabraCondicional.Valor = ">=";
                            PalabrasCondicionales.Add(PalabraCondicional);
                            PalabraCondicional = new Palabra();
                            PalabraCondicional.Nombre = "DIFERENTE";
                            PalabraCondicional.Valor = "<>";
                            PalabrasCondicionales.Add(PalabraCondicional);
                            if (FinalPalabraDependiente + 1 < NombrePalabras.Length)
                            {
                                for (int i = FinalPalabraDependiente + 1; i < NombrePalabras.Length; i++)
                                {
                                    PalabraAnalizar = NombrePalabras[i].ToString();
                                    foreach (Palabra _PalabraCondicional in PalabrasCondicionales)
                                    {
                                        if (PalabraAnalizar.ToUpper() == _PalabraCondicional.Valor.ToUpper() || PalabraAnalizar.ToUpper() == _PalabraCondicional.Nombre.ToUpper())
                                        {
                                            /// Hay que buscar la palabra antes y despues
                                            /// 
                                            string SinonimoPalabraIndependiente = "";
                                            PalabraAntes = NombrePalabras[i - 1].ToString().ToUpper();
                                            PalabraDespues = NombrePalabras[i + 1].ToString().ToUpper();
                                            if (VerificarSiPalabraIndependienteTieneSinonimo(Diccionarios, PalabraDependiente.ToUpper(), PalabraAntes.ToUpper(), out SinonimoPalabraIndependiente))
                                            {
                                                PalabraAntes = SinonimoPalabraIndependiente.ToUpper();
                                            }
                                            if (VerificarSiPalabraIndependienteTieneSinonimo(Diccionarios, PalabraDependiente.ToUpper(), PalabraDespues.ToString().ToUpper(), out SinonimoPalabraIndependiente))
                                            {
                                                PalabraDespues = SinonimoPalabraIndependiente.ToUpper();
                                            }
                                            EsNumero1 = decimal.TryParse(PalabraAntes, out EsDecimal1);
                                            EsNumero2 = decimal.TryParse(PalabraDespues, out EsDecimal2);
                                            string CondicionTemporal = String.Empty;
                                            if (EsNumero1 != EsNumero2)
                                            {
                                                if (EsNumero1)
                                                {
                                                    CondicionTemporal = PalabraDespues + _PalabraCondicional.Valor + "'" + PalabraAntes + "'";
                                                    Condicion = CondicionTemporal;
                                                }
                                                else
                                                {
                                                    CondicionTemporal = PalabraAntes + _PalabraCondicional.Valor + "'" + PalabraDespues + "'";
                                                    Condicion = CondicionTemporal;
                                                }
                                            }
                                            else
                                            {
                                                if (_PalabraCondicional.Nombre.ToUpper() == "TENGA")
                                                {
                                                    Condicion = PalabraAntes + " LIKE " + "'%" + PalabraDespues + "%'";
                                                }
                                                else
                                                {
                                                    Condicion = PalabraAntes + " LIKE " + "'" + PalabraDespues + "'";
                                                }

                                            }
                                            Condiciones.Add(Condicion);
                                        }//PalabraAnalizar.ToUpper() == _PalabraCondicional.Valor.ToUpper() || PalabraAnalizar.ToUpper() == _PalabraCondicional.Nombre.ToUpper())
                                    }//foreach (Palabra _PalabraCondicional in PalabrasCondicionales)
                                }//for (int i = FinalPalabraDependiente + 1; i < NombrePalabras.Length; i++)
                            }// if (FinalPalabraDependiente + 1 < NombrePalabras.Length)
                            #endregion // PalabrasCondicionales

                            #region ObtenerPalabrasIndependientes
                                PalabrasUnicas = NombrePalabras.Distinct().ToList();
                                foreach (string Palabra in PalabrasUnicas)
                                {
                                string SinonimoPalabraIndependiente = "";
                                if (VerificarSiPalabraEsContextoUniversal(Diccionarios, Palabra, out SinonimoPalabraIndependiente))
                                {
                                    PalabrasIndependientes += SinonimoPalabraIndependiente;
                                    if (SinonimoPalabraIndependiente.Contains('*'))
                                    {
                                        PalabrasIndependientes = SinonimoPalabraIndependiente;
                                        break;
                                    }
                                    else
                                    {
                                        HayPalabraUniversal = true;
                                    }
                                }

                                if (VerificarSiPalabraIndependienteTieneSinonimo(Diccionarios, PalabraDependiente.ToUpper(), Palabra, out SinonimoPalabraIndependiente))
                                {
                                    PalabraIndependientesContexto++;
                                    PalabrasContexto += SinonimoPalabraIndependiente.ToUpper() + ",";
                                    PalabrasIndependientes += "(" + SinonimoPalabraIndependiente + "),";

                                }
                                if (ValidaPalabraEnContexto(PalabraDependiente.ToUpper(), Palabra.ToUpper(), Diccionarios) &&
                                        SinonimoPalabraIndependiente.ToUpper() != Palabra.ToUpper())
                                {
                                    PalabraIndependientesContexto++;
                                    PalabrasContexto += Palabra + ",";
                                    PalabrasIndependientes += "(" + Palabra + "),";
                                }

                                }
                            #endregion //ObtenerPalabrasIndependientes

                                if (string.IsNullOrEmpty(DependienteContextoAnterior.Trim()))
                                {
                                    PalabrasContexto = PalabrasContexto.TrimEnd(',');
                                    PalabrasIndependientes = PalabrasIndependientes.TrimEnd(',');
                                    DependienteContextoAnterior = PalabraDependiente;
                                    Contexto1 = "select " + PalabrasIndependientes + " from " + PalabraDependiente;
                                    if (HayPalabraUniversal)
                                    {
                                        Agrupar = " group by " + PalabrasContexto;
                                    }
                                }
                                else
                                {
                                    PalabrasIndependientes = PalabrasIndependientes.TrimEnd(',');
                                    string PrimaryKey = DevolverPrimaryKey(Diccionarios, DependienteContextoAnterior);
                                    if (PalabrasIndependientes != "*")
                                    {
                                        if (DependienteContextoAnterior.ToUpper() != PalabraDependiente.ToUpper())
                                        {
                                            Condicion += Condicion + PrimaryKey + " " + UnirContextos + " (select " + PrimaryKey + " from " + PalabraDependiente + ")";
                                        }
                                    }
                                }
                                #endregion //Consultar
                                string ConsultaTemp = "";
                                ConsultaTemp = Contexto1;
                                if (ConsultaTemp != Consulta)
                                {
                                    Consulta = Contexto1;
                                }
                            }
                        }
                }
                Condiciones = Condiciones.Distinct().ToList();
                Condicion = String.Empty;
                foreach (string Condic in Condiciones)
                {
                    if (Condiciones.IndexOf(Condic) == 0)
                    {
                        Condicion += " where " + Condic;
                    }
                    else
                    {
                        Condicion += " and " + Condic;
                    }
                }
                Consulta = Consulta + Condicion + Agrupar;
                if (AprenderSQL || RefrescarMemoria) { respuesta = Consulta + "\n"; }
                else { respuesta += DevolverRespuestaText(Consulta, EnunciadoPregunta, "TEXTO", idUsuario); }
                #endregion 
                }

                if (RefrescarMemoria)
                {
                    respuesta = DevolverRespuestaText(respuesta, EnunciadoPregunta, "TEXTO", idUsuario);
                }

                if (respuesta.Trim() == String.Empty)
                {
                    respuesta = "Ops algo no le entendimos, intente de otra forma... yo entendi esto: " + Consulta;
                }
                else
                {
                    string SQL = "insert into MDObjetoHistoria(Dependiente, Independiente,Valor)";
                    SQL += " values ('" + EnunciadoPregunta + "', 'SQL','" + Consulta.ToUpper() + "')";
                    da_ConsultaDummy.PushData(SQL, idUsuario);
                }
                return respuesta;
            }
            catch (Exception)
            {
                respuesta = "Ops algo no le entendimos, intente de otra forma, Codigo:TID-AD-MDS-000005";
                return respuesta;
            }
        }

        public static bool VerificarSiPalabraIndependienteTieneSinonimo(List<Diccionario> Diccionarios,string PalabraDependiente, string PalabraIndependiende, out string Sinonimo)
        {
            bool respuesta = false;
            string sinonimo = "";
            try
            {
                foreach (Diccionario _Diccionario in Diccionarios) //2
                {
                    foreach (Dependiente _Dependiente in _Diccionario.Dependientes) //3
                    {
                        if (_Dependiente._Dependiente.Nombre.ToUpper() == PalabraDependiente.ToUpper())
                        {
                            if (_Dependiente._Dependiente.Nombre.ToUpper() == PalabraDependiente.ToUpper())
                            {
                                foreach (Independiente _Independiente in _Dependiente.Independientes)
                                {
                                    foreach(Palabra _Sinonimo in _Independiente.Sinonimos) //4
                                    {
                                        if (_Sinonimo.Nombre.ToUpper() == PalabraIndependiende.ToUpper() && _Sinonimo.Valor.ToUpper() != PalabraDependiente.ToUpper())
                                        {
                                            sinonimo = _Sinonimo.Valor;
                                            respuesta = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Sinonimo = sinonimo;
                return respuesta;
            }
            catch (Exception)
            {
                Sinonimo = "";
                return respuesta;

            }
        }

        public static bool VerificarSiPalabraEsContextoUniversal(List<Diccionario> Diccionarios, string PalabraIndependiende, out string Sinonimo)
        {
            bool respuesta = false;
            string sinonimo = "";
            try
            {
                foreach (Diccionario _Diccionario in Diccionarios)
                {
                    foreach (Dependiente _Dependiente in _Diccionario.Dependientes)
                    {
                        foreach (Palabra _Independiente in _Dependiente.Sinonimos)
                        {
                            if (_Independiente.Valor.ToUpper() == PalabraIndependiende.ToUpper() )
                            {
                                if (_Independiente.Valor != _Independiente.Nombre)
                                {
                                    sinonimo = _Independiente.Nombre;
                                    respuesta = true;
                                }
                            }
                        }
                    }
                }
                Sinonimo = sinonimo;
                return respuesta;
            }
            catch (Exception)
            {
                Sinonimo = "";
                return respuesta;
            
            }
        }

        public static bool ValidaPalabraEnContexto(string PalabraDependiente, string NombrePalabra, List<Diccionario> Diccionarios)
        {
            bool respuesta = false;
            foreach (Diccionario _Diccionario in Diccionarios)
            {
                foreach (Dependiente _Dependiente in _Diccionario.Dependientes)
                {
                    if (_Dependiente._Dependiente.Nombre.ToUpper() == PalabraDependiente.ToUpper())
                    {
                        foreach (Independiente _Independiente in _Dependiente.Independientes)
                        {
                            if (_Independiente._Independiente.Nombre.ToUpper() == NombrePalabra.ToUpper())
                            {
                                respuesta = true;
                                break;
                            }
                        }
                    }
                }
            }
            return respuesta;
 
        }

        public static string ValidaPalabraDependiente(List<Diccionario> Diccionarios, string NombrePalabra)
        {
            string respuesta = "";
           
            foreach (Diccionario _Diccionario in Diccionarios)
            {
                foreach (Dependiente _Dependiente in _Diccionario.Dependientes)
                {

                    if (_Dependiente._Dependiente.Nombre.ToUpper() == NombrePalabra)
                    {
                        respuesta = NombrePalabra;
                         break;
                    }
                }
            }
            return respuesta;
        }

        public static bool ValidaPalabraContieneNumeros(List<Diccionario> Diccionarios,  string PalabraDependiente, string NombrePalabra)
        {
            bool respuesta = false;
            foreach (Diccionario _Diccionario in Diccionarios)
            {
                foreach (Dependiente _Dependiente in _Diccionario.Dependientes)
                {
                    if (_Dependiente._Dependiente.Nombre.ToUpper() == PalabraDependiente.ToUpper())
                    {
                        foreach (Independiente _Independiente in _Dependiente.Independientes)
                        {
                            if (_Independiente._Independiente.Nombre.ToUpper() == NombrePalabra.ToUpper())
                            {
                                respuesta = _Independiente._Independiente.TieneNumeros;
                                break;
                            }
                        }
                    }
                }
            }
            return respuesta;
        }

        public static string DevolverPrimaryKey(List<Diccionario> Diccionarios, string PalabraDependiente)
        {
            string respuesta = "";
            foreach (Diccionario _Diccionario in Diccionarios)
            {
                foreach (Dependiente _Dependiente in _Diccionario.Dependientes)
                {
                    if (_Dependiente._Dependiente.Nombre.ToUpper() == PalabraDependiente.ToUpper())
                    {
                        foreach (Independiente _Independiente in _Dependiente.Independientes)
                        {
                            if (_Independiente._Independiente.is_identity == true)
                            {
                                respuesta = _Independiente._Independiente.Nombre;
                                break;
                            }
                        }
                    }
                }
            }
            return respuesta;
        }

        #endregion // Manejo Palabras

        #region AccionesServicio

        public static List<e_AccionFlujo> ObtenerAccionObjetoMD(string BaseDatos, string tabla)
        {
            List<e_AccionFlujo> Acciones = new List<e_AccionFlujo>();
            try
            {
                DataSet DS = new DataSet();
                List<string> Campos = new List<string>();
                Type type = typeof(e_VistaCamposAccion);
                string and = "";
                Campos = da_MotorDecision.VariablesDeLaClass(type);
                if (!string.IsNullOrEmpty(BaseDatos.Trim()))
                {
                    and = " and " + e_VistaCamposAccion.Fuente() + " = '" + BaseDatos + "' and " + e_VistaCamposAccion.ObjetoFuente() + " = '" + tabla + "'";
                }

                DS = ObtenerDataSetAcciones(and);
                e_AccionFlujo Accion = new e_AccionFlujo();
                List<e_ParametrosWF> Parametros = new List<e_ParametrosWF>();
                e_Metodo Metodo = new e_Metodo();
                List<e_Metodo> Metodos = new List<e_Metodo>();
                ///Carga los parametros 
                ///
                int contadorMetodo = 0;
                string NombreMetodo = "";
                foreach (DataRow DR in DS.Tables[0].Rows)
                {
                    if (DR.Table.Rows.IndexOf(DR) == 0)
                    {
                        ///si se ingresa aqui o cambio de metodo o es la primea vez y se tiene que cargar todo... Accion, Metodo y Parametro

                        Accion = ObtenerAccion(DR);
                        Acciones.Add(Accion);
                        NombreMetodo = ObtenerMetodo(DR).Nombre; //esto sirve solo para identidicar el metodo cuando se valide en cual metodo se esta
                        Metodo = ObtenerMetodo(DR);
                        Parametros.Add(ObtenerParametro(DR));

                    }
                    else// si no es la primera
                    {

                        if (DR[e_VistaCamposAccion.NombreMetodo()].ToString() == NombreMetodo)//si el metodo a cargar es el mismo se vuelve a cargar parametro
                        {
                            if (DR.Table.Rows.IndexOf(DR) == DR.Table.Rows.Count - 1) //Ultima vez que se ejecuta el ciclo
                            {
                                Parametros.Add(ObtenerParametro(DR));
                                Metodo.Parametros = Parametros;
                                Metodos.Add(Metodo); //se crea nuevo metodo
                                Acciones[0].Metodos = Metodos;
                            }
                            else
                            {
                                Parametros.Add(ObtenerParametro(DR));
                            }

                        }
                        else //si el metodo a cargar NO es el mismo se asignan los parametros al metodo anterior y se cargan nuevoa paramtros
                        {
                            contadorMetodo++;
                            Metodo.Parametros = Parametros; //Se cargan los parametros al metodo finalizado
                            Metodos.Add(Metodo); //se crea nuevo metodo

                            Metodo = new e_Metodo();
                            Parametros = new List<e_ParametrosWF>();
                            Metodo = ObtenerMetodo(DR);
                            NombreMetodo = ObtenerMetodo(DR).Nombre; //esto sirve solo para identidicar el metodo cuando se valide en cual metodo se esta
                            Parametros.Add(ObtenerParametro(DR));

                        }
                    }
                }

                return Acciones;
            }
            catch (Exception)
            {
                return Acciones;
            }
        }

        public string ObtieneIdTablaEvento(string BaseDatos, string Tabla, string Evento, string CampoBuscado, string ValorDevolver, string idUsuario)
        {
            string respuesta = "";
            try
            {
                DataSet DSTran = new DataSet();
                //string PKId = "";
                string SQL = "SELECT TipoTrn, PKNombre, PKId, " + ValorDevolver + " FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + "LogTransacciones WHERE Procesado = 0 AND Tabla = '" + BaseDatos + "." + Tabla + "' AND TipoTrn = '" + Evento + "' AND Campo = '" + CampoBuscado + "' GROUP BY TipoTrn, Tabla, PKNombre, PKId, " + ValorDevolver + "";
                DSTran = da_ConsultaDummy.GetDataSet(SQL, idUsuario);
                respuesta = DSTran.Tables[0].Rows[0][ValorDevolver].ToString();


                return respuesta;
            }
            catch (Exception)
            {
                respuesta = "Ops! Ha ocurrido un Error,Codigo:TID-AD-MDS-000006";
                return respuesta;
            }
        }

        public static string AgregarTRAIngresoSalidaArticulos(string trama)
        {

            string[] sp = trama.Split(';');

            string SumUno_RestaCero = sp[0].ToString().Trim();
            string idArticulo = sp[1].ToString().Trim();
            string FechaVencimiento = sp[2].ToString().Trim();
            string Lote = sp[3].ToString().Trim();
            string idUsuario = sp[4].ToString().Trim();
            string idAccion = sp[5].ToString().Trim();
            string idTablaCampoDocumentoAccion = sp[6].ToString().Trim();
            string idCampoDocumentoAccion = sp[7].ToString().Trim();
            string NumDocumentoAccion = sp[8].ToString().Trim();
            string idUbicacion = sp[9].ToString().Trim();
            string Procesado = sp[10].ToString().Trim();
            string idEstado = sp[11].ToString().Trim();
            string Cantidad = sp[12].ToString().Trim();

            string SQL = "";
            try
            {
                SQL = "INSERT INTO " + e_TablasBaseDatos.TblTransaccion() + " (" + e_TblTransaccionFields.SumUno_RestaCero() + ", " + e_TblTransaccionFields.idArticulo() + ", " + e_TblTransaccionFields.FechaVencimiento() + ", " + e_TblTransaccionFields.Lote() + ", " + e_TblTransaccionFields.idUsuario() + ", " + e_TblTransaccionFields.idMetodoAccion() + ", " + e_TblTransaccionFields.idTablaCampoDocumentoAccion() + ", " + e_TblTransaccionFields.idCampoDocumentoAccion() + ", " + e_TblTransaccionFields.NumDocumentoAccion() + ", " + e_TblTransaccionFields.idUbicacion() + ", " + e_TblTransaccionFields.Cantidad() + ", " + e_TblTransaccionFields.Procesado() + ", " + e_TblTransaccionFields.idEstado() + ") VALUES('" + SumUno_RestaCero + "', '" + idArticulo + "', '" + FechaVencimiento + "', '" + Lote + "', '" + idUsuario + "', '" + idAccion + "', '" + idTablaCampoDocumentoAccion + "', '" + idCampoDocumentoAccion + "', '" + NumDocumentoAccion + "', '" + idUbicacion + "', '" + Cantidad + "', '" + Procesado + "', '" + idEstado + "')";
                if (da_ConsultaDummy.PushData(SQL, idUsuario))
                {
                    return "ok";
                }
                return "Ops! Ha ocurrido un Error,Codigo:TID-AD-MDS-000007";
            }
            catch (Exception)
            {
                return SQL;
            }
        }

        public static string AgregarTRAIngresoSalidaArticulos(string SumUno_RestaCero, string idArticulo, string FechaVencimiento, string Lote, string idUsuario, string idAccion, string idTablaCampoDocumentoAccion, string idCampoDocumentoAccion, string NumDocumentoAccion, string idUbicacion, string Cantidad, string Procesado, string idEstado)
        {
            string SQL = "";
            try
            {                
                SQL = "INSERT INTO " + e_TablasBaseDatos.TblTransaccion() + " (" + e_TblTransaccionFields.SumUno_RestaCero() + ", " + e_TblTransaccionFields.idArticulo() + ", " + e_TblTransaccionFields.FechaVencimiento() + ", " + e_TblTransaccionFields.Lote() + ", " + e_TblTransaccionFields.idUsuario() + ", " + e_TblTransaccionFields.idMetodoAccion() + ", " + e_TblTransaccionFields.idTablaCampoDocumentoAccion() + ", " + e_TblTransaccionFields.idCampoDocumentoAccion() + ", " + e_TblTransaccionFields.NumDocumentoAccion() + ", " + e_TblTransaccionFields.idUbicacion() + ", " + e_TblTransaccionFields.Cantidad() + ", " + e_TblTransaccionFields.Procesado() + ", " + e_TblTransaccionFields.idEstado() + ") VALUES('" + SumUno_RestaCero + "', '" + idArticulo + "', '" + FechaVencimiento + "', '" + Lote + "', '" + idUsuario + "', '" + idAccion + "', '" + idTablaCampoDocumentoAccion + "', '" + idCampoDocumentoAccion + "', '" + NumDocumentoAccion + "', '" + idUbicacion + "', '" + Cantidad + "', '" + Procesado + "', '" + idEstado + "')";
                if (da_ConsultaDummy.PushData(SQL, idUsuario))
                {
                    return "ok";
                }
                return "error";
            }
            catch (Exception)
            {
                return "Ops! Ha ocurrido un Error,Codigo:TID-AD-MDS-000008";
            }
        }

        #endregion //AccionesServicio

       
    }
}
