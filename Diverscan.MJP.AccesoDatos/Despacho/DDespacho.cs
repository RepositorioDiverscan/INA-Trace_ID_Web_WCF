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
        public string AsignarPedidoEncargado(EAsignarDespacho input, string PIN)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("uspAsignaPedidoEncargado");
            dbTse.AddInParameter(dbCommand, "@NumeroTransaccion", DbType.String, input.NumeroTransaccion);
            dbTse.AddInParameter(dbCommand, "@PIN", DbType.String, PIN);
            dbTse.AddInParameter(dbCommand, "@idUsuario", DbType.Int64, input.IdUsuario);
            dbTse.AddInParameter(dbCommand, "@correoEnvioPIN", DbType.String, input.CorreoEnvioPIN);

            dbTse.AddOutParameter(dbCommand, "@Resultado", DbType.String, 200);

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                return dbTse.GetParameterValue(dbCommand, "@Resultado").ToString();
            }
        }
    }
}
