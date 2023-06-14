using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.AccesoDatos.ProcesarRecepcionUbicacion;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Diverscan.MJP.Negocio.ProcesarRecepcionUbicacion
{
    public class n_ProcesarRecepcionUbicacion
    {
        public string ProcesaRecepcion(Int64 idMaestroOC, Int64 idArticulo, Int64 Cantidad, int idUsuario, string idCompania, 
                                       string ZonaRecepcion, Int64 IdBodega, Int64 IdMetodoAccion, string FV, string Lote)
        {
            da_ProcesarRecepcionUbicacion Recepcion = new da_ProcesarRecepcionUbicacion();
            string result = Recepcion.ProcesarRecepcion(idMaestroOC, idArticulo, Cantidad, idUsuario, idCompania, ZonaRecepcion, IdBodega, IdMetodoAccion, FV, Lote);
            return result;
        }

        public string ProcesaUbicacion(Int64 idArticulo, Int64 Cantidad, int idUsuario, string idCompania, string ZonaPic, string ZonaAlm, Int64 IdMetodoAccion,
                                        string FV, string Lote, string EtiqUbic)
        {
            da_ProcesarRecepcionUbicacion Ubicacion = new da_ProcesarRecepcionUbicacion();
            string result = Ubicacion.ProcesarUbicacion(idArticulo, Cantidad, idUsuario, idCompania, ZonaPic, ZonaAlm, IdMetodoAccion, FV, Lote, EtiqUbic);
            return result;
        }

        public string TotalArticuloyPendienteOC(Int64 idArticulo, Int64 idMaestroOC)
        {
            da_ProcesarRecepcionUbicacion Contador = new da_ProcesarRecepcionUbicacion();
            string result = Contador.TotalArticuloyPendienteOC(idArticulo, idMaestroOC);
            return result;
        }
    }


}
