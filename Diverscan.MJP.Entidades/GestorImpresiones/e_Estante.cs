using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.GestorImpresiones.Entidades
{
    public class e_Estante
    {
        public int IdEstante { get; set; }
        public int IdBodega { get; set; }
        public string Almacen { get; set;}
        public string Bodega { get; set; }
        public string Nombre { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public int Pos { get; set; }
        public double Largo { get; set; }
        public double AreaAncho { get; set; }
        public double Alto { get; set; }
        public string Cara { get; set; }
        public int Profundidad { get; set; }

        public int TipoImpresion { get; set; }
        public string idUbicacionPiso { get; set; }
        public string idMaestroUbicacion { get; set; }

    }
}
