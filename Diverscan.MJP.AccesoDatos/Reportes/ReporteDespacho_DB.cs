using Diverscan.MJP.AccesoDatos.Reportes.RotacionInventario.Entidad;
using Diverscan.MJP.Negocio.Reportes;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes
{
    public class ReporteDespacho_DB : ReporteDespacho
    {
        public List<IEntidad_Despacho> Obtener_Reporte_Despacho(DateTime Inicio, DateTime Final, int idArticulo)
        {
            var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_Reporte_Despacho_V2");

            db.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, Inicio);
            db.AddInParameter(dbCommand, "@FechaFinal", DbType.DateTime, Final);
            db.AddInParameter(dbCommand, "@idArticulo", DbType.Int32, idArticulo);

            var Obtener_Reporte_Despacho = new List<IEntidad_Despacho>();
            var data = db.ExecuteReader(dbCommand);
            using (IDataReader reader = data)
            {
                while (reader.Read())
                {

                    int solicitud = Convert.ToInt32(reader["Solicitud"].ToString());
                    string NombreArticulo = reader["NombreArticulo"].ToString();
                    string referencia = reader["Referencia"].ToString();
                    double cantidad = Convert.ToDouble(reader["Cantidad"].ToString());
                    string sscc = reader["SSCC"].ToString();
                    string destino = reader["Destino"].ToString();
                   
                    DateTime FechaDespacho = Convert.ToDateTime(reader["FechaDespacho"].ToString());



                    Obtener_Reporte_Despacho.Add(new Entidad_Despacho(solicitud, NombreArticulo, referencia, cantidad, sscc, destino, FechaDespacho));

                    //Obtener_Reporte_Despacho.Add(new Entidad_Despacho(NombreArticulo, Lote, FechaVencimiento, FechaRegistro));

                }
            }
            return Obtener_Reporte_Despacho;
        }

        

    }
}

