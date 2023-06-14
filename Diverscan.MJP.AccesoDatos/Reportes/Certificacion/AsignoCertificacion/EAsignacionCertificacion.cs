using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.AsignoCertificacion
{
    [Serializable]
    public class EAsignacionCertificacion
    {
        DateTime _fechaInicio,
            _fechaFin;
        int _idOla;
        string _usuario;

        public DateTime FechaInicio { get => _fechaInicio; set => _fechaInicio = value; }
        public DateTime FechaFin { get => _fechaFin; set => _fechaFin = value; }
        public int IdOla { get => _idOla; set => _idOla = value; }
        public string Usuario { get => _usuario; set => _usuario = value; }
    }
}
