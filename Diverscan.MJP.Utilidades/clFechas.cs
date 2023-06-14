using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.Visitas.Utilidades
{
    public class clFechas
    {
        /// <summary>
        /// Autor: Cristian Gutierrez Asenjo 
        /// Description: En captura con formato dd-MM-yyyy
        /// </summary>
        /// <param name="Fecha">Fecha con el formato dd-MM-yyyy</param>
        /// <returns></returns>
        public static string GetDate(string Fecha)
        {
            char[] delimiterChars = { '|', ':', '/', '-', };

            string[] PartDate = Fecha.Split(delimiterChars);

            var dd = PartDate[0];
            var mm = PartDate[1];
            var yyyy = PartDate[2];

            return yyyy +"-"+ mm +"-"+ dd;
        }

        /// <summary>
        /// Autor: Cristian Gutierrez Asenjo
        /// Description: Convertir un string de fecha con formato dd-MM-yyyy a un datetime
        /// </summary>
        /// <param name="Fecha">fecha con formato dd-MM-yyyy</param>
        /// <returns>fecha como Datetime</returns>
        public static DateTime GetDateEnglish(string Fecha)
        {
            char[] delimiterChars = { '|', ':', '/', '-', };

            string[] PartDate = Fecha.Split(delimiterChars);

            var dd = int.Parse(PartDate[0]);
            var mm = int.Parse(PartDate[1]);
            var yy = int.Parse(PartDate[2]);

            DateTime time = new DateTime(yy,mm,dd);

            return time;
        }

        /// <summary>
        /// Retorna la fecha en formato SQL mas la hora en que se hace la conversion.
        /// </summary>
        /// <param name="Fecha">Fecha en formato dd-MM-yyyy</param>
        /// <returns>Fecha sql mas la hora</returns>
        public static string GetDateHour(string Fecha)
        {
            return GetDate(Fecha) + " " + DateTime.Now.ToString("HH:mm:ss");
        }

    }//
}//


