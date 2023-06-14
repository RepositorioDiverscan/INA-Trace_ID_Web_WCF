using Diverscan.MJP.AccesoDatos.InventarioBasico;
using Diverscan.MJP.Entidades.TRAIngresoSalidaArticulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.InventarioBasico
{
    public class N_AjusteInventarioBasico
    {
        public static void AjusteEntrada(TRAIngresoSalidaArticulosRecord tRAIngresoSalidaArticulosRecord, long idInventarioBasico)
        {
            AjusteInventarioBasicoDBA ajusteInventarioBasicoDBA = new AjusteInventarioBasicoDBA();
            ajusteInventarioBasicoDBA.AjusteEntrada(tRAIngresoSalidaArticulosRecord, idInventarioBasico);
        }

        public static void AjusteSalida(TRAIngresoSalidaArticulosRecord tRAIngresoSalidaArticulosRecord, long idInventarioBasico)
        {
            AjusteInventarioBasicoDBA ajusteInventarioBasicoDBA = new AjusteInventarioBasicoDBA();
            ajusteInventarioBasicoDBA.AjusteSalida(tRAIngresoSalidaArticulosRecord, idInventarioBasico);
        }

        public static void AjusteEntrada(List<TRAIngresoSalidaArticulosRecord> tRAIngresoSalidaArticulosRecord, long idInventarioBasico, int idUsuario, int idBodega)
        {
            AjusteInventarioBasicoDBA ajusteInventarioBasicoDBA = new AjusteInventarioBasicoDBA();
            ajusteInventarioBasicoDBA.AjusteEntrada(tRAIngresoSalidaArticulosRecord, idInventarioBasico, idUsuario, idBodega);
        }

        public static void AjusteSalida(List<TRAIngresoSalidaArticulosRecord> tRAIngresoSalidaArticulosRecord, long idInventarioBasico, int idUsuario, int idBodega)
        {
            AjusteInventarioBasicoDBA ajusteInventarioBasicoDBA = new AjusteInventarioBasicoDBA();
            ajusteInventarioBasicoDBA.AjusteSalida(tRAIngresoSalidaArticulosRecord, idInventarioBasico, idUsuario, idBodega);
        }
    }
}
