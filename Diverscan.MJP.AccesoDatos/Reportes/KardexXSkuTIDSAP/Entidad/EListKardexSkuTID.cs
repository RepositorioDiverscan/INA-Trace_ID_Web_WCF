using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.KardexXSkuTIDSAP.Entidad
{
    [Serializable]
    public class EListKardexSkuTID
    {
        string _tipo,
            _socio;
        DateTime _fecha;
        decimal _cantidad,
            _saldo;
        string _usuario;

        public EListKardexSkuTID(string tipo, string socio, DateTime fecha, decimal cantidad, decimal saldo, string usuario)
        {
            _tipo = tipo;
            _socio = socio;
            _fecha = fecha;
            _cantidad = cantidad;
            _saldo = saldo;
            _usuario = usuario;
        }

        public string Tipo { get => _tipo; set => _tipo = value; }
        public string Socio { get => _socio; set => _socio = value; }
        public DateTime Fecha { get => _fecha; set => _fecha = value; }
        public decimal Cantidad { get => _cantidad; set => _cantidad = value; }
        public decimal Saldo { get => _saldo; set => _saldo = value; }
        public string Usuario { get => _usuario; set => _usuario = value; }
    }
}
