using Diverscan.MJP.AccesoDatos.Reportes.OlasFinalizadas;
using Diverscan.MJP.Entidades.Reportes.OlasFinalizadas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Reportes.OlasFinalizadas
{
 public    class NOlaFinalizada
    {
        //Metodo para obtener la devolucion por buen estado 
        public List<EOlaFinalizada> ConsultaOla( DateTime f1, DateTime f2)
        {
            try
            {
                DOlaFinalizada ola = new DOlaFinalizada();
                return ola.ConsultarOla( f1, f2);
            }
            catch (Exception e)
            {
                var p = e.Message;
                return null;
            }
        }
    }
}
