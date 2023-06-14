using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{
    [Serializable] 
    public class e_KardexReport
    {
        public e_KardexReport()
        {
            this.KardexReportList = new List<e_KardexReport>();
        }

        //public int Linea { get; set; }
        //public int IdRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        //public string IdArticulo { get; set; }
        public string ArticuloSAP { get; set; }
        public string Articulo { get; set; }
        public string Zona { get; set; }
        public string Motivo { get; set; }
        public char Operacion { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal Cantidad { get; set; }
        //public decimal Cantidad_Unidad { get; set; }
        public decimal Saldo { get; set; }
        public decimal Saldo_actual { get; set; }
        public string Lote { get; set; }
        public DateTime FechaVencimiento { get; set; }
        //public string Orden { get; set; }
        public string OCDestino { get; set; }
        public int IdMetodoAccion { get; set; }
        public decimal Peso { get; set; }
        public string Unidad { get; set; }
        public decimal Unid_inventario { get; set; }
        public string Unidad_medida { get; set; }
        public string Saldo_Unidad { get; set; }
        public string FecharegistroExport { get  {return FechaRegistro.ToShortDateString();}  }
        public string FechaVencimientoExport { get { return FechaVencimiento.ToShortDateString(); } }
        public IList<e_KardexReport> KardexReportList { get; set; }

    }
}
