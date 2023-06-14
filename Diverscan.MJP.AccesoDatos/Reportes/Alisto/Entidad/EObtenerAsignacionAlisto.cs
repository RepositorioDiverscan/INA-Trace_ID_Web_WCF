using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Alisto.Entidad
{
    [Serializable]
    public class EObtenerAsignacionAlisto
    {
        DateTime _fechaInicio,
            _fechaFin;
        string _usuario;

        public DateTime FechaInicio { get => _fechaInicio; set => _fechaInicio = value; }
        public DateTime FechaFin { get => _fechaFin; set => _fechaFin = value; }
        public string Usuario { get => _usuario; set => _usuario = value; }
    }
}
