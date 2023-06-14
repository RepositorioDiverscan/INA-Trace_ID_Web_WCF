using Diverscan.MJP.Entidades.Reportes.Kardex;
using Diverscan.MJP.AccesoDatos.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Reportes
{
    public class n_kardexSKU
    {
        //metodo para obtener el kardex 
        public List<e_kardexSKU> ObtenerKardex(int idBodega, string Sku, string Lote, bool Transitos, DateTime f1, DateTime f2)
        {
            try
            {
                da_kardexSKU kardex = new da_kardexSKU();
                return kardex.ObtenerKardex(idBodega, Sku, Lote, Transitos, f1, f2);
            }
            catch (Exception e)
            {
                var p = e.Message;
                return null;
            }
        }
    }
}
