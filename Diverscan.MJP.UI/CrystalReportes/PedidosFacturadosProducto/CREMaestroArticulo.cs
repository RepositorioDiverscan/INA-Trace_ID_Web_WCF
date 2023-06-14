using Diverscan.MJP.Entidades.MaestroArticulo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diverscan.MJP.UI.CrystalReportes.PedidosFacturadosProducto
{
    public class CREMaestroArticulo : e_MaestroArticulo
    {
        private string _lote;
        private string _fechaExp;

        public string Lote { get => _lote; set => _lote = value; }
        public string FechaExp { get => _fechaExp; set => _fechaExp = value; }
    }
}