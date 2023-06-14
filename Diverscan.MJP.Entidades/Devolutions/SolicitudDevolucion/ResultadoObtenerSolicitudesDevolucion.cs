using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Devolutions.SolicitudDevolucion
{
    public class ResultadoObtenerSolicitudesDevolucion : ResultWS
    {
        private List<e_EncabezadoSolicitudDevolucion> _EncabezadosSolicitudDev = new List<e_EncabezadoSolicitudDevolucion>();

        public List<e_EncabezadoSolicitudDevolucion> encabezadosSolicitudDev { get => _EncabezadosSolicitudDev; set => _EncabezadosSolicitudDev = value; }
    }
}
