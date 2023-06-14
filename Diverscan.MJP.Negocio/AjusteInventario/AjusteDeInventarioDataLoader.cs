using Diverscan.MJP.AccesoDatos.AjusteInventario;
using Diverscan.MJP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.AjusteInventario
{
    public class AjusteDeInventarioDataLoader
    {
        public static void InsertLog(LogAjusteDeInventario logAjusteDeInventario)
        {
            AjusteDeInventarioDataAcces ajusteDeInventarioDataAcces = new AjusteDeInventarioDataAcces();
            ajusteDeInventarioDataAcces.InsertLogAjusteDeInventario(logAjusteDeInventario);
        }
    }
}
