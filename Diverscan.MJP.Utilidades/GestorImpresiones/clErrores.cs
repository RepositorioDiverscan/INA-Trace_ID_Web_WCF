using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.GestorImpresiones.Utilidades
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
            string ruta = ConfigurationSettings.AppSettings["RutaLogErrores"].ToString();
            ruta = ruta + "Errores_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            // Crea o Abre el archivo para ecribir en el.
            using (StreamWriter sw = File.AppendText(ruta))
            {
                sw.WriteLine("********************************************************************************************************");
                sw.WriteLine("********************************************************************************************************");
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine(error);
                sw.WriteLine(traceError);
                sw.WriteLine("////////////////////////////////////////////////////////////////////////////////////////////////////////");
            }
        }

        /// <summary>
        /// Descripción: Crea un registro de error general.
        /// Autor y Fecha: Esteban Castro. 03/12/2012.
        /// </summary>
        /// <param name="Clase">Clase en la cual se genero el error.</param>
        /// <param name="Metodo">Metodo en el cual se genero el error.</param>
        /// <param name="Usuario">Usuario que ejecuto la funcionalidad y al cual se le genero el error.</param>
        /// <param name="LineaCodigo">Linea de codigo en la que se genero el error.</param>
        /// <param name="NombreProcedimiento">Nombre del procedimiento almacenado que retorna un error. Solo aplica para capa de datos.</param>
        /// <param name="Parametros">Parametros enviados al procedimiento almacenado que genero el error.</param>
        /// <param name="error">Detalle del error que se presento.</param>
        /// <param name="traceError"></param>
        public void escribirErrorDetallado(string Clase, string Metodo, string Usuario, string LineaCodigo, string NombreProcedimiento, string Parametros, string error)
        {
            // Lee el .config de la capa de servicios que lo llama
            string ruta = ConfigurationSettings.AppSettings["RutaLogErrores"].ToString();
            ruta = ruta + "Errores_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            // Crea o Abre el archivo para ecribir en el.
            using (StreamWriter archivoError = File.AppendText(ruta))
            {
                archivoError.WriteLine("********************************************************************************************************");
                archivoError.WriteLine("********************************************************************************************************");
                archivoError.WriteLine(DateTime.Now.ToString());
                archivoError.WriteLine("Clase: " + Clase);
                archivoError.WriteLine("Metodo: " + Metodo);
                archivoError.WriteLine("Usuario: " + Usuario);
                archivoError.WriteLine("Linea de código: " + LineaCodigo);
                archivoError.WriteLine("Procedimiento almacenado: " + NombreProcedimiento);
                archivoError.WriteLine("Parametros: " + Parametros);
                archivoError.WriteLine("Error: " + error);
                archivoError.WriteLine("////////////////////////////////////////////////////////////////////////////////////////////////////////");
            }
        }
    }
}
