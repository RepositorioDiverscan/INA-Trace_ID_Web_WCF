using Diverscan.MJP.AccesoDatos.Trazabilidad;
using Diverscan.MJP.Entidades.TRAIngresoSalidaArticulos;
using Diverscan.MJP.Entidades.Trazabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Trazabilidad
{
    public class N_ArticuloTrazabilidad
    {
        public static List<ArticuloTrazabilidad> ObtenerTrazabilidadArticulo(long idArticulo, DateTime fechaInicio, DateTime fechaFin)
        {
            ArticuloTrazabilidadDBA articuloTrazabilidadDBA = new ArticuloTrazabilidadDBA();
            return articuloTrazabilidadDBA.ObtenerTrazabilidadArticulo(idArticulo, fechaInicio, fechaFin);
        }      

    }
}
