using Diverscan.MJP.AccesoDatos.Inventario;
using Diverscan.MJP.Entidades.Invertario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Inventario
{
    public class N_ArticulosInventarioCiclicoRealiazar
    {
        public static List<ArticuloCiclicoRealizarRecord> ObtenerArticulosInventarioCiclicoRealizar(long idInventario, int estado)
        {
            ArticulosInventarioCiclicoRealizarDBA articulosInventarioCiclicoRealizarDBA = new ArticulosInventarioCiclicoRealizarDBA();
            return articulosInventarioCiclicoRealizarDBA.ObtenerArticulosInventarioCiclicoRealizar(idInventario, estado);
        }
    }
}
