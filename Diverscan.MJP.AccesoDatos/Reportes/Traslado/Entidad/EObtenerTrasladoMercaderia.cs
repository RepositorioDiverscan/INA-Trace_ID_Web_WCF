using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Traslado.Entidad
{
    [Serializable]
    public class EObtenerTrasladoMercaderia
    {
        DateTime _fechaInicio,
           _fechaFin;
        string _sku;
        int _idBodega;

        public DateTime FechaInicio { get => _fechaInicio; set => _fechaInicio = value; }
        public DateTime FechaFin { get => _fechaFin; set => _fechaFin = value; }
        public string Sku { get => _sku; set => _sku = value; }
        public int IdBodega { get => _idBodega; set => _idBodega = value; }
    }
}
