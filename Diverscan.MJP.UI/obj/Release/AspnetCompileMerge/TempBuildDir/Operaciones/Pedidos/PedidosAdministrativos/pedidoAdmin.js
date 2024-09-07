const vuePedido = new Vue({

    el: '#pedidoAdmin',

    data: {
        mensaje: '',
        listaEncabezados: [],
        listaDetalles: [],
    },


    methods: {
        //Método para obtener los encabezados de los pedidos Admin
        ObtenerEncabezadosPedidos() {
            var self = this;
            $.post('pedidoAdminAjax.aspx', {
                Opcion: 'ObtenerEncabezadosPedidosAdministrativo',
            }, function (data) {
                self.listaEncabezados = JSON.parse(data);

                //Crear la tabla de JQuery con los datos asignados
                $(document).ready(function () {
                    $('#tablaPedidosAdmin').DataTable({
                        destroy: true,
                        language: {
                            url: 'https://cdn.datatables.net/plug-ins/1.12.1/i18n/es-ES.json',
                            data: self.listaEncabezados
                        }
                    });
                });
            });
        },


        //Método para obtener los detalles de los pedidos de Cursos
        ObtenerDetallePedidos(idPedidoOriginal) {
            var self = this;
            $.post('pedidoAdminAjax.aspx', {
                Opcion: 'ObtenerDetallePedidos',
                IdPedidoOriginal: idPedidoOriginal
            }, function (data) {
                self.listaDetalles = JSON.parse(data);
                document.getElementById("DivTablaDetalle").style.display = "block"

            });
        },


        //Método para anular pedido de cursos
        anularPedido(idPedido) {
            Swal.fire({
                title: '¿Está seguro de anular este pedido?',
                icon: 'question',
                cancelButtonText: 'Cancelar',
                showCancelButton: true,
                confirmButtonColor: '#DC3545',
                cancelButtonColor: '#6C757D',
                confirmButtonText: 'Anular'
            }).then((result) => {
                if (result.isConfirmed) {
                    var self = this;
                    $.post('pedidoAdminAjax.aspx', {
                        Opcion: 'AnularPedidoCurso',
                        IdPedidoOriginal: idPedido
                    }, function (data) {
                        self.ObtenerEncabezadosPedidos();
                        $('#ModalDetalle').modal('hide');
                        self.mostrarMensaje(data)
                        document.getElementById("DivTablaDetalle").style.display = "none"

                    });
                }
            })
        },


        //Método para aprobar pedido de cursos
        aprobarPedido(idPedido) {
            Swal.fire({
                title: '¿Está seguro de mandar el Pedido a Solicitud de Olas? ',
                icon: 'question',
                cancelButtonText: 'Cancelar',
                showCancelButton: true,
                confirmButtonColor: '#28a745',
                cancelButtonColor: '#6C757D',
                confirmButtonText: 'Agregar Solicitud Ola'
            }).then((result) => {
                if (result.isConfirmed) {
                    var self = this;
                    $.post('pedidoAdminAjax.aspx', {
                        Opcion: 'AceptarPedidoAdmin',
                        IdPedidoOriginal: idPedido
                    }, function (data) {
                        self.ObtenerEncabezadosPedidos();
                        self.mostrarMensaje(data)
                    });
                }
            })
        },

       
        //Método para mostrar mensajes en general
        mostrarMensaje(mensaje) {
            this.mensaje = mensaje
            $('#ModalMensaje').modal('show');
        }
    },


    mounted: function () {
        this.ObtenerEncabezadosPedidos();
        document.getElementById("DivTablaDetalle").style.display = "none"
    }

})