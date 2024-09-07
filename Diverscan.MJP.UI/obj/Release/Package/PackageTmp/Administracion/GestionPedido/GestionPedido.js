const gestionPedido = new Vue({
    el: '#maincontext',

    data: {        
        pedidosEncabezados: [],
        pedidosDetalle: [],

        bodegasSolicitudes: [],
        listaEncabezadosSolicitudBodega: [],

        listaEncabezadosSolicitudes: [],
        listaDetalleSolicitudes: [],

        listaEncabezadosCC: [],
        listaDetalleCC: [],

        listaEncabezadosSolicitudAlisto: [],
        listaDetalleSolicitudAlisto: [],

        mensaje: '',
        idInternoSolicitud: '',
        pedidosDetalleSeleccionado: '',
        idCajaChica: '',
        idMaestroSolicitud: '',

        txt_cantidadAlisto: 0,
        pedidoSolicitudAlisto: [],

        txt_cantidadTB: 0,

        txt_cantidadCJ: 0,
        pedidoCajaChica: []
    },

   
    methods: {

        /* MÉTODOS DE PEDIDOS */

        //Metodo para obtener una lista de encabezados de los pedidos
        ObtenerEncabezadosPedidos: function () {
            var self = this;
            $.post('GestionPedidoAjax.aspx', {
                Opcion: 'ConsultaPedidoOriginalEncabezado'
            }, function (data) {
               self.pedidosEncabezados = JSON.parse(data);
            });
        },

        //Metodo para mostrar el detalle de un encabezado
        ObtenerDetallePedidos: function (pedido) {
            //Obtener en una variable el ID del Pedido original
            this.idInternoSolicitud = pedido.Id;
            this.pedidoOriginal = pedido;

            //Invocar un ajax para mostrar el detalle del pedido
            var self = this;
            $.post('GestionPedidoAjax.aspx', {
                Opcion: 'ObtenerPedidoOriginalDetalle',
                IdPedidoOriginal: pedido.Id
            }, function (data) {
                    self.pedidosDetalle = JSON.parse(data);
            });

            document.getElementById("tablaDetallePedidos").style.display = "block";
        },

        /* FIN MÉTODOS DE PEDIDOS */


        /* MÉTODOS DE ALISTO */

        //Metodo para mostrar los encabezados de Solicitud Alisto
        MostrarEncabezadosSolicitudAlisto() {
            var self = this;
            $.post('GestionPedidoAjax.aspx', {
                Opcion: 'ObtenerEncabezadosSolicitudAlisto'
            }, function (data) {
                self.listaEncabezadosSolicitudAlisto = JSON.parse(data);
            });
        },


        //Metodo para mostrar los detalles de Solicitud Alisto
        MostrarDetalleSolicitudAlisto(idMaestroSolicitud) {
            this.idMaestroSolicitud = idMaestroSolicitud;

            var self = this;
            $.post('GestionPedidoAjax.aspx', {
                Opcion: 'ObtenerDetallesSolicitudAlisto',
                IdMaestroSolicitud: idMaestroSolicitud
            }, function (data) {
                self.listaDetalleSolicitudAlisto = JSON.parse(data);
            });

            $('#ModalDetalleSolicitudesAlisto').modal('show');
        },




        //Metodo para ingresar una solicitud de alisto
        IngresarSolicitudAlisto() {
            var self = this;
            $.post('GestionPedidoAjax.aspx', {
                Opcion: 'InsertarSolicitudAlisto',
                IdPedidoOriginal: self.pedidoSolicitudAlisto.IdPedidoOriginal,
                IdArticulo: self.pedidoSolicitudAlisto.IdArticulo,
                CantidadAlisto: self.txt_cantidadAlisto
            }, function (data) {
                document.getElementById("tablaDetallePedidos").style.display = "none";
                self.MostrarEncabezadosSolicitudAlisto();
                self.mostrarMensaje(data);
                self.txt_cantidadAlisto = ''
                $('#ModalCantidadAlisto').modal('hide');
            });
        },


        //Metodo para eliminar un articulo para Solicitudes de Alisto
        EliminarArticuloSolicitudAlisto(alistoDetalle) {
            var self = this;
            $.post('GestionPedidoAjax.aspx', {
                Opcion: 'EliminarArticuloSolicitudAlisto',
                IdMaestroSolicitud: self.idMaestroSolicitud,
                IdDetalle: alistoDetalle.IdDetalle
            }, function (data) {
                if (data == "Todos los articulos han sido eliminados, se elimina su encabezado") {
                    $('#ModalDetalleSolicitudesAlisto').modal('hide');
                }
                self.mostrarMensaje(data);
                self.MostrarEncabezadosSolicitudAlisto();
                self.MostrarDetalleSolicitudAlisto(self.idMaestroSolicitud)
                document.getElementById("tablaDetallePedidos").style.display = "none";
            });
        },


        //Metodo para eliminar una solicitud de alisto por completo
        EliminarSolicitudAlisto(IdMaestroSolicitud) {
            Swal.fire({
                title: '¿Está seguro de eliminar esta solicitud de alisto por completo?',
                icon: 'question',
                cancelButtonText: 'Cancelar',
                showCancelButton: true,
                confirmButtonColor: '#DC3545',
                cancelButtonColor: '#6C757D',
                confirmButtonText: 'Eliminar'
            }).then((result) => {
                if (result.isConfirmed) {
                    var self = this;
                    $.post('GestionPedidoAjax.aspx', {
                        Opcion: 'EliminarSolicitudAlisto',
                        IdMaestroSolicitud: IdMaestroSolicitud
                    }, function (data) {
                        self.mostrarMensaje(data);
                        self.MostrarEncabezadosSolicitudAlisto();
                        document.getElementById("tablaDetallePedidos").style.display = "none";
                    });
                }
            })
        },

        //Metodo para mostrar la cantidad a alistar
        MostrarCantidadAlistar(pedido) {
            this.pedidoSolicitudAlisto = pedido;
            $('#ModalCantidadAlisto').modal('show');
        },



        /* FIN MÉTODOS DE ALISTO */



        //Metodo para mostar el modal de Solicitud de Bodegas
        MostrarBodegas(pedidosDetalle) {

            this.pedidosDetalleSeleccionado = pedidosDetalle;

            //Invocar un ajax para mostrar las bodegas que tengan cantidad disponible de un articulo
            var self = this;
            $.post('GestionPedidoAjax.aspx', {
                Opcion: 'MostrarBodegasSolicitudes',
                IdArticulo: pedidosDetalle.IdArticulo
            }, function (data) {
                self.bodegasSolicitudes = JSON.parse(data);
                $('#ModalBodegas').modal('show'); //Mostrar el modal
            });
        },


        //Metodo para agregar a una lista de articulos a una solicitud
        AgregarArticuloSolicitud(solicitud) {
            //Cerrar el modal
            $('#ModalBodegas').modal('hide');

            //Agregar una nueva solicitud
            var self = this;
            $.post('GestionPedidoAjax.aspx', {
                Opcion: 'IngresarSolicitud',
                IdBodegaDestino: solicitud.IdBodega,
                IdArticulo: solicitud.IdArticulo,
                CantidadSolicitada: self.txt_cantidadTB,
                IdPedidoOriginal: self.idInternoSolicitud
            }, function (data) {
                self.mostrarMensaje(data);
                self.obtenerEncabezadosSolicitudes();
                document.getElementById("tablaDetallePedidos").style.display = "none";
            });
        },


        //Metodo para mostrar el modal de mensaje 
        mostrarMensaje(mensaje) {
            this.mensaje = mensaje;
            $('#ModalMensaje').modal('show');
        },


        //Metodo para obtener los encabezados de las solicitdes
        obtenerEncabezadosSolicitudes() {
            var self = this;
            $.post('GestionPedidoAjax.aspx', {
                Opcion: 'ObtenerEncabezadosSolicitudTraslado',
            }, function (data) {
                self.listaEncabezadosSolicitudes = JSON.parse(data);;
            });
        },


        //Metodo para obtener los detalles de la solicitud que se escoga
        obtenerDetalleSolicitudes(idSolicitudTraslado) {
            var self = this;
            $.post('GestionPedidoAjax.aspx', {
                Opcion: 'ObtenerDetallesSolicitudTraslado',
                IdSolicitudTraslado: idSolicitudTraslado
            }, function (data) {
                self.listaDetalleSolicitudes = JSON.parse(data);
            });

            $('#ModalDetalleSolicitudes').modal('show');
            document.getElementById("tablaDetallePedidos").style.display = "none";
        },


        //Metodo para eliminar una solicitud de traslado
        eliminarSolicitudTraslado(idSolicitudTraslado) {
            Swal.fire({
                title: '¿Está seguro de eliminar esta Solicitud de Traslado de Bodega?',
                icon: 'question',
                cancelButtonText: 'Cancelar',
                showCancelButton: true,
                confirmButtonColor: '#FFC107',
                cancelButtonColor: '#6C757D',
                confirmButtonText: 'Eliminar'
            }).then((result) => {
                if (result.isConfirmed) {
                    var self = this;
                    $.post('GestionPedidoAjax.aspx', {
                        Opcion: 'EliminarSolicitudTraslado',
                        IdSolicitudTraslado: idSolicitudTraslado
                    }, function (data) {
                        self.mostrarMensaje(data);
                        self.obtenerEncabezadosSolicitudes();
                        document.getElementById("tablaDetallePedidos").style.display = "none";
                    });
                }
            })       
        },


        //Metodo para mostrar la cantidad a comprar en Caja Chica
        MostrarCantidadCajaChica(pedido) {
            this.pedidoCajaChica = pedido;
            $('#ModalCantidadCJ').modal('show');
        },


        //Metodo para ingresar a caja chica
        ComprarCajaChica() {
            var self = this;
            $.post('GestionPedidoAjax.aspx', {
                Opcion: 'InsertarCajaChica',
                idPedidoOrinalCJ: self.pedidoCajaChica.IdPedidoOriginal,
                idArticuloCJ: self.pedidoCajaChica.IdArticulo,
                cantidadSolicitadaCJ: self.txt_cantidadCJ
            }, function (data) {
                self.MostrarEncabezadosCC();
                self.mostrarMensaje(data);
                self.txt_cantidadCJ = ''
                $('#ModalCantidadCJ').modal('hide');
                document.getElementById("tablaDetallePedidos").style.display = "none";
            });
        },


        //Metodo para mostrar los encabezados de la Caja chica
        MostrarEncabezadosCC() {
            var self = this;
            $.post('GestionPedidoAjax.aspx', {
                Opcion: 'MostrarEncabezadoCC'
            }, function (data) {
                self.listaEncabezadosCC = JSON.parse(data);
            });
        },


        //Metodo para mostrar los detalles de la Caja Chica
        MostrasDetalleCC(IDCajaChica) {
            this.idCajaChica = IDCajaChica;

            var self = this;
            $.post('GestionPedidoAjax.aspx', {
                Opcion: 'MostrarDetalleCC',
                IDCajaChica: IDCajaChica
            }, function (data) {
                self.listaDetalleCC = JSON.parse(data);
            });

            $('#ModalDetalleSolicitudesCC').modal('show');
        },


        //Metodo para eliminar un articulo de la caja chica
        EliminarArticuloCajaChica(idDetalle) {
            var self = this;
            $.post('GestionPedidoAjax.aspx', {
                Opcion: 'EliminarArticuloCC',
                IDCajaChica: self.idCajaChica,
                IdDetalle: idDetalle
            }, function (data) {
                if (data == "Todos los articulos han sido eliminados, se elimina su encabezado") {
                    $('#ModalDetalleSolicitudesCC').modal('hide');
                }
                self.mostrarMensaje(data);
                self.MostrarEncabezadosCC();
                self.MostrasDetalleCC(self.idCajaChica)
                document.getElementById("tablaDetallePedidos").style.display = "none";
            });
        },


        //Metodo para eliminar una caja chica por completo
        EliminarCajaChica(idCajaChica) {
            Swal.fire({
                title: '¿Está seguro de eliminar esta caja chica?',
                icon: 'question',
                cancelButtonText: 'Cancelar',
                showCancelButton: true,
                confirmButtonColor: '#DC3545',
                cancelButtonColor: '#6C757D',
                confirmButtonText: 'Eliminar'
            }).then((result) => {
                if (result.isConfirmed) {
                    var self = this;
                    $.post('GestionPedidoAjax.aspx', {
                        Opcion: 'EliminarCC',
                        IDCajaChica: idCajaChica,
                    }, function (data) {
                        self.mostrarMensaje(data);
                        self.MostrarEncabezadosCC();
                        document.getElementById("tablaDetallePedidos").style.display = "none";
                    });
                }
            })
        },


        //Metodo para procesar una caja chica por completo
        ProcesarCajaChica(idCajaChica) {
            Swal.fire({
                title: '¿Está seguro de mandar a comprar esta caja chica?',
                icon: 'question',
                cancelButtonText: 'Cancelar',
                showCancelButton: true,
                confirmButtonColor: '#28A745',
                cancelButtonColor: '#6C757D',
                confirmButtonText: 'Comprar'
            }).then((result) => {
                if (result.isConfirmed) {
                    var self = this;
                    $.post('GestionPedidoAjax.aspx', {
                        Opcion: 'ProcesarCC',
                        IDCajaChica: idCajaChica,
                    }, function (data) {
                        self.mostrarMensaje(data);
                        self.MostrarEncabezadosCC();
                    });
                }
            })
        },


       


        //Validar que solo pueda ingresar numeros
        ValidarSoloNumeros(event) {
            //Obtener el valor de lo que digita el usuario en la tecla
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);

            //Configurar el regex con las expresiones regulares que desea o que se desean
            regex = /^[0-9]$/

            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        }
    },


    //Metodo del ciclo de vida
    mounted: function () {
        this.ObtenerEncabezadosPedidos();
        this.obtenerEncabezadosSolicitudes();
        this.MostrarEncabezadosCC();
        this.MostrarEncabezadosSolicitudAlisto();
        document.getElementById("tablaDetallePedidos").style.display = "none";
    }
})