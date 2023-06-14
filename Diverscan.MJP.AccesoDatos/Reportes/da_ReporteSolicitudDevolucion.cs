using Diverscan.MJP.Entidades.Reportes;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.Reportes
{
    public class da_ReporteSolicitudDevolucion
    {
        public List<e_ReporteSolicitudDevolucion> ReporteSolicitudDevolucion(DateTime fechaInicio, DateTime fechafin, int idBodega)
        {
            //Configuracion de la BD y SP a ejecutar
            var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("ReporteSolicitudDevolucion");

            db.AddInParameter(dbCommand, "@P_IdBodega", DbType.Int32, idBodega);
            db.AddInParameter(dbCommand, "@P_FechaInicio", DbType.Date, fechaInicio);
            db.AddInParameter(dbCommand, "@P_FechaFin", DbType.Date, fechafin);


            //Lista que almacena la informacion
            List<e_ReporteSolicitudDevolucion> reporte = new List<e_ReporteSolicitudDevolucion>();

            //Leer la informacion de la BD
            using (var reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    reporte.Add(new e_ReporteSolicitudDevolucion(reader));
                }
            }

            return reporte;
        }

        public List<e_bodega> CargarBodegas()
        {
            //Configuracion de la BD y SP a ejecutar
            var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_GETBODEGA");
            //Lista que almacena la informacion
            List<e_bodega> bodegas = new List<e_bodega>();

            //Leer la informacion de la BD
            using (var reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    bodegas.Add(new e_bodega(reader));
                }
            }

            return bodegas;
        }
    }
}
