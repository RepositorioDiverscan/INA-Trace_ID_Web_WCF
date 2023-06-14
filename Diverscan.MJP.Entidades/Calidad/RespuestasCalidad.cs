using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Calidad
{
    [Serializable]
    public class RespuestasCalidad
    {
        string _pregunta = "";
        string _respuesta = "";
        string _comentario = "";
        string _ordenCompra = "";
        string _comentarioSeparado = "";
        string _usuario = "";
        string _usuarioAutoriza = "";

        public RespuestasCalidad(string pregunta, string respuesta, string comentario, string ordenCompra, string comentarioSeparado, string usuario, string usuarioAutoriza)
        {
            _pregunta = pregunta;
            _respuesta = respuesta;
            _comentario = comentario;
            _ordenCompra = ordenCompra;
            _comentarioSeparado = comentarioSeparado;
            _usuario = usuario;
            _usuarioAutoriza = usuarioAutoriza;
        }
        public RespuestasCalidad(IDataReader reader)
        {
            _pregunta = reader["Pregunta"].ToString(); ;
            _respuesta = reader["Respuesta"].ToString(); ;
            _comentario = reader["Comentarios"].ToString();
            _ordenCompra = reader["OrdenCompra"].ToString();
            _comentarioSeparado = reader["ComentarioSeparado"].ToString();
            _usuario = reader["Usuario"].ToString();
            _usuarioAutoriza = reader["UsuarioAutoriza"].ToString();
        }

        public string Pregunta
        {
            set { _pregunta = value; }
            get { return _pregunta; }
        }
        public string Respuesta
        {
            set { _respuesta = value; }
            get { return _respuesta; }
        }
        public string Comentario
        {
            set { _comentario = value; }
            get { return _comentario; }
        }
        public string OrdenCompra
        {
            set { _ordenCompra = value; }
            get { return _ordenCompra; }
        }

        public string ComentarioSeparado
        {
            set { _comentarioSeparado = value; }
            get { return _comentarioSeparado; }
        }
        public string Usuario
        {
            set { _usuario = value; }
            get { return _usuario; }
        }
        public string UsuarioAutoriza
        {
            set { _usuarioAutoriza = value; }
            get { return _usuarioAutoriza; }
        }
    }
}