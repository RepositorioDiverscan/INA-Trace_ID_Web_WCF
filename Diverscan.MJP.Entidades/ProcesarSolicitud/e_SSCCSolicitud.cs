namespace Diverscan.MJP.Entidades.ProcesarSolicitud
{
    public class e_SSCCSolicitud
    {
        private string _consecutivoSSCC;
        private string _estadoTraslado;
        private string _estadoDespacho;

        public e_SSCCSolicitud() { }

        public e_SSCCSolicitud(string _consecutivoSSCC, string _estadoTraslado, string _estadoDespacho)
        {
            this._consecutivoSSCC = _consecutivoSSCC;
            this._estadoTraslado = _estadoTraslado;
            this._estadoDespacho = _estadoDespacho;
        }
        public string ConsecutivoSSCC{ get { return _consecutivoSSCC; } set { value = _consecutivoSSCC; } }
        public string EstadoTraslado { get { return _estadoTraslado; } set { value = _estadoTraslado; } }
        public string EstadoDespacho { get { return _estadoDespacho; } set { value = _estadoDespacho; } }
    }
}
