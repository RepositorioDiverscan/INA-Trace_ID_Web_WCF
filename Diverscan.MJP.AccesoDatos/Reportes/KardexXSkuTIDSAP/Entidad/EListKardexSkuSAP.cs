using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.KardexXSkuTIDSAP.Entidad
{
    [Serializable]
    public class EListKardexSkuSAP
    {
        string _tipo,
           _socio;
        DateTime _fecha;
        decimal _cantidad,
            _saldo;

        public EListKardexSkuSAP(string tipo, string socio, DateTime fecha, decimal cantidad, decimal saldo)
        {
            _tipo = tipo;
            _socio = socio;
            _fecha = fecha;
            _cantidad = cantidad;
            _saldo = saldo;
        }

        public string Tipo { get => _tipo; set => _tipo = value; }
        public string Socio { get => _socio; set => _socio = value; }
        public DateTime Fecha { get => _fecha; set => _fecha = value; }
        public decimal Cantidad { get => _cantidad; set => _cantidad = value; }
        public decimal Saldo { get => _saldo; set => _saldo = value; }
    }
}
