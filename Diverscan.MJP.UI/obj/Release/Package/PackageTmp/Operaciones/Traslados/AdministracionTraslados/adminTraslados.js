const vueTraslado = new Vue({

    el: '#adminTraslado',


    data: {
        mensaje: '',
        listaEncabezadosSolicitudes: [],
        listaDetalleSolicitudes: [],
        listaArticulos: []
    },


    methods: {
        //Metodo para obtener los encabezados de las solicitdes
        obtenerEncabezadosSolicitudes() {
            var self = this;
            $.post('adminTrasladosAjax.aspx', {
                Opcion: 'ObtenerEncabezadosSolicitudTraslado',
            }, function (data) {
                self.listaEncabezadosSolicitudes = JSON.parse(data);

                //Crear la tabla de JQuery con los datos asignados
                $(document).ready(function () {
                    $('#tablaEncabezadosSolicitud').DataTable({
                        destroy: true,
                        language: {
                            url: 'https://cdn.datatables.net/plug-ins/1.12.1/i18n/es-ES.json',
                            data: self.listaEncabezadosSolicitudes
                        }
                    });
                });

            });
        },


        //Metodo para obtener los detalles de la solicitud que se escoga
        obtenerDetalleSolicitudes(idSolicitudTraslado) {
            this.solicitudTraslado = idSolicitudTraslado

            var self = this;
            $.post('adminTrasladosAjax.aspx', {
                Opcion: 'ObtenerDetallesSolicitudTraslado',
                IdSolicitudTraslado: idSolicitudTraslado
            }, function (data) {
                self.listaDetalleSolicitudes = JSON.parse(data);
            });

            $('#ModalDetalleSolicitud').modal('show');
        },


        //Método para rechazar una solicitud 
        rechazarSolicitudTraslado(idSolicitudTraslado) {
            var self = this;

            $.post('adminTrasladosAjax.aspx', {
                Opcion: 'RechazarSolicitudTrasladoBodega',
                IdSolicitudTraslado: idSolicitudTraslado
            }, function (data) {
                self.mostrarMensaje(data);
                self.obtenerEncabezadosSolicitudes()
            });
        },


        //Método para aceptar una solicitud 
        aceptarSolicitudTraslado(idSolicitudTraslado) {

            for (i = 0; i < this.listaDetalleSolicitudes.length; i++){
                var Articulos = new Object()

                let idInterno = this.listaDetalleSolicitudes[i].IdInternoArticulo
                let cantidad = this.listaDetalleSolicitudes[i].Cantidad
                
                Articulos.IdInternoArticulo = idInterno
                Articulos.Cantidad = cantidad

                this.listaArticulos.push(Articulos)
            }

            console.table(this.listaArticulos)

            var self = this;
            $.post('adminTrasladosAjax.aspx', {
                Opcion: 'AceptarSolicitudTrasladoBodega',
                IdSolicitudTraslado: idSolicitudTraslado,
                ListaArticulos: JSON.stringify(self.listaArticulos)
            }, function (data) {
                self.mostrarMensaje(data);
                self.obtenerEncabezadosSolicitudes()
            });
        },


        //Metodo para mostar un mensaje
        mostrarMensaje(mensaje) {
            this.mensaje = mensaje
            $('#ModalMensaje').modal('show');
        }
    },


    mounted: function () {
        this.obtenerEncabezadosSolicitudes()
    }
})