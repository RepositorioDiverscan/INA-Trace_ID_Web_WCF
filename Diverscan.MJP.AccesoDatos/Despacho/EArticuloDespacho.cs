using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Despacho
{
    [Serializable]
    public class EArticuloDespacho
    {
        private long _idArticulo;
        private string _idInternoArticulo;
        private string _nombreArticulo;
        private int _cantidadSolicitada;
        private int _cantidadDespachada;
        private int _diferencia;

        public EArticuloDespacho()
        {
        }
        public EArticuloDespacho(IDataReader reader)
        {
            _idArticulo = long.Parse(reader["idArticulo"].ToString());
            _idInternoArticulo = reader["idInterno"].ToString();
            _nombreArticulo = reader["Nombre"].ToString();
            _cantidadSolicitada = Convert.ToInt32(reader["CantidadSolicitada"].ToString());
            _cantidadDespachada = Convert.ToInt32(reader["CantidadAlistada"].ToString());
            _diferencia = Convert.ToInt32(reader["Diferencia"].ToString());
        }

        public long IdArticulo { get => _idArticulo; set => _idArticulo = value; }
        public string NombreArticulo { get => _nombreArticulo; set => _nombreArticulo = value; }
        public int CantidadSolicitada { get => _cantidadSolicitada; set => _cantidadSolicitada = value; }
        public int CantidadDespachada { get => _cantidadDespachada; set => _cantidadDespachada = value; }
        public int Diferencia { get => _diferencia; set => _diferencia = value; }
        public string IdInternoArticulo { get => _idInternoArticulo; set => _idInternoArticulo = value; }
    }
}
