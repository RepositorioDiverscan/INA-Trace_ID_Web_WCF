using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diverscan.MJP.Entidades.Devolutions.DevolucionHeader
{
    public class ResultGetDevolutionHeader : ResultWS
    {       
        private List<EDevolutionsHeader> eListHeader;   
        public List<EDevolutionsHeader> EListHeader { get => eListHeader; set => eListHeader = value; }

    }
}