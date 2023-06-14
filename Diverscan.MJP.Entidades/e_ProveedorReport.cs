using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{
    [Serializable]
    public class e_ProveedorReport
    {
        public e_ProveedorReport()
        {
            this.ProveedorReportList = new List<e_ProveedorReport>();
        }

        public int OrdenCompra { get; set; }
        public string Proveedor { get; set; }
        public int NumeroDocumentoAccion { get; set; }
        public string ArtSap { get; set; }
        public int IdArticulo { get; set; }
        public string NombreArticulo { get; set; }
        public string Lote { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal CantidadRecibida { get; set; }
        public decimal CantidadRecibidaMostrar 
        {
            get 
            {
                if (EsGranel)
                    return (decimal) (CantidadRecibida / 1000);
                return CantidadRecibida;
            }
        
        }
        public decimal CantidadPorRecibir { get; set; }
        public string Diferencia { get; set; }
        public decimal DiferenciaMostrar 
        {
            get 
            {
                return CantidadPorRecibir - CantidadRecibidaMostrar;
            }
        }
        public char Simbolo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool EsGranel { get; set; }

        public IList<e_ProveedorReport> ProveedorReportList { get; set; }
    }

    public class e_ProveedorGrid
    {
        public int OrdenCompra { get; set; }
        public string Proveedor { get; set; }
        public string DescripcionArticulo { get; set; }
        public string Lote { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal CantidadRecibida { get; set; }
        public decimal CantidadPorRecibir { get; set; }
        public string Diferencia { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
