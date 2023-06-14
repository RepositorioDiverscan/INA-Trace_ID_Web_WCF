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
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.Negocio.MotorDecisiones;
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

namespace Diverscan.MJP.Negocio.GS1
{
    public class CargarEntidadesGS1
    {
        public static int GS1128_DigitoVerificadorSSCC(String sscc)
        {
            int resultado = 0;
            int Acumulado = 0;

            int ValorMultiplicar = 1;
            int suma = 0;
            int AcomodarDecena = 0;
            string codigo = "";
            try
            {
                codigo = sscc.ToString();

                for (int j = codigo.Length - 1; j > -1; j--)
                {
                    string letraChar = codigo[j].ToString();
                    int letra = Convert.ToInt32(letraChar);

                    if (ValorMultiplicar == 1)
                    {
                        Acumulado = letra * 3;

                        //Asigna nuevo Valor a multiplicar
                        ValorMultiplicar = 3;
                    }
                    else
                    {
                        Acumulado = letra * 1;

                        //Asigna nuevo Valor a multiplicar
                        ValorMultiplicar = 1;
                    }
                    suma = suma + Acumulado;

                }

                int UltimoDigito = int.Parse((suma.ToString().Substring(1)));
                if (UltimoDigito != 0)
                {
                    AcomodarDecena = 10 - UltimoDigito;
                }
                else
                {
                    AcomodarDecena = 0;
                }

                int ProxDecena = suma + AcomodarDecena;
                resultado = ProxDecena - suma;

                return resultado;
            }
            catch (Exception)
            {
                return resultado;
            }
        }
       
        public static int GS1128_DigitoVerificador(EntidadesGS1.e_GTIN GTIN)
        {
            int resultado = 0;
            int Acumulado = 0;
            int pos = 1;
            int evalua = 10;
            int resulta = 0;

            try
            {
                foreach (EntidadesGS1.e_Digito Dg in GTIN.Digitos)
                {
                    if (IsOdd(pos))
                    {
                        Acumulado += Dg.Valor * 3;
                    }
                    else
                    {
                        Acumulado += Dg.Valor * 1;
                    }
                    pos++;
                }

                for (int x = 1; x <= 100; x++)
                {
                    resulta = (evalua * x) - Acumulado;
                    if (resulta >= 0 && resulta < 10)
                        break;
                }

                return Math.Abs(resulta);
            }
            catch (Exception)
            {
                return resultado;
            }
        }


        public static string GS1128_DevolverFechaVencimiento(string CodLeido)
        {
            string respuesta = "";
            try
            {
                if (CodLeido.StartsWith("02"))
                {
                    CodLeido = "01" + CodLeido.Substring(2);
                }

                List<EntidadesGS1.e_IdentificadorAplicacion> AIs = new List<EntidadesGS1.e_IdentificadorAplicacion>();
                EntidadesGS1.e_IdentificadorAplicacion AI = new EntidadesGS1.e_IdentificadorAplicacion();
                AIs = GS1128_DevolverAIs(CodLeido);
                if (AIs != null)
                {
                    AI = AIs.Find(x => x.idIdentificadorAplicacion == "17");
                    if (AI != null)
                    {
                        DateTime FechaVenc;
                        respuesta = AI.ValorEncontrado;
                        string FechaVencTemp = respuesta;
                        string dia = respuesta.Substring(4);
                        if (dia == "00") dia = "01";
                        respuesta = "20" + respuesta.Substring(0, 2) + "-" + respuesta.Substring(2, 2) + "-" + dia;
                        bool EsFecha = DateTime.TryParse(respuesta, out FechaVenc);
                        if (!EsFecha) respuesta = "NULL";
                        if (FechaVencTemp.Substring(4) == "00")
                        {
                            dia = DateTime.DaysInMonth(FechaVenc.Year, FechaVenc.Month).ToString();
                            respuesta = "20" + FechaVencTemp.Substring(0, 2) + "-" + FechaVencTemp.Substring(2, 2) + "-" + dia;
                        }
                    }
                }
                return respuesta;
            }
            catch (Exception)
            {
                return respuesta;

            }
        }

