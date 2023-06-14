using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Traslados.ManejoExcepciones
{
    public class GuardarMensajeError
    {
        public static void GuardarError(string mensajeError, string traceError)
        {

            if (mensajeError.Contains("Thread was being aborted."))
            {
                return;
            }

            var ruta = ConfigurationSettings.AppSettings["RutaLogErroresTraslados"];

            ruta = ruta + "Error_" + "Dia[" + DateTime.Now.ToString("dd-MM-yyyy") + "]" + "_Hora[" + DateTime.Now.ToString("hh-mm-ss") + "]" + ".txt";

            using (var sw = File.AppendText(ruta))
            {
                sw.WriteLine("********************************************************************************************************");
                sw.WriteLine("********************************************************************************************************");
                sw.WriteLine(mensajeError);
                sw.WriteLine(traceError);
                sw.WriteLine("////////////////////////////////////////////////////////////////////////////////////////////////////////");
            }
        }
    }
}
