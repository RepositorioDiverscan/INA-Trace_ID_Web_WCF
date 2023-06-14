using Diverscan.MJP.AccesoDatos.Operacion.TrasladoBodegas;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.Operacion.TrasladoBodegas;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
namespace Diverscan.MJP.UI.Operaciones.Traslados.AdministracionTraslados
{
    public partial class adminTrasladosAjax : System.Web.UI.Page
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
                case "ObtenerEncabezadosSolicitudTraslado":
                    var encabezadosJSON = dBTrasladoBodegas.ObtenerEncabezadosSolicitudesDestino(IdBodega);
                    Response.Write(JsonConvert.SerializeObject(encabezadosJSON, Formatting.Indented));
                    break;


                case "ObtenerDetallesSolicitudTraslado":
                    int IdSolicitudTraslado = Convert.ToInt32(Request.Form["IdSolicitudTraslado"]);
                    var detalleJSON = dBTrasladoBodegas.ObtenerDetalleSolicitudes(IdSolicitudTraslado);
                    Response.Write(JsonConvert.SerializeObject(detalleJSON, Formatting.Indented));
                    break;


                case "RechazarSolicitudTrasladoBodega":
                    //Obtener los parametros que se envian
                    int idSolicitudTrasladoBodega = Convert.ToInt32(Request.Form["IdSolicitudTraslado"]);
                    string respuesta = dBTrasladoBodegas.RechazarSolicitudTrasladoBodega(idSolicitudTrasladoBodega, IdUsuario);
                    Response.Write(respuesta);
                    break;

                case "AceptarSolicitudTrasladoBodega":
                    //Obtener los parametros que se envian
                    int idSolicitudTrasladoBodegaAcepta = Convert.ToInt32(Request.Form["IdSolicitudTraslado"]);

                   
                    //Instanciar un arreglo de los artículos
                    List<EListaArticulos> listaArt = new List<EListaArticulos>();

                  

                    //Instanciar la clase para crear la Solicitud de la Ola
                    EEncabezadoOla ola = new EEncabezadoOla(idSolicitudTrasladoBodegaAcepta, IdUsuario, IdBodega, listaArt);


                    string respuestaAceptar = dBTrasladoBodegas.AceptarSolicitudTrasladoBodega(ola);
                    Response.Write(respuestaAceptar);
                    break;

            }
        }
    }
}