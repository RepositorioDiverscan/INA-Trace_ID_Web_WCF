using Diverscan.MJP.AccesoDatos.Reportes.Trazabilidad.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Trazabilidad
{
    public interface ITrazabilidadDBA
    {
        List<EConsultaArticulos> ObtenerArticulos(string Dato);

        List<EListadoTrazabilidad> ObtenerDatosTrazabilidad(int idArticulo, DateTime FechaIncicio, DateTime FechaFinal, int idBodega);
    }
}
