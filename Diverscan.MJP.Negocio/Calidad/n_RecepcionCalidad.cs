using Diverscan.MJP.AccesoDatos.Calidad;
using Diverscan.MJP.Entidades.Calidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Calidad
{
    public class n_RecepcionCalidad
    {
        public static List<RecepcionRecord> ObtenerCalidadRecepciónOC(long idArticulo, string lote)
        {
            RecepcionDBA recepcionDBA = new RecepcionDBA();
            return recepcionDBA.ObtenerCalidadRecepciónOC(idArticulo, lote);
        }
    }
}
