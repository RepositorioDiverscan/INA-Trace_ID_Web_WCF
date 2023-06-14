using Diverscan.MJP.Entidades.PICKING;
using Diverscan.MJP.Entidades.SSCC;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Diverscan.MJP.AccesoDatos.Alistos;
using Diverscan.MJP.AccesoDatos.Bodega;

namespace Diverscan.MJP.AccesoDatos.SSCC
{
    public class ConsultarSSCC
    {
        public List<SSCCRecord> GetConsultarSSCC(string Detalle_SSCC)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_CONSULTAR_SSCC_HH");
            dbTse.AddInParameter(dbCommand, "@Text", DbType.String, Detalle_SSCC);
            List<SSCCRecord> mAIList = new List<SSCCRecord>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    mAIList.Add(new SSCCRecord(reader));
                }
            }
            return mAIList;
        }

        public string UbicarSSCC(int idUbicacion, string consecutivoSSCC)
        {
            string resultado = "";
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_UbicarSSCC_VP");
            dbTse.AddInParameter(dbCommand, "@idUbicacion", DbType.Int32, idUbicacion);
            dbTse.AddInParameter(dbCommand, "@ssccGenerado", DbType.String, consecutivoSSCC);
            dbTse.AddOutParameter(dbCommand, "@Resultado", DbType.String, 200);

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                resultado = dbTse.GetParameterValue(dbCommand, "@Resultado").ToString();
            }

            return resultado;
        }

        public string TransferSSCC(int idUbicacion, int idSSCC)
        {
            string resultado = "";
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_TransferSSCC_VP");
            dbTse.AddInParameter(dbCommand, "@idUbicacion", DbType.Int32, idUbicacion);
            dbTse.AddInParameter(dbCommand, "@idSSCC", DbType.Int32, idSSCC);
            dbTse.AddOutParameter(dbCommand, "@Resultado", DbType.String, 200);

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                resultado = dbTse.GetParameterValue(dbCommand, "@Resultado").ToString();
            }

            return resultado;
        }

        public EEncabezadoOlaSSCC GetSSCCProducts(string consecutivoSSCC)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("GetSSCCProducts");
            dbTse.AddInParameter(dbCommand, "@consecutivoSSCC", DbType.String, consecutivoSSCC);

          EEncabezadoOlaSSCC encabezadoOla = new EEncabezadoOlaSSCC();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                bool firstTime = false;
                while (reader.Read())
                {
                    if (firstTime == false)
                    {
                        encabezadoOla.IdMaestroSolicitud = Convert.ToInt32(reader["IdMaestroSolicitud"].ToString());
                        encabezadoOla.Nombre = reader["Nombre"].ToString();
                        encabezadoOla.Destino = reader["Comentarios"].ToString();
                        firstTime = true;
                    }

                    EDetalleSSCCOla detalleSSCCOla = new EDetalleSSCCOla();
                    detalleSSCCOla.IdArticulo = Convert.ToInt32(reader["idArticulo"].ToString());
                    detalleSSCCOla.GTIN = reader["GTIN"].ToString();
                    detalleSSCCOla.Nombre = reader["NombreHH"].ToString();
                    detalleSSCCOla.Lote = reader["Lote"].ToString();
                    detalleSSCCOla.FechaAndroid = DateTime.Parse(reader["FechaVencimiento"].ToString()).ToString("dd-MM-yyyy");
                    detalleSSCCOla.Cantidad = Convert.ToInt32(reader["Cantidad"].ToString());
                    detalleSSCCOla.Certificado = bool.Parse(reader["certificado"].ToString());

                    encabezadoOla.Articulos.Add(detalleSSCCOla);
                }
            }

            return encabezadoOla;
        }

        public ESSCC ObtenerSSCC(string ssccGenerado)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerSSCC");
            dbTse.AddInParameter(dbCommand, "@consecutivoSSCC", DbType.String, ssccGenerado);
            ESSCC eSSCC = new ESSCC();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    if (reader.GetName(0).Equals("Resultado"))
                    {                      
                        eSSCC.DescripcionSSCC = reader["Resultado"].ToString();
                        return eSSCC;
                    }
                    else
                        eSSCC = new ESSCC(reader);
                }
            }
            return eSSCC;
        }

        public List<ESSCC> ObtenerSSCCXIdSolicitud(long idMaestroSolicitud)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerSSCCXIdMaestroSolicitud");
            dbTse.AddInParameter(dbCommand, "@idInterno", DbType.Int64, idMaestroSolicitud);
            List<ESSCC> listaSSCC = new List<ESSCC>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ESSCC sscc = new ESSCC ();
                    sscc.IdSSCC = Convert.ToInt32(reader["idConsecutivoSSCC"].ToString());
                    sscc.DescripcionSSCC = reader["Descripcion"].ToString();
                    sscc.ConsecutivoSSCC = reader["SSCCGenerado"].ToString();
                    sscc.FechaProcesadoSSCC = reader["FechaProcesado"].ToString();
                    sscc.UbicacionSSCC = reader["Ubicacion"].ToString();
                    sscc.UsuarioCertificador = long.Parse(reader["IdUsuario"].ToString());
                    sscc.NombreUsuario = reader["Usuario"].ToString();
                    sscc.Total = Convert.ToInt32(reader["Total"].ToString());
                    sscc.Avance = Convert.ToInt32(reader["Avance"].ToString());
                    listaSSCC.Add(sscc);
                }
            }
            return listaSSCC;
        }

        public List<ESSCC> ObtenerSSCCXDespacho(long idMaestroSolicitud, long idSSCC)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerSSCCXIdMaestroDespacho");
            dbTse.AddInParameter(dbCommand, "@idMaestroSolicitud", DbType.Int64, idMaestroSolicitud);
            dbTse.AddInParameter(dbCommand, "@idSSCC", DbType.Int64, idSSCC);
            List<ESSCC> listaSSCC = new List<ESSCC>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ESSCC sscc = new ESSCC();
                    sscc.IdSSCC = Convert.ToInt32(reader["idConsecutivoSSCC"].ToString());
                    sscc.DescripcionSSCC = reader["Descripcion"].ToString();
                    sscc.ConsecutivoSSCC = reader["SSCCGenerado"].ToString();
                    sscc.FechaProcesadoSSCC = reader["FechaProcesado"].ToString();
                    sscc.IdUbicacionSSCC = reader["IdUbicacionSSCC"].ToString();
                    sscc.UbicacionSSCC = reader["Ubicacion"].ToString();
                    sscc.UsuarioCertificador = long.Parse(reader["IdUsuario"].ToString());
                    sscc.NombreUsuario = reader["Usuario"].ToString();

                    listaSSCC.Add(sscc);
                }
            }
            return listaSSCC;
        }

        public List<EDetalleSSCCCertificado> GetSSCCProducts(int idSSCC)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerDetalleSSCC");
            dbTse.AddInParameter(dbCommand, "@idConsecutivoSSCC", DbType.Int32, idSSCC);

            List<EDetalleSSCCCertificado> _detalleSSCCLista = new List<EDetalleSSCCCertificado>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {                
                while (reader.Read())
                {
                    EDetalleSSCCCertificado detalleSSCCOla = new EDetalleSSCCCertificado();
                    detalleSSCCOla.IdArticulo = Convert.ToInt32(reader["idArticulo"].ToString());
                    detalleSSCCOla.GTIN = reader["GTIN"].ToString();
                    detalleSSCCOla.IdInterno = reader["idInterno"].ToString();
                    detalleSSCCOla.Nombre = reader["NombreHH"].ToString();
                    detalleSSCCOla.Lote = reader["Lote"].ToString();
                    detalleSSCCOla.CantidadDiferencia = Convert.ToInt32(reader["diferencia"].ToString());
                    string fecha = DateTime.Parse(reader["FechaVencimiento"].ToString()).ToString("dd-MM-yyyy");
                    if (fecha.Contains("01-01-1900"))
                        fecha = "NA";
                    detalleSSCCOla.FechaVencimiento = fecha;
                    detalleSSCCOla.Cantidad = Convert.ToInt32(reader["Cantidad"].ToString());
                    detalleSSCCOla.Certificado = Convert.ToBoolean(reader["certificado"].ToString()) ? "Si":"No";

                    _detalleSSCCLista.Add(detalleSSCCOla);
                }
            }
            return _detalleSSCCLista;
        }

        public string RevertirArticuloSSCCCertificado(ERevertirArticuloSSCC articuloSSCC)
        {
            string resultado = "";
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("RevertirArticuloSSCCCertificado_VP");
            dbTse.AddInParameter(dbCommand, "@idConsecutivoSSCC", DbType.Int64, articuloSSCC.IdSSCC);
            dbTse.AddInParameter(dbCommand, "@IdMaestroSolicitud", DbType.Int64, articuloSSCC.IdMaestroSolicitud);
            dbTse.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, articuloSSCC.IdArticulo);
            dbTse.AddInParameter(dbCommand, "@IdUbicacionDestino", DbType.String, articuloSSCC.IdUbicacionDestino);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, articuloSSCC.Lote);
            dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.Date, articuloSSCC.FechaVencimientoAndroid);
            dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Int32, articuloSSCC.Cantidad);
            dbTse.AddInParameter(dbCommand, "@IdUsuario", DbType.Int32, articuloSSCC.IdUsuario);            
            dbTse.AddOutParameter(dbCommand, "@Resultado", DbType.String, 200);

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                resultado = dbTse.GetParameterValue(dbCommand, "@Resultado").ToString();
            }

            return resultado;
        }

        public List<ESSCC> CantidadSSCCActivosUsuario(long idUsuario) 
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("CantidadSSCCActivosUsuario");          
            dbTse.AddInParameter(dbCommand, "@IdUsuario", DbType.Int64, idUsuario);

            List<ESSCC> listaSSCC = new List<ESSCC>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ESSCC sscc = new ESSCC();                
                    sscc.ConsecutivoSSCC = reader["SSCCGenerado"].ToString();                  
                    listaSSCC.Add(sscc);
                }
            }

            return listaSSCC;
        }

        public List<ESSCC> GenerarSSCC(int cantidad)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("GenerarSSCC_VP");
            dbTse.AddInParameter(dbCommand, "@cantidad", DbType.Int64, cantidad);
            List<ESSCC> listaSSCC = new List<ESSCC>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ESSCC sscc = new ESSCC();
                    sscc.ConsecutivoSSCC = reader["SSCC"].ToString();
                    listaSSCC.Add(sscc);
                }
            }
            return listaSSCC;
        }
    }
}
