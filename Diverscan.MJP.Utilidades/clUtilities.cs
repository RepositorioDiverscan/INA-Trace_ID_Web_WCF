using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Utilidades
{
    public class clUtilities 
    {
        /// <summary>
        /// Autor: Jose Mora Ramirez
        /// Description: Obtienes un string y reemplaza los caracteres no permitidos
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>

        public static string ConvertString(string dato)
        {
            dato = dato.Replace("!e!", " ");
            dato = dato.Replace("!g!", "#");
            dato = dato.Replace("!d!", "$"); 
            dato = dato.Replace("!p!", "%");
            dato = dato.Replace("!a!", "&");
            dato = dato.Replace("!t!", "*");
            dato = dato.Replace("!m!", "+");
            dato = dato.Replace("!s!", "/");
            dato = dato.Replace("!b!", "\\");
            dato = dato.Replace("!2!", ":");
            dato = dato.Replace("!c!", ";");
            dato = dato.Replace("!z!", "<");
            dato = dato.Replace("!y!", ">");
            dato = dato.Replace("!i!", "=");
            dato = dato.Replace("!u!", "?");
            dato = dato.Replace("!6!", "[");
            dato = dato.Replace("!7!", "]");
            dato = dato.Replace("!t!", "ˆ");
            dato = dato.Replace("!v!", "|");
            dato = dato.Replace("!1!", "á");
            dato = dato.Replace("!3!", "Á");
            dato = dato.Replace("!4!", "é");
            dato = dato.Replace("!5!", "É");
            dato = dato.Replace("!8!", "í");
            dato = dato.Replace("!9!", "Í");
            dato = dato.Replace("!0!", "ó");
            dato = dato.Replace("!f!", "Ó");
            dato = dato.Replace("!h!", "ú");
            dato = dato.Replace("!j!", "Ú");
            dato = dato.Replace("!k!", "ü"); 
            dato = dato.Replace("!l!", "Ü");
            dato = dato.Replace("!n!", "ñ");
            dato = dato.Replace("!o!", "Ñ");
            dato = dato.Replace("!q!", "'");
            dato = dato.Replace("!r!", ".");
            dato = dato.Replace("!x!", "~");

            return dato;
        }

        public static string ConvertCaracteresInvalidos(string texto)
        {
            texto = texto.Replace("Ñ", "N");
            texto = texto.Replace("ñ", "n");
            texto = texto.Replace("Á", "A");
            texto = texto.Replace("á", "A");
            texto = texto.Replace("É", "E");
            texto = texto.Replace("é", "e");
            texto = texto.Replace("Í", "I");
            texto = texto.Replace("í", "i");
            texto = texto.Replace("Ó", "O");
            texto = texto.Replace("ó", "o");
            texto = texto.Replace("Ú", "U");
            texto = texto.Replace("ú", "u");
            texto = texto.Replace("Ü", "U");
            texto = texto.Replace("ü", "u");
            texto = texto.Replace("Ń", "N");
            texto = texto.Replace("'", "");

            return texto;
        }

    }//
}//


