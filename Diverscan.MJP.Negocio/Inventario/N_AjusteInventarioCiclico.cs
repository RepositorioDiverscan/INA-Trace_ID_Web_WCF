using Diverscan.MJP.Entidades.TRAIngresoSalidaArticulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Inventario
{
    public class N_AjusteInventarioCiclico
    {
        public static void AjusteEntrada(TRAIngresoSalidaArticulosRecord tRAIngresoSalidaArticulosRecord, long idInventarioBasico)
        {
            AjusteInventarioCiclicoDBA ajusteInventarioBasicoDBA = new AjusteInventarioCiclicoDBA();
            ajusteInventarioBasicoDBA.AjusteEntrada(tRAIngresoSalidaArticulosRecord, idInventarioBasico);
        }

        public static void AjusteSalida(TRAIngresoSalidaArticulosRecord tRAIngresoSalidaArticulosRecord, long idInventarioBasico)
        {
            AjusteInventarioCiclicoDBA ajusteInventarioBasicoDBA = new AjusteInventarioCiclicoDBA();
            ajusteInventarioBasicoDBA.AjusteSalida(tRAIngresoSalidaArticulosRecord, idInventarioBasico);
        }
    }
}
