using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Operaciones
{
    [Serializable]
    public class eIngresoCajaChica
    {
        public int IdMaestroIngresoCajaChica { get; set; }
        public string IdInterno { get; set; }
        public string DesCorta { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string NumeroValeCajaChica { get; set; }
        public string NumeroTransaccion { get; set; }
        public string IdCompania { get; set; }
        public string NumeroCertificado { get; set; }
        public string NumeroFactura { get; set; }
        public string NombreProveedor { get; set; }
        public Boolean Procesada { get; set; }
        public DateTime FechaProcesamiento { get; set; }
        public double PorcentajeRecepcion { get; set; }
        public string Usuario { get; set; }
        public  string BodegaDefecto { get; set; }

    }
}
