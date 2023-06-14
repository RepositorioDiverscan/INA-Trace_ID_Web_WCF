using Diverscan.MJP.AccesoDatos.Inventario;
using Diverscan.MJP.Entidades.Invertario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Inventario
{
    public  class N_UbicacionesInventarioCiclico
    {
        public static List<UbicacionesInventarioCiclicoRecord> ObtenerUbicacionesInventarioCiclico(long idInventario, int estado)
        {
            UbicacionesInventarioCiclicoDBA ubicacionesInventarioCiclicoDBA = new UbicacionesInventarioCiclicoDBA();
            return ubicacionesInventarioCiclicoDBA.ObtenerUbicacionesInventarioCiclico(idInventario, estado);
        }

        public static void Update_UbicacionesRealizarInventarioCiclico(long idUbicacionesInventario, int estado)
        {
            UbicacionesInventarioCiclicoDBA ubicacionesInventarioCiclicoDBA = new UbicacionesInventarioCiclicoDBA();
            ubicacionesInventarioCiclicoDBA.Update_UbicacionesRealizarInventarioCiclico(idUbicacionesInventario,estado);
        }

        public static void ActualizarArticulosInventarioCiclicoRealizar(long idArticulosInventarioCiclico, int estado)
        {
            UbicacionesInventarioCiclicoDBA ubicacionesInventarioCiclicoDBA = new UbicacionesInventarioCiclicoDBA();
            ubicacionesInventarioCiclicoDBA.ActualizarArticulosInventarioCiclicoRealizar(idArticulosInventarioCiclico, estado);
        }
    }
}
