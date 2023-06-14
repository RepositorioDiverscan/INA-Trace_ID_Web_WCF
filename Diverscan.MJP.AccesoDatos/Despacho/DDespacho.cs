using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Despacho
{
    public class DDespacho
    {

        public List<EArticuloDespacho> ObtenerFaltantesDespacho(DateTime dateInit, DateTime dateFinal, int idWarehouse) 
        {
            List<EArticuloDespacho> faltantesDespacho = new List<EArticuloDespacho>();
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerFaltantesDespachoArticulos");
           
            dbTse.AddInParameter(dbCommand, "@dateInit", DbType.DateTime, dateInit);
            dbTse.AddInParameter(dbCommand, "@dateFinal", DbType.DateTime, dateFinal);
            dbTse.AddInParameter(dbCommand, "@idWareHouse", DbType.Int32, idWarehouse);

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    EArticuloDespacho order = new EArticuloDespacho(reader);

                    faltantesDespacho.Add(order);
                }
            }
            return faltantesDespacho;
        }

        public List<EOlaDespacho> ObtenerOlasFaltanteDespacho(DateTime dateInit, DateTime dateFinal,
                                                                    long idArtiluco, int idWarehouse)
        {
            List<EOlaDespacho> olasDespacho = new List<EOlaDespacho>();
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerOlasArticuloDespachado");

            dbTse.AddInParameter(dbCommand, "@dateInit", DbType.DateTime, dateInit);
            dbTse.AddInParameter(dbCommand, "@dateFinal", DbType.DateTime, dateFinal);
            dbTse.AddInParameter(dbCommand, "@idWareHouse", DbType.Int32, idWarehouse);
            dbTse.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, idArtiluco);

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    EOlaDespacho ola = new EOlaDespacho(reader);

                    olasDespacho.Add(ola);
                }
            }
            return olasDespacho;
        }
        public String CargarVehiculoXPlaca(string placa, long idSSCC,long idUbicacion,
                                                bool capacidadExcedida,bool sobreCargar)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SPCargarSSCCVehiculo");
            dbTse.AddInParameter(dbCommand, "@placa", DbType.String, placa);
            dbTse.AddInParameter(dbCommand, "@idSSCC", DbType.Int64, idSSCC);
            dbTse.AddInParameter(dbCommand, "@idubicacion", DbType.Int64, idUbicacion);
            dbTse.AddInParameter(dbCommand, "@capacidadExcedida", DbType.Boolean, capacidadExcedida);
            dbTse.AddInParameter(dbCommand, "@sobreCargar", DbType.Boolean, sobreCargar);
            dbTse.AddOutParameter(dbCommand, "@Resultado", DbType.String, 200);
            String result = "";

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                result = dbTse.GetParameterValue(dbCommand, "@Resultado").ToString();
            }

            return result;
        }
    }
}
