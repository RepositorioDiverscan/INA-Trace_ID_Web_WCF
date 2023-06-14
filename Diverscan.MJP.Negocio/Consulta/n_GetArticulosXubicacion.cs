using Diverscan.MJP.AccesoDatos.Consultas;
using Diverscan.MJP.Entidades.Consultas;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Consulta
{
    public class n_GetArticulosXubicacion
    {

        DGetArticuloXubicacion getArtXubi;
        private IFileExceptionWriter _fileException;

        public n_GetArticulosXubicacion(IFileExceptionWriter fileException)
        {
            this._fileException = fileException;
            getArtXubi = new DGetArticuloXubicacion();
        }

        public List<EGetArticulosXubicacion> GetArticulosXubicacion(string ubi)
        {
            return getArtXubi.GetOnePurchaseOrder(ubi);
        }


    }
}
