using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Usuarios
{
    [Serializable]
    public class e_Usuarios
    {
        int _IdUsuario;
        string _Nombre;
        string _IdCompania;
        string _Usuario;
        string _Contrasena;
        long _IdRol;
        string _Email;
        string _Comentario;
        bool _EstaBloqueado;
        string _NombrePila;
        string _ApellidosPila;
        decimal _HorasProductivas;
        private int _idBodega;
        private int _idSector;

        public e_Usuarios()
        {

        }
        public e_Usuarios(int IdUsuario,string Nombre,string IdCompania, string Usuario, string Contrasena,long IdRol, string Email,string Comentario, bool EstaBloqueado,string NombrePila,string ApellidosPila,decimal HorasProductivas) 
        {
            _IdUsuario = IdUsuario;
            _Nombre = Nombre;
            _IdCompania = IdCompania;
            _Usuario = Usuario;
            _Contrasena = Contrasena;
            _IdRol = IdRol;
            _Email = Email;
            _Comentario = Comentario;
            _EstaBloqueado = EstaBloqueado;
            _NombrePila = NombrePila;
            _ApellidosPila = ApellidosPila;
            _HorasProductivas = HorasProductivas;
        }

        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string IdCompania
        {
            get { return _IdCompania; }
            set { _IdCompania = value; }
        }

        public string Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }

        public string Contrasena
        {
            get { return _Contrasena; }
            set { _Contrasena = value; }
        }

        public long IdRol
        {
            get { return _IdRol; }
            set { _IdRol = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string Comentario
        {
            get { return _Comentario; }
            set { _Comentario = value; }
        }

        public bool EstaBloqueado
        {
            get { return _EstaBloqueado; }
            set { _EstaBloqueado = value; }
        }

        public string NombrePila
        {
            get { return _NombrePila; }
            set { _NombrePila = value; }
        }

        public string ApellidosPila
        {
            get { return _ApellidosPila; }
            set { _ApellidosPila = value; }
        }

        public decimal HorasProductivas
        {
            get { return _HorasProductivas; }
            set { _HorasProductivas = value; }
        }

        public int IdBodega
        {
            get => _idBodega;
            set => _idBodega = value;
        }
        public int IdSector
        {
            get => _idSector;
            set => _idSector = value;
        }
    }
}
