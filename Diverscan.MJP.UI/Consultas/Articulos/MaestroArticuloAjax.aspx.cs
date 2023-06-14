using Diverscan.MJP.AccesoDatos.Consultas;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Reportes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Consultas.Articulos
{
    public partial class MaestroArticuloAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var opcion = Request.Form["opcion"];

            try
            {
                switch (opcion)
                {
                    case "ObtenerMaestroArticulo":
                        DConsultarMaestroArticulo maestro = new DConsultarMaestroArticulo();
                        List<EMaestroArticulo> m = maestro.ObtenerMaestroArticulosDisponiblesBodega();
                        var jsonStringAlisto = new JavaScriptSerializer();
                        jsonStringAlisto.MaxJsonLength = 2147483644;
                        var jsonStringResultDevolucion = jsonStringAlisto.Serialize(m);
                        Response.Write(jsonStringResultDevolucion);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}