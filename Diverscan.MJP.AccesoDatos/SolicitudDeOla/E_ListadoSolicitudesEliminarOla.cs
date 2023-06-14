using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.SolicitudDeOla
{
    public class E_ListadoSolicitudesEliminarOla
    {

        public E_ListadoSolicitudesEliminarOla(int idOla, int idPreMaestroSolicitud, string nombre, DateTime fechaIngreso)
        {
            IdOla = idOla;
            IdPreMaestroSolicitud = idPreMaestroSolicitud;
            Nombre = nombre;
            FechaIngreso = fechaIngreso;
        }

        public int IdOla { get; set; }
        public int IdPreMaestroSolicitud { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaIngreso { get; set; }
     
    }
}
