using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CodeUtilities;


namespace Diverscan.MJP.AccesoDatos.DetalleOrdenCompra
{
   public class NDetalleOrdenC
    {
        public string InsertarArticuloRR(List<EArticulos> eArticulos)
        {
            DDetalleOrdenC dDetalle = new DDetalleOrdenC();

            DataTable inputArticulos = eArticulos.ToDataTable();

            return dDetalle.InsertarArticulosRR(inputArticulos);
        }

    }
}
