namespace Diverscan.MJP.Entidades.MaestroArticulo
{
    public class e_ArticuloSinUnidadAlistoDefecto
    {
        private string _idERP;
        private long _idArticulo;
        private string _nombreMaestro;
        private string _GTIN13;
        private string _GTIN13Activo;
        private string _empaqueMaestro;
        private string _GTIN14;
        private string _GTIN14Activo;
        private string _empaque;

        public e_ArticuloSinUnidadAlistoDefecto() { }

        public e_ArticuloSinUnidadAlistoDefecto(
            string idERP,
            long idArticulo,
            string nombreMaestro,
            string GTIN13,
            string GTIN13Activo,
            string empaqueMaestro,
            string GTIN14,
            string GTIN14Activo,
            string empaque)
        {
            _idERP          = idERP;
            _idArticulo     = idArticulo;
            _nombreMaestro  = nombreMaestro;
            _GTIN13         = GTIN13;
            _GTIN13Activo   = GTIN13Activo;
            _empaqueMaestro = empaqueMaestro;
            _GTIN14         = GTIN14;
            _GTIN14Activo   = GTIN14Activo;
            _empaque        = empaque;            
        }

        public string IdERP             { get { return _idERP; } set { _idERP = value; } }
        public long IdArticulo          { get { return _idArticulo; } set { _idArticulo = value; } }
        public string NombreMaestro     { get { return _nombreMaestro; } set { _nombreMaestro = value; } }
        public string GTIN13            { get { return _GTIN13; } set { _GTIN13 = value; } }
        public string GTIN13Activo      { get { return _GTIN13Activo; } set { _GTIN13Activo = value; } }
        public string EmpaqueMaestro    { get {return _empaqueMaestro; } set {_empaqueMaestro = value; } }
        public string GTIN14            { get { return _GTIN14; } set { _GTIN14 = value; } }
        public string GTIN14Activo      { get { return _GTIN14Activo; } set { _GTIN14Activo = value; } }
        public string Empaque           { get { return _empaque; } set { _empaque = value; } }
    }
}
