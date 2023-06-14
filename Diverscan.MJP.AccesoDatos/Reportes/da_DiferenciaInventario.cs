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
    public class da_DiferenciaInventario
    {
        public static List<e_DiferenciaInventario> ObtenerReporteDiferenciaInventario(string IdArticulo, string OpcionReporte, string idCompania)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("Obtener_DiferenciaInventario");
 
                dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.String, IdArticulo);
                dbTse.AddInParameter(dbCommand, "@OpcionReporte", DbType.String, OpcionReporte);
                dbTse.AddInParameter(dbCommand, "@IdCompania", DbType.String, idCompania);

                var HistoricoDemanda = new List<e_DiferenciaInventario>();

                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        HistoricoDemanda.Add(CargarDiferenciaInventario(reader));
                    }
                }

                return HistoricoDemanda;
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

        private static e_DiferenciaInventario CargarDiferenciaInventario(IDataReader reader)
        {
            var eDiferenciaInventario = new e_DiferenciaInventario();
            eDiferenciaInventario.CodigoInterno = reader["idInterno"].ToString();
            eDiferenciaInventario.NombreArticulo = reader["NombreArticulo"].ToString();
            eDiferenciaInventario.DisponiblesInventario = Convert.ToDouble(reader["DiferenciaInventario"].ToString());
            eDiferenciaInventario.DisponiblesERP = Convert.ToDouble(reader["DiferenciaERP"].ToString());
            eDiferenciaInventario.DiferenciaERPInventario = Convert.ToDouble(reader["DiferenciaERPInventario"].ToString());
            eDiferenciaInventario.UnidadMedida = reader["UnidadMedida"].ToString();
            return eDiferenciaInventario;
        }
    }
}
