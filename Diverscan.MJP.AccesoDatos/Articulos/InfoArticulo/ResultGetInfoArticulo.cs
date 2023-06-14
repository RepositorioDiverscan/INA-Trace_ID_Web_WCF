using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Articulos.InfoArticulo
{
    public class ResultGetInfoArticulo : ResultWS
    {
        private ArticuloBaseInfo _articuloBaseInfo;

        public ArticuloBaseInfo ArticuloBaseInfo { get => _articuloBaseInfo; set => _articuloBaseInfo = value; }
    }
}
