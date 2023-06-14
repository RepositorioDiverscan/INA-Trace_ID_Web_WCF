using Diverscan.MJP.AccesoDatos.Inventario;
using Diverscan.MJP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Inventario
{
    public class N_InventarioCiclico
    {
        public static void InsertInventarioCiclico(e_InventarioCiclicoRecord e_inventarioCiclicoRecord)
        {
            InventarioCiclicoDBA inventarioCiclicoDBA = new InventarioCiclicoDBA();
            inventarioCiclicoDBA.InsertInventarioCiclico(e_inventarioCiclicoRecord);
        }

        public static void InsertInventarioCiclico(List<e_InventarioCiclicoRecord> e_inventarioCiclicoData)
        {
            InventarioCiclicoDBA inventarioCiclicoDBA = new InventarioCiclicoDBA();
            inventarioCiclicoDBA.InsertInventarioCiclico(e_inventarioCiclicoData);
        }

        public static List<e_InventarioCiclicoRecord> GetInventariosCiclicos(DateTime fechaInicio, DateTime fechaFin)
        {
            InventarioCiclicoDBA inventarioCiclicoDBA = new InventarioCiclicoDBA();
            return inventarioCiclicoDBA.GetInventariosCiclicos(fechaInicio, fechaFin);
        }
    }
}
