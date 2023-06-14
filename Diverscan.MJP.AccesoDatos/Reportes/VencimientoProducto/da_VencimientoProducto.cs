using Diverscan.MJP.AccesoDatos.Reportes.VencimientoProducto.Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.VencimientoProducto
{
    class da_VencimientoProducto
    {
        public List<EListVencimientoProducto> ObtenerDiaVecimientoArticulo(EVencimientoProducto vencimientoProducto)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerDiaVecimientoArticulo");
            dbTse.AddInParameter(dbCommand, "@idWarehouse", DbType.Int32, vencimientoProducto.IdBodega);
            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, vencimientoProducto.FechaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, vencimientoProducto.FechaFin);
            dbTse.AddInParameter(dbCommand, "@Sku", DbType.String, vencimientoProducto.Sku);

            List<EListVencimientoProducto> listaArticulos = new List<EListVencimientoProducto>();
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {

                    while (reader.Read())
                    {
                        int DiasVencimiento = Convert.ToInt32(reader["DiasVencimiento"].ToString());
                        string SKU = reader["SKU"].ToString();
                        int Cantidad = Convert.ToInt32(reader["Cantidad"].ToString());
                        string Nombre = reader["Nombre"].ToString();
                        string Descripcion = reader["Descripcion"].ToString();
                        listaArticulos.Add(new EListVencimientoProducto
                        (
                            DiasVencimiento,
                            Cantidad,
                            SKU,
                            Nombre,
                            Descripcion
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaArticulos;
        }
    }
}
