using Diverscan.MJP.AccesoDatos.OrdenCompa;
using Diverscan.MJP.Entidades;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.OrdenCompa
{
    public class n_DetalleOrdenCompra
    {
        public void InsertarDetalleOrdenCompra(e_ProcesarOrdenCompra e_procesarOrdenCompra)
        {
            da_DetalleOrdenCompra da_detalleOrdenCompra = new da_DetalleOrdenCompra();
            da_detalleOrdenCompra.InsertarDetalleOrdenCompra(e_procesarOrdenCompra);
        }

        public string InsertarDetalleOC(DataTable DetalleOC, Int64 idproveedor, DateTime fechacreacion, DateTime fecharecepcion, string numOC, string comentario, string IdCompania, int idUsuario)
        {
            string mensaje = "";
            da_DetalleOrdenCompra detalleOrdenCompra = new da_DetalleOrdenCompra();
            mensaje = detalleOrdenCompra.InsertarDetalleOrdenCompra(DetalleOC, idproveedor, fechacreacion, fecharecepcion, numOC, comentario, IdCompania, idUsuario);
            return mensaje;
        }
    }
}
