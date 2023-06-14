using Diverscan.MJP.AccesoDatos.MaestroArticulo;
using Diverscan.MJP.AccesoDatos.Reportes.Alisto;
using Diverscan.MJP.AccesoDatos.Reportes.Alisto.Entidad;
using Diverscan.MJP.AccesoDatos.Reportes.Alisto.RecargaGuanacaste.Entidad;
using Diverscan.MJP.AccesoDatos.Reportes.AsignoCertificacion;
using Diverscan.MJP.AccesoDatos.Reportes.Certificacion.SSCCSINOFinalizado;
using Diverscan.MJP.AccesoDatos.Reportes.Cliente;
using Diverscan.MJP.AccesoDatos.Reportes.Inventario.UbicacionSku.Entidad;
using Diverscan.MJP.AccesoDatos.Reportes.Inventario.UnidadDisponibleTIDSAP.Entidad;
using Diverscan.MJP.AccesoDatos.Reportes.KardexXSkuTIDSAP;
using Diverscan.MJP.AccesoDatos.Reportes.KardexXSkuTIDSAP.Entidad;
using Diverscan.MJP.AccesoDatos.Reportes.Ola.DespachoOla;
using Diverscan.MJP.AccesoDatos.Reportes.Ola.DisponibleFcturacion;
using Diverscan.MJP.AccesoDatos.Reportes.ReportePedidoSinOla;
using Diverscan.MJP.AccesoDatos.Reportes.RotacionInventario;
using Diverscan.MJP.AccesoDatos.Reportes.RotacionInventario.Entidad;
using Diverscan.MJP.AccesoDatos.Reportes.TransitoMercaderia;
using Diverscan.MJP.AccesoDatos.Reportes.TransitoMercaderia.Entidad;
using Diverscan.MJP.AccesoDatos.Reportes.Traslado;
using Diverscan.MJP.AccesoDatos.Reportes.Traslado.Entidad;
using Diverscan.MJP.AccesoDatos.Reportes.VencimientoProducto;
using Diverscan.MJP.AccesoDatos.Reportes.VencimientoProducto.Entidades;
using Diverscan.MJP.Entidades.MaestroArticulo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes
{
    //public class NReporte : IReportes
    //{
    //    ArticuloGTINDBA articuloGTINDBA = new ArticuloGTINDBA();
    //    da_ReportesOla _reportesOla = new da_ReportesOla();
    //    da_Inventario _inventario = new da_Inventario();
    //    da_VencimientoProducto _VencimientoProducto = new da_VencimientoProducto();
    //    da_Alisto _alisto = new da_Alisto();
    //    da_Traslado _traslado = new da_Traslado();
    //    da_Certificacion _daCertificacion = new da_Certificacion();
    //    da_Transito _transito = new da_Transito();
    //    KardexXSkuTIDSAPDBA _xSkuTIDSAPDBA = new KardexXSkuTIDSAPDBA();
    //    public List<EArticulo> ObtenerArticulosYGTINReport()
    //    {

    //        return articuloGTINDBA.ObtenerArticulosYGTINReport();
    //    }

    //    public List<e_MaestroArticulo> GetProductsReportStorage()
    //    {

    //        return articuloGTINDBA.GetProductsReportStorage();
    //    }

    //    public List<EListPedidoSinOla> PedidosSinOlas(EPedidosSinOla ePedidos)
    //    {
    //        return _reportesOla.ObtenerListadoPedidosSinOlas(ePedidos);
    //    }

    //    public List<EListBodega> ListaBodegas()
    //    {
    //        return _reportesOla.ObtenerBodegas();
    //    }

    //    public List<EListRotacionInventario> ListRotacionInventarios(ERotacionInventario rotacionInventario)
    //    {
    //        return _inventario.GetListRotacionInventarios(rotacionInventario);
    //    }

    //    public List<EListVencimientoProducto> ObtenerDiaVecimientoArticulo(EVencimientoProducto vencimientoProducto)
    //    {
    //        return _VencimientoProducto.ObtenerDiaVecimientoArticulo(vencimientoProducto);
    //    }

        

    //    public List<EListObtenerOlasDisponiblesFacturacion> ObtenerOlasDisponiblesFacturacion(EObtenerOlasDisponiblesFacturacion disponiblesFacturacion)
    //    {
    //        return _reportesOla.ObtenerOlasDisponiblesFacturacion(disponiblesFacturacion);
    //    }

    //    public List<EListObtenerRecargaBodegaGuanacaste> ObtenerRecargaBodegaGuanacaste(EObtenerRecargaGuanacaste eObtenerRecargaGuanacaste)
    //    {
    //        return _alisto.ObtenerRecargaBodegaGuanacaste(eObtenerRecargaGuanacaste);
    //    }

    //    public List<EListObtenerUbicacionSku> ObtenerTotalUbicacionesXsku(EObtenerUbicacionSku ubicacionSku)
    //    {
    //        return _inventario.ObtenerTotalUbicacionesXsku(ubicacionSku);
    //    }

    //    public List<EListObtenerUbicacion> ObtenerUbicacion(EObtenerUbicacion ubicacion)
    //    {
    //        return _inventario.ObtenerUbicacion(ubicacion);
    //    }

    //    public List<EListObtenerTrasladoMercaderia> ObtenerTrasladoMercaderia(EObtenerTrasladoMercaderia obtenerTrasladoMercaderia)
    //    {
    //        return _traslado.ObtenerTrasladoMercaderia(obtenerTrasladoMercaderia);
    //    }

    //    public List<EListAsignacionCertificacion> ObtenerListadoAsignacionCertificacion(EAsignacionCertificacion asignacionCertificacion)
    //    {
    //        return _daCertificacion.ObtenerListadoAsignacionCertificacion(asignacionCertificacion);
    //    }

    //    public List<EListSSCCSINOFinalizado> ObtenerListadoSSCCSINOFinalizado(ESSCCSINOFinalizado eSSCCSINOFinalizado)
    //    {
    //        return _daCertificacion.ObtenerListadoSSCCSINOFinalizado(eSSCCSINOFinalizado);
    //    }

    //    public List<EListObtenerDespachoMercaderia> ObtenerDespachoMercaderia(EObtenerDespachoMercaderia despachoMercaderia)
    //    {
    //        return _reportesOla.ObtenerDespachoMercaderia(despachoMercaderia);
    //    }

    //    public List<EListObtenerTransitoMercaderia> ObtenerTransitoMercaderia(EObtenerTransitoMercaderia obtenerTransitoMercaderia)
    //    {
    //        return _transito.ObtenerTransitoMercaderia(obtenerTransitoMercaderia);
    //    }

    //    public List<EListObtenerCliente> ObtenerClientes(EObtenerCliente obtenerCliente)
    //    {
    //        return _daCertificacion.ObtenerClientes(obtenerCliente);
    //    }

    //    public List<EListUnidadDisponibleTIDSAP> ObternerUnidadesDisponoblesTIDSAP(EUnidadDisponibleTIDSAP unidadDisponibleTIDSAP)
    //    {
    //        return _inventario.ObternerUnidadesDisponoblesTIDSAP(unidadDisponibleTIDSAP);
    //    }

    //    public List<EListKardexSkuTID> ObtenerKardexSkuTID(EKardexSkuTIDSAP kardexSkuTIDSAP)
    //    {
    //        return _xSkuTIDSAPDBA.ObtenerKardexSkuTID(kardexSkuTIDSAP);
    //    }

    //    public List<EListKardexSkuSAP> ObtenerKardexSkuSAP(EKardexSkuTIDSAP kardexSkuTIDSAP)
    //    {
    //        return _xSkuTIDSAPDBA.ObtenerKardexSkuSAP(kardexSkuTIDSAP);
    //    }
    //}
}
