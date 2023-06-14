using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.SolicitudDeOla
{
    [Serializable]
    public class E_ListadoPreMaestro
    {
        
        public E_ListadoPreMaestro(int idMaestroSolicitud, string nombre, string nombreCliente, string comentarios,
            DateTime fechaCreacion, string ruta, string direccion)
        {
            this.idMaestroSolicitud = idMaestroSolicitud;
            Nombre = nombre;
            NombreCliente = nombreCliente;
            Comentarios = comentarios;
            FechaCreacion = fechaCreacion;
            Ruta = ruta;
            Direccion = direccion;
        }

        //idMaestroSolicitud, Nombre, Comentarios, FechaCreacion
        public int idMaestroSolicitud { get; set; }
        public string Nombre { get; set; }
        public string NombreCliente { get; set; }
        public string Comentarios { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Ruta { get; set; }
        public string Direccion { get; set; }
    }
}
