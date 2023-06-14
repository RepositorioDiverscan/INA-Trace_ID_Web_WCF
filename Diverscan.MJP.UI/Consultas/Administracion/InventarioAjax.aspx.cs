using Diverscan.MJP.AccesoDatos.Consultas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Consultas.Administracion
{
    public partial class InventarioAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //se instancia la capa 
            DInventario inventario = new DInventario();


            //Se obtiene la opcion a ejecutar
            var opcion = Request.Form["opcion"];

            switch (opcion)
            {
                //case para cargar encabezado del inventario
                case "ObtenerInventario":
                    DateTime f1 = Convert.ToDateTime(Request.Form["f1"]);
                    DateTime f2 = Convert.ToDateTime(Request.Form["f2"]);
                    var inve = inventario.InventarioEncabezado(f1, f2);
                    Response.Write(JsonConvert.SerializeObject(inve, Formatting.Indented));
                    break;

                //case para cargar detalle del inventario
                case "ObtenerDetalle":
                    int id = Convert.ToInt32(Request.Form["id"]);
                    var invDetalle = inventario.ObtenerDetalleInventario(id);
                    Response.Write(JsonConvert.SerializeObject(invDetalle, Formatting.Indented));
                    break;
            }

        }
    }
}