using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.AccesoDatos.Traslados;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Diverscan.MJP.Negocio.Traslados
{
   public class n_Traslados
    {
        public string ProcesarTraslados(Int64 idArticulo, string Lote, string FV, Int64 idUbicacionorigen, Int64 idUbicaciondestino, Int64 Cantidad, int idUsuario, int idMetodoAccionSalida, int idMetodoAccionEntrada)
        {
            da_Traslados Ptraslados = new da_Traslados();
            string result = Ptraslados.ProcesarTraslados(idArticulo, Lote, FV, idUbicacionorigen, idUbicaciondestino, Cantidad, idUsuario, idMetodoAccionSalida, idMetodoAccionEntrada);
            return result;
        }

        public string ObtenerIdUbicacion(string Descripcion)
        {
            da_Traslados Ptraslados = new da_Traslados();
            string result = Ptraslados.ObtenerIdUbicacion(Descripcion);
            return result;
        }


    }
}
