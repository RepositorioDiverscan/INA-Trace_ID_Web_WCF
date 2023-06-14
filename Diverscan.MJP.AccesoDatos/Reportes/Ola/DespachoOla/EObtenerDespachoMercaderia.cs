using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Ola.DespachoOla
{
    [Serializable]
    public class EObtenerDespachoMercaderia
    {
        DateTime _fechaInicio,
            _fechaFin;
        string _idOla;

        public DateTime FechaInicio { get => _fechaInicio; set => _fechaInicio = value; }
        public DateTime FechaFin { get => _fechaFin; set => _fechaFin = value; }
        public string IdOla { get => _idOla; set => _idOla = value; }
    }
}
