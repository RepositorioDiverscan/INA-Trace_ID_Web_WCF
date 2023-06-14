using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.MotivoAjusteInventario
{
     [Serializable]
     [DataContract]
    public class ArticuloRecord :IdArticuloRecord
    {
        string _codigoInterno;
        string _nombreArticulo;
        string _unidadMedida;
        bool _esGranel;

        public ArticuloRecord()
        {
        }

        public ArticuloRecord(long idArticulo,string codigoInterno, string nombreArticulo,
        string unidadMedida, bool esGranel)
        {
            IdArticulo = idArticulo;
            _codigoInterno = codigoInterno;
            _nombreArticulo = nombreArticulo;
            _unidadMedida = unidadMedida;
            _esGranel = esGranel;
        }

        [DataMember]
        public string CodigoInterno
        {
            get { return _codigoInterno; }
            set { _codigoInterno = value; }
        }
        [DataMember]
        public string NombreArticulo
        {
            get { return _nombreArticulo; }
            set { _nombreArticulo = value; }
        }
        [DataMember]
        public string UnidadMedida
        {
            get { return _unidadMedida; }
            set { _unidadMedida = value; }
        }

        [DataMember]
        public bool EsGranel
        {
            get { return _esGranel; }
            set { _esGranel = value; }
        }
    }
}
