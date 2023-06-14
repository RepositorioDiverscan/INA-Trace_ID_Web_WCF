using Diverscan.MJP.AccesoDatos.Reportes.Alisto;
using Diverscan.MJP.AccesoDatos.Reportes.Alisto.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Alistos
{
   public  class NAlisto
    {
        //metodo para obtener  la asignacion de alisto 
        public List<EListObtenerAsignacionAlisto> ConsultarAsignacionAlisto(DateTime f1, DateTime f2, string id)
        {
            try
            {
                da_Alisto alisto = new da_Alisto();
                return alisto.ObtenerDiaVecimientoArticulo(f1, f2, id);
            }
            catch (Exception e)
            {
                var p = e.Message;
                return null;
            }
        }
    }
}
