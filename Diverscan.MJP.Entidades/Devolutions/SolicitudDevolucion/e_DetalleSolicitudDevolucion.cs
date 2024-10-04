using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.Devolutions.SolicitudDevolucion
{
    [Serializable]
    public class e_DetalleSolicitudDevolucion
    {
        private long _IdArticulo;
        private int _Cantidad;
        private string _Placa;
        private string _Lote;
        private string _FechaVencimiento;
        private string _NombreArticulo;
        private string _Marca;
        private long _IdDetalleSolicitudDevolucion;
        private bool _esActivo;

        public e_DetalleSolicitudDevolucion(long idArticulo, int cantidad, string placa, string lote, string fechaVencimiento)
        {
            this._IdArticulo = idArticulo;
            this._Cantidad = cantidad;
            this._Placa = placa;
            this._Lote = lote;
            this._FechaVencimiento = fechaVencimiento;
        }

        public e_DetalleSolicitudDevolucion(long idArticulo, int cantidad, string placa, string lote, string fechaVencimiento, string nombreArticulo, string marca, long idDetalleSolicitudDevolucion, bool esActivo)
        {
            _IdArticulo = idArticulo;
            _Cantidad = cantidad;
            _Placa = placa;
            _Lote = lote;
            _FechaVencimiento = fechaVencimiento;
            _NombreArticulo = nombreArticulo;
            _Marca = marca;
            _IdDetalleSolicitudDevolucion = idDetalleSolicitudDevolucion;
            _esActivo = esActivo;
        }

        public long IdArticulo { get => _IdArticulo; set => _IdArticulo = value; }
        public int Cantidad { get => _Cantidad; set => _Cantidad = value; }
        public string Placa { get => _Placa; set => _Placa = value; }
        public string Lote { get => _Lote; set => _Lote = value; }
        public string FechaVencimiento { get => _FechaVencimiento; set => _FechaVencimiento = value; }
        public string NombreArticulo { get => _NombreArticulo; set => _NombreArticulo = value; }
        public string Marca { get => _Marca; set => _Marca = value; }
        public long IdDetalleSolicitudDevolucion { get => _IdDetalleSolicitudDevolucion; set => _IdDetalleSolicitudDevolucion = value; }
        public bool EsActivo { get => _esActivo; set => _esActivo = value; }
    }
}
