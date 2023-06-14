using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
//using System.Runtime.InteropServices;

namespace ServicioMotorDecisiones
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        //[AttributeUsageAttribute(AttributeTargets.Method)]
        //[ComVisibleAttribute(true)]
        //[STAThreadAttribute] //: Attribute { }

        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new Service1() 
            };
            ServiceBase.Run(ServicesToRun);
        }

    }
}
