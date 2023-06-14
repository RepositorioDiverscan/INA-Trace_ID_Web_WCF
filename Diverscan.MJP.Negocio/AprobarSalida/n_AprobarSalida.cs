using Diverscan.MJP.AccesoDatos.AprobarSalida;
using Diverscan.MJP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.AprobarSalida
{
    public class n_AprobarSalida
    {
        public static List<e_AprobarSalida> GetAprobarSalidas()
        {
            da_AprobarSalida da_aprobarSalida = new da_AprobarSalida();
            return da_aprobarSalida.GetAprobarSalidas();
        }
    }
}
