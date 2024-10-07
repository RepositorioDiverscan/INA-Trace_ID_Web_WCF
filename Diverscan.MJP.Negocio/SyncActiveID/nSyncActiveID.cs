using Diverscan.MJP.AccesoDatos.SyncActiveID;
using Diverscan.MJP.Entidades.SyncActiceID;

namespace Diverscan.MJP.Negocio.SyncActiveID
{
    public class nSyncActiveID
    {
        da_SyncActiveID da_SyncActiveID = new da_SyncActiveID();

        public eUbicacionActivo ObtenerUbicacionActual(string input)
        {
            return da_SyncActiveID.ObtenerUbicacionActual(input);
        }

        public bool ActualizarUbicacionDefecto(string input)
        {
            string ubi = da_SyncActiveID.ObtenerUbicacionDefecto(input);

            if (ubi == null)
            {
                return false;
            }

            string[] ubicacionDefecto = ubi.Split('_');

            eUbicacionActivo ubiActivo = new eUbicacionActivo(ubicacionDefecto[0], ubicacionDefecto[1], ubicacionDefecto[2], ubicacionDefecto[3]);
            return da_SyncActiveID.ActualizaUbiDefecto(ubiActivo, input);

        }
    }
}