        public static string GS1128_DevolveLote(string CodLeido)
        {
            string respuesta = "";
            try
            {
                if (CodLeido.StartsWith("02"))
                {
                    CodLeido = "01" + CodLeido.Substring(2);
                }

                List<EntidadesGS1.e_IdentificadorAplicacion> AIs = new List<EntidadesGS1.e_IdentificadorAplicacion>();
                EntidadesGS1.e_IdentificadorAplicacion AI = new EntidadesGS1.e_IdentificadorAplicacion();
                AIs = GS1128_DevolverAIs(CodLeido);
                if (AIs != null)
                {
                    AI = AIs.Find(x => x.idIdentificadorAplicacion == "10");
                    if (AI != null)
                    {
                        respuesta = AI.ValorEncontrado;
                    }
                }
                return respuesta;
            }
            catch (Exception)
            {
                return respuesta;

            }
        }

        public static string GS1128_DevolverInfoCodLeidoTxt(string CodLeido)
        {
            string respuesta = "";
            try
            {
                if (CodLeido.StartsWith("02"))
                {
                    CodLeido = "01" + CodLeido.Substring(2);
                }

                n_MotorDecisiones.Metodos MD = new n_MotorDecisiones.Metodos();
                List<string> NombresAIS = MD.NombresObjeto(typeof(EntidadesGS1.e_IdentificadorAplicacion));
                List<EntidadesGS1.e_IdentificadorAplicacion> EC = new List<EntidadesGS1.e_IdentificadorAplicacion>();
                EC = CargarEntidadesGS1.GS1128_DevolverAIs(CodLeido);
                foreach (EntidadesGS1.e_IdentificadorAplicacion AI in EC)
                {
                    foreach (string str in NombresAIS)
                    {
                        respuesta += str + ": " + AI.GetType().GetProperty(str).GetValue(AI, null) + "\n";
                    }
                    respuesta += "\n";

                }
                return respuesta;
            }
            catch (Exception)
            {
                return respuesta;

            }
        }

        public static string GS1128_ConvertirGTINaString(EntidadesGS1.e_GTIN GTIN)
        {
            string respuesta = "";
            try
            {
                respuesta = GTIN.VL.idVL.ToString();
                foreach (EntidadesGS1.e_Digito D in GTIN.Digitos)
                {
                    if (D.NumDigito > 1)
                        respuesta += D.Valor.ToString();
                }
                return respuesta;
            }
            catch (Exception)
            {
                return respuesta;
            }
        }

