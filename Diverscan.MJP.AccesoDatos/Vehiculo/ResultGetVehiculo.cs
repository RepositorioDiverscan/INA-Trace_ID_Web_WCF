using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Vehiculo
{
     public class ResultGetVehiculo : ResultWS
    {
        private SEVehiculo _vehiculo;

        public SEVehiculo Vehiculo { get => _vehiculo; set => _vehiculo = value; }
    }
}
