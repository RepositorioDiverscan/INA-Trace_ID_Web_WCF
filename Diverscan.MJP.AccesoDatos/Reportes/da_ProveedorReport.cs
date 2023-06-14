using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Utilidades;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Diverscan.MJP.AccesoDatos.Reportes
{
    public class da_ProveedorReport
    {
        /// <summary>
        /// Autor:Guillermo Ramirez
        /// Description: Retorna los datos para cargar el reporte de Proveedores.
        /// </summary>
        /// <param name="fechaIni"></param>
        /// <param name="fechaFin"></param>
        /// /// <param name="idProvedor"></param>
        /// <returns></returns>
        public static List<e_ProveedorReport> ObtenerProveedorReportInfo(DateTime fechaIni, DateTime fechaFin, string idProvedor)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("Obtener_ProveedorReportInfo");

                dbTse.AddInParameter(dbCommand, "@Fechaini", DbType.DateTime, fechaIni);
                dbTse.AddInParameter(dbCommand, "@Fechafin", DbType.DateTime, fechaFin);
                dbTse.AddInParameter(dbCommand, "@IdProvedor", DbType.String, idProvedor);
                dbTse.AddInParameter(dbCommand, "@Exportar", DbType.String, "1");

                var eproveedor = new List<e_ProveedorReport>();

                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        eproveedor.Add(CargarInfoProveedor(reader));
                    }
                }

                return eproveedor;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "ProveedorReportInfo";
                const string listaParametros = "";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0)
                    indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal);
                // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_ProveedorReport.cs", "ObtenerProveedorReportInfo()", "", lineaError,
                                             nombreProcedimiento, listaParametros, exError.Message);

                return null;
            }
        }

        /// <summary>
        /// Autor:Guillermo Ramirez
        /// Description: Retorna los datos para cargar el reporte de Proveedores.
        /// </summary>
        /// <param name="fechaIni"></param>
        /// <param name="fechaFin"></param>
        /// /// <param name="idArticulo"></param>
        /// <returns></returns>
        public static List<e_ProveedorGrid> ObtenerProveedorGridInfo(DateTime fechaIni, DateTime fechaFin, string idArticulo)
        {

            try
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("Obtener_ProveedorReportInfo");

                dbTse.AddInParameter(dbCommand, "@Fechaini", DbType.DateTime, fechaIni);
                dbTse.AddInParameter(dbCommand, "@Fechafin", DbType.DateTime, fechaFin);
                dbTse.AddInParameter(dbCommand, "@art", DbType.String, idArticulo);
                dbTse.AddInParameter(dbCommand, "@Exportar", DbType.String, "0");

                var eproveedor = new List<e_ProveedorGrid>();

                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        eproveedor.Add(CargarInfoProveedorGrid(reader));
                    }
                }

                return eproveedor;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "ProveedorReportInfo";
                const string listaParametros = "";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0)
                    indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal);
                // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_ProveedorReport.cs", "ObtenerProveedorReportInfo()", "", lineaError,
                                             nombreProcedimiento, listaParametros, exError.Message);

                return null;
            }
        }

        /// <summary>
        /// Autor: Guillermo Ramirez
        /// Description: Retorna los datos que se mostraban en el reporte de Proveedores.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static e_ProveedorReport CargarInfoProveedor(IDataReader reader)
        {            
                var eproveedor = new e_ProveedorReport();

                eproveedor.OrdenCompra = int.Parse(reader["Orden_compra"].ToString());
                eproveedor.Proveedor = reader["Proveedor"].ToString();
                eproveedor.NumeroDocumentoAccion = int.Parse(reader["numdocumentoaccion"].ToString());
                eproveedor.ArtSap = reader["ID_SAP"].ToString();
                eproveedor.IdArticulo = int.Parse(reader["idArticulo"].ToString());
                eproveedor.NombreArticulo = reader["nombre_articulo"].ToString();
                eproveedor.Lote = reader["lote"].ToString();
                eproveedor.FechaVencimiento = DateTime.Parse(reader["fechavencimiento"].ToString());
                eproveedor.CantidadRecibida = decimal.Parse(reader["Cantidad_Recibida"].ToString());
                eproveedor.CantidadPorRecibir = decimal.Parse(reader["cantidadxrecibir"].ToString());
                eproveedor.Diferencia = reader["diferencia"].ToString();// int.Parse(reader["diferencia"].ToString());
                eproveedor.Simbolo = char.Parse(reader["simbolo"].ToString());
                eproveedor.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                eproveedor.FechaCreacion = DateTime.Parse(reader["fechacreacion"].ToString());
                eproveedor.EsGranel = bool.Parse(reader["Granel"].ToString());

                return eproveedor;
                    
        }

        /// <summary>
        /// Autor: Guillermo Ramirez
        /// Description: Retorna los datos que se mostraban en el reporte de Proveedores.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static e_ProveedorGrid CargarInfoProveedorGrid(IDataReader reader)
        {         
                var eproveedor = new e_ProveedorGrid();

                eproveedor.OrdenCompra = int.Parse(reader["Orden_compra"].ToString());
                eproveedor.Proveedor = reader["Proveedor"].ToString();
                eproveedor.DescripcionArticulo = reader["Descripcion_Articulo"].ToString();
                eproveedor.Lote = reader["lote"].ToString();
                eproveedor.FechaVencimiento = DateTime.Parse(reader["fechavencimiento"].ToString());
                eproveedor.CantidadRecibida = decimal.Parse(reader["Cantidad_Recibida"].ToString());
                eproveedor.CantidadPorRecibir = decimal.Parse(reader["cantidadxrecibir"].ToString());
                eproveedor.Diferencia = reader["diferencia"].ToString();
                eproveedor.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());

                return eproveedor;
           
         }
    }
}
