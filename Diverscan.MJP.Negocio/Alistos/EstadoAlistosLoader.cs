using Diverscan.MJP.AccesoDatos.Alistos;
using Diverscan.MJP.Entidades.Alistos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Alistos
{
    public class EstadoAlistosLoader
    {
        public static List<EstadoAlisto> StatusActualPedido(string idMaestroArticulo)
        {
            IEstadoAlistos estadoAlistos = new EstadoAlistos();
            return estadoAlistos.StatusActualPedido(idMaestroArticulo);
        }

       

    }
}
