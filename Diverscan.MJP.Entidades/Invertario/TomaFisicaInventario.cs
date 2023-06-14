using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.Invertario
{
    [DataContract]
    public class TomaFisicaInventario
    {
        long _idTomaFisicaInventario;
        long _idInventario;
        long _idArticulo;
        long _idUbicacion;
        string _lote;
        DateTime _fechaVencimiento;
        int _cantidad;
        int _idUsuario;
        string _fechaVencimientoAndroid;

        public TomaFisicaInventario(long idTomaFisicaInventario, long idInventario,long idArticulo,
            long idUbicacion, string lote, DateTime fechaVencimiento, int cantidad, int UsuarioID)
        {
            _idTomaFisicaInventario = idTomaFisicaInventario;
            _idInventario = idInventario;
            _idArticulo = idArticulo;
            _idUbicacion = idUbicacion;
            _lote = lote;
            _fechaVencimiento = fechaVencimiento;
            _cantidad = cantidad;
            _idUsuario = UsuarioID;
        }

        public TomaFisicaInventario(long idInventario, long idArticulo, long idUbicacion,
            string lote, DateTime fechaVencimiento, int cantidad, int UsuarioID)
        {
            _idInventario = idInventario;
            _idArticulo = idArticulo;
            _idUbicacion = idUbicacion;
            _lote = lote;
            _fechaVencimiento = fechaVencimiento;
            _cantidad = cantidad;
            _idUsuario = UsuarioID;
        }
        
        public long IdTomaFisicaInventario
        {
            get { return _idTomaFisicaInventario; }
            set { _idTomaFisicaInventario = value; }
        }
        [DataMember]
        public long IdInventario
        {
            get { return _idInventario; }
            set { _idInventario = value; }
        }
        [DataMember]
        public long IdArticulo
        {
            get { return _idArticulo; }
            set { _idArticulo = value; }
        }
        [DataMember]
        public long IdUbicacion
        {
            get { return _idUbicacion; }
            set { _idUbicacion = value; }
        }
        [DataMember]
        public string Lote
        {
            get { return _lote; }
            set { _lote = value; }
        }
        
        public DateTime FechaVencimiento
        {
            get { return _fechaVencimiento; }
            set { _fechaVencimiento = value; }
        }
        [DataMember]
        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        [DataMember]
        public int UsuarioID
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }       

        [DataMember]
        public string FechaVencimientoAndroid
        {
            get
            {
                if (FechaVencimiento != null)
                    return FechaVencimiento.ToShortDateString();

                return "";
            }
            set
            {
                _fechaVencimientoAndroid = value;
                FechaVencimiento = DateTime.ParseExact(_fechaVencimientoAndroid, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
            }
        }
    }
}
