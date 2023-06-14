using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.Entidades.Reportes.Despachos;
using Diverscan.MJP.Entidades.Reportes.Kardex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Reportes
{
    public class n_Despachos
    {
        private da_DespachosDBA da_DespachosDBA = new da_DespachosDBA();
        public List<e_Detalle_Despacho_Por_Numero_Solicitud> ObtenerDetalleDespachoPorNumeroSolicitud(long pNumeroSolicitud, ref DateTime fechaPedidoOUT, ref string destinoDescripcion, ref string idInternoSolicitud, ref string idSolicitudTID)
        {
            return da_DespachosDBA.ObtenerDetalleDespachoPorNumeroSolicitud(pNumeroSolicitud, ref fechaPedidoOUT, ref destinoDescripcion, ref idInternoSolicitud, ref idSolicitudTID);
        }

        //Se obtiene la cosulta para el CReport        
        public List<e_Detalle_Despacho_Por_Numero_Solicitud_Reporte> ObtenerDetalleDespachoPorNumeroSolicitudReporte(long pNumeroSolicitud)
        {            
            List<e_Detalle_Despacho_Por_Numero_Solicitud_Reporte> listaDatos = da_DespachosDBA.ObtenerDetalleDespachoPorNumeroSolicitudReporte(pNumeroSolicitud);
            return ObtenerTotalesDespachoPorNumeroSolicitudReporte(listaDatos); //Se obtienen los totales            
        }

        public List<e_Detalle_Despacho_Por_Numero_Solicitud_Reporte> ObtenerTotalesDespachoPorNumeroSolicitudReporte(List<e_Detalle_Despacho_Por_Numero_Solicitud_Reporte> listaRegistos)
        {
            try
            {
                decimal _totalCosto = 0;
                decimal _cantidadBultos = 0;
                int _cantidadLineas = listaRegistos.Count;

                //Obtención de totales 
                foreach (var item in listaRegistos)
                {
                    _totalCosto += Decimal.Round(item.Total, 2);
                    _cantidadBultos += Decimal.Round(item.Bultos, 2);
                }

                //SeTting de totales
                foreach (var item in listaRegistos)
                {
                    item.TotalCosto = Decimal.Round(_totalCosto, 2);
                    item.CantidadBultos = _cantidadBultos;
                    item.CantidadLineas = _cantidadLineas;
                }
            }
            catch (Exception ex)
            { throw ex; }

            return listaRegistos;
        }
        public decimal ObtenerSubTotalDespachoPorNumSolitud(List<e_Detalle_Despacho_Por_Numero_Solicitud> listaDatos)
        {
            decimal subtotal = 0;
            foreach (var item in listaDatos)
            {
                subtotal += Decimal.Round(item.Total, 2);
            }
            return subtotal;
        }
        public List<e_Pedidos_Despacho> ObtenerPedidosDespachos(DateTime fechaInicio, DateTime fechaFin, bool filtroSoloDespachados)
        {
            return da_DespachosDBA.ObtenerPedidosDespachos(fechaInicio, fechaFin, filtroSoloDespachados);
        }
    }
}
