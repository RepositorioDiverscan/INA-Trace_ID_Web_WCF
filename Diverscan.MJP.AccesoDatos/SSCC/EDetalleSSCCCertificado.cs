using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.SSCC
{
    [Serializable]
    public class EDetalleSSCCCertificado
    {
        private string _idInterno;
        private string _nombre;
        private int _idArticulo;
        private double _cantidad;
        private string _lote;
        private string _fechaVencimiento;
        private string _GTIN;
        private string _certificado;
        private int _cantidadDiferencia;


        public EDetalleSSCCCertificado()
        {
        }
       
        public string IdInterno { get => _idInterno; set => _idInterno = value; }       
        public string Nombre { get => _nombre; set => _nombre = value; }                          
        public string Lote { get => _lote; set => _lote = value; }                                   
        public string FechaVencimiento { get => _fechaVencimiento; set => _fechaVencimiento = value; }
        public double Cantidad { get => _cantidad; set => _cantidad = value; }
        public string Certificado { get => _certificado; set => _certificado = value; }
        public string GTIN { get => _GTIN; set => _GTIN = value; }
        public int IdArticulo { get => _idArticulo; set => _idArticulo = value; }
        public int CantidadDiferencia { get => _cantidadDiferencia; set => _cantidadDiferencia = value; }
    }
}
