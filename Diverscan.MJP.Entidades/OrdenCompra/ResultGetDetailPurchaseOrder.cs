using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.OrdenCompra
{
    public class ResultGetDetailPurchaseOrder : ResultWS
    {
        private List<EDetailPurchaseOrder> detailList;

        public List<EDetailPurchaseOrder> DetailList { get => detailList; set => detailList = value; }
    }
}
