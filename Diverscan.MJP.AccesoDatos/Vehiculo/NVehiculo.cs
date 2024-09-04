using Diverscan.MJP.Utilidades;
using System;

namespace Diverscan.MJP.AccesoDatos.Vehiculo
{
    public class NVehiculo
    {
        //private IFileExceptionWriter _fileExceptionWriter;
        //private DVehiculo dVehiculo;

        //public NVehiculo(IFileExceptionWriter fileExceptionWriter)
        //{
        //    dVehiculo = new DVehiculo();
        //    _fileExceptionWriter = fileExceptionWriter;
        //}
        //public ResultGetVehiculo GetVehiculoXPlaca(string placa)
        //{
        //    ResultGetVehiculo resultGetVehiculo = new ResultGetVehiculo();
        //    try 
        //    {
        //        resultGetVehiculo.state = true;
        //        resultGetVehiculo.Description = "Succesful";
        //        resultGetVehiculo.Vehiculo = dVehiculo.GetVehiculoXPlaca(placa);
        //    } catch (Exception ex)
        //    {
        //        resultGetVehiculo.state = false;
        //        resultGetVehiculo.Description = ex.Message;
        //        resultGetVehiculo.Vehiculo = null;
        //        _fileExceptionWriter.WriteException(ex, PathFileConfig.VEHICLEFILEPATHEXCEPTION);
        //    }
        //    return resultGetVehiculo;
        //}

        //public String DescargarVehiculoXPlaca(string placa)
        //{            
        //    try
        //    {
        //        return dVehiculo.DescargarVehiculoXPlaca(placa);
        //    }
        //    catch (Exception ex)
        //    {               
        //        _fileExceptionWriter.WriteException(ex, PathFileConfig.VEHICLEFILEPATHEXCEPTION);
        //        return ex.Message;
        //    }
        //}

        //public String InsertVehiculo(EVehiculo vehiculo) 
        //{
        //    try
        //    {
        //        return dVehiculo.InsertVehiculo(vehiculo);
        //    }
        //    catch (Exception ex)
        //    {
        //        _fileExceptionWriter.WriteException(ex, PathFileConfig.VEHICLEFILEPATHEXCEPTION);
        //        return ex.Message;
        //    }
        //}
     
    }
}
