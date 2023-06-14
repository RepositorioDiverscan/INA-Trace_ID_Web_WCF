namespace Diverscan.MJP.Entidades.ProcesarSolicitud
{
    public class e_ArticulosPendientesSolicitud
    {

        private string _idInternoArticulo;
        private string _nombreArticulo;
        private string _estadoArticulo;

        public e_ArticulosPendientesSolicitud() { }

        public e_ArticulosPendientesSolicitud(string _idInternoArticulo, string _nombreArticulo, string _estadoArticulo)
        {
            this._idInternoArticulo = _idInternoArticulo;
            this._nombreArticulo = _nombreArticulo;
            this._estadoArticulo = _estadoArticulo;
        }

        public string IdInternoArticulo { get { return _idInternoArticulo; } set { value = _idInternoArticulo; } }
        public string NombreArticulo { get { return _nombreArticulo; } set { value = _nombreArticulo; } }
        public string EstadoArticulo { get { return _estadoArticulo; } set { value = _estadoArticulo; } }
    }
}
