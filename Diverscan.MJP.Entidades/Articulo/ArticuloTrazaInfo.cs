using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.Articulo
{
    [DataContract]
    public class ArticuloTrazaInfo
    {
        long _idArticulo;
        string _lote;
        DateTime _fechaVencimiento;
        string _fechaVencimientoAndroid;
        long _idUbicacion;

        public ArticuloTrazaInfo(long idArticulo, string lote, DateTime fechaVencimiento, long idUbicacion)
        {
            _idArticulo = idArticulo;
            _lote = lote;
            _fechaVencimiento = fechaVencimiento;
            _idUbicacion = idUbicacion;
        }

        [DataMember]
        public long IdArticulo
        {
            get { return _idArticulo; }
            set { _idArticulo = value; }
        }

        [DataMember]
        public string Lote
        {
            get { return _lote; }
            set { _lote = value; }
        }

        [DataMember]
        public DateTime FechaVencimiento
        {
            get { return _fechaVencimiento; }
            set { _fechaVencimiento = value; }
        }

        [DataMember]
        public string FechaVencimientoAndroid
        {
            get
            {
                if (FechaVencimiento == null)
                {
                    return "";
                }
                return FechaVencimiento.ToShortDateString();
            }
            set
            {
                _fechaVencimientoAndroid = value;
                if (!String.IsNullOrEmpty(_fechaVencimientoAndroid))
                {
                    FechaVencimiento = DateTime.ParseExact(_fechaVencimientoAndroid, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
                }
            }
        }

        [DataMember]
        public long IdUbicacion
        {
            get { return _idUbicacion; }
            set { _idUbicacion = value; }
        }
    }
}
