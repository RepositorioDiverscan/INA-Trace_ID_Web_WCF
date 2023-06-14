using Diverscan.MJP.AccesoDatos.Reportes.KardexXSkuTIDSAP.Entidad;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.KardexXSkuTIDSAP
{
    public class KardexXSkuTIDSAPDBA
    {
        public List<EListKardexSkuTID> ObtenerKardexSkuTID(EKardexSkuTIDSAP kardexSkuTIDSAP)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerKardexSkuTID");
            dbTse.AddInParameter(dbCommand, "@IdBodega", DbType.Int32, kardexSkuTIDSAP.IdBodega);
            dbTse.AddInParameter(dbCommand, "@Sku", DbType.String, kardexSkuTIDSAP.Sku);
            dbTse.AddInParameter(dbCommand, "@FechaInicial", DbType.DateTime, kardexSkuTIDSAP.FechaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFinal", DbType.DateTime, kardexSkuTIDSAP.FechaFin);

            List<EListKardexSkuTID> listObtenerKardexTID = new List<EListKardexSkuTID>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    string Tipo = reader["Tipo"].ToString();
                    string Socio = reader["Socio"].ToString();
                    DateTime Fecha = Convert.ToDateTime(reader["Fecha"].ToString());
                    decimal Cantidad = decimal.Parse(reader["Cantidad"].ToString());
                    decimal Saldo = decimal.Parse(reader["Saldo"].ToString());
                    string Usuario = reader["Usuario"].ToString();

                    listObtenerKardexTID.Add(new EListKardexSkuTID(Tipo,Socio,Fecha,Cantidad,Saldo,Usuario));
                }
            }
            return listObtenerKardexTID;
        }

        public List<EListKardexSkuSAP> ObtenerKardexSkuSAP(EKardexSkuTIDSAP kardexSkuTIDSAP)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerKardexSkuSAP");
            dbTse.AddInParameter(dbCommand, "@IdBodega", DbType.Int32, kardexSkuTIDSAP.IdBodega);
            dbTse.AddInParameter(dbCommand, "@Sku", DbType.String, kardexSkuTIDSAP.Sku);
            dbTse.AddInParameter(dbCommand, "@FechaInicial", DbType.DateTime, kardexSkuTIDSAP.FechaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFinal", DbType.DateTime, kardexSkuTIDSAP.FechaFin);

            List<EListKardexSkuSAP> listObtenerKardexSAP = new List<EListKardexSkuSAP>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    string Tipo = reader["Tipo"].ToString();
                    string Socio = reader["Socio"].ToString();
                    DateTime Fecha = Convert.ToDateTime(reader["Fecha"].ToString());
                    decimal Cantidad = decimal.Parse(reader["Cantidad"].ToString());
                    decimal Saldo = decimal.Parse(reader["Saldo"].ToString());

                    listObtenerKardexSAP.Add(new EListKardexSkuSAP(Tipo, Socio, Fecha, Cantidad, Saldo));
                }
            }
            return listObtenerKardexSAP;
        }
    }
}
