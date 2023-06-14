using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Devolutions.SolicitudDevolucion
{
    public class ResultadoObtenerDetallesSolicitud : ResultWS
    {
        private List<e_DetalleSolicitudDevolucion> _Detalles = new List<e_DetalleSolicitudDevolucion>();

        public List<e_DetalleSolicitudDevolucion> detalles { get => _Detalles; set => _Detalles = value; }

    }
}
