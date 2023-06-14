using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.Invertario
{
    [DataContract]
    [Serializable]
    public class MaestroArticuloRecord
    {
        long _idArticulo;
        string _nombre;

        public MaestroArticuloRecord(long idArticulo,string nombre)
        {
            _idArticulo = idArticulo;
            _nombre = nombre;
        }
         [DataMember]
        public long IdArticulo
        {
            get { return _idArticulo; }
            set { _idArticulo = value; }
        }
         [DataMember]
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
    }
}
