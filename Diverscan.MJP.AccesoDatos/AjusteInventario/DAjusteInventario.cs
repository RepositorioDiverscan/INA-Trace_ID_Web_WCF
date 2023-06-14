using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.AjusteInventario
{
    public class DAjusteInventario
    {
        public List<EAjusteInventario> ObtenerAjusteInvetarioXSolicitud(int IdSolicitudAjuste)
        {
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_AjusteInventario");
            db.AddInParameter(dbCommand, "@IdSolicitudAjusteInventario", DbType.Int32, IdSolicitudAjuste);

            List<EAjusteInventario> ListAjusteInventario = new List<EAjusteInventario>();
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    EAjusteInventario datos = new EAjusteInventario();
                    datos.Nombre = reader["Nombre"].ToString();
                    datos.IdSapArticulo = reader["IdSapArticulo"].ToString();
                    datos.Cantidad = Convert.ToInt32(reader["Cantidad"].ToString());
                    datos.IdSapBodega = reader["IdSapBodega"].ToString();
                    datos.FechaAprobado = Convert.ToDateTime(reader["FechaAprobado"].ToString());
                    datos.Justificacion = reader["Justificacion"].ToString();
                    datos.TipoAjuste = reader["TipoAjuste"].ToString();
                    datos.EsRegalia = Convert.ToString(reader["EsRegalia"].ToString());
                    datos.IdBodega = Convert.ToInt32(reader["IdBodega"].ToString());
                    ListAjusteInventario.Add(datos);
                }
            }
            return ListAjusteInventario;
        }
    }
}
