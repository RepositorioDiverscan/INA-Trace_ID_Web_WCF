using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.MotivoAjusteInventario
{
    public class CentroCostosRegistro
    {
        long _idCentroCostos;
        string _nombre;

        public CentroCostosRegistro(long idCentroCostos, string nombre)
        {
            _idCentroCostos = idCentroCostos;
            _nombre = nombre;
        }
        public long IdCentroCostos
        {
            get { return _idCentroCostos; }
            set { _idCentroCostos = value; }
        }
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
    }
}
