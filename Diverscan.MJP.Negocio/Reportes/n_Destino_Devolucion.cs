using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.Entidades.Reportes.Despachos;
using Diverscan.MJP.Entidades.Reportes.Kardex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Diverscan.MJP.Negocio.Reportes
{
    public class n_Destino_Devolucion
    {

        public void LlenarDestino(DropDownList CB)
        {
            da_Destino destino = new da_Destino(); 
            DataSet dsConsulta = destino.dsConsultarProducto(-1);

            destino.cargarControl(ref CB, dsConsulta);
        }
           
    }
}
