using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Operaciones
{
    [Serializable]
    public class eOrdenCompra
    {

        public int idMaestroOrdenCompra { get; set; }
        public string NumeroTransaccion { get; set; }
        public string comentario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NumeroCertificado { get; set; }
        public Boolean Procesada { get; set; }
        public DateTime FechaProcesamiento { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreProveedor { get; set; }
        public string Estado { get; set; }
        public double PorcentajeRecepcion { get; set; }
        public string BodegaDefecto { get; set; }
    }
}
