using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Utilidades
{
    /// <summary>
    /// Descripción: Crea el numero verificador de un GTIN.
    /// Autor y Fecha: Cristian Gutiérrez. 14/03/2013.
    /// </summary>
    public class clGTIN
    {

        #region Generacion

        #region GTIN 13

        public string GenerarGTIN13Interno(string valor, string Consecutivo)
        {
            try
            {
                var gtin = "";
                if (!string.IsNullOrEmpty(valor))
                {
                    var ci = Int32.Parse(Consecutivo) + 1;

                    gtin = "21" + ZeroFillConsec(10, ci);
                    gtin += GenerarNumVerficador(gtin);

                }

                return gtin;
            }
            catch (Exception ex)
            {
                var clErr = new clErrores();
                clErr.escribirError(ex.Message, ex.StackTrace);
                return null;
            }
        }

        #endregion


        #region GTIN 14

        public string GenerarGTIN14Interno(string valor, string Consecutivo, string indicador)
        {
            try
            {
                var GTIN = "";
                if (!string.IsNullOrEmpty(valor))
                {
                    var CI = Int32.Parse(Consecutivo) + 1;
                    GTIN = indicador + "21" + ZeroFillConsec(10, CI);
                    GTIN += GenerarNumVerficador(GTIN);
                } //fin if & else
                return GTIN;
            }
            catch (Exception ex)
            {
                var clErr = new clErrores();
                clErr.escribirError(ex.Message, ex.StackTrace);
                return null;
            }
        }



        public string GenerarGTIN14Int(string GTIN13, string indicador)
        {
            try
            {
                var GTIN = "";
                if (!string.IsNullOrEmpty(GTIN13))
                {
                    GTIN = indicador + GTIN13.Substring(0, 12);
                    GTIN += GenerarNumVerficador(GTIN);
                }
                return GTIN;
            }
            catch (Exception ex)
            {
                var clErr = new clErrores();
                clErr.escribirError(ex.Message, ex.StackTrace);
                return null;
            }
        }



        #endregion

        #region SSCC

        /// <summary>
        /// Autor: Fernando Torres Siles
        /// Descripcione: Genera el codigo SSCC de la tarima o Despachos
        /// </summary>
        /// <returns></returns>
        public string GenerearSSCCint(string gtin, int conse)
        {
            //primero es saber si es GTIN 13 o 14
            //despues si es 13 va un 0 en la primera posicion
            //y despues fechas

            try
            {
                if (!string.IsNullOrEmpty(gtin))
                {
                    var SSCC = "";

                    //trabajamos con 02 en la primeras posiciones
                    //Digito extencion
                    //Prefijo de compañia
                    //Seriado de referencia
                    //Digito de Control

                    if (gtin.Length == 13 || gtin.Length == 14)
                    {

                        if (esGTIN13(gtin))
                        {
                            //sumar 0
                            gtin = "0" + gtin; //lo tenemos con un peso de 14 caracteres

                            //SSCC  = "(02)" +

                        }
                        else
                        {
                            if (gtin.Length == 14)
                            {
                                //SSCC  = "(02)" +

                            }
                            else
                            {
                                return null;
                            }
                        }

                    }

                    return SSCC;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }
    

    #endregion

        #endregion

        //-----------------------------------------------------------------------------------\\

        #region Verificacion


        /// <summary>
        /// Autor: Cristian Gutierrez Asenjo
        /// Description:
        /// Devuelve el numero verificador de un GTIN
        /// </summary>
        /// <param name="codeGTIN"></param>
        /// <returns></returns>
        public int NumVerficador(string codeGTIN)
        {
            //String to char array
            char[] szArr = codeGTIN.ToCharArray();

            var cant = szArr.Length - 2;

            var cont = 3;
            var suma = 0;

            for (var i = cant; i >= 0; i--)
            {
                suma += (Int32.Parse(szArr.ElementAt(i).ToString()) * cont);

                cont = cont == 3 ? 1 : 3;
            } // fin For

            //Calcular Proximo multiplo de 10
            var multiplo = (int)Math.Ceiling(suma / 10d) * 10;

            var result = multiplo - suma;
            return result;
        }

        /// <summary>
        /// Autor: Cristian Gutierrez Asenjo
        /// Description:
        /// Devuelve el numero verificador de un GTIN
        /// </summary>
        /// <param name="codeGTIN"></param>
        /// <returns></returns>
        public int NumVerificadorGTIN14(string codeGTIN)
        {
            //String to char array
            char[] szArr = codeGTIN.ToCharArray();

            var cant = szArr.Length - 1;
            var pos = szArr.Length - 2;

            var cont = 3;
            var suma = 0;

            for (var i = cant; i > 0; i--)
            {
                suma += (Int32.Parse(szArr.ElementAt(pos).ToString()) * cont);
                pos--;
                cont = cont == 3 ? 1 : 3;
            } // fin For

            //Calcular Proximo multiplo de 10
            var multiplo = (int)Math.Ceiling(suma / 10d) * 10;

            var result = multiplo - suma;
            return result;
        }



        #endregion

        //-----------------------------------------------------------------------------------\\

        #region Utilidades
        public string ZeroFillConsec(int lenght, int val)
        {
            var newVal = "";
            var textValue = Convert.ToString(val);

            for (int idx = 1; idx <= lenght - textValue.Length; idx++)
            {
                newVal = String.Concat("0", newVal);
            }
            var rs = String.Concat(newVal, textValue).Substring(0, lenght);
            return rs;
        }


        public int GenerarNumVerficador(string codeGTIN)
        {
            //String to char array
            char[] szArr = codeGTIN.ToCharArray();
            var cant = szArr.Length - 1;

            var cont = 3;
            var suma = 0;

            for (var i = cant; i >= 0; i--)
            {
                suma += (Int32.Parse(szArr.ElementAt(i).ToString()) * cont);

                cont = cont == 3 ? 1 : 3;
            } // fin For

            //Calcular Proximo multiplo de 10
            var multiplo = (int)Math.Ceiling(suma / 10d) * 10;

            var result = multiplo - suma;
            return result;
        }
        #endregion
        
        //-----------------------------------------------------------------------------------\\

        #region "Comprobar GTIN"

        /// <summary>
        /// Autor: Cristian Gutierrez Asenjo.
        /// Description:
        /// Verifica que esté compuesto por 14 digitos.
        /// Además, verifica que el GTIN es valido comparando el código verificador
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public bool esGTIN14(string barcode)
        {
            barcode = barcode.Trim();
            try
            {
                bool result = false;
                if (barcode.Length == 14)
                {
                    var numVerif = NumVerificadorGTIN14(barcode);

                    char[] arr = barcode.ToCharArray();
                    int cant = arr.Length - 1;
                    int val = Int32.Parse(arr[cant].ToString());
                    if (val == numVerif)
                    {
                        result = true;
                    }
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch (Exception )
            {
                return false;
            }
        }

        /// <summary>
        /// Autor: Cristian Gutierrez Asenjo.
        /// Description:
        /// Verifica que esté compuesto por 13 digitos.
        /// Además, verifica que el GTIN es valido comparando el código verificador
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public bool esGTIN13(string barcode)
        {
            barcode = barcode.Trim();

            try
            {
                bool result = false;
                if (barcode.Length == 13)
                {
                    var numVerif = NumVerficador(barcode);

                    char[] arr = barcode.ToCharArray();
                    int cant = arr.Length - 1;
                    int val = Int32.Parse(arr[cant].ToString());
                    if (val == numVerif)
                    {
                        result = true;
                    }
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch (Exception )
            {
                return false;
            }
        }

        #endregion
    }
}
