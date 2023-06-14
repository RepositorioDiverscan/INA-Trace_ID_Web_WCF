using Diverscan.MJP.Negocio.Reportes;
using Diverscan.MJP.Entidades.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes
{
    public interface ReporteDespacho
    {
        List<IEntidad_Despacho> Obtener_Reporte_Despacho(DateTime Inicio, DateTime Final, int idArticulo); //obtener reporte

    }
}
