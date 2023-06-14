using Diverscan.MJP.Entidades.TRAIngresoSalidaArticulos;
using Diverscan.MJP.Entidades.Trazabilidad;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Trazabilidad
{
    public class ArticuloTrazabilidadDBA
    {

        public List<ArticuloTrazabilidad> ObtenerTrazabilidadArticulo(long idArticulo, DateTime fechaInicio, DateTime fechaFin)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerTrazabilidad");           
            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);
            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, fechaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, fechaFin);

            List<ArticuloTrazabilidad> cantidadList = new List<ArticuloTrazabilidad>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    var idEstado = int.Parse(reader["idEstado"].ToString());
                    var lote = reader["Lote"].ToString();
                    var cantidadPorLote = int.Parse(reader["cantidadPorLote"].ToString());
                    var idUbicacion = long.Parse(reader["idUbicacion"].ToString());
                    var fechaMovimiento = DateTime.Parse(reader["FechaMovimiento"].ToString());
                    var etiqueta = reader["ETIQUETA"].ToString();
                    var esGranel = bool.Parse(reader["Granel"].ToString());
                    var unidad_Medida = reader["Unidad_Medida"].ToString();

                    cantidadList.Add(new ArticuloTrazabilidad(idEstado, lote, cantidadPorLote, idUbicacion,
                        fechaMovimiento, etiqueta, esGranel, unidad_Medida));
                }
            }
            return cantidadList;
        }

      
    }
}
