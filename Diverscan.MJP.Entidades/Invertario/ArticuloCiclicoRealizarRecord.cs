using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.Invertario
{
    [DataContract]
    [Serializable]
    public class ArticuloCiclicoRealizarRecord : MaestroArticuloRecord
    {
        long _idArticulosCiclicoRealizar;
        public ArticuloCiclicoRealizarRecord(long idArticulo, string nombre, long idArticulosCiclicoRealizar)
            :base (idArticulo,nombre)
        {            
            _idArticulosCiclicoRealizar =idArticulosCiclicoRealizar;
        }

        [DataMember]
        public long IdArticulosCiclicoRealizar
        {
            get { return _idArticulosCiclicoRealizar; }
            set { _idArticulosCiclicoRealizar = value; }
        }
    }
}
