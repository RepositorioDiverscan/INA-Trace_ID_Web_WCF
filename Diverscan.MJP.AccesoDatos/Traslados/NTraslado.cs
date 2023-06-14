using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Traslados
{
    public class NTraslado
    {
        private da_Traslados dTraslados = new da_Traslados();

        public string OutTransferProduct(int IdArticulo, string Lote, DateTime FechaVencimiento, int IdUbicacionOrigen, int Cantidad, int IdUsuario, int IdMetodoAccionSalida)
        {
            return dTraslados.SetTransferProduct(IdArticulo,Lote,FechaVencimiento,IdUbicacionOrigen,Cantidad,IdUsuario,IdMetodoAccionSalida);
        }

        public List<ETraslado> ProductDetailFromGtin(string GTIN)
        {
            return dTraslados.ProductDetailFromGtin(GTIN);
        }

        public string InTransferProduct(int IdArticulo, string Lote, DateTime FechaVencimiento, int IdUbicacionOrigen,int IdUbicacionDestino, int Cantidad, int IdUsuario, int IdMetodoAccionEntrada)
        {
            return dTraslados.InTransferProduct(IdArticulo, Lote, FechaVencimiento, IdUbicacionOrigen,IdUbicacionDestino, Cantidad, IdUsuario, IdMetodoAccionEntrada);
        }

       public List<ETraslado> ObtenerEstadoSalidaUsuario(int IdUsuario, int IdMetodoAccion)
        {
            return dTraslados.ObtenerEstadoSalidaUsuario(IdUsuario,IdMetodoAccion);
        }
    }
}
