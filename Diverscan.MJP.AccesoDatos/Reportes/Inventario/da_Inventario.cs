using Diverscan.MJP.AccesoDatos.Reportes.Inventario.UbicacionSku.Entidad;
using Diverscan.MJP.AccesoDatos.Reportes.Inventario.UnidadDisponibleTIDSAP.Entidad;
using Diverscan.MJP.AccesoDatos.Reportes.RotacionInventario.Entidad;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.RotacionInventario
{
    class da_Inventario
    {
        public List<EListRotacionInventario> GetListRotacionInventarios(ERotacionInventario rotacionInventario)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_GetRotacionInventario");
            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, rotacionInventario.FechaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, rotacionInventario.FechaFin);
            dbTse.AddInParameter(dbCommand, "@IdInterno", DbType.String, rotacionInventario.IdInterno);
            dbTse.AddInParameter(dbCommand, "@IdBodega", DbType.String, rotacionInventario.IdBodega);

            List<EListRotacionInventario> listaDisponibilidadArticulosPedidoBodega = new List<EListRotacionInventario>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    int UnidadesFrecuencia = Convert.ToInt32(reader["UnidadesFrecuencia"].ToString());
                    string IdInternoArticulo = reader["SKU"].ToString();
                    int Unidades = Convert.ToInt32(reader["Unidades"].ToString());
                    string NombreArticulo = reader["Nombre"].ToString();
                    int Promedio = Convert.ToInt32(reader["Promedio"].ToString());
                    listaDisponibilidadArticulosPedidoBodega.Add(new EListRotacionInventario(UnidadesFrecuencia, Unidades, NombreArticulo, IdInternoArticulo,Promedio));
                }
            }
            return listaDisponibilidadArticulosPedidoBodega;
        }
        public List<EListObtenerUbicacionSku> ObtenerTotalUbicacionesXsku(EObtenerUbicacionSku ubicacionSku)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerTotalUbicacionesXsku");
            dbTse.AddInParameter(dbCommand, "@idUbicacion", DbType.Int32, ubicacionSku.IdUbicacion);
            dbTse.AddInParameter(dbCommand, "@idInterno", DbType.String, ubicacionSku.IdInterno);

            List<EListObtenerUbicacionSku> listObtenerUbicacionSkus = new List<EListObtenerUbicacionSku>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    string IdInterno = reader["idInterno"].ToString();
                    string Nombre = reader["Nombre"].ToString();
                    string Descripcion = reader["Descripcion"].ToString();
                    int CantidadDisponible = Convert.ToInt32(reader["CantidadDisponible"].ToString());
                    listObtenerUbicacionSkus.Add(new EListObtenerUbicacionSku(IdInterno,Nombre,Descripcion,CantidadDisponible));
                }
            }
            return listObtenerUbicacionSkus;
        }
        public List<EListObtenerUbicacion> ObtenerUbicacion(EObtenerUbicacion ubicacion)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerUbicaciones");
            dbTse.AddInParameter(dbCommand, "@idZona", DbType.Int32, ubicacion.IdZona);

            List<EListObtenerUbicacion> listObtenerUbicacion = new List<EListObtenerUbicacion>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    int IdUbicacion = Convert.ToInt32(reader["idUbicacion"].ToString());
                    string Descripcion = reader["Descripcion"].ToString();
                    listObtenerUbicacion.Add(new EListObtenerUbicacion(IdUbicacion,Descripcion));
                }
            }
            return listObtenerUbicacion;
        }
        public List<EListUnidadDisponibleTIDSAP> ObternerUnidadesDisponoblesTIDSAP(EUnidadDisponibleTIDSAP unidadDisponibleTIDSAP)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_GetUnidadesDisponoblesTIDSAP");
            dbTse.AddInParameter(dbCommand, "@IdBodega", DbType.Int32, unidadDisponibleTIDSAP.IdBodega);
            dbTse.AddInParameter(dbCommand, "@IdInterno", DbType.String, unidadDisponibleTIDSAP.IdInterno);

            List<EListUnidadDisponibleTIDSAP> listObtenerUbicacionSkus = new List<EListUnidadDisponibleTIDSAP>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    string Nombre = reader["Nombre"].ToString();
                    string IdInterno = reader["idInterno"].ToString();
                    int UnidadTraceID = Convert.ToInt32(reader["UnidadTraceID"].ToString());
                    int UnidadERP = Convert.ToInt32(reader["UnidadERP"].ToString());
                    int Diferencia = Convert.ToInt32(reader["Diferencia"].ToString());
                    listObtenerUbicacionSkus.Add(new EListUnidadDisponibleTIDSAP(Nombre,IdInterno,UnidadTraceID,UnidadERP,Diferencia));
                }
            }
            return listObtenerUbicacionSkus;
        }
    }
}
