using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Operacion.DespachoPedidos.Entidades
{
    [Serializable]
    public class E_ListadoDetalleOla
    {
        public E_ListadoDetalleOla(int idRegistroOla, int idMaestroSolicitud, string idInternoPanal, string nombre, decimal cantidadSolicitada, decimal cantidadAlistada, decimal cantidadDisponible, string nombreUsuario)
        {
            IdRegistroOla = idRegistroOla;
            IdMaestroSolicitud = idMaestroSolicitud;
            IdInternoPanal = idInternoPanal;
            Nombre = nombre;
            CantidadSolicitada = cantidadSolicitada;
            CantidadAlistada = cantidadAlistada;
            CantidadDisponible = cantidadDisponible;
            NombreUsuario = nombreUsuario;
         
        }      
        public E_ListadoDetalleOla()
        { }
        public int IdRegistroOla { get; set; }
        public int IdMaestroSolicitud { get; set; }
        public string IdInternoPanal { get; set; }
        public string Nombre { get; set; }
        public string NombreUsuario { get; set; }
        public decimal CantidadSolicitada { get; set; }
        public decimal CantidadAlistada { get; set; }
        public decimal CantidadDisponible{ get; set; }


    }
}
