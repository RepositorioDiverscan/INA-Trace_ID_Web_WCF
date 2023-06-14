using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Text;
using Diverscan.MJP.AccesoDatos.Reportes.Trazabilidad.Entidades;

namespace Diverscan.MJP.AccesoDatos.Reportes.Trazabilidad
{
    public class TrazabilidadDBA : ITrazabilidadDBA
    {
        public List<EConsultaArticulos> ObtenerArticulos(string Dato)
        {

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_ArticulosTrazabilidad");
            dbTse.AddInParameter(dbCommand, "@Dato", DbType.String, Dato);
            List<EConsultaArticulos> articulosList = new List<EConsultaArticulos>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    int idArticulo = int.Parse(reader["idArticulo"].ToString()); ;
                    string nombre = reader["Nombre"].ToString();
                    string GTIN = reader["GTIN"].ToString();
                    articulosList.Add(new EConsultaArticulos(idArticulo, nombre, GTIN));
                }
            }
            return articulosList;
        }

        public List<EListadoTrazabilidad> ObtenerDatosTrazabilidad(int idArticulo, DateTime FechaIncicio, DateTime FechaFinal, int idBodega)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ConsultaTrazabilidad");
            dbTse.AddInParameter(dbCommand, "@Articulo", DbType.Int32, idArticulo);
            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.Date, FechaIncicio);
            dbTse.AddInParameter(dbCommand, "@FechaFinal", DbType.Date, FechaFinal);
            dbTse.AddInParameter(dbCommand, "@IdBodega", DbType.Int32, idBodega);

            dbCommand.CommandTimeout = 3600;
            List<EListadoTrazabilidad> listaRegistros = new List<EListadoTrazabilidad>();
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        int _idTrazabilidad = Convert.ToInt32(reader["IdTrazabilidad"].ToString());
                        decimal _cantidad = Convert.ToDecimal(reader["Cantidad"].ToString());
                        int _idEstado = Convert.ToInt32(reader["IdEstado"].ToString());
                        string _operacion = reader["Operacion"].ToString();
                        DateTime _fechaRegistro = Convert.ToDateTime(reader["FechaRegistro"].ToString());
                        string _nombre = reader["Nombre"].ToString();
                        decimal _saldo = Convert. ToDecimal(reader["Saldo"].ToString());

                        listaRegistros.Add(new EListadoTrazabilidad
                        (
                            _idTrazabilidad,
                            _cantidad,
                            _idEstado,
                            _operacion,
                            _fechaRegistro,
                            _nombre,
                            _saldo
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaRegistros;
        }
    }
    /*
      
     */
}
