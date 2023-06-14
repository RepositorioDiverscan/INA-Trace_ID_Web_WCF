using Diverscan.MJP.AccesoDatos.Reportes.Alisto.Entidad;
using Diverscan.MJP.Negocio.Alistos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Reportes.Alisto
{
    public partial class ReporteAsignacionAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //recive la opcion para ejecutar la setencia
            var opcion = Request.Form["opcion"];

            //case para validar la accion segun la opcion
            switch (opcion)
            {
                case "ObtenerAlisto":
                    //invocar la capa de Negocio
                    NAlisto nAlisto = new NAlisto();
                    string id = Convert.ToString(Request.Form["id"]);
                    var f1 = Convert.ToDateTime(Request.Form["F1"]);
                    var f2 = Convert.ToDateTime(Request.Form["F2"]);

                    //almacenar en una lista 
                    List<EListObtenerAsignacionAlisto> listaAlisto = nAlisto.ConsultarAsignacionAlisto(f1, f2, id);
                    //Convertir la informacion en un JSON
                    var jsonStringAlisto = new JavaScriptSerializer();
                    jsonStringAlisto.MaxJsonLength = 2147483644;
                    var jsonStringResultDevolucion = jsonStringAlisto.Serialize(listaAlisto);
                    //Enviar el JSON con la informacion de la BD
                    Response.Write(jsonStringResultDevolucion);
                    break;
            }
        }
    }
}