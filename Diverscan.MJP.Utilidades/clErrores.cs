using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Utilidades
{
    public class clErrores
    {
        /// <summary>
        /// Descripción: Crea un registro de error general.
        /// Autor y Fecha: Esteban Castro. 03/12/2012.
        /// </summary>
        /// <param name="error">Error.</param>
        /// <param name="traceError"></param>
        public void escribirError(string error, string traceError)
        {
            // Lee el .config de la capa de servicios que lo llama

            if (error.Contains("Thread was being aborted."))
            {
                return;
            }

            var ruta = ConfigurationSettings.AppSettings["RutaLogErrores"];

            ruta = ruta + "Errores_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

            // Crea o Abre el archivo para ecribir en el.
            using (var sw = File.AppendText(ruta))
            {
                sw.WriteLine("********************************************************************************************************");
                sw.WriteLine("********************************************************************************************************");
                sw.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                sw.WriteLine(error);
                sw.WriteLine(traceError);
                sw.WriteLine("////////////////////////////////////////////////////////////////////////////////////////////////////////");
            }
        }

        public void escribirLogSincro(string log, string Metodo, string Accion)
        {
            var ruta = ConfigurationSettings.AppSettings["RutaLogSincro"];

            ruta = ruta +  log + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

            // Crea o Abre el archivo para ecribir en el.
            using (var sw = File.AppendText(ruta))
            {
                sw.WriteLine("********************************************************************************************************");
                sw.WriteLine("********************************************************************************************************");
                sw.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                sw.WriteLine(Metodo);
                sw.WriteLine(Accion);
                sw.WriteLine("////////////////////////////////////////////////////////////////////////////////////////////////////////");
            }
        }

        public void escribirErrorImport(string error, string traceError)
        {
            // Lee el .config de la capa de servicios que lo llama

            if (error.Contains("Thread was being aborted."))
            {
                return;
            }

            var ruta = ConfigurationSettings.AppSettings["RutaLogErrores"].ToString();

            ruta = ruta + "Errores_Excel_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

            // Crea o Abre el archivo para ecribir en el.
            using (var sw = File.AppendText(ruta))
            {
                sw.WriteLine("********************************************************************************************************");
                sw.WriteLine("********************************************************************************************************");
                sw.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                sw.WriteLine(error);
                sw.WriteLine(traceError);
                sw.WriteLine("////////////////////////////////////////////////////////////////////////////////////////////////////////");
            }
        }

        /// <summary>
        /// Descripción: Crea un registro de error general.
        /// Autor y Fecha: Esteban Castro. 03/12/2012.
        /// </summary>
        /// <param name="clase">Clase en la cual se genero el error.</param>
        /// <param name="metodo">Metodo en el cual se genero el error.</param>
        /// <param name="usuario">Usuario que ejecuto la funcionalidad y al cual se le genero el error.</param>
        /// <param name="lineaCodigo">Linea de codigo en la que se genero el error.</param>
        /// <param name="nombreProcedimiento">Nombre del procedimiento almacenado que retorna un error. Solo aplica para capa de datos.</param>
        /// <param name="Parametros">Parametros enviados al procedimiento almacenado que genero el error.</param>
        /// <param name="error">Detalle del error que se presento.</param>
        /// <param name="traceError"></param>
        /// 
        public void escribirErrorDetallado(string clase, string metodo, string usuario, string lineaCodigo, string nombreProcedimiento, string Parametros, string error)
        {
            if (error.Contains("Thread was being aborted."))
            {
                return;
            }

            // Lee el .config de la capa de servicios que lo llama
            var ruta = ConfigurationSettings.AppSettings["RutaLogErrores"];
            ruta = ruta + "Errores_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

            // Crea o Abre el archivo para ecribir en el.
            using (var archivoError = File.AppendText(ruta))
            {
                archivoError.WriteLine("********************************************************************************************************");
                archivoError.WriteLine("********************************************************************************************************");
                archivoError.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                archivoError.WriteLine("Clase: " + clase);
                archivoError.WriteLine("Metodo: " + metodo);
                archivoError.WriteLine("Usuario: " + usuario);
                archivoError.WriteLine("Linea de código: " + lineaCodigo);
                archivoError.WriteLine("Procedimiento almacenado: " + nombreProcedimiento);
                archivoError.WriteLine("Parametros: " + Parametros);
                archivoError.WriteLine("Error: " + error);
                archivoError.WriteLine("////////////////////////////////////////////////////////////////////////////////////////////////////////");
            }
        }
    }
}
