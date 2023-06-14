using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Operacion.TrasladoBodegas
{
    public class EListaArticulos
    {
        //Atributos de la clase
        private string _idInternoArticulo;
        private int _cantidad;

        //Constructor
        public EListaArticulos(string idInternoArticulo, int cantidad)
        {
            _idInternoArticulo = idInternoArticulo;
            _cantidad = cantidad;
        }

        //Métodos Getters and Setters
        public string IdInternoArticulo { get => _idInternoArticulo; set => _idInternoArticulo = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }
    }
}
