using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Articulos.InfoArticulo
{
    public class ArticuloBaseInfo
    {
        int _idArticulo;
        string _idInterno;
        string _nombre;
        bool _conTrazabilidad;
        string _gtin;
        string _nombreGtin14;
        string _lote;
        DateTime _fechaVencimiento;
        double _cantidad;
        string _fechaVencimientoAndroid;
        int _diasMaxVencimientoAlisto;
        DateTime _today;

        public ArticuloBaseInfo()
        {           
        }

        public ArticuloBaseInfo(int idArticulo, string idInterno, string nombre, bool conTrazabilidad
            , string gtin, string nombreGtin14, string lote, DateTime fechaVencimiento, double cantidad
            , string fechaVencimientoAndroid, int diasMaxVencimientoAlisto )
        {
            _idArticulo = idArticulo;
            _idInterno= idInterno;
            _nombre = nombre;
            _conTrazabilidad = conTrazabilidad;
            _gtin = gtin;
            _nombreGtin14 = nombreGtin14;
            _lote = lote;
            _fechaVencimiento = fechaVencimiento;
            _cantidad = cantidad;
            _fechaVencimientoAndroid = fechaVencimientoAndroid;
            _diasMaxVencimientoAlisto = diasMaxVencimientoAlisto;
        }
        [DataMember]
        public int IdArticulo { get { return _idArticulo; }  set { _idArticulo = value; } }
        [DataMember]
        public string IdInterno { get { return _idInterno; } set { _idInterno = value; } }
        [DataMember]
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        [DataMember]
        public bool ConTrazabilidad { get { return _conTrazabilidad; } set { _conTrazabilidad = value; } }
        [DataMember]
        public string Gtin { get { return _gtin; } set { _gtin = value; } }
        [DataMember]
        public string NombreGtin14 { get { return _nombreGtin14; } set { _nombreGtin14 = value; } }
        [DataMember]
        public string Lote { get { return _lote; } set { _lote = value; } }
        [DataMember]
        public DateTime FechaVencimiento { get { return _fechaVencimiento; } set { _fechaVencimiento = value; } }
        [DataMember]
        public double Cantidad { get { return _cantidad; } set { _cantidad = value; } }
        [DataMember]
        public string FechaVencimientoAndroid { get { return _fechaVencimientoAndroid; } set { _fechaVencimientoAndroid = value; } }
        [DataMember]
        public int DiasMaxVencimientoAlisto { get => _diasMaxVencimientoAlisto; set => _diasMaxVencimientoAlisto = value; }
        [DataMember]
        public DateTime Today { get => DateTime.Now ; set => _today = value; }
    }
}
