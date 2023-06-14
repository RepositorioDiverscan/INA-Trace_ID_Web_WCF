using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.AccesoDatos.UsoGeneral;
using Diverscan.MJP.Entidades;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;
using Telerik.Web.UI.PersistenceFramework;


namespace Diverscan.MJP.Negocio.UsoGeneral
{
    public class n_ConsultaDummy
    {

        public static void CargarDropDown(DropDownList DDL, string SQL, string NombreValorDB) 
        {
            da_ConsultaDummy.CargarDropDown(DDL, SQL, NombreValorDB);       
        }

        public static DataSet GetDataSet(string Vista, TextBox KEY, string idUsuario)
        {
            return da_ConsultaDummy.GetDataSet(Vista,  KEY,  idUsuario);
        }

        public static DataSet GetDataSet(string Query, string idUsuario)
        {
             return da_ConsultaDummy.GetDataSet(Query, idUsuario);
        }

        public static DataSet GetDataSet2(string Query, string idUsuario)
        {
            return da_ConsultaDummy.GetDataSet2(Query, idUsuario);
        }

        public static DataSet GetDataSet3(string Query, string idUsuario)
        {
            return da_ConsultaDummy.GetDataSet3(Query, idUsuario);
        }

        public static bool PushData(string SQL, string idUsuario)
        {
            return da_ConsultaDummy.PushData(SQL, idUsuario);
        }

        public static string GetUniqueValue(string SQL, string idUsuario)
        {
            return da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
        }

    }
}
