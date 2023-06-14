using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Recaudo
{
    [Serializable]
    public class ERecaudoDetalle
    {
        string _numeroFactura;
        string _tipoDocumento;
        decimal _monto;

        public ERecaudoDetalle(string numeroFactura,
        string tipoDocumento,
        decimal monto)
        {
            _numeroFactura = numeroFactura;
            _tipoDocumento = tipoDocumento;
            _monto = monto;
        }

        public ERecaudoDetalle(IDataReader reader)
        {
            _numeroFactura = reader["NumeroFactura"].ToString();
            _tipoDocumento = reader["TipoDocumento"].ToString();      
            _monto = decimal.Parse(reader["monto"].ToString());
        }

        public string NumeroFactura { get { return _numeroFactura; } }
        public string TipoDocumento { get { return _tipoDocumento; } }   
        public decimal Monto { get { return _monto; } }
    }

}
