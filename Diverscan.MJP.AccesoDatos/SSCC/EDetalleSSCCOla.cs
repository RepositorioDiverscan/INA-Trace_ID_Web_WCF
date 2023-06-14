using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.SSCC
{
    [DataContract]
    public class EDetalleSSCCOla
    {       
        private string _idInterno;
        private string _nombre;
        private int _idArticulo;
        private double _cantidad;
        private string _lote;
        private string _descripcion;
        private string _fechaAndroid;
        private string _GTIN;
        private int _cantidadCertificada;
        private int _cantidadMas;
        private int _cantidadMenos;
        private int _diferenciaCertificacion;
        private bool _Certificado;

        [DataMember]
        public string IdInterno { get => _idInterno; set => _idInterno = value; }
        [DataMember]
        public string Nombre { get => _nombre; set => _nombre = value; }
        [DataMember]
        public int IdArticulo { get => _idArticulo; set => _idArticulo = value; }
        [DataMember]
        public double Cantidad { get => _cantidad; set => _cantidad = value; }
        [DataMember]
        public string Lote { get => _lote; set => _lote = value; }
        [DataMember]
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        [DataMember]
        public string FechaAndroid { get => _fechaAndroid; set => _fechaAndroid = value; }       
        [DataMember]        
        public string GTIN { get => _GTIN; set => _GTIN = value; }
        [DataMember]
        public int CantidadCertificada { get => _cantidadCertificada; set => _cantidadCertificada = value; }
        [DataMember]
        public int CantidadMas { get => _cantidadMas; set => _cantidadMas = value; }
        [DataMember]
        public int CantidadMenos { get => _cantidadMenos; set => _cantidadMenos = value; }
        [DataMember]
        public int DiferenciaCertificacion { get => _diferenciaCertificacion; set => _diferenciaCertificacion = value; }
        [DataMember]
        public bool Certificado { get => _Certificado; set => _Certificado = value; }
    }
}
