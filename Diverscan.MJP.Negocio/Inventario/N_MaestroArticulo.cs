using Diverscan.MJP.AccesoDatos.Inventario;
using Diverscan.MJP.AccesoDatos.MaestroArticulo;
using Diverscan.MJP.Entidades.GTIN14VariableLogistic;
using Diverscan.MJP.Entidades.Invertario;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Inventario
{
    public class N_MaestroArticulo
    {
        private IFileExceptionWriter _fileExceptionWriter;
        private da_MaestroArticulo articuloData;

        public N_MaestroArticulo(IFileExceptionWriter fileExceptionWriter)
        {
            this._fileExceptionWriter = fileExceptionWriter;
            articuloData = new da_MaestroArticulo();
        }

        public static List<MaestroArticuloRecord> GetArticulosInventarioCiclico(int idCategoriaArticulo)
        {
            MaestroArticuloDBA maestroArticuloDBA = new MaestroArticuloDBA();
            return maestroArticuloDBA.GetArticulosInventarioCiclicos(idCategoriaArticulo);
        }
    }
}
