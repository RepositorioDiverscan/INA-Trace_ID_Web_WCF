using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.KardexXSkuTIDSAP.Entidad
{
    [Serializable]
    public class EKardexSkuTIDSAP
    {
        int _idBodega;
        string _sku;
        DateTime _fechaInicio,
            _fechaFin;

        public int IdBodega { get => _idBodega; set => _idBodega = value; }
        public string Sku { get => _sku; set => _sku = value; }
        public DateTime FechaInicio { get => _fechaInicio; set => _fechaInicio = value; }
        public DateTime FechaFin { get => _fechaFin; set => _fechaFin = value; }
    }
}
