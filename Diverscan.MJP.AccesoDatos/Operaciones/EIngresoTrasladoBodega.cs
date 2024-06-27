using System;

namespace Diverscan.MJP.AccesoDatos.Operaciones
{
    public class EIngresoTrasladoBodega
    {
        public int IdMaestroIngresoTraslado { get; set; }
        public int IdBodega { get; set; }
        public int IdBodegaTraslado { get; set; }
        public string IdCompania { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Comentario { get; set; }
        public int IdUsuario { get; set; }
        public bool Procesada { get; set; }
        public DateTime FechaProcesamiento { get; set; }
        public string NumeroTransaccion { get; set; }
        public string Estado { get; set; }

        public int IdArticulo { get; set; }
        public int CantidadSolicitada { get; set; }
    }
}
