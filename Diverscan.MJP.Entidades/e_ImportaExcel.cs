using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{
    public class e_ImportaExcel
    {
        public string CodInterno { get; set; }
        public string NomFamilia { get; set; }
        public string Clase { get; set; }
        public string SubClase { get; set; }
        public string Mercancia { get; set; }
        public string CategotiaMJP { get; set; }
        public string DescripcionCorta { get; set; }
        public string DescripcionLarga { get; set; }

        public string UnidadEmpaque { get; set; }
        public string TipoEmpaque { get; set; }
        public string UnidadMedida { get; set; }


        public string Marca { get; set; }
        public string Talla { get; set; }
        public string Color { get; set; }
        public string Alto { get; set; }
        public string CalifUnidMedioAltoArt { get; set; }
        public string Ancho { get; set; }
        public string CalifUnidMedioAnchoArt { get; set; }
        public string Profundidad { get; set; }
        public string CalifUnidMedioProfArt { get; set; }
        public string PesoNeto { get; set; }
        public string CalifUnidMedioPesoNeto { get; set; }
        public string PrecioNeto { get; set; }
        public string Temperatura { get; set; }
        public string Sabor { get; set; }
        public string Fragancia { get; set; }
        public string CondicionesManipulacion { get; set; }
        public string MedidaUso { get; set; }
        public string TipoMaterial { get; set; }
        public string Programa { get; set; }

    }
}
