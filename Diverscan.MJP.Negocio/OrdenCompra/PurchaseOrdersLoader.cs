using Diverscan.MJP.AccesoDatos.OrdenCompra;
using Diverscan.MJP.Entidades.OrdenCompra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.OrdenCompra
{
    public class PurchaseOrdersLoader
    {
        public static List<e_OrdenCompra> GetPurchaseOrderDetails(string PurchaseOrderId)
        {
            IPurchaseOrders purchaseOrders = new PurchaseOrdersImpl();
            return purchaseOrders.GetPurchaseOrderDetails(PurchaseOrderId);
        }
    }
}
