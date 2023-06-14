using Diverscan.MJP.AccesoDatos.Inventario;
using Diverscan.MJP.Entidades.TRAIngresoSalidaArticulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Inventario
{
    public class N_AjusteInventarioCiclico
    {
        public static void AjusteEntrada(TRAIngresoSalidaArticulosRecord tRAIngresoSalidaArticulosRecord, long idInventarioCiclico)
        {
            AjusteInventarioCiclicoDBA ajusteInventarioCiclicoDBA = new AjusteInventarioCiclicoDBA();
            ajusteInventarioCiclicoDBA.AjusteEntrada(tRAIngresoSalidaArticulosRecord, idInventarioCiclico);
        }

        public static void AjusteSalida(TRAIngresoSalidaArticulosRecord tRAIngresoSalidaArticulosRecord, long idInventarioCiclico)
        {
            AjusteInventarioCiclicoDBA ajusteInventarioCiclicoDBA = new AjusteInventarioCiclicoDBA();
            ajusteInventarioCiclicoDBA.AjusteSalida(tRAIngresoSalidaArticulosRecord, idInventarioCiclico);
        }

        public static void AjusteEntrada(List<TRAIngresoSalidaArticulosRecord> tRAIngresoSalidaArticulosRecord, long idInventarioBasico, int idUsuario)
        {
            AjusteInventarioCiclicoDBA ajusteInventarioCiclicoDBA = new AjusteInventarioCiclicoDBA();
            ajusteInventarioCiclicoDBA.AjusteEntrada(tRAIngresoSalidaArticulosRecord, idInventarioBasico, idUsuario);
        }

        public static void AjusteSalida(List<TRAIngresoSalidaArticulosRecord> tRAIngresoSalidaArticulosRecord, long idInventarioBasico, int idUsuario)
        {
            AjusteInventarioCiclicoDBA ajusteInventarioCiclicoDBA = new AjusteInventarioCiclicoDBA();
            ajusteInventarioCiclicoDBA.AjusteSalida(tRAIngresoSalidaArticulosRecord, idInventarioBasico, idUsuario);
        }
    }
}
