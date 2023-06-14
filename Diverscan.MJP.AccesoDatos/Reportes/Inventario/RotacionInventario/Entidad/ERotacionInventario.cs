using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.RotacionInventario.Entidad
{
    [Serializable]
    public class ERotacionInventario
    {
        DateTime _FechaInicio,
            _FechaFin;
        string _IdInterno;
        int _IdBodega;

        public DateTime FechaInicio { get => _FechaInicio; set => _FechaInicio = value; }
        public DateTime FechaFin { get => _FechaFin; set => _FechaFin = value; }
        public string IdInterno { get => _IdInterno; set => _IdInterno = value; }
        public int IdBodega { get => _IdBodega; set => _IdBodega = value; }
    }
}
