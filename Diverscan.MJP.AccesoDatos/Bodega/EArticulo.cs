using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.ModuloConsultas
{
    [Serializable]
    public class EArticulo
    {
        public string IdInterno { get; set; }
        public string Nombre { get; set; }
        public int IdArticulo { get; set; }
        public double Cantidad { get; set; }
        public string Lote { get; set;}
        public string Descripcion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string FechaAndroid { get; set; }
        public DateTime FUTSAlida { get; set; }
        public bool ConTrazabilidad { get; set; } 
        


        public string FechaVencimientoToShow
        {
            get
            {
                if (FechaVencimiento.Year == 1900)
                    return "NA";
                return FechaVencimiento.ToString("dd/MM/yyyy");
            }
            set { FechaVencimientoToShow = value; }
        }
    }
}
