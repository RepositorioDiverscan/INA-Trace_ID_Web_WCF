using Diverscan.MJP.AccesoDatos.GestionPedido.SolicitudTraslado;
using Diverscan.MJP.AccesoDatos.Operacion.TrasladoBodegas;
using Diverscan.MJP.Entidades;
using Newtonsoft.Json;
using System;

namespace Diverscan.MJP.UI.Operaciones.Traslados.SolicitudTraslados
{
    public partial class SolicitudTrasladoAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Instanciar la BD
            DBTrasladoBodegas dBTrasladoBodegas = new DBTrasladoBodegas();

            //Obtener la informacion del Usuario
            e_Usuario _eUsuario = (e_Usuario)Session["USUARIO"];
            int IdUsuario = Convert.ToInt32(_eUsuario.IdUsuario);
            var IdBodega = _eUsuario.IdBodega;

            var opcion = Request.Form["Opcion"];

            switch (opcion)
            {
                case "ObtenerAriculosBodega":
                    var ArticulosBodegaJSON = dBTrasladoBodegas.ObtenerArticulosBodegas();
                    Response.Write(JsonConvert.SerializeObject(ArticulosBodegaJSON, Formatting.Indented));
                    break;


                case "ObtenerAriculosBodegaEspecifica":
                    int IdBodegaEspecifica = Convert.ToInt32(Request.Form["IdBodega"]);
                    var ArticulosBodegaEspecificaJSON = dBTrasladoBodegas.ObtenerArticulosBodegaEspecifica(IdBodegaEspecifica);
                    Response.Write(JsonConvert.SerializeObject(ArticulosBodegaEspecificaJSON, Formatting.Indented));
                    break;


                case "CrearSolicitudTraslado":
                    //Obtener los parametros que se envian
                    int IdBodegaDestino = Convert.ToInt32(Request.Form["IdBodegaDestino"]);
                    int IdArticulo = Convert.ToInt32(Request.Form["IdArticulo"]);
                    int CantidadSolicitada = Convert.ToInt32(Request.Form["CantidadSolicitada"]);
                    //Instanciar la clase de Solicitud de Traslado
                    ESolicitudTraslado solicitudTraslado = new ESolicitudTraslado(IdUsuario, IdBodega, IdBodegaDestino, 0, IdArticulo, CantidadSolicitada);
                    string respuesta = dBTrasladoBodegas.CrearSolicitudTrasladoBodega(solicitudTraslado);
                    Response.Write(respuesta);
                    break;


                case "ActualizarSolicitudTraslado":
                    //Obtener los parametros que se envian
                    int idSolicitudAct = Convert.ToInt32(Request.Form["IdSolicitudTraslado"]);
                    int IdArticuloAct = Convert.ToInt32(Request.Form["IdArticulo"]);
                    int CantidadSolicitadaAct = Convert.ToInt32(Request.Form["CantidadSolicitada"]);

                    string respuestaAct = dBTrasladoBodegas.ActualizarSolicitudTrasladoBodega(idSolicitudAct, IdArticuloAct, CantidadSolicitadaAct);
                    Response.Write(respuestaAct);
                    break;


                case "EliminarSolicitudTraslado":
                    int idSolicitudEliminar = Convert.ToInt32(Request.Form["IdSolicitudTraslado"]);
                    string respuestaEliminar = dBTrasladoBodegas.ElimarSolicitudTrasladoBodega(idSolicitudEliminar);
                    Response.Write(respuestaEliminar);
                    break;


                case "ObtenerEncabezadosSolicitudTraslado":
                    var encabezadosJSON = dBTrasladoBodegas.ObtenerEncabezadosSolicitudes(IdBodega);
                    Response.Write(JsonConvert.SerializeObject(encabezadosJSON, Formatting.Indented));
                    break;


                case "ObtenerDetallesSolicitudTraslado":
                    int IdSolicitudTraslado = Convert.ToInt32(Request.Form["IdSolicitudTraslado"]);
                    var detalleJSON = dBTrasladoBodegas.ObtenerDetalleSolicitudes(IdSolicitudTraslado);
                    Response.Write(JsonConvert.SerializeObject(detalleJSON, Formatting.Indented));
                    break;
            }
        }
    }
}