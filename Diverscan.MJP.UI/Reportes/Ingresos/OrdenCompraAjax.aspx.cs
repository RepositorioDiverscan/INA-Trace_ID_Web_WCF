using Diverscan.MJP.AccesoDatos.Operaciones;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Reportes.ReporteOrdenCompra
{
    public partial class OrdenCompraAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // se instancia a la capa
            OrdenesCompras orden = new OrdenesCompras();
            //se obtiene la opcion a ejecutar
            var opcion = Request.Form["opcion"];

            switch (opcion)
            {
                // case para cargar la orden de compra 
                case "ObtenerOrdenCompra":
                    int idBodega = Convert.ToInt32(Request.Form["idBodega"]);
                    DateTime f1 = Convert.ToDateTime(Request.Form["f1"]);
                    DateTime f2 = Convert.ToDateTime(Request.Form["f2"]);
                    string numero = Convert.ToString(Request.Form["numero"]);
                    var ordenCompra = orden.OrdenCompras(idBodega, f1, f2, numero);
                    Response.Write(JsonConvert.SerializeObject(ordenCompra, Formatting.Indented));
                    break;

                //opcion para cargar detalle de la orden de compra
                case "ObtenerDetalleOrdenCompra":
                   
                    int idMaestroArticulo = Convert.ToInt32(Request.Form["idMaestroArticulo"]);
                    var ordenCompraDetalle = orden.ObtenerDetalleOrdenCompras(idMaestroArticulo);
                    Response.Write(JsonConvert.SerializeObject(ordenCompraDetalle, Formatting.Indented));
                    break;

            }
        }
    }
}