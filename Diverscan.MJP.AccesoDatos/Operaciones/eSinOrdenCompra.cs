using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Operaciones
{
    [Serializable]
    public class eSinOrdenCompra
    {

        public string NumeroTransaccion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombreProveedor { get; set; }
        public string NumeroCertificado { get; set; }
        public DateTime FechaProcesamiento { get; set; }
        public int IdMaestroSinOrdenCompra { get; set; }
        public string NumeroFactura { get; set; }
        public double PorcentajeRecepcion { get; set; }
        public string BodegaDefecto { get; set; }
        public string DescCorta { get; set; }
        public string NombreUsuario { get; set; }
        public string Estado { get; set; }


    }
}
