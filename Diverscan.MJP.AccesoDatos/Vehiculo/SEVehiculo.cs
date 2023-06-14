using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Vehiculo
{
    public class SEVehiculo:EVehiculo
    {
        public SEVehiculo():base()
        {

        }

        public SEVehiculo(IDataReader reader) : base(reader)
        {
            
        }

        public SEVehiculo(int idTransportista, int idTipo, int idMarca, int idColor, string numeroPlaca, string modelo, decimal volumen, int peso, string comentario, int idBodega, bool activo, int idUsuarioRegistro) : base( idTransportista,  idTipo,  idMarca,  idColor,  numeroPlaca,  modelo,  volumen,  peso,  comentario,  idBodega,  activo, idUsuarioRegistro)
        {
            
        }
    }
}
