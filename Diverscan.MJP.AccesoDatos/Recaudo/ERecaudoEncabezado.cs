using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Recaudo
{
    [Serializable]
    public class ERecaudoEncabezado
    {
        string _idCliente;
        string _nombreCliente;
        DateTime _fechaRegistro;
        long _idRecaudo;
        decimal _montoFacturas;
        decimal _montoNotasCredito;
        decimal _montoNotasCreditoManual;

        public ERecaudoEncabezado(string idCliente,
        string nombreCliente,
        DateTime fechaRegistro,
        long idRecaudo,
        decimal montoFacturas,
        decimal montoNotasCredito,
        decimal montoNotasCreditoManual)
        {
            _idCliente = idCliente;
            _nombreCliente = nombreCliente;
            _fechaRegistro = fechaRegistro;
            _idRecaudo = idRecaudo;
            _montoFacturas = montoFacturas;
            _montoNotasCredito = montoNotasCredito;
            _montoNotasCreditoManual = montoNotasCreditoManual;
        }

        public ERecaudoEncabezado(IDataReader reader)
        {
            _idCliente = reader["idCliente"].ToString();
            _nombreCliente = reader["nombreCliente"].ToString();
            _fechaRegistro = Convert.ToDateTime(reader["fechaRegistro"].ToString()); ;
            _idRecaudo = Convert.ToInt64(reader["idRecaudo"].ToString());
            _montoFacturas = decimal.Parse(reader["MontoFacturas"].ToString()); 
            _montoNotasCredito = decimal.Parse(reader["MontoNotasCredito"].ToString());
            _montoNotasCreditoManual = decimal.Parse(reader["MontoNotasCreditoManual"].ToString());
        }

        public string IdCliente { get {return _idCliente;} }
        public string NombreCliente { get { return _nombreCliente; } }
        public DateTime FechaRegistro { get { return _fechaRegistro; } }
        public long IdRecaudo { get { return _idRecaudo; } }
        public decimal MontoFacturas { get { return _montoFacturas; } }
        public decimal MontoNotasCredito { get { return _montoNotasCredito; } }
        public decimal MontoNotasCreditoManual { get { return _montoNotasCreditoManual; } }
    }
}
