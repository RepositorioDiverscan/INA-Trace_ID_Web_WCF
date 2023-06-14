using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Despacho
{
    public class NDespacho
    {
        private IFileExceptionWriter _fileExceptionWriter;
        private DDespacho dDespacho;

        public NDespacho(IFileExceptionWriter fileExceptionWriter)
        {
            _fileExceptionWriter = fileExceptionWriter;
            dDespacho = new DDespacho();
        }
        public String CargarVehiculoXPlaca(string placa, long idSSCC,long idUbicacion, 
                                            bool capacidadExcedida, bool sobreCargar)
        {
            try
            {
                return dDespacho.CargarVehiculoXPlaca(placa, idSSCC, idUbicacion, capacidadExcedida, sobreCargar);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.VEHICLEFILEPATHEXCEPTION);
                return ex.Message;
            }
        }
    }
}
