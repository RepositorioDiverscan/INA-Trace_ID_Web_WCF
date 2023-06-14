using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Diverscan.MJP.Entidades.Alistos;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Diverscan.MJP.AccesoDatos.Alistos
{
    public class EstadoAlistos : IEstadoAlistos
    {
        public List<EEnlist> ProductDetailFromGtin(string GTIN)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_GETPRODUCTFROMGTIN");

                dbTse.AddInParameter(dbCommand, "@p_gtin", DbType.String, GTIN);

                List<EEnlist> productdetail = new List<EEnlist>();
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        productdetail.Add(
                            new EEnlist(reader));
                    }
                }
                return productdetail;
            }
            catch (Exception)
            {
                return new List<EEnlist>();
            }
        }
    

        public List<EstadoAlisto> StatusActualPedido(string idMaestroArticulo)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_StatusActualPedido_Despacho");

                dbTse.AddInParameter(dbCommand, "@idMaestroSolicitud", DbType.String, idMaestroArticulo);

                List<EstadoAlisto> statusList = new List<EstadoAlisto>();
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        statusList.Add(
                            new EstadoAlisto(reader));
                    }
                }
                return statusList;
            }
            catch (Exception)
            {
                return new List<EstadoAlisto>();
            }
        }

        public string SetProductManualEnlist(List<EEnlist> ProductList)
        {
            DataTable inputArticulos = ProductList.ToDataTable();
            string respuesta = "";
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_ArticulosRecepcion");
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@eArticulos";
            parameter.SqlDbType = System.Data.SqlDbType.Structured;
            parameter.Value = inputArticulos;
            dbCommand.Parameters.Add(parameter);

            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    respuesta = reader["Resultado"].ToString();
                }
            }

            return respuesta;
        }

        public List<EEncabezadoSalida> GetEncabezadoSalidas(int idUsuario)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("GetEncabezadosOrdenesSalidas");
            dbTse.AddInParameter(dbCommand, "@p_idusuario", DbType.Int32, idUsuario);

            List<EEncabezadoSalida> ListEncabezadosSalida = new List<EEncabezadoSalida>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ListEncabezadosSalida.Add(new EEncabezadoSalida(reader));
                }
            }

             return ListEncabezadosSalida;
        }

        public string InsertSSCCCode(string SSCCCode, int idMaestroSolicitud)
        {
            string resultado = "";
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_InsertSSCCCode_vp");
            dbTse.AddInParameter(dbCommand, "@p_ssccgenerado", DbType.String, SSCCCode);
            dbTse.AddInParameter(dbCommand, "@p_idMaestroSolicitud", DbType.Int32, idMaestroSolicitud);

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    resultado= reader["Resultado"].ToString();
                }
            }

            return resultado;
        }

        public string IngresarArticuloSSCC(long idConsecutivoSSCC, long idMaestroSolicitud, long idArticulo, string lote, DateTime FechaVencimiento,
                                           int cantidad, long idUbicacion, long idLineaDetalleSolicitud, int idUsuario, int idMetodoAccionSalida)
        {
            string resultado = "";
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("IngresarArticuloAlistoSSCC_VP");
            dbTse.AddInParameter(dbCommand, "@idConsecutivoSSCC ", DbType.Int64, idConsecutivoSSCC);
            dbTse.AddInParameter(dbCommand, "@IdMaestroSolicitud", DbType.Int64, idMaestroSolicitud);
            dbTse.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, idArticulo);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, lote);
            dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.DateTime, FechaVencimiento);
            dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Int32, cantidad);
            dbTse.AddInParameter(dbCommand, "@idUbicacion", DbType.Int64, idUbicacion);
            dbTse.AddInParameter(dbCommand, "@idLineaDetalleSolicitud", DbType.Int64, idLineaDetalleSolicitud);
            dbTse.AddInParameter(dbCommand, "@IdUsuario", DbType.Int32, idUsuario);
            dbTse.AddInParameter(dbCommand, "@IdMetodoAccionSalida", DbType.Int32, idMetodoAccionSalida);
            dbTse.AddOutParameter(dbCommand, "@Resultado", DbType.String, 200);

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                resultado = dbTse.GetParameterValue(dbCommand, "@Resultado").ToString();
            }

            return resultado;
        }

        public string RevertirArticuloSSCC(long idConsecutivoSSCC, long idMaestroSolicitud, long idArticulo, string lote, DateTime FechaVencimiento,
                                           int cantidad,long idUbicacionDestino, long idLineaDetalleSolicitud, int idUsuario, int idMetodoAccionSalida)
        {
            string resultado = "";
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("RevertirArticuloAlistoSSCC_VP");
            dbTse.AddInParameter(dbCommand, "@idConsecutivoSSCC ", DbType.Int64, idConsecutivoSSCC);
            dbTse.AddInParameter(dbCommand, "@IdMaestroSolicitud", DbType.Int64, idMaestroSolicitud);
            dbTse.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, idArticulo);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, lote);
            dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.DateTime, FechaVencimiento);
            dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Int32, cantidad);           
            dbTse.AddInParameter(dbCommand, "@IdUbicacionDestino", DbType.Int64, idUbicacionDestino);
            dbTse.AddInParameter(dbCommand, "@idLineaDetalleSolicitud", DbType.Int64, idLineaDetalleSolicitud);
            dbTse.AddInParameter(dbCommand, "@IdUsuario", DbType.Int32, idUsuario);
            dbTse.AddInParameter(dbCommand, "@IdMetodoAccionEntrada", DbType.Int32, idMetodoAccionSalida);
            dbTse.AddOutParameter(dbCommand, "@Resultado", DbType.String, 200);

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                resultado = dbTse.GetParameterValue(dbCommand, "@Resultado").ToString();
            }

            return resultado;
        }


    }
}
