using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.AccesoDatos.Operaciones;
using Diverscan.MJP.Entidades.Operaciones;

namespace Diverscan.MJP.Negocio.OrdenesCompras
{
    public class ordenesCompras
    {
        public DateTime fechaInicioBusqueda { get; set; }
        public DateTime fechaFinBusqueda { get; set; }
        public string numOrdenCompra { get; set; }
        public string idProveedor { get; set; }
    }
}
