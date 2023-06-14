using Diverscan.MJP.AccesoDatos.CategoriaArticulo;
using Diverscan.MJP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Inventario
{
    public class N_CategoriaArticulo
    {
        public static void InsertCategoriaArticulo(e_CategoriaArticulo e_categoriaArticulo)
        {
            CategoriaArticuloDBA categoriaArticuloDBA = new CategoriaArticuloDBA();
            categoriaArticuloDBA.InsertCategoriaArticulo(e_categoriaArticulo);
        }

        public static List<e_CategoriaArticulo> GetCategoriaArticulo()
        {
            CategoriaArticuloDBA categoriaArticuloDBA = new CategoriaArticuloDBA();
            return categoriaArticuloDBA.GetCategoriaArticulo();
        }

        public static void UpdateCategoriaArticulo(e_CategoriaArticulo e_categoriaArticulo)
        {
            CategoriaArticuloDBA categoriaArticuloDBA = new CategoriaArticuloDBA();
            categoriaArticuloDBA.UpdateCategoriaArticulo(e_categoriaArticulo);
        }
    }
}
