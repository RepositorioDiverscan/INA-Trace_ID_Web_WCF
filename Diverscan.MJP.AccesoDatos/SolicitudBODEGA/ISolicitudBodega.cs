using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.SolicitudBODEGA
{
    public interface ISolicitudBodega
    {
        string InsertarPreMaestro(int idUsuario, string nombre, string comentario, string idCompania, string idDestino, string idInterno, string idInternoSAP, DateTime fechaEntrega);

        void InsertarPreDetalle(string nombre, int idPreMaestroSolicitud, string idDestino, string IdInterno, decimal Cantidad, string descripcion, 
            string idCompania, int idUsuario, int numLinea, string gtin);
    }
}
