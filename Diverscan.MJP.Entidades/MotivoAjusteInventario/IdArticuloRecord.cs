using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.MotivoAjusteInventario
{
     [Serializable]
    [DataContract]
    public class IdArticuloRecord
    {
        long _idArticulo;

        [DataMember]
        public long IdArticulo
        {
            get { return _idArticulo; }
            set { _idArticulo = value; }
        }
    }
}
