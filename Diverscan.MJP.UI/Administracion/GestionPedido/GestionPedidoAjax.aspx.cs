using Diverscan.MJP.AccesoDatos.GestionPedido.PedidoOriginal;
using Diverscan.MJP.AccesoDatos.GestionPedido.SolicitudAlistos;
using Diverscan.MJP.AccesoDatos.GestionPedido.SolicitudTraslado;
using Diverscan.MJP.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Administracion.GestionPedido
{
    public partial class GestionPedidoAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBAGestionPedido dBAGestionPedido = new DBAGestionPedido();

            //Obtener la informacion del Usuario
            e_Usuario _eUsuario = (e_Usuario)Session["USUARIO"];
            int IdUsuario = Convert.ToInt32(_eUsuario.IdUsuario);
            var IdBodega = _eUsuario.IdBodega;

            var opcion = Request.Form["Opcion"];

            switch (opcion)
            {
                case "ConsultaPedidoOriginalEncabezado":
                    var pedidosOriginales = dBAGestionPedido.ObtenerPedidoOriginalEncabezado(IdBodega);       
                    Response.Write(JsonConvert.SerializeObject(pedidosOriginales, Formatting.Indented));
                    break;


                case "ObtenerPedidoOriginalDetalle":
                    var idPedidoOriginal = Convert.ToInt64(Request.Form["IdPedidoOriginal"]);
                    var detallesOriginal = dBAGestionPedido.ObtenerPedidoOriginalDetalle(idPedidoOriginal);
                    Response.Write(JsonConvert.SerializeObject(detallesOriginal, Formatting.Indented));
                    break;

                #region Solicitud Traslado Bodega
                case "MostrarBodegasSolicitudes":
                    int idArticulo = Convert.ToInt32(Request.Form["IdArticulo"]);
                    var bodegasJSON = dBAGestionPedido.ObtenerCantidadesBodega(idArticulo);
                    Response.Write(JsonConvert.SerializeObject(bodegasJSON, Formatting.Indented));
                    break;


                case "IngresarSolicitud":                    
                    //Obtener los parametros que se envian
                    int IdBodegaDestino = Convert.ToInt32(Request.Form["IdBodegaDestino"]);
                    int IdArticulo = Convert.ToInt32(Request.Form["IdArticulo"]);
                    int CantidadSolicitada = Convert.ToInt32(Request.Form["CantidadSolicitada"]);
                    int IdPedidoOriginal = Convert.ToInt32(Request.Form["IdPedidoOriginal"]);

                    //Instanciar la clase de Solicitud de Traslado
                    ESolicitudTraslado solicitudTraslado = new ESolicitudTraslado(IdUsuario, IdBodega, IdBodegaDestino, IdPedidoOriginal,
                        IdArticulo, CantidadSolicitada);

                    string respuesta = dBAGestionPedido.InsertarSolicitudTrasladoBodega(solicitudTraslado);
                    Response.Write(respuesta);
                    break;


                case "ObtenerEncabezadosSolicitudTraslado":
                    var encabezadosJSON = dBAGestionPedido.ObtenerEncabezadosSolicitudes();
                    Response.Write(JsonConvert.SerializeObject(encabezadosJSON, Formatting.Indented));
                    break;


                case "ObtenerDetallesSolicitudTraslado":
                    int IdSolicitudTraslado = Convert.ToInt32(Request.Form["IdSolicitudTraslado"]);
                    var detalleJSON = dBAGestionPedido.ObtenerDetalleSolicitudes(IdSolicitudTraslado);
                    Response.Write(JsonConvert.SerializeObject(detalleJSON, Formatting.Indented));
                    break;


                case "ComprobarArticuloEnDetalle":
                    //Obtener los parametros que se envian
                    int IdBodegaDestinoComprobar = Convert.ToInt32(Request.Form["IdBodegaDestino"]);
                    int IdArticuloComprobar = Convert.ToInt32(Request.Form["IdArticulo"]);
                    string respuestaValidacion = dBAGestionPedido.ComprobarArticuloEnDetalle(IdBodegaDestinoComprobar, IdArticuloComprobar);
                    Response.Write(respuestaValidacion);
                    break;

                case "EliminarSolicitudTraslado":
                    int IdSolicitudTrasladoEliminacion = Convert.ToInt32(Request.Form["IdSolicitudTraslado"]);
                    string respuestaEliminacion = dBAGestionPedido.EliminarSolicitudTraslado(IdSolicitudTrasladoEliminacion);
                    Response.Write(respuestaEliminacion);
                    break;

                #endregion Solicitud Traslado Bodega

                #region Caja Chica
                case "InsertarCajaChica":
                    int idPedidoOrinalCJ = Convert.ToInt32(Request.Form["idPedidoOrinalCJ"]);
                    int idArticuloCJ = Convert.ToInt32(Request.Form["idArticuloCJ"]);
                    int cantidadSolicitadaCJ = Convert.ToInt32(Request.Form["cantidadSolicitadaCJ"]);
                    string respuestaInsertarCajaChica = dBAGestionPedido.InsertarCajaChica(idPedidoOrinalCJ, IdUsuario, idArticuloCJ, cantidadSolicitadaCJ);
                    Response.Write(respuestaInsertarCajaChica);
                    break;


                case "MostrarEncabezadoCC":
                    var bodegasEncabezadoCC = dBAGestionPedido.ObtenerEncabezadosCC();
                    Response.Write(JsonConvert.SerializeObject(bodegasEncabezadoCC, Formatting.Indented));
                    break;


                case "MostrarDetalleCC":
                    int idCajaChica = Convert.ToInt32(Request.Form["IDCajaChica"]);
                    var bodegasDetalleCC = dBAGestionPedido.ObtenerDetallesCC(idCajaChica);
                    Response.Write(JsonConvert.SerializeObject(bodegasDetalleCC, Formatting.Indented));
                    break;


                case "EliminarArticuloCC":
                    int idCajaChicaArtEli = Convert.ToInt32(Request.Form["IDCajaChica"]);
                    int idDetalleEli = Convert.ToInt32(Request.Form["IdDetalle"]);
                    var respuestaArticuloEliminado = dBAGestionPedido.EliminarArticuloCC(idCajaChicaArtEli, idDetalleEli);
                    Response.Write(respuestaArticuloEliminado);
                    break;


                case "EliminarCC":
                    int idCajaChicaTotal = Convert.ToInt32(Request.Form["IDCajaChica"]);
                    var respuestaCCEliminado = dBAGestionPedido.EliminarCC(idCajaChicaTotal);
                    Response.Write(respuestaCCEliminado);
                    break;


                case "ProcesarCC":
                    int idCajaChicaProcesar = Convert.ToInt32(Request.Form["IDCajaChica"]);
                    var respuestaCCProcesar = dBAGestionPedido.ProcesarCajaChica(idCajaChicaProcesar);
                    Response.Write(respuestaCCProcesar);
                    break;

                #endregion Caja Chica

                #region Solicitud Alisto

                case "ObtenerEncabezadosSolicitudAlisto":
                    var encabezadosAlistos = dBAGestionPedido.ObtenerEncabezadosAlistos();
                    Response.Write(JsonConvert.SerializeObject(encabezadosAlistos, Formatting.Indented));
                    break;


                case "ObtenerDetallesSolicitudAlisto":
                    int idMaestroSolicitudDetalle = Convert.ToInt32(Request.Form["IdMaestroSolicitud"]);
                    var detallesSolicitudAlisto = dBAGestionPedido.ObtenerDetalleAlistos(idMaestroSolicitudDetalle);
                    Response.Write(JsonConvert.SerializeObject(detallesSolicitudAlisto, Formatting.Indented));
                    break;


                case "InsertarSolicitudAlisto":
                    int idPedidoOriginalAlisto = Convert.ToInt32(Request.Form["IdPedidoOriginal"]);
                    int idArticuloAlisto = Convert.ToInt32(Request.Form["IdArticulo"]);
                    int cantidadAlisto = Convert.ToInt32(Request.Form["CantidadAlisto"]);

                    ESolicitudAlisto solicitudAlisto = new ESolicitudAlisto(IdUsuario, idPedidoOriginalAlisto, idArticuloAlisto, IdBodega, cantidadAlisto);

                    string respuestaInsertarAlisto = dBAGestionPedido.InsertarSolicitudAlisto(solicitudAlisto);
                    Response.Write(respuestaInsertarAlisto);
                    break;


                case "EliminarArticuloSolicitudAlisto":
                    int idMaestroSolicitudESA = Convert.ToInt32(Request.Form["IdMaestroSolicitud"]);
                    int idDetalleESA = Convert.ToInt32(Request.Form["IdDetalle"]);
                    var respuestaArticuloEliminadoSA = dBAGestionPedido.EliminarArticuloSolicitudAlisto(idMaestroSolicitudESA, idDetalleESA);
                    Response.Write(respuestaArticuloEliminadoSA);
                    break;


                case "EliminarSolicitudAlisto":
                    int IdMaestroSolicitud = Convert.ToInt32(Request.Form["IdMaestroSolicitud"]);
                    var respuestaSA = dBAGestionPedido.EliminarSolicitudAlisto(IdMaestroSolicitud);
                    Response.Write(respuestaSA);
                    break;
                    #endregion Solicitud Alisto
            }
        }
    }
}