using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{
    [Serializable]
    public class e_HistoricoDemanda
    {
        //public e_HistoricoDemanda()
        //{
        //    this.HistoricoDemandaList = new List<e_HistoricoDemanda>();
        //}

        public string NombreArticulo { get; set; }
        public string CodInterno { get; set; }

        public string Año { get; set; }
        public string Mes { get; set; }
        public string Semana { get; set; }

        public float Lunes { get; set; }
        public float Martes { get; set; }
        public float Miercoles { get; set; }
        public float Jueves { get; set; }
        public float Viernes { get; set; }
        public float Sabado { get; set; }
        public float Domingo { get; set; }

        public float TotalSemana { get; set; }

        public float TotalLunes { get; set; }
        public float TotalMartes { get; set; }
        public float TotalMiercoles { get; set; }
        public float TotalJueves { get; set; }
        public float TotalViernes { get; set; }
        public float TotalSabado { get; set; }
        public float TotalDomingo { get; set; }
        

        //public IList<e_HistoricoDemanda> HistoricoDemandaList { get; set; }
    }

    //public class e_ProveedorGrid
    //{
    //    public int OrdenCompra { get; set; }
    //    public string Proveedor { get; set; }
    //    public string DescripcionArticulo { get; set; }
    //    public string Lote { get; set; }
    //    public DateTime FechaVencimiento { get; set; }
    //    public decimal CantidadRecibida { get; set; }
    //    public decimal CantidadPorRecibir { get; set; }
    //    public string Diferencia { get; set; }
    //    public DateTime FechaRegistro { get; set; }
    //}
}
