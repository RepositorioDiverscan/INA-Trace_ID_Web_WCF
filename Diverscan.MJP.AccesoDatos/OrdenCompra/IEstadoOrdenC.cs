using Diverscan.MJP.Entidades.OrdenCompra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.OrdenCompra
{
    public interface IEstadoOrdenC
    {
        List<EstadoOrdenCompra> StatusActualOrden(string idMaestroArticulo);
    }
}
