using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.TransitoMercaderia.Entidad
{
    [Serializable]
    public class EObtenerTransitoMercaderia
    {
        DateTime _fechaInicio,
            _fechaFin;
        int _idBodega;
        string _idInterno;

        public DateTime FechaInicio { get => _fechaInicio; set => _fechaInicio = value; }
        public DateTime FechaFin { get => _fechaFin; set => _fechaFin = value; }
        public int IdBodega { get => _idBodega; set => _idBodega = value; }
        public string IdInterno { get => _idInterno; set => _idInterno = value; }
    }
}
