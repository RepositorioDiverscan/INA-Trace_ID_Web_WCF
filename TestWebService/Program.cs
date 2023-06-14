using Diverscan.MJP.AccesoDatos.ProcesarSolicitud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestWebService
{
    class Program
    {
        static void Main(string[] args)
        {
            // exec SP_GeneraTarea 4,511,'0',1
            da_ProcesarSolicitud test = new da_ProcesarSolicitud();
            test.GeneraTarea(4, 511, "0", 1);
        }
    }
}
