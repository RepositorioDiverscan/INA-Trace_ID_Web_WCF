using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Vehiculo
{
    public class DVehiculo
    {
        //public List<EVehiculo> GetVehiculoXBodega(int idBodega, string buscar)
        //{
        //    var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
        //    var dbCommand = dbTse.GetStoredProcCommand("SPObtenerVehiculoXBodega");
        //    dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);
        //    dbTse.AddInParameter(dbCommand, "@buscar", DbType.String, buscar);
        //    List<EVehiculo> vehiculos = new List<EVehiculo>();

        //    using (var reader = dbTse.ExecuteReader(dbCommand))
        //    {
        //        while (reader.Read())
        //        {
        //            EVehiculo eVehiculo = new EVehiculo();
        //            eVehiculo.IdVehiculo = long.Parse(reader["IdVehiculo"].ToString());
        //            eVehiculo.NombreTransportista = reader["Transportista"].ToString();
        //            eVehiculo.TipoVehiculo = reader["TipoVehiculo"].ToString();
        //            eVehiculo.Marca = reader["Marca"].ToString();
        //            eVehiculo.Placa = reader["Placa"].ToString();
        //            eVehiculo.NombreVehiculo = reader["Nombre"].ToString();
        //            eVehiculo.Color = reader["Color"].ToString();
        //            eVehiculo.Modelo = reader["Modelo"].ToString();
        //            eVehiculo.Comentario = reader["Comentario"].ToString();
        //            eVehiculo.CapacidadPeso = int.Parse(reader["CapacidadPeso"].ToString());
        //            eVehiculo.CapacidadVolumen = long.Parse(reader["CapacidadVolumen"].ToString());
        //            eVehiculo.CapacidadPesoUsado = int.Parse(reader["CapacidadPesoUsado"].ToString());
        //            eVehiculo.CapacidadVolumenUsado = long.Parse(reader["CapacidadVolumenUsado"].ToString());
        //            eVehiculo.IdInternoBodega = reader["idInternoBodega"].ToString();
        //            eVehiculo.FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"].ToString());
        //            bool activo = false;
        //            Boolean.TryParse(reader["activo"].ToString(), out activo);
        //            eVehiculo.Activo = activo;
        //            vehiculos.Add(eVehiculo);
        //        }
        //    }
        //    return vehiculos;
        //}
        //public SEVehiculo GetVehiculoXPlaca(string placa)
        //{
        //    var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
        //    var dbCommand = dbTse.GetStoredProcCommand("SPObtenerVehiculoXPlaca");
        //    dbTse.AddInParameter(dbCommand, "@placa", DbType.String, placa);
        //    SEVehiculo vehiculo = new SEVehiculo();

        //    using (var reader = dbTse.ExecuteReader(dbCommand))
        //    {
        //        while (reader.Read())
        //        {
        //            vehiculo = new SEVehiculo(reader);
        //        }
        //    }
        //    return vehiculo;
        //}

        //public String DescargarVehiculoXPlaca(string placa)
        //{
        //    var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
        //    var dbCommand = dbTse.GetStoredProcCommand("SPDescargarVehiculoXPlaca");
        //    dbTse.AddInParameter(dbCommand, "@placa", DbType.String, placa);
        //    dbTse.AddOutParameter(dbCommand, "@Resultado", DbType.String, 200);
        //    String result = "";

        //    using (var reader = dbTse.ExecuteReader(dbCommand))
        //    {            
        //         result = dbTse.GetParameterValue(dbCommand, "@Resultado").ToString();                
        //    }

        //    return result;
        //}

        //public string InsertVehiculo(EVehiculo vehiculo)
        //{
        //    var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
        //    var dbCommand = dbTse.GetStoredProcCommand("InsertVehiculo");
        //    dbTse.AddInParameter(dbCommand, "@idTransportista", DbType.Int32, vehiculo.IdTransportista);
        //    dbTse.AddInParameter(dbCommand, "@idTipoVehiculo", DbType.Int32, vehiculo.IdTipoVehiculo);
        //    dbTse.AddInParameter(dbCommand, "@idMarcaVehiculo", DbType.Int32, vehiculo.IdMarca);
        //    dbTse.AddInParameter(dbCommand, "@Placa", DbType.String, vehiculo.Placa);
        //    dbTse.AddInParameter(dbCommand, "@idColor", DbType.Int64, vehiculo.IdColor);
        //    dbTse.AddInParameter(dbCommand, "@Modelo", DbType.String, vehiculo.Modelo);
        //    dbTse.AddInParameter(dbCommand, "@Comentario", DbType.String, vehiculo.Comentario);
        //    dbTse.AddInParameter(dbCommand, "@CapacidadVolumen", DbType.Decimal, vehiculo.CapacidadVolumen);
        //    dbTse.AddInParameter(dbCommand, "@CapacidadPeso", DbType.Int32, vehiculo.CapacidadPeso); 
        //    dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, vehiculo.IdBodega);
        //    dbTse.AddInParameter(dbCommand, "@activo", DbType.Boolean, vehiculo.Activo);
        //    dbTse.AddInParameter(dbCommand, "@idUsuarioRegistro", DbType.Int32, vehiculo.IdUsuarioRegistro);
        //    dbTse.AddOutParameter(dbCommand, "@result", DbType.String, 200);
        //    String result = "";

        //    using (var reader = dbTse.ExecuteReader(dbCommand))
        //    {
        //        result = dbTse.GetParameterValue(dbCommand, "@result").ToString();
        //    }

        //    return result;
        //}

        //public string UpdateVehiculo(EVehiculo vehiculo)
        //{
        //    var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
        //    var dbCommand = dbTse.GetStoredProcCommand("UpdateVehiculo");
        //    dbTse.AddInParameter(dbCommand, "@idVehiculo", DbType.Int32, vehiculo.IdVehiculo);
        //    dbTse.AddInParameter(dbCommand, "@idTransportista", DbType.Int32, vehiculo.IdTransportista);
        //    dbTse.AddInParameter(dbCommand, "@idTipoVehiculo", DbType.Int32, vehiculo.IdTipoVehiculo);
        //    dbTse.AddInParameter(dbCommand, "@idMarcaVehiculo", DbType.Int32, vehiculo.IdMarca);
        //    dbTse.AddInParameter(dbCommand, "@Placa", DbType.String, vehiculo.Placa);
        //    dbTse.AddInParameter(dbCommand, "@idColor", DbType.Int64, vehiculo.IdColor);
        //    dbTse.AddInParameter(dbCommand, "@Modelo", DbType.String, vehiculo.Modelo);
        //    dbTse.AddInParameter(dbCommand, "@Comentario", DbType.String, vehiculo.Comentario);
        //    dbTse.AddInParameter(dbCommand, "@CapacidadVolumen", DbType.Decimal, vehiculo.CapacidadVolumen);
        //    dbTse.AddInParameter(dbCommand, "@CapacidadPeso", DbType.Int32, vehiculo.CapacidadPeso);            
        //    dbTse.AddInParameter(dbCommand, "@activo", DbType.Boolean, vehiculo.Activo);
        //    dbTse.AddOutParameter(dbCommand, "@result", DbType.String, 200);
        //    String result = "";

        //    using (var reader = dbTse.ExecuteReader(dbCommand))
        //    {
        //        result = dbTse.GetParameterValue(dbCommand, "@result").ToString();
        //    }

        //    return result;
        //}
    }
}