        /// <summary>
        /// Se deben enviar 14 digitos; este es el procedimiento que "rellena" todas las propiedades del GTIN.
        /// </summary>
        /// <param name="strGTIN"></param>
        /// <returns></returns>
        private static bool ObtenerGTIN(string strGTIN, out EntidadesGS1.e_GTIN GTIN)
        {
            EntidadesGS1.e_GTIN GT = new EntidadesGS1.e_GTIN();
            bool respuesta = false;
            try
            {
                if (strGTIN.Length == 14)
                {
                    Int64 numGTIN;
                    bool EsNumero = Int64.TryParse(strGTIN, out numGTIN);
                    int Pos = 1;
                    if (EsNumero)
                    {
                        List<EntidadesGS1.e_Digito> Digitos = new List<EntidadesGS1.e_Digito>();
                        foreach (Char CR in strGTIN.Substring(0, 13))
                        {
                            EntidadesGS1.e_Digito D = new EntidadesGS1.e_Digito();
                            D.NumDigito = Pos;
                            D.Valor = int.Parse(CR.ToString());
                            Digitos.Add(D);
                            GT.Digitos = Digitos;
                            Pos++;
                        }
                        EntidadesGS1.e_Digito D1 = new EntidadesGS1.e_Digito();
                        D1.NumDigito = Pos;
                        int DigVerificador = GS1128_DigitoVerificador(GT);
                        D1.Valor = DigVerificador;
                        Digitos.Add(D1);
                        GT.Digitos = Digitos;
                        GT.DigitoVerificador = DigVerificador;
                        EntidadesGS1.e_ValorLogistico VL = new EntidadesGS1.e_ValorLogistico();
                        List<EntidadesGS1.e_ValorLogistico> VLS = new List<EntidadesGS1.e_ValorLogistico>();
                        ///Aqui se debe buscar en base de datos por
                        ///pais, num compania, y articulo la representacion
                        ///del VL. Vamos a asumir 15 por mientras

                        string GTINBusqueda = string.Empty;
                        bool NoEntroUnUno = false;
                        bool SiEntroUnUno = true;
                        bool EntroUnUno = NoEntroUnUno;
                        int digitosGTIN = ValoresUsoComun.Digitos_GTIN;//int.Parse(ConfigurationManager.AppSettings["Digitos_GTIN"].ToString());
                        if (strGTIN.TrimStart('0').Trim().Length >= digitosGTIN)
                        {
                            GTINBusqueda = strGTIN.Substring(1);
                            //GTINBusqueda = strGTIN.TrimStart('0');
                            int GTINInicial = int.Parse (strGTIN.Substring(0,1));
                            
                            for (int contador = GTINInicial; contador >= 0; contador--)
                            {
                                VL = new EntidadesGS1.e_ValorLogistico();
                                Single  cant_ =  Single.Parse(GS1128_DevolverValorVariableLogistica(contador.ToString() +  GTINBusqueda));
                                if (EntroUnUno == NoEntroUnUno)
                                {
                                    if (contador >= 0)
                                    {
                                        VL.idVL = contador;
                                        VL.Cantidad = cant_;
                                        VL.Codigo = contador.ToString() + GTINBusqueda;
                                        VLS.Add(VL);
                                        if (cant_ == 1)
                                          EntroUnUno = SiEntroUnUno;
                                    }
                                }
                            }
                        }
                        //VL.idVL = 0;
                        if (VL.Cantidad < 1)
                          VL.Cantidad = 1;

                        GT.VLs = VLS;
                        GT.VL = VL;
                        string Gtin2string = GS1128_ConvertirGTINaString(GT);
                        if (Gtin2string == strGTIN)
                        {
                            GT.Tipo = GS1128_TipoGTIN(GT);
                        }
                        else
                        {
                            GT.Tipo = GS1128_TipoGTIN(GT) + "[NS]";
                        }

                        GT.ValorLeido = ObtenerGTIN13 (strGTIN);
                        respuesta = true;
                    }
                    else
                    {
                        respuesta = false;
                    }
                }
                else
                {
                    respuesta = false;
                }
                GTIN = GT;
                return respuesta;
            }
            catch (Exception)
            {
                GTIN = GT;
                return respuesta;
            }
        }

        private static string ObtenerGTIN13(string strGTIN)
        {
            string respuesta = "000";
            string SQL = "";

            try
            {
                if (strGTIN.Substring(0, 1) == "0")
                    strGTIN = strGTIN.Substring(1);

                SQL = "";
                SQL = "SELECT " + e_VistaRelacionGTIN13GTIN14.GTIN13() +
                      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaRelacionGTIN13GTIN14() +
                      "  WHERE " + e_VistaRelacionGTIN13GTIN14.GTIN13() + " = '" + strGTIN + "'" +
                      "         OR " + e_VistaRelacionGTIN13GTIN14.GTIN14() + " = '" + strGTIN + "'";
                respuesta = n_ConsultaDummy.GetUniqueValue(SQL, "0");

                if (string.IsNullOrEmpty(respuesta))
                    strGTIN = strGTIN.TrimStart('0');

                SQL = "";
                SQL = "SELECT " + e_VistaRelacionGTIN13GTIN14.GTIN13() +
                      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaRelacionGTIN13GTIN14() +
                      "  WHERE " + e_VistaRelacionGTIN13GTIN14.GTIN13() + " = '" + strGTIN + "'" +
                      "         OR " + e_VistaRelacionGTIN13GTIN14.GTIN14() + " = '" + strGTIN + "'";
                respuesta = n_ConsultaDummy.GetUniqueValue(SQL, "0");

                if (string.IsNullOrEmpty(respuesta))
                {
                   return "00000";
                }
                else
                {
                    return respuesta;
                }
            }
            catch
            {
               return  "00000";
            }
        }

