using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Diverscan.MJP.Entidades.TRAIngresoSalidaArticulos;
using Diverscan.MJP.Entidades.Invertario;
using Diverscan.MJP.AccesoDatos.AjusteInventario;
using Diverscan.MJP.AccesoDatos.Inventario;
using System.ComponentModel;

namespace Diverscan.MJP.AccesoDatos.TRAIngresoSalidaArticulos
{
    public class ConsultasTRA
    {
        public List<TRAIngresoSalidaArticulosRecord> GetTRA(long idArticulo, string lote, long idUbicacion)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("Obtener_UltimaTransaccion");

            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, lote);
            dbTse.AddInParameter(dbCommand, "@IdUbicacion", DbType.Int64, idUbicacion);

            List<TRAIngresoSalidaArticulosRecord> TRAList = new List<TRAIngresoSalidaArticulosRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    TRAList.Add(new TRAIngresoSalidaArticulosRecord(reader));
                }
            }
            return TRAList;
        }
                        

        public List<BodegaFisica_SistemaRecord> ObtenerExistencias(long idArticulo, long idInventario)
        {
            var sistemaData = ObtenerArticulosDisponibles(idArticulo);
            var tomaFisica = ObtenerCantidadArticulosInventario(idInventario, idArticulo);

            List<BodegaFisica_SistemaRecord> existenciaList = new List<BodegaFisica_SistemaRecord>();

#warning
            bool esgranel = false;
            string unidadMedida = "Cambiar";
            for (int i = 0; i < sistemaData.Count; i++)
            {
                var tomaFisicaRecord = tomaFisica.FirstOrDefault(x => x.IdUbicacion == sistemaData[i].IdUbicacion);
                var cantidadTomaFisica = 0;
                if (tomaFisicaRecord != null)
                {
                    cantidadTomaFisica = tomaFisicaRecord.Cantidad;
                    tomaFisica.Remove(tomaFisicaRecord);
                }
                var cantidadSistema = sistemaData[i].Cantidad;
                if (cantidadSistema == 0 && cantidadTomaFisica == 0)
                    continue;

                BodegaFisica_SistemaRecord bodegaFisica_SistemaRecord = new BodegaFisica_SistemaRecord(
                    sistemaData[i].IdUbicacion, sistemaData[i].Etiqueta, cantidadTomaFisica, cantidadSistema, unidadMedida, esgranel, sistemaData[i].NombreArticulo, sistemaData[i].UnidadInventario);
                existenciaList.Add(bodegaFisica_SistemaRecord);
            }

            for (int i = 0; i < tomaFisica.Count; i++)
            {
                BodegaFisica_SistemaRecord bodegaFisica_SistemaRecord = new BodegaFisica_SistemaRecord(
                    sistemaData[i].IdUbicacion, sistemaData[i].Etiqueta, tomaFisica[i].Cantidad, 0,unidadMedida,esgranel,sistemaData[i].NombreArticulo, sistemaData[i].UnidadInventario);
                existenciaList.Add(bodegaFisica_SistemaRecord);
            }

            return existenciaList;
        }

        public List<ArticulosDisponibles> ObtenerArticulosDisponibles(long idArticulo)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerArticulosDisponibles");
            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);

            List<ArticulosDisponibles> cantidadList = new List<ArticulosDisponibles>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    cantidadList.Add(new ArticulosDisponibles(reader));
                }
            }
            return cantidadList;
        }

        public List<ICantidadPorUbicacionArticuloRecord> ObtenerCantidadArticulosInventario(long idInventario, long idArticulo)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerCantidadTomaFisica");
            dbTse.AddInParameter(dbCommand, "@IdInventario", DbType.Int64, idInventario);
            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);

            List<ICantidadPorUbicacionArticuloRecord> cantidadList = new List<ICantidadPorUbicacionArticuloRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    cantidadList.Add(new CantidadPorUbicacionArticuloRecord(reader));
                }
            }
           
            return cantidadList;
        }


        public List<ICantidadPorUbicacionArticuloRecord> ObtenerCantidadTodosArticulosInventario(long idInventario)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerCantidadTodosTomaFisica");
            dbTse.AddInParameter(dbCommand, "@IdInventario", DbType.Int64, idInventario);

            List<ICantidadPorUbicacionArticuloRecord> cantidadList = new List<ICantidadPorUbicacionArticuloRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    cantidadList.Add(new CantidadPorUbicacionArticuloRecord(reader));
                }
            }

            return cantidadList;
        }
       
    }
}
