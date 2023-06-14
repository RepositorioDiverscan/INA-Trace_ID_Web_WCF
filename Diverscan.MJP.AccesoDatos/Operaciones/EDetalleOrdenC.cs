using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Operaciones
{
    [Serializable]
    public class EDetalleOrdenC
    {
        public int IdArticulo { get; set; }
        public string IdInterno { get; set; }
        public string numFactura { get; set; }
        public string GTIN { get; set; }
        public string Nombre { get; set; }
        public double CantidadxRecibir { get; set; }
        public double CantidadRecibidos { get; set; }
        public double CantidadRechazados { get; set; }
        public string DescripcionRechazo { get; set; }
        public int CantidadBodega { get; set; }
        public string NumLinea { get; set; }
        public double CantidadTransito { get; set; }
        public int CantidadPendientes
        {
            get
            {
                return Convert.ToInt32(CantidadxRecibir - CantidadRecibidos);
            }
        }

        public double PorcentajeCumplimiento
        {
            get
            {
                var result = (100 / CantidadxRecibir * CantidadRecibidos);
                return Convert.ToDouble(string.Format("{0:0.00}", result));
            }
        }


    }
}
