using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.AccesoDatos.ProcesarSolicitud;
using Diverscan.MJP.Entidades;

namespace Diverscan.MJP.Negocio.ProcesarSolicitud
{
    class n_Insertapredetallesolicitud
    {
        public string InsertarPreDetalleSolicitud(e_Insertapredetallesolicitud InsertarPreDetalleSolicitud)
        {
            da_InsertaPreDetalleSolicitud InsertaPreDetalleSolicitud = new da_InsertaPreDetalleSolicitud();
            string result = InsertaPreDetalleSolicitud.InsertarPreDetalleSolicitud(InsertarPreDetalleSolicitud);
            return result;
        }
    }
}
