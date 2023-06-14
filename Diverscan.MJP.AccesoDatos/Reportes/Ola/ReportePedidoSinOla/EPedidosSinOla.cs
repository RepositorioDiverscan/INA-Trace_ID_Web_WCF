using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.ReportePedidoSinOla
{
    public class EPedidosSinOla
    {
        int _idMaestroSolicitud
            , _idBodega;
        string _Ruta;
        DateTime _FechaInicio,
            _FechaFinal;

        public int IdMaestroSolicitud { get => _idMaestroSolicitud; set => _idMaestroSolicitud = value; }
        public int IdBodega { get => _idBodega; set => _idBodega = value; }
        public string Ruta { get => _Ruta; set => _Ruta = value; }
        public DateTime FechaInicio { get => _FechaInicio; set => _FechaInicio = value; }
        public DateTime FechaFinal { get => _FechaFinal; set => _FechaFinal = value; }
    }
}