        public static bool GS1128_ObtenerGTIN(string CodLeido, out EntidadesGS1.e_GTIN GTIN)
        {
            EntidadesGS1.e_GTIN GT = new EntidadesGS1.e_GTIN();
            bool respuesta = false;

            try
            {
                if (CodLeido.StartsWith("02"))
                {
                    CodLeido = "01" + CodLeido.Substring(2);
                }

                List<EntidadesGS1.e_IdentificadorAplicacion> AIs = new List<EntidadesGS1.e_IdentificadorAplicacion>();
                EntidadesGS1.e_IdentificadorAplicacion AI = new EntidadesGS1.e_IdentificadorAplicacion();
                AIs = GS1128_DevolverAIs(CodLeido);
                if (AIs != null)
                {
                    AI = AIs.Find(x => x.idIdentificadorAplicacion == "02" || x.idIdentificadorAplicacion == "01");
                    if (AI != null)
                    {
                        if (AI.ValorEncontrado.Length == 14 || AI.ValorEncontrado.Length == 13)
                        {
                            respuesta = ObtenerGTIN(AI.ValorEncontrado, out GT);
                        }       
                    }
                    else
                        GT = null;
                }
                GTIN = GT;
                return respuesta;
            }
            catch (Exception)
            {
                GTIN = GT;
                return respuesta;
            }
        }

