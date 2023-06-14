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
    public class da_AuditoriaReport
    {
        public static List<e_KardexReport> ObtenerAuditoriaReportInfo(DateTime fechaIni, DateTime fechaFin, string idArticulo)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("Obtener_AuditoriaReportInfo");

                dbTse.AddInParameter(dbCommand, "@Fechain", DbType.DateTime, fechaIni);
                dbTse.AddInParameter(dbCommand, "@fechafin", DbType.DateTime, fechaFin);
                dbTse.AddInParameter(dbCommand, "@idArticulo", DbType.String, idArticulo);

                var ekardex = new List<e_KardexReport>();

                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ekardex.Add(CargarInfoAuditoria(reader));
                    }
                }

                return ekardex;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "GetProgramas";
                const string listaParametros = "";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0)
                    indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal);
                // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_Programa.cs", "GetProgramas()", "", lineaError,
                                             nombreProcedimiento, listaParametros, exError.Message);

                return null;
            }
        }

        /// <summary>
        /// Autor: Guillermo Ramirez
        /// Description: Retorna los datos que se mostraban en el reporte llamado Kardex.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static e_KardexReport CargarInfoAuditoria(IDataReader reader)
        {
            var ekardex = new e_KardexReport();

            //ekardex.Linea = int.Parse(reader["linea"].ToString());
            //ekardex.IdRegistro = int.Parse(reader["idregistro"].ToString());
            ekardex.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
            //ekardex.IdArticulo = reader["idarticulo"].ToString();
            ekardex.ArticuloSAP = reader["art_sap"].ToString();
            ekardex.Articulo = reader["articulo"].ToString();
            ekardex.Zona = reader["zona"].ToString();
            ekardex.Motivo = reader["motivo"].ToString();
            ekardex.Operacion = char.Parse(reader["operacion"].ToString());
            ekardex.SaldoInicial = decimal.Parse(reader["saldo_inicial"].ToString());
            ekardex.Cantidad = decimal.Parse(reader["cantidad"].ToString());
            ekardex.Saldo = decimal.Parse(reader["saldo"].ToString());
            //ekardex.DocTienda = reader["doc_tienda"].ToString();
            ekardex.Lote = reader["lote"].ToString();
            ekardex.FechaVencimiento = DateTime.Parse(reader["fechavencimiento"].ToString());
            //ekardex.Orden = reader["orden"].ToString();
            ekardex.OCDestino = reader["OC_Destino"].ToString();
            ekardex.IdMetodoAccion = int.Parse(reader["idmetodoaccion"].ToString());

            return ekardex;
        }
    }
}
