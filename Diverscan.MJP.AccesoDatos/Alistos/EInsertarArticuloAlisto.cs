using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Alistos
{
    public class EInsertarArticuloAlisto
    {
        long _idConsecutivoSSCC;
        long _idMaestroSolicitud;
        long _idArticulo;
        string _lote;
        DateTime _FechaVencimiento;
        int _cantidad;
        long _idUbicacion;
        long _idLineaDetalleSolicitud;

        public EInsertarArticuloAlisto(long idConsecutivoSSCC, long idMaestroSolicitud, long idArticulo,string lote, DateTime FechaVencimiento,
                                       int cantidad, long idUbicacion, long idLineaDetalleSolicitud)
        {
           _idConsecutivoSSCC= idConsecutivoSSCC;
           _idMaestroSolicitud= idMaestroSolicitud;
           _idArticulo= idArticulo;
           _lote= lote;
           _FechaVencimiento= FechaVencimiento;
           _cantidad= cantidad;
           _idUbicacion= idUbicacion;
           _idLineaDetalleSolicitud= idLineaDetalleSolicitud;
        }


        public long IdConsecutivoSSCC { get => _idConsecutivoSSCC; set => _idConsecutivoSSCC = value; }
        public long IdMaestroSolicitud { get => _idMaestroSolicitud; set => _idMaestroSolicitud = value; }
        public long IdArticulo { get => _idArticulo; set => _idArticulo = value; }
        public string Lote { get => _lote; set => _lote = value; }
        public DateTime FechaVencimiento { get => _FechaVencimiento; set => _FechaVencimiento = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }
        public long IdUbicacion { get => _idUbicacion; set => _idUbicacion = value; }
        public long IdLineaDetalleSolicitud { get => _idLineaDetalleSolicitud; set => _idLineaDetalleSolicitud = value; }
    }
}
