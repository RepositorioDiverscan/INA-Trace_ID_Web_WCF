using Diverscan.MJP.AccesoDatos.MotivoAjusteInventario;
using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.MotivoAjusteInventario
{
    public class MotivoAjusteInventarioLoader
    {
        public static List<MotivoAjusteInventarioRecord> ObtenerRegistros(bool tipoAjuste)
        {
            ConsultaMotivoAjusteInvertario consultasTRA = new ConsultaMotivoAjusteInvertario();
            var mAIList = consultasTRA.GetMotivoAjusteInvertario(tipoAjuste);
            return mAIList;
        }

        public static List<MotivoAjusteInventarioRecord> ObtenerTodosRegistros(bool tipoAjuste)
        {
            ConsultaMotivoAjusteInvertario consultasTRA = new ConsultaMotivoAjusteInvertario();
            var mAIList = consultasTRA.GetAllMotivoAjusteInvertario(tipoAjuste);
            return mAIList;
        }
    }
}
