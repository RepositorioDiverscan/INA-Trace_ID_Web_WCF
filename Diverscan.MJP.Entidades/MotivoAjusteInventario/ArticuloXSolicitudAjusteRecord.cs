using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.MotivoAjusteInventario
{
    [Serializable]
    [DataContract]
    public class ArticuloXSolicitudAjusteRecord : ArticuloRecord
    {
        protected long _idSolicitudAjusteInventario;
        string _lote;
        DateTime _fechaVencimiento;
        long _idUbicacionActual;
        long _idUbicacionMover;
        int _cantidad;
        decimal _cantidadUI;
        DateTime _sincronizado;
        string _fechaVencimientoAndroid;
        string _sincronizadoAndroid;

        public ArticuloXSolicitudAjusteRecord()
        {
        }

        public ArticuloXSolicitudAjusteRecord(long idArticulo, string codigoInterno, string nombreArticulo,
        string unidadMedida, bool esGranel, string lote,
           DateTime fechaVencimiento, long idUbicacionActual, long idUbicacionMover, int cantidad)
            : base(idArticulo, codigoInterno, nombreArticulo, unidadMedida, esGranel)
        {
            _lote = lote;
            _fechaVencimiento = fechaVencimiento;
            _idUbicacionActual = idUbicacionActual;
            _idUbicacionMover = idUbicacionMover;
            _cantidad = cantidad;
        }

        public ArticuloXSolicitudAjusteRecord(long idSolicitudAjusteInventario, long idArticulo, string lote,
            DateTime fechaVencimiento, long idUbicacionActual, long idUbicacionMover, int cantidad, DateTime sincronizado)
        {
            _idSolicitudAjusteInventario = idSolicitudAjusteInventario;
            IdArticulo = idArticulo;
            _lote = lote;
            _fechaVencimiento = fechaVencimiento;
            _idUbicacionActual = idUbicacionActual;
            _idUbicacionMover = idUbicacionMover;
            _cantidad = cantidad;
            _sincronizado = sincronizado;
        }

        public ArticuloXSolicitudAjusteRecord(long idArticulo, string lote,
            DateTime fechaVencimiento, long idUbicacionActual, long idUbicacionMover, int cantidad)
        {
            IdArticulo = idArticulo;
            _lote = lote;
            _fechaVencimiento = fechaVencimiento;
            _idUbicacionActual = idUbicacionActual;
            _idUbicacionMover = idUbicacionMover;
            _cantidad = cantidad;
        }

        //Constructor para mostrar los datos con cantidadUI
        public ArticuloXSolicitudAjusteRecord(long idArticulo, string codigoInterno, string nombreArticulo,
            string unidadMedida, bool esGranel, string lote,
            DateTime fechaVencimiento, long idUbicacionActual, long idUbicacionMover, int cantidad, decimal cantidadUI)
            : base(idArticulo, codigoInterno, nombreArticulo, unidadMedida, esGranel)
        {
            _lote = lote;
            _fechaVencimiento = fechaVencimiento;
            _idUbicacionActual = idUbicacionActual;
            _idUbicacionMover = idUbicacionMover;
            _cantidad = cantidad;
            _cantidadUI = cantidadUI;
        }

        [DataMember]
        public long IdSolicitudAjusteInventario
        {
            get { return _idSolicitudAjusteInventario; }
            set { _idSolicitudAjusteInventario = value; }
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
        public long IdUbicacionActual
        {
            get { return _idUbicacionActual; }
            set { _idUbicacionActual = value; }
        }

        [DataMember]
        public long IdUbicacionMover
        {
            get { return _idUbicacionMover; }
            set { _idUbicacionMover = value; }
        }

        [DataMember]
        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        [DataMember]
        public decimal CantidadUI
        {
            get { return _cantidadUI; }
            set { _cantidadUI = value; }
        }

        
        public DateTime Sincronizado
        {
            get { return _sincronizado; }
            set { _sincronizado = value; }
        }

        public decimal CantidadToShow
        {
            get
            {
                if (EsGranel)
                    return (decimal)(_cantidad / 1000d);
                return _cantidad;
            }

        }

        [DataMember]
        public string FechaVencimientoAndroid {
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
        public string SincronizadoAndroid {
            get
            {
                if (Sincronizado == null)
                {
                    return "";
                }
                return Sincronizado.ToShortDateString();
            }
            set
            {
                _sincronizadoAndroid = value;
                if (!String.IsNullOrEmpty(_sincronizadoAndroid))
                {
                    FechaVencimiento = DateTime.ParseExact(_sincronizadoAndroid, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
                }
            }
        }
      
    }
}
