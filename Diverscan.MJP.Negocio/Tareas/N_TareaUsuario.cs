using Diverscan.MJP.AccesoDatos.Tareas;
using Diverscan.MJP.Entidades.Tareas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Tareas
{
    public class N_TareaUsuario
    {
        public static void AgregarTareaUsuario(TareaUsuarioRecord tareaUsuario)
        {
            TareaUsuarioDBA tareaUsuarioDBA = new TareaUsuarioDBA();
            tareaUsuarioDBA.AgregarTareaUsuario(tareaUsuario);
        }
    }
}
