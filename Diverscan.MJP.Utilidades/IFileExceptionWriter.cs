using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diverscan.MJP.Utilidades
{
    public interface IFileExceptionWriter
    {
        void WriteExpection(string error, string traceError, string ruta);
        void WriteException(Exception exception, string ruta);
    }
}
