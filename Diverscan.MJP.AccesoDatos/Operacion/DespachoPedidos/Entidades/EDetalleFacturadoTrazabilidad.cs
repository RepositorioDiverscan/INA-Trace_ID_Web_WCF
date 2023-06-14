using Diverscan.MJP.AccesoDatos.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Operacion.DespachoPedidos.Entidades
{
    [Serializable]
    public class EDetalleFacturadoTrazabilidad
    {

        private string _lote;
        private string _fechaVencimiento;
        private string _idInternoArticulo;
        private string _nombre;
        private decimal _cantidad;
        private long _idMaestroSolicitud;

        public EDetalleFacturadoTrazabilidad()
        {
        }

        public EDetalleFacturadoTrazabilidad(string idInternoArticulo, string nombreArticulo,
            decimal cantidad, string lote, string fechaVencimiento)
        {
            _idInternoArticulo = idInternoArticulo;
            _nombre = nombreArticulo;
            _cantidad = cantidad;
            _lote = lote;
            _fechaVencimiento = fechaVencimiento;           
        }

        public string IdInternoArticulo { get => _idInternoArticulo; set => _idInternoArticulo = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public decimal Cantidad { get => _cantidad; set => _cantidad = value; }
        public string Lote { get => _lote; set => _lote = value; }
        public string FechaVencimiento {
            get {
                if (_fechaVencimiento.CompareTo("1/1/1900") == 0 ||
                    _fechaVencimiento.CompareTo("01/01/1900") == 0)
                    _fechaVencimiento = "NA";

                return _fechaVencimiento;
            }
            set => _fechaVencimiento = value; }

        public long IdMaestroSolicitud { get => _idMaestroSolicitud; set => _idMaestroSolicitud = value; }
    }
}
