using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.AccesoDatos.UsoGeneral;
using Diverscan.MJP.AccesoDatos.MotorDecision;
using Diverscan.MJP.Negocio.MotorDecisiones;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.Negocio.GS1;
using Diverscan.MJP.Utilidades;
using Diverscan.Visitas.Utilidades;
using Telerik.Web.UI;
using Telerik.Web.UI.PersistenceFramework;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Meta.Numerics.Matrices; //https://metanumerics.codeplex.com/
using Meta.Numerics.Statistics;

namespace Diverscan.MJP.Negocio.App
{
    public static class n_App
    {
        #region SELECT
        public static List<e_MenuApp> ObtenerMenuApp(string idUsuario)
        {
            var eMenuApp = new List<e_MenuApp>();
            string SQL = "SELECT * FROM APPMenu";
            DataSet DS = da_ConsultaDummy.GetDataSet(SQL, idUsuario);

            foreach (DataRow dsRow in DS.Tables[0].Rows)
            {
                eMenuApp.Add(new e_MenuApp
                {
                    idAppMenu = Convert.ToInt32(dsRow["idAppMenu"].ToString()),
                    Menu = dsRow["Menu"].ToString(),
                    Orden = Convert.ToInt32(dsRow["Orden"].ToString())
                });
            }

            return eMenuApp;
        }
        #endregion    
    }
}
