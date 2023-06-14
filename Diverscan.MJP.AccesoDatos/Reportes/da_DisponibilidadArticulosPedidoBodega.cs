using Diverscan.MJP.Entidades.Reportes.DisponibilidadPorBodega;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.Reportes
{
    public class da_DisponibilidadArticulosPedidoBodega
    {
        public List<e_DisponibilidadArticulosPedidoBodega> GetListaDisponibilidadArticulosPedidoBodega(string idCompania, long idMaestroSolicitud, int idBodega)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_Disponibilidad_Bodega_X_Pedido");
            dbTse.AddInParameter(dbCommand, "@PIdCompania", DbType.String, idCompania);
            dbTse.AddInParameter(dbCommand, "@PIdMaestroSolicitud", DbType.Int64, idMaestroSolicitud);
            dbTse.AddInParameter(dbCommand, "@PIdBodega", DbType.Int32, idBodega);

            List<e_DisponibilidadArticulosPedidoBodega> listaDisponibilidadArticulosPedidoBodega = new List<e_DisponibilidadArticulosPedidoBodega>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    string IdInternoArticulo = reader["IdInternoArticulo"].ToString();
                    string NombreArticulo = reader["NombreArticulo"].ToString();
                    string UnidadMedida = reader["UnidadMedida"].ToString();
                    decimal? CantidadEnPedido = Convert.ToDecimal(reader["CantidadEnPedido"].ToString());
                    decimal? CantidadEnBodega = Convert.ToDecimal(reader["CantidadEnBodega"].ToString());
                    decimal? DiferenciaBodegaPedido = Convert.ToDecimal(reader["DiferenciaBodegaPedido"].ToString());
                    string TipoArticulo = reader["TipoArticulo"].ToString();
                    listaDisponibilidadArticulosPedidoBodega.Add(new e_DisponibilidadArticulosPedidoBodega
                    (
                             IdInternoArticulo,
                             NombreArticulo,
                             UnidadMedida,
                             CantidadEnPedido,
                             CantidadEnBodega,
                             DiferenciaBodegaPedido,
                             TipoArticulo
                    ));
                }
            }
            return listaDisponibilidadArticulosPedidoBodega;
        }
    }
}
