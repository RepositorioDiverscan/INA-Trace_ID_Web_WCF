using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Ola.DisponibleFcturacion
{
    [Serializable]
    public class EObtenerOlasDisponiblesFacturacion
    {
        DateTime _fechaInicio,
            _fechaFin;

        public DateTime FechaInicio { get => _fechaInicio; set => _fechaInicio = value; }
        public DateTime FechaFin { get => _fechaFin; set => _fechaFin = value; }
    }
}
