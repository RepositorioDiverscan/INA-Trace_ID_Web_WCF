using Diverscan.MJP.Entidades.Invertario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diverscan.MJP.UI.Administracion.Inventario
{
    public class BodegaSistema
    {
        public List<ICantidadPorUbicacionArticuloRecord> ArticulosBodega { get; set; }
        public List<ArticulosDisponibles> ArticulosSistema { get; set; }

        public BodegaSistema(List<ICantidadPorUbicacionArticuloRecord> articulosBodega, List<ArticulosDisponibles> articulosSistema)
        {
            ArticulosBodega = articulosBodega;
            ArticulosSistema = articulosSistema;
        }
    }
}