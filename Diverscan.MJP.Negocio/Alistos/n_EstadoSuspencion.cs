using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.AccesoDatos.Alistos;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Utilidades;

namespace Diverscan.MJP.Negocio.Alistos
{
    public class n_EstadoSuspencion
    {
        public static string GetEstadoSuspencion(int idTareaU)
        {
            return da_EstadoSuspencion.GetEstadoSuspencion(idTareaU);
        }
    }
}
