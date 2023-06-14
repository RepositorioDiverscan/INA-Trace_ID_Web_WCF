using Diverscan.MJP.Entidades.Reportes;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes
{
    public class da_ReporteDespachadoVsRecibido
    {
        public List<e_ReporteDespachadoVsRecibido> ReporteDespachadoVsRecibido(DateTime fechaInicio, DateTime fechaFin, int idBodega)
        {
            //Configuracion de la BD y SP a ejecutar
            var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("ReporteDespachadoVsRecibido");

            db.AddInParameter(dbCommand, "@P_IdBodega", DbType.Int32, idBodega);
            db.AddInParameter(dbCommand, "@P_FechaInicio", DbType.Date, fechaInicio);
            db.AddInParameter(dbCommand, "@P_FechaFin", DbType.Date, fechaFin);


            //Lista que almacena la informacion
            List<e_ReporteDespachadoVsRecibido> reporte = new List<e_ReporteDespachadoVsRecibido>();

            //Leer la informacion de la BD
            using (var reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    reporte.Add(new e_ReporteDespachadoVsRecibido(reader));
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
