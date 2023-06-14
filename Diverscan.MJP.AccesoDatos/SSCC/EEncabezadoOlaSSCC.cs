using Diverscan.MJP.AccesoDatos.Alistos;
using Diverscan.MJP.AccesoDatos.Bodega;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.SSCC
{
    [Serializable]
    public class EEncabezadoOlaSSCC : EEncabezadoSalida
    {
        private List<EDetalleSSCCOla> _articulos = new List<EDetalleSSCCOla>();

        [DataMember]
        public List<EDetalleSSCCOla> Articulos { get => _articulos; set => _articulos = value; }
    }
}
