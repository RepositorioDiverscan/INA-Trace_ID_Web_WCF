using Diverscan.MJP.AccesoDatos.OrdenCompra;
using Diverscan.MJP.Entidades.OrdenCompra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.OrdenCompra
{
    public class EstadoOrdenCLoader
    {
        public static List<EstadoOrdenCompra> StatusActualOrden(string idMaestroArticulo)
        {
            IEstadoOrdenC estadoAlistos = new EstadoOrdenC();
            return estadoAlistos.StatusActualOrden(idMaestroArticulo);
        }
    }
}