        private static string GS1128_TipoGTIN(EntidadesGS1.e_GTIN GTIN)
        {
            string respuesta = "";
            try
            {
                if (GTIN.Digitos[0].Valor > 0)
                {
                    respuesta = "GTIN14";
                }
                else
                {
                    if (GTIN.Digitos[1].Valor > 0)
                    {
                        respuesta = "GTIN13";
                    }
                    else
                    {
                        if (GTIN.Digitos[2].Valor > 0)
                        {
                            respuesta = "GTIN12";
                        }
                        else
                        {
                            if (GTIN.Digitos[3].Valor > 0)
                            {
                                respuesta = "GTIN12";
                            }
                            else
                            {
                                if (GTIN.Digitos[4].Valor > 0)
                                {
                                    respuesta = "GTIN10";
                                }
                                else
                                {
                                    if (GTIN.Digitos[5].Valor > 0)
                                    {
                                        respuesta = "GTIN10";
                                    }
                                    else
                                    {
                                        if (GTIN.Digitos[6].Valor > 0)
                                        {
                                            respuesta = "GTIN08";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return respuesta;
            }
            catch (Exception)
            {
                return respuesta;
            }
        }

       

        private static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }

        public static List<EntidadesGS1.e_IdentificadorAplicacion> GS1128_DevolverAIs(string CodLeido)
        {
            EntidadesGS1.e_EstructuraCodigo EC = new EntidadesGS1.e_EstructuraCodigo();
            List<EntidadesGS1.e_IdentificadorAplicacion> AIsFound = new List<EntidadesGS1.e_IdentificadorAplicacion>();
            try
            {
                if (CodLeido.StartsWith("02"))
                {
                    CodLeido = "01" + CodLeido.Substring(2);
                }

                List<EntidadesGS1.e_IdentificadorAplicacion> AIs = new List<EntidadesGS1.e_IdentificadorAplicacion>();
                AIs = GS1128_CargarAIs();
                int IndexIni = 0;
                int LargoMinCodigo = 0;
                int LargoMaxCodigo = 0;
                string CodigoAnalizar = "";
                int repiticion = 0;
                while (CodLeido.Length > 0)
                {
                    foreach (EntidadesGS1.e_IdentificadorAplicacion AI in AIs)
                    {
                        if (repiticion == 16) // agoto la cantidad de veces que puede recorrer el codigo
                        {
                            CodLeido = "";
                        }
                        string IDAI = AI.idIdentificadorAplicacion;
                        string IDAIAnalizar = "";
                        if (CodLeido.Length > AI.idIdentificadorAplicacion.Length)
                        { IDAIAnalizar = CodLeido.Substring(0, AI.idIdentificadorAplicacion.Length); }
                        else
                        { IDAIAnalizar = ""; if (CodLeido.Length < 3) CodLeido = ""; }
                        if (IDAI == IDAIAnalizar)
                        {
                            IndexIni = AI.idIdentificadorAplicacion.Length;
                            if (AI.Variable == true)
                            {
                                LargoMinCodigo = 1;
                            }
                            else
                            {
                                LargoMinCodigo = AI.CantidadDigitos;
                            }
                            LargoMaxCodigo = AI.CantidadDigitos;
                            if (CodLeido.Length >= LargoMinCodigo + IndexIni)
                            {
                                if (LargoMinCodigo != LargoMaxCodigo && CodLeido.Length > LargoMaxCodigo)
                                {
                                    if (CodLeido.Length >= IndexIni + LargoMaxCodigo)
                                    {
                                        CodigoAnalizar = CodLeido.Substring(IndexIni, LargoMaxCodigo);
                                        if (AI.EsNumero && AI.Nombre != "GTIN")
                                        {
                                            AI.ValorEncontrado = CodigoAnalizar.TrimStart('0');
                                        }
                                        else
                                        {
                                            AI.ValorEncontrado = CodigoAnalizar;
                                        }
                                        AIsFound.Add(AI);
                                        CodLeido = CodLeido.Substring(LargoMaxCodigo + IndexIni);
                                    }
                                    else
                                    {
                                        //Codigo mal elaborado.
                                        //Los codigos de longitud variable deben ir de ultimo
                                        CodLeido = "";
                                        break;
                                    }
                                }
                                else
                                {
                                    if (LargoMinCodigo != LargoMaxCodigo)
                                    {
                                        CodigoAnalizar = CodLeido.Substring(IndexIni);
                                        if (AI.EsNumero && AI.Nombre != "GTIN")
                                        {
                                            AI.ValorEncontrado = CodigoAnalizar.TrimStart('0');
                                        }
                                        else
                                        {
                                            AI.ValorEncontrado = CodigoAnalizar;
                                        }
                                        AIsFound.Add(AI);
                                        CodLeido = "";
                                    }
                                    else
                                    {
                                        CodigoAnalizar = CodLeido.Substring(IndexIni, LargoMaxCodigo);
                                        if (AI.EsNumero && AI.Nombre != "GTIN")
                                        {
                                            AI.ValorEncontrado = CodigoAnalizar.TrimStart('0');
                                        }
                                        else
                                        {
                                            AI.ValorEncontrado = CodigoAnalizar;
                                        }
                                        AIsFound.Add(AI);
                                        CodLeido = CodLeido.Substring(LargoMaxCodigo + IndexIni);
                                    }
                                }
                            }
                        }
                    }
                    repiticion++;
                }

                return AIsFound;
            }
            catch (Exception)
            {
                return AIsFound;
            }
        }

        private static List<EntidadesGS1.e_IdentificadorAplicacion> GS1128_CargarAIs()
        {
            List<EntidadesGS1.e_IdentificadorAplicacion> AIs = new List<EntidadesGS1.e_IdentificadorAplicacion>();
            try
            {
                DataSet DS = new DataSet();
                List<string> Campos = new List<string>();
                Type type = typeof(e_TblGS1AIFields);
                Campos = da_MotorDecision.VariablesDeLaClass(type);
                string SQL = "select ";
                foreach (string str in Campos)
                {
                    SQL += str + ",";
                }
                SQL = SQL.Substring(0, SQL.Length - 1);
                SQL += " from " + e_TablasBaseDatos.TblGS1AI();
                DS = n_ConsultaDummy.GetDataSet(SQL, "MD");

                foreach (DataRow DR in DS.Tables[0].Rows)
                {
                    EntidadesGS1.e_IdentificadorAplicacion AI = new EntidadesGS1.e_IdentificadorAplicacion();
                    AI.CantidadDigitos = int.Parse(DR[e_TblGS1AIFields.CantidadDigitos()].ToString());
                    AI.Descripcion = DR[e_TblGS1AIFields.Descripcion()].ToString();
                    AI.Nombre = DR[e_TblGS1AIFields.Nombre()].ToString();
                    AI.EsFecha = bool.Parse(DR[e_TblGS1AIFields.EsFecha()].ToString());
                    AI.EsNumero = bool.Parse(DR[e_TblGS1AIFields.EsNumero()].ToString());
                    AI.EsTexto = bool.Parse(DR[e_TblGS1AIFields.EsTexto()].ToString());
                    AI.Estructura = DR[e_TblGS1AIFields.Estructura()].ToString();
                    AI.Variable = bool.Parse(DR[e_TblGS1AIFields.Variable()].ToString());
                    AI.idIdentificadorAplicacion = DR[e_TblGS1AIFields.idIdentificadorAplicacion()].ToString();
                    AIs.Add(AI);
                }

                return AIs;
            }
            catch (Exception)
            {
                return AIs;
            }
        }

        public static string GS1128_DevolverCantidad(string CodLeido)
        {
            string respuesta = "";
            try
            {
                if (CodLeido.StartsWith("02"))
                {
                    CodLeido = "01" + CodLeido.Substring(2);
                }

                List<EntidadesGS1.e_IdentificadorAplicacion> AIs = new List<EntidadesGS1.e_IdentificadorAplicacion>();
                EntidadesGS1.e_IdentificadorAplicacion AI = new EntidadesGS1.e_IdentificadorAplicacion();
                AIs = GS1128_DevolverAIs(CodLeido);
                if (AIs != null)
                {
                    AI = AIs.Find(x => x.idIdentificadorAplicacion == "01"); 
                    if (AI != null)
                    {
                        long NumGTIN = long.Parse(AI.ValorEncontrado);
                        respuesta = GS1128_DevolverValorVariableLogistica(NumGTIN.ToString());
                    }
                }
                if (AIs != null)
                {
                    AI = AIs.Find(x => x.idIdentificadorAplicacion == "37");
                    if (AI != null)
                    {
                        respuesta = AI.ValorEncontrado;
                    }
                }
                return respuesta;
            }
            catch (Exception)
            {
                return respuesta;

            }
        }

        private static string GS1128_DevolverValorVariableLogistica(string GTIN)
        {
            string respuesta = "1";
            try
            {
                DataSet DS = new DataSet();

                string codigo_barras = "";

                if (GTIN.StartsWith("0") && GTIN.Length ==14)
                {
                    codigo_barras = GTIN.Substring(1, 13);
                }else
                {
                    codigo_barras = GTIN;
                }
                //string SQL = "SELECT " + e_VistaRelacionGTIN13GTIN14.Equivalencia()  + "," +  e_VistaRelacionGTIN13GTIN14.Cantidad() +
                //    "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaRelacionGTIN13GTIN14() +
                //    "  WHERE " + e_VistaRelacionGTIN13GTIN14.GTIN13() + " = '" + GTIN.TrimStart('0').Trim() + "'";

                string SQL = "EXEC SP_OBTIENEEquivalencia_Cantidad_Alisto '" + codigo_barras + "'";
                DS = n_ConsultaDummy.GetDataSet(SQL, "0");

                if (DS.Tables[0].Rows.Count > 0)  // si es GTIN13
                {
                    foreach (DataRow DR in DS.Tables[0].Rows)
                    {
                        Single cant = Single.Parse(DR[e_VistaRelacionGTIN13GTIN14.Cantidad()].ToString());
                        Single equiv = Single.Parse(DR[e_VistaRelacionGTIN13GTIN14.Equivalencia()].ToString());
                        if (cant < equiv)
                        {
                            respuesta = DR[e_VistaRelacionGTIN13GTIN14.Cantidad()].ToString();
                            break;
                        }
                        else
                        {
                            respuesta = DR[e_VistaRelacionGTIN13GTIN14.Cantidad()].ToString();
                            //respuesta = DR[e_VistaRelacionGTIN13GTIN14.Equivalencia()].ToString();
                            break;
                        }

                    }
                }
                else
                {
                   //SQL = "SELECT " + e_VistaRelacionGTIN13GTIN14.Equivalencia()  + "," +  e_VistaRelacionGTIN13GTIN14.Cantidad() +
                   //       "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaRelacionGTIN13GTIN14() +
                   //       "  WHERE " + e_VistaRelacionGTIN13GTIN14.GTIN14() + " = '" + GTIN.TrimStart('0').Trim() + "'";

                    SQL = SQL = "EXEC SP_OBTIENEEquivalencia_Cantidad_Alisto '" + GTIN + "'";
                    DS = n_ConsultaDummy.GetDataSet(SQL, "0");

                    if (DS.Tables[0].Rows.Count > 0)  // si es GTIN14
                    {
                        foreach (DataRow DR in DS.Tables[0].Rows)
                        {
                            Single cant = Single.Parse(DR[e_VistaRelacionGTIN13GTIN14.Cantidad()].ToString());
                            Single equiv = Single.Parse(DR[e_VistaRelacionGTIN13GTIN14.Equivalencia()].ToString());
                            if (cant > equiv)
                            {
                                respuesta = DR[e_VistaRelacionGTIN13GTIN14.Cantidad()].ToString();
                                break;
                            }
                            else
                            {
                                respuesta = DR[e_VistaRelacionGTIN13GTIN14.Equivalencia()].ToString();
                                break;
                            }
                        }
                    }
                }

                return respuesta;
            }
            catch (Exception)
            {
                return respuesta;
            }

        }

        public static string GS1128_DevolveridUbicacion(string CodLeido)
        {
            string respuesta = "";
            try
            {
                if (CodLeido.StartsWith("02"))
                {
                    CodLeido = "01" + CodLeido.Substring(2);
                }

                List<EntidadesGS1.e_IdentificadorAplicacion> AIs = new List<EntidadesGS1.e_IdentificadorAplicacion>();
                EntidadesGS1.e_IdentificadorAplicacion AI = new EntidadesGS1.e_IdentificadorAplicacion();

                AIs = GS1128_DevolverAIs(CodLeido);
                if (AIs != null)
                {
                    AI = AIs.Find(x => x.idIdentificadorAplicacion == "91");
                    if (AI != null)
                    {
                        string NumGTIN = AI.ValorEncontrado;
                        respuesta = GS1128_DevolverValoridUbicacion(NumGTIN.ToString());
                    }
                }
                return respuesta;
            }
            catch (Exception)
            {
                return respuesta;

            }
        }

        private static string GS1128_DevolverValoridUbicacion(string GTIN)
        {
            string respuesta = "";
            try
            {
                DataSet DS = new DataSet();
                string SQL = "SELECT " + e_VistaCodigosUbicacionFields.idUbicacion() + 
                             "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaCodigosUbicacion() + 
                             "  WHERE " + e_VistaCodigosUbicacionFields.CODIGOGS1UBICACION() + " = '" + GTIN + "'";
                DS = n_ConsultaDummy.GetDataSet(SQL, "0");

                if (DS != null)
                {
                    foreach (DataRow DR in DS.Tables[0].Rows)
                    {
                        respuesta = DR[e_VistaCodigosUbicacionFields.idUbicacion()].ToString();
                    }
                }
                return respuesta;
            }
            catch (Exception)
            {
                return respuesta;
            }
        }

        public static string GS1128_DevolverSSCCGenerado(string CodLeido)
        {
            string respuesta = "";
            try
            {
                List<EntidadesGS1.e_IdentificadorAplicacion> AIs = new List<EntidadesGS1.e_IdentificadorAplicacion>();
                EntidadesGS1.e_IdentificadorAplicacion AI = new EntidadesGS1.e_IdentificadorAplicacion();
                AIs = GS1128_DevolverAIs(CodLeido);
                if (AIs != null)
                {
                    AI = AIs.Find(x => x.idIdentificadorAplicacion == "00");
                    if (AI != null)
                    {
                        string NumGTIN = AI.ValorEncontrado;
                        respuesta = NumGTIN;
                    }
                }
                return respuesta;
            }
            catch (Exception)
            {
                return respuesta;

            }
        }

        public static bool GS1128_EsArticuloGranel(string CodLeido)
        {
           // este metodo es para determibat si el GTIN es de un artículo a granel o no.
            bool EsGTIN, EsGranel = false;
            EntidadesGS1.e_GTIN GTIN;

            try
            {
                if (CodLeido.StartsWith("02"))
                {
                   CodLeido = "01" + CodLeido.Substring(2);
                }

                EsGTIN = GS1128_ObtenerGTIN(CodLeido, out GTIN);
                if (EsGTIN)
                {
                    string SQL = "SELECT " + e_TblMaestroArticulosFields.Granel() + 
                                 "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroArticulos() + 
                                 "  WHERE " + e_TblMaestroArticulosFields.GTIN() + " = '" + GTIN.ValorLeido + "'";
                    EsGranel = bool.Parse(n_ConsultaDummy.GetUniqueValue(SQL, "0"));
                }
            }

            catch (Exception)
            {
                return false;
            }

            return EsGranel ;
        }

         public static string GS1128_DevolverPeso(string CodLeido)
         {
            string respuesta = "0";
            try
            {
                if (CodLeido.StartsWith("02"))
                {
                    CodLeido = "01" + CodLeido.Substring(2);
                }

                List<EntidadesGS1.e_IdentificadorAplicacion> AIs = new List<EntidadesGS1.e_IdentificadorAplicacion>();
                EntidadesGS1.e_IdentificadorAplicacion AI = new EntidadesGS1.e_IdentificadorAplicacion();
                AIs = GS1128_DevolverAIs(CodLeido);
                if (AIs != null)
                {
                    AI = AIs.Find(x => x.idIdentificadorAplicacion == "01");  
                    if (AI != null)
                    {
                        long NumGTIN = long.Parse(AI.ValorEncontrado);
                        //respuesta = GS1128_DevolverValorVariableLogistica(NumGTIN.ToString());
                    }
                }

                if (AIs != null)
                {
                    AI = AIs.Find(x => x.idIdentificadorAplicacion == "310");
                    if (AI != null)
                    {
                        string resptemp = AI.ValorEncontrado;
                        int numdecimal = int.Parse(resptemp.Substring(0, 1));
                        int largo = resptemp.Length;
                        resptemp = resptemp.Substring(0, largo - numdecimal) + "." + resptemp.Substring(largo - numdecimal);
                        resptemp = resptemp.Substring(1, largo);
                        resptemp = Single.Parse(resptemp, System.Globalization.CultureInfo.InvariantCulture).ToString();
                        //Cantisingle = Single.Parse(cantidad, System.Globalization.CultureInfo.InvariantCulture);
                        respuesta = resptemp.Replace(",",".");
                    }
                }
                return respuesta;
            }
            catch (Exception)
            {
                return respuesta;

            }
        }

         public static string GS1128_DevolverUnidadInventario_Alisto(string GTIN)
         {
             string respuesta = "1";
             try
             {
                 DataSet DS = new DataSet();
                 string SQL = "SELECT TOP 1 CAST(" + e_VistaRelacionGTIN13GTIN14.Equivalencia() + " AS nvarchar(max)) + '[' + " +
                               e_VistaRelacionGTIN13GTIN14.unidad_medida() + " + ']-[' + " +
                               e_VistaRelacionGTIN13GTIN14.empaque_maestro() + " + ']' AS Cantidad_HH" +
                     "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaRelacionGTIN13GTIN14() +
                     "  WHERE " + e_VistaRelacionGTIN13GTIN14.GTIN13() + " = '" + GTIN.TrimStart('0').Trim() + "' ORDER BY Cantidad";
                 DS = n_ConsultaDummy.GetDataSet(SQL, "0");

                 if (DS.Tables[0].Rows.Count > 0)  // si es GTIN13
                 {
                     foreach (DataRow DR in DS.Tables[0].Rows)
                     {
                         respuesta = DR["Cantidad_HH"].ToString();
                         break;
                     }
                 }
                 else
                 {
                     SQL = "SELECT TOP 1 CAST(" + e_VistaRelacionGTIN13GTIN14.contenido() + " AS nvarchar(max)) + '[' + " +
                               e_VistaRelacionGTIN13GTIN14.unidad_medida() + " + ']-[' + " +
                               e_VistaRelacionGTIN13GTIN14.Empaque() + " + ']' AS Cantidad_HH" +
                     "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaRelacionGTIN13GTIN14() +
                     "  WHERE " + e_VistaRelacionGTIN13GTIN14.GTIN14() + " = '" + GTIN.TrimStart('0').Trim() + "' ORDER BY Cantidad";
                     DS = n_ConsultaDummy.GetDataSet(SQL, "0");

                     if (DS.Tables[0].Rows.Count > 0)  // si es GTIN14
                     {
                         foreach (DataRow DR in DS.Tables[0].Rows)
                         {
                             respuesta = DR["Cantidad_HH"].ToString();
                             break;
                         }
                     }
                 }

                 return respuesta;

             }
             catch (Exception ex)
             {
                 return ex + "-" + respuesta;

             }
         }
     }
}