using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace Diverscan.MJP.Utilidades
{ 
    public class FileExceptionWriter : IFileExceptionWriter
    {
        public void WriteExpection(string error, string traceError, string ruta)
        {            
            if (Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }
            DateTime dateTime = DateTime.Now;
            ruta = ruta + @"\\Log_" + dateTime.ToString("yyyyMMdd") + "_" +
                dateTime.Second.ToString() + "_" +
                dateTime.Millisecond.ToString() + "_" +
                ".txt";
          
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

        public void WriteException(Exception exception, string ruta)
        {
            WriteExpection(exception.Message, exception.StackTrace, ruta);
        }
    }
}