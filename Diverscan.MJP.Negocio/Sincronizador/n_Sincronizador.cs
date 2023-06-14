using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.AccesoDatos.Sincronizador;
using Diverscan.MJP.Entidades.Rol;
using Diverscan.MJP.Entidades.Usuarios;
using Diverscan.MJP.Utilidades;
using System.Configuration;
using Diverscan.MJP.Entidades.Pedidos;

namespace Diverscan.MJP.Negocio.Sincronizador
{
    public class n_Sincronizador
    {
        private IFileExceptionWriter _fileExceptionWriter;
        private da_Sincronizador Da_Sincronizador;

        public n_Sincronizador(IFileExceptionWriter fileExceptionWriter)
        {
            _fileExceptionWriter = fileExceptionWriter;
            Da_Sincronizador = new da_Sincronizador();
        }
        public ResultadoObtenerUsuarios ObtenerUsuarios(string idbodega)
        {
            ResultadoObtenerUsuarios resultado = new ResultadoObtenerUsuarios();
            try
            {
                resultado.usuarios = Da_Sincronizador.ObtenerUsuarios(idbodega);
                resultado.Description = "Succesful";
                resultado.state = true;
                
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SINCRONIZADORFILEPATHEXCEPTION);
                resultado.Description = ex.Message;
                resultado.state = false;
                
            }
            return resultado;

        }

        public ResultadoObtenerRoles ObtenerRoles(string servicePass)
        {
            ResultadoObtenerRoles resultado = new ResultadoObtenerRoles();
            var passService = ConfigurationManager.AppSettings["servicepass"];

            if (servicePass.Equals(passService))
            {
                try
                {
                    resultado.rolesHH = Da_Sincronizador.ObtenerRoles();
                    resultado.Description = "Succesful";
                    resultado.state = true;

                }
                catch (Exception ex)
                {
                    _fileExceptionWriter.WriteException(ex, PathFileConfig.SINCRONIZADORFILEPATHEXCEPTION);
                    resultado.Description = ex.Message;
                    resultado.state = false;

                }
            }
            else
            {
                resultado.Description = "servicepass no coincide";
                resultado.state = false;
            }

            return resultado;
            
        }

        public ResultadoObtenerPedidos ObtenerPedidos(string idbodega)
        {
            ResultadoObtenerPedidos resultado = new ResultadoObtenerPedidos();
            try
            {
                resultado.pedidosSincronizador = Da_Sincronizador.ObtenerPedidos(idbodega);
                resultado.Description = "Succesful";
                resultado.state = true;

            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SINCRONIZADORFILEPATHEXCEPTION);
                resultado.Description = ex.Message;
                resultado.state = false;

            }
            return resultado;

        }

        public ResultadoObtenerDetallePedido ObtenerDetallesPedidos(string idbodega)
        {
            ResultadoObtenerDetallePedido resultado = new ResultadoObtenerDetallePedido();
            try
            {
                resultado.detallesPedidosSincronizador = Da_Sincronizador.ObtenerDetallesPedidos(idbodega);
                resultado.Description = "Succesful";
                resultado.state = true;

            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SINCRONIZADORFILEPATHEXCEPTION);
                resultado.Description = ex.Message;
                resultado.state = false;

            }
            return resultado;

        }

        public ResultadoIngresarPedidosRecibidos IngresarPedidosRecibidos(List<e_PedidoRecibido> lista_pedidos)
        {

            ResultadoIngresarPedidosRecibidos resultado = new ResultadoIngresarPedidosRecibidos();
            try
            {
                List<string> listaRespuesta = new List<string>();

                foreach (e_PedidoRecibido e_Pedido in lista_pedidos)
                {
                    string respuestaP = Da_Sincronizador.IngresarPedidosRecibidos(e_Pedido);
                    bool pedidoIngresado = true;

                    if (respuestaP.Equals("Ingresado"))
                    {
                        foreach(e_DetalleRecibido e_Detalle in e_Pedido.DetallesRecibidos)
                        {
                            string respuestaD = Da_Sincronizador.IngresarDetallesRecibidos(e_Detalle, e_Pedido.IdPedido);

                            if (!respuestaD.Equals("Ingresado"))
                            {
                                pedidoIngresado = false;

                                listaRespuesta.Add(respuestaD);

                                string respuestaR = Da_Sincronizador.RevertirPedidoRecibido(e_Pedido.IdPedido);
                                break;
                            }
                        }
                        if (pedidoIngresado)
                        {
                            listaRespuesta.Add(e_Pedido.IdPedido.ToString());
                        }
                    }
                    else
                    {
                        listaRespuesta.Add(respuestaP);
                    }
                }

                resultado.listaPedidosRecibidos = listaRespuesta;
                resultado.Description = "Succesful";
                resultado.state = true;

            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SINCRONIZADORFILEPATHEXCEPTION);
                resultado.Description = ex.Message;
                resultado.state = false;

            }
            return resultado;
        }
    }
}
