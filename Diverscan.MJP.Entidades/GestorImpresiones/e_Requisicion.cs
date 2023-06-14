using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.GestorImpresiones.Entidades
{
    public class e_Requisicion
    {
        public string IdRequisicion { get; set; }
        public string Fecha { get; set; }
        public string FechaMaxima { get; set; }
        public string Responsable { get; set; }
        public string Observaciones { get; set; }
        public string Programa { get; set; }
        public string Destino { get; set; }
        public string Almacen { get; set; }
        public string idTipoEstado { get; set; }
        public string Alistador { get; set; }
    }
}
