using Diverscan.MJP.AccesoDatos.Reportes.Alisto.Entidad;
using Diverscan.MJP.AccesoDatos.Reportes.Alisto.RecargaGuanacaste.Entidad;
using Diverscan.MJP.AccesoDatos.Reportes.AsignoCertificacion;
using Diverscan.MJP.AccesoDatos.Reportes.Certificacion.SSCCSINOFinalizado;
using Diverscan.MJP.AccesoDatos.Reportes.Cliente;
using Diverscan.MJP.AccesoDatos.Reportes.Inventario.UbicacionSku.Entidad;
using Diverscan.MJP.AccesoDatos.Reportes.Inventario.UnidadDisponibleTIDSAP.Entidad;
using Diverscan.MJP.AccesoDatos.Reportes.KardexXSkuTIDSAP.Entidad;
using Diverscan.MJP.AccesoDatos.Reportes.Ola.DespachoOla;
using Diverscan.MJP.AccesoDatos.Reportes.Ola.DisponibleFcturacion;
using Diverscan.MJP.AccesoDatos.Reportes.RotacionInventario.Entidad;
using Diverscan.MJP.AccesoDatos.Reportes.TransitoMercaderia.Entidad;
using Diverscan.MJP.AccesoDatos.Reportes.Traslado.Entidad;
using Diverscan.MJP.AccesoDatos.Reportes.VencimientoProducto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.ReportePedidoSinOla
{
    public interface IReportes
    {
        List<EListPedidoSinOla> PedidosSinOlas(EPedidosSinOla ePedidos);
        List<EListBodega> ListaBodegas();
        List<EListRotacionInventario> ListRotacionInventarios(ERotacionInventario rotacionInventario);
        List<EListVencimientoProducto> ObtenerDiaVecimientoArticulo(EVencimientoProducto vencimientoProducto);
        List<EListObtenerAsignacionAlisto> ObtenerDiaVecimientoArticulo(EObtenerAsignacionAlisto asignacionAlisto);
        List<EListObtenerOlasDisponiblesFacturacion> ObtenerOlasDisponiblesFacturacion(EObtenerOlasDisponiblesFacturacion disponiblesFacturacion);
        List<EListObtenerRecargaBodegaGuanacaste> ObtenerRecargaBodegaGuanacaste(EObtenerRecargaGuanacaste eObtenerRecargaGuanacaste);
        List<EListObtenerUbicacionSku> ObtenerTotalUbicacionesXsku(EObtenerUbicacionSku ubicacionSku);
        List<EListObtenerUbicacion> ObtenerUbicacion(EObtenerUbicacion ubicacion);
        List<EListObtenerTrasladoMercaderia> ObtenerTrasladoMercaderia(EObtenerTrasladoMercaderia obtenerTrasladoMercaderia);
        List<EListAsignacionCertificacion> ObtenerListadoAsignacionCertificacion(EAsignacionCertificacion asignacionCertificacion);
        List<EListSSCCSINOFinalizado> ObtenerListadoSSCCSINOFinalizado(ESSCCSINOFinalizado eSSCCSINOFinalizado);
        List<EListObtenerDespachoMercaderia> ObtenerDespachoMercaderia(EObtenerDespachoMercaderia despachoMercaderia);
        List<EListObtenerTransitoMercaderia> ObtenerTransitoMercaderia(EObtenerTransitoMercaderia obtenerTransitoMercaderia);
        List<EListObtenerCliente> ObtenerClientes(EObtenerCliente obtenerCliente);
        List<EListUnidadDisponibleTIDSAP> ObternerUnidadesDisponoblesTIDSAP(EUnidadDisponibleTIDSAP unidadDisponibleTIDSAP);
        List<EListKardexSkuTID> ObtenerKardexSkuTID(EKardexSkuTIDSAP kardexSkuTIDSAP);
        List<EListKardexSkuSAP> ObtenerKardexSkuSAP(EKardexSkuTIDSAP kardexSkuTIDSAP);
    }
}
