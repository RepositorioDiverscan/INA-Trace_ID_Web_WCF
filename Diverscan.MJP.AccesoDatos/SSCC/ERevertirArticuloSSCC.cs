using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.SSCC
{
    public class ERevertirArticuloSSCC
    {
        private long _idSSCC;
        private long _idMaestroSolicitud;
        private long _idArticulo;
        private long _idUbicacionDestino;
        private int  _cantidad;
        private int _idUsuario;
        private DateTime _fechaVencimiento;
        private string _fechaVencimientoAndroid;
        private string _lote;

        public ERevertirArticuloSSCC()
        {
        }

        public long IdSSCC { get => _idSSCC; set => _idSSCC = value; }
        public long IdMaestroSolicitud { get => _idMaestroSolicitud; set => _idMaestroSolicitud = value; }
        public long IdArticulo { get => _idArticulo; set => _idArticulo = value; }
        public long IdUbicacionDestino { get => _idUbicacionDestino; set => _idUbicacionDestino = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }
        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public DateTime FechaVencimiento { get => _fechaVencimiento; set => _fechaVencimiento = value; }
        public string Lote { get => _lote; set => _lote = value; }
        public string FechaVencimientoAndroid { get => _fechaVencimientoAndroid; set => _fechaVencimientoAndroid = value; }
    }
}
