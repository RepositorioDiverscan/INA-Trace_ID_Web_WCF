
using EAN128;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Articulos.InfoArticulo
{
    public class ArticuloInfoGetter
    {
        public ArticuloBaseInfo GetArticuloBaseInfo(string data)
        {
            ArticuloBaseInfo articuloBaseInfo = new ArticuloBaseInfo();

            if(data.Substring(5,1) == "-")
            {
                articuloBaseInfo.Gtin = data;
                setDBInfo(ref articuloBaseInfo, data);
            }
                      
            else if (data.Length > 15)
            {
                setEAN128(ref articuloBaseInfo, data);
                setDBInfo(ref articuloBaseInfo, articuloBaseInfo.Gtin);
            }
            else
            {
                articuloBaseInfo.Gtin = data;
                setDBInfo(ref articuloBaseInfo, data);
            }
            return articuloBaseInfo;
        }

        private void setEAN128(ref ArticuloBaseInfo articuloBaseInfo, string data)
        {
            EAN128Parser ean128Parser = new EAN128Parser();
            var aiFound = ean128Parser.Parse(data);
            var data01 = aiFound.FirstOrDefault(x => x.Key.AI == "01");
            if (data01.Key != null)
                articuloBaseInfo.Gtin = Convert.ToInt64(data01.Value).ToString();
            var data02 = aiFound.FirstOrDefault(x => x.Key.AI == "02");
            if (data02.Key != null)
                articuloBaseInfo.Gtin = Convert.ToInt64(data02.Value).ToString();
            var data10 = aiFound.FirstOrDefault(x => x.Key.AI == "10");
            if (data10.Key != null)
                articuloBaseInfo.Lote = data10.Value.ToUpper();
            try
            {
                var data17 = aiFound.FirstOrDefault(x => x.Key.AI == "17");
                if (data17.Key != null)
                {
                    articuloBaseInfo.FechaVencimiento = getDateExp(data17.Value);
                    articuloBaseInfo.FechaVencimientoAndroid = articuloBaseInfo.FechaVencimiento.ToString("dd-MM-yyyy");
                }
            }
            catch (Exception)
            {
                throw new Exception("Formato de fecha vencimiento incorrecto");
            }
            try
            {
                var data37 = aiFound.FirstOrDefault(x => x.Key.AI == "37");
                if (data37.Key != null)
                    articuloBaseInfo.Cantidad = int.Parse(data37.Value);
            }
            catch (Exception)
            {
                throw new Exception("Cantidad incorrecto");
            }
        }

        private void setDBInfo(ref ArticuloBaseInfo articuloBaseInfo, string gtin)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_GETPRODUCTFROMGTIN");

                dbTse.AddInParameter(dbCommand, "@p_gtin", DbType.String, gtin);
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        articuloBaseInfo.IdArticulo = Convert.ToInt32(reader["IdArticulo"].ToString());
                        articuloBaseInfo.IdInterno = reader["idInterno"].ToString();
                        string trazabilida = reader["ConTrazabilidad"].ToString();
                        articuloBaseInfo.ConTrazabilidad = bool.Parse(reader["ConTrazabilidad"].ToString());
                        if (articuloBaseInfo.Cantidad < 1)
                            articuloBaseInfo.Cantidad = Convert.ToDouble(reader["CANTIDADGTIN14"].ToString());
                        if (articuloBaseInfo.Cantidad == 0)
                            articuloBaseInfo.Cantidad = 1;
                        articuloBaseInfo.Nombre = reader["NOMBREARTICULO"].ToString();
                        articuloBaseInfo.NombreGtin14 = reader["NOMBREGTIN14"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al obtener informacion de articulo en base de datos");
            }
        }

        private DateTime getDateExp(string date)
        {
            if (date.StartsWith("00"))
            {
                return new DateTime(1900, 01, 01);
            }
            int year = int.Parse(date.Substring(0, 2)) + 2000;
            int mount = int.Parse(date.Substring(2, 2));
            int day = int.Parse(date.Substring(4, 2));
            return new DateTime(year, mount, day);
        }
    }
}
