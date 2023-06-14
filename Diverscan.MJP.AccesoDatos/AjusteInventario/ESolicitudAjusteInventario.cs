using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.AjusteInventario
{
    [Serializable]
    public class ESolicitudAjusteInventario
    {
        int _idArticulo;
        string _lote;
        DateTime _fechaVencimiento;
        int _idUbicacionActual;
        int _idUbicacionMover;
        int _cantidad;

        public ESolicitudAjusteInventario(int IdArticulo, string Lote, DateTime FechaVencimiento, int IdUbicacionActual, int IdUbicacionMover, int Cantidad)
        {
            _idArticulo = IdArticulo;
            _lote = Lote;
            _fechaVencimiento = FechaVencimiento;
            _idUbicacionActual = IdUbicacionActual;
            _idUbicacionMover = IdUbicacionMover;
            _cantidad = Cantidad;
        }

        public int IdArticulo 
        { 
            get
            {
                return _idArticulo;
            }
        }
        string Lote 
        {
            get
            {
                return _lote;
            }
        }
        DateTime FechaVencimiento 
        { 
            get
            {
                return _fechaVencimiento;
            }
        }
        public int IdUbicacionActual
        { 
            get
            {
                return _idUbicacionActual;
            }
        }
        public int IdUbicacionMover 
        { 
            get
            {
                return _idUbicacionMover;
            }
        }
        public int Cantidad 
        { 
            get
            {
                return _cantidad;
            }
        }
    }
}
