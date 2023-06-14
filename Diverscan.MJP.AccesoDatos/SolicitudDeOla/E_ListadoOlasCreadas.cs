using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.SolicitudDeOla
{
    [Serializable]
    public class E_ListadoOlasCreadas
    {
        public E_ListadoOlasCreadas( int idRegistroOla, string idPreMaestroSolicitud, string fechaIngreso, string estado, string observacion)
        {
          
            this.idRegistroOla = idRegistroOla;
            this.idPreMaestroSolicitud = idPreMaestroSolicitud;
            FechaIngreso = fechaIngreso;
            Estado = estado;
            Observacion = observacion;
        }

       
        public int idRegistroOla { get; set; }
        public string idPreMaestroSolicitud { get; set; }
        public string FechaIngreso { get; set; }
        public string Observacion { get; set; }
        public string Estado { get; set; }
        
    }
}
