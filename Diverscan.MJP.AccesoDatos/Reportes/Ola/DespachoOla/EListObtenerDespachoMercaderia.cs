using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Ola.DespachoOla
{
    [Serializable]
    public class EListObtenerDespachoMercaderia
    {
        long _idOla;
        string _nombreTransportista,
            _fechaDespacho
            , _unidadTransporte;

        public EListObtenerDespachoMercaderia(long idOla, string unidadTransporte, string nombreTransportista, string fechaDespacho)
        {
            _idOla = idOla;
            _unidadTransporte = unidadTransporte;
            _nombreTransportista = nombreTransportista;
            _fechaDespacho = fechaDespacho;
        }

        public long IdOla { get => _idOla; set => _idOla = value; }
        public string UnidadTransporte { get => _unidadTransporte; set => _unidadTransporte = value; }
        public string NombreTransportista { get => _nombreTransportista; set => _nombreTransportista = value; }
        public string FechaDespacho { get => _fechaDespacho; set => _fechaDespacho = value; }
    }
}
