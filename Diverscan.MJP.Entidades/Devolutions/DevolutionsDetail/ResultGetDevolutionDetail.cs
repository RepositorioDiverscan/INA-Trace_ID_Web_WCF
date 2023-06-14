using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diverscan.MJP.Entidades.Devolutions.DevolutionsDetail
{
    public class ResultGetDevolutionDetail : ResultWS
    {
        private List<EDevolutionDetail> eListDetails;
        public List<EDevolutionDetail> EListDetails { get => eListDetails; set => eListDetails = value; }
    }
}