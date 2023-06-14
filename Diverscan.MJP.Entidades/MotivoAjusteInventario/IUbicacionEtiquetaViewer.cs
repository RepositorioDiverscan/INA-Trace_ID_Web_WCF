using Diverscan.MJP.Entidades.Common;
using Diverscan.MJP.Entidades.CustomEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.MotivoAjusteInventario
{
    public interface IUbicacionEtiquetaViewer : IMessageUser
    {
        void SetIdUbicacion(long idUbicacion);
        event EventHandler UbicacionEtiqueta;

    }
}
