using System;

namespace Diverscan.MJP.Entidades.Reportes.Articulos
{
    [Serializable]
    public class e_ArticuloIdERP
    {
        private string _idArticuloERP;
        private string _nombreArticulo;
        private string _nombreArticuloIDInterno;
        private int? _cantidadArticulosPorIdInterno;

        public string IdArticuloERP 
        {
            get { return _idArticuloERP; }
            set { _idArticuloERP = value; }
        }
        public string NombreArticulo  {
            get { return _nombreArticulo; }
            set { _nombreArticulo = value; }
        }
        public string NombreArticuloIDInterno   {
            get { return _nombreArticuloIDInterno; }
            set { _nombreArticuloIDInterno = value; }
        }
        public int? CantidadArticulosPorIdInterno
        {
            get { return _cantidadArticulosPorIdInterno; }
            set { _cantidadArticulosPorIdInterno = value; }
        }

        public e_ArticuloIdERP(string idArticuloERP, string nombreArticulo, string nombreArticuloIDInterno, int? cantidadArticulosPorIdInterno)
        {
            IdArticuloERP = idArticuloERP;
            NombreArticulo = nombreArticulo;
            NombreArticuloIDInterno = nombreArticuloIDInterno;
            CantidadArticulosPorIdInterno = cantidadArticulosPorIdInterno;
        }
        public e_ArticuloIdERP() { }        
    }
}
