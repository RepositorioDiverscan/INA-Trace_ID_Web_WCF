const vueTraslado = new Vue({

    el: '#solicitudTraslado',


    data: {
        mensaje: '',
        txt_NumeroTrans: '',
        txt_cantidadTB: '',
        solicitudTraslado: '',
        idSolicitudTraslado: '',
        txt_cantidadAct: '',
        listaArticulosBodega: [],
        listaArticulosBodegaEspecifica: [],
        articuloSolicitar: [],
        articuloActualizar: [],
        listaEncabezadosSolicitudes: [],
        listaDetalleSolicitudes: [],
        
    },


    methods: {

        //Metodo para obtener los artículos
        obtenerArticulosBodega() {
            var self = this;
            $.post('SolicitudTrasladoAjax.aspx', {
                Opcion: 'ObtenerAriculosBodega',
            }, function (data) {
                self.listaArticulosBodega = JSON.parse(data);

                //Crear la tabla de JQuery con los datos asignados
                $(document).ready(function () {
                    $('#tablaArticulosBodega').DataTable({
                        destroy: true,
                        language: {
                            url: 'https://cdn.datatables.net/plug-ins/1.12.1/i18n/es-ES.json',
                            data: self.listaAriculosBodega
                        }
                    });
                });
            });
        },


        //Metodo para obtener los artículos de una bodega en especifico
        obtenerArticulosBodegaEspecifica(IdBodegaDestino, IdSolicitudTrasladoBodega) {

            this.idSolicitudTraslado = IdSolicitudTrasladoBodega

            var self = this;
            $.post('SolicitudTrasladoAjax.aspx', {
                Opcion: 'ObtenerAriculosBodegaEspecifica',
                IdBodega: IdBodegaDestino
            }, function (data) {
                self.listaArticulosBodegaEspecifica = JSON.parse(data);

                //Crear la tabla de JQuery con los datos asignados
                $(document).ready(function () {
                    $('#tablaArticulosBodegaEspecifica').DataTable({
                        destroy: true,
                        language: {
                            url: 'https://cdn.datatables.net/plug-ins/1.12.1/i18n/es-ES.json',
                            data: self.listaArticulosBodegaEspecifica
                        }
                    });
                });

                $('#ModalArticulosBodegaEspecifica').modal('show');
            });
        },


        //Método para mostrar la cantidad a solicitar
        mostrarModalCantidadSolicitar(solicitud) {
            this.articuloSolicitar = solicitud
            $('#ModalCantidadSolicitar').modal('show');
        },


        //Método para mostrar la cantidad a actualizar
        mostrarModalCantidadAct(articuloBodega){
            this.articuloActualizar = articuloBodega
            $('#ModalCantidadActualizar').modal('show');
        },

        //Método para crear una solicitud a las bodegas
        crearSolicitudTrasladoBodega() {
            var self = this;

            $.post('SolicitudTrasladoAjax.aspx', {
                Opcion: 'CrearSolicitudTraslado',
                IdBodegaDestino: self.articuloSolicitar.IdBodega,
                IdArticulo: self.articuloSolicitar.IdArticulo,
                IdInterno: self.articuloSolicitar.IdInterno,
                NumeroTransaccion: self.txt_NumeroTrans,
                CantidadSolicitada: self.txt_cantidadTB,
            }, function (data) {
                $('#ModalCantidadSolicitar').modal('hide');
                self.txt_NumeroTrans = ''
                self.txt_cantidadTB =  ''
                self.mostrarMensaje(data);
                self.obtenerEncabezadosSolicitudes()
            });
        },


        //Método para crear una solicitud a las bodegas
        actualizarSolicitudTrasladoBodega() {
            var self = this;

            $.post('SolicitudTrasladoAjax.aspx', {
                Opcion: 'ActualizarSolicitudTraslado',
                IdSolicitudTraslado: self.idSolicitudTraslado,
                IdArticulo: self.articuloActualizar.IdArticulo,
                NombreArticulo: self.articuloActualizar.Articulo,
                IdInterno: self.articuloActualizar.IdInterno,
                CantidadSolicitada: self.txt_cantidadAct,
            }, function (data) {
                $('#ModalCantidadActualizar').modal('hide');
                $('#ModalArticulosBodegaEspecifica').modal('hide');
                self.txt_NumeroTrans = ''
                self.txt_cantidadAct = ''
                self.mostrarMensaje(data);
                self.obtenerEncabezadosSolicitudes()
            });
        },


        //Método para eliminar una solicitud a las bodegas
        eliminarSolicitud(idSolicitudTraslado) {
            var self = this;
            $.post('SolicitudTrasladoAjax.aspx', {
                Opcion: 'EliminarSolicitudTraslado',
                IdSolicitudTraslado: idSolicitudTraslado
            }, function (data) {
                self.mostrarMensaje(data);
                self.obtenerEncabezadosSolicitudes()
            });
        },


        //Metodo para obtener los encabezados de las solicitdes
        obtenerEncabezadosSolicitudes() {
            var self = this;
            $.post('SolicitudTrasladoAjax.aspx', {
                Opcion: 'ObtenerEncabezadosSolicitudTraslado',
            }, function (data) {
                self.listaEncabezadosSolicitudes = JSON.parse(data);
            });
        },


        //Metodo para obtener los detalles de la solicitud que se escoga
        obtenerDetalleSolicitudes(idSolicitudTraslado) {
            this.solicitudTraslado = idSolicitudTraslado

            var self = this;
            $.post('SolicitudTrasladoAjax.aspx', {
                Opcion: 'ObtenerDetallesSolicitudTraslado',
                IdSolicitudTraslado: idSolicitudTraslado
            }, function (data) {
                self.listaDetalleSolicitudes = JSON.parse(data);
            });

            $('#ModalDetalleSolicitud').modal('show');
        },


        //Metodo para mostar un mensaje
        mostrarMensaje(mensaje) {
            this.mensaje = mensaje
            $('#ModalMensaje').modal('show');
        },


        //Método para ingresar solo numeros
        validaNumeros(event) {
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


    mounted: function () {
        this.obtenerEncabezadosSolicitudes()
        this.obtenerArticulosBodega()
    }
})