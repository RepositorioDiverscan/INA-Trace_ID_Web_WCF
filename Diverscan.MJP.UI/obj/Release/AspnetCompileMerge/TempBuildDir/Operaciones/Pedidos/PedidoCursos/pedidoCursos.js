const vuePedido = new Vue({

    el: '#pedidoCurso',

    data: {
        mensaje: '',
        listaEncabezados: [],
        listaDetalles: [],
    },


    methods: {
        //Método para obtener los encabezados de los pedidos de Cursos
        ObtenerEncabezadosPedidos() {
            var self = this;
            $.post('pedidoCursosAjax.aspx', {
                Opcion: 'ObtenerEncabezadosPedidos',
            }, function (data) {
                self.listaEncabezados = JSON.parse(data);

                //Crear la tabla de JQuery con los datos asignados
                $(document).ready(function () {
                    $('#tablaPedidosCursos').DataTable({
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
            $.post('pedidoCursosAjax.aspx', {
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
                    $.post('pedidoCursosAjax.aspx', {
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


        //Método para enviar a depurar un pedido al profesor
        depurarPedido(idPedido) {
            Swal.fire({
                title: '¿Está seguro de enviar a depurar este pedido?',
                icon: 'question',
                cancelButtonText: 'Cancelar',
                showCancelButton: true,
                confirmButtonColor: '#28a745',
                cancelButtonColor: '#6C757D',
                confirmButtonText: 'Enviar a Depurar'
            }).then((result) => {
                if (result.isConfirmed) {
                    var self = this;
                    $.post('pedidoCursosAjax.aspx', {
                        Opcion: 'EnviarDepuracionPedidoCurso',
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
                    $.post('pedidoCursosAjax.aspx', {
                        Opcion: 'AceptarPedidoCurso',
                        IdPedidoOriginal: idPedido
                    }, function (data) {
                        self.ObtenerEncabezadosPedidos();
                        self.mostrarMensaje(data)
                    });
                }
            })
        },


        //Método para generar el PDF de los Reportes de los Bienes Faltantes
        GenerarPDFBienesFaltantes() {
            const element = document.getElementById("DivTablaDetalle");

            var opt = {
                margin: 0.5,
                filename: 'Reporte_Bienes_Faltantes.pdf',
                image: {
                    type: 'jpeg',
                    quality: 0.98
                },
                html2canvas: {
                    scale: 2,
                    width: 1050,
                },
                jsPDF: {
                    unit: 'in',
                    format: 'a4',
                    orientation: 'landscape'
                }
            };
            html2pdf().set(opt).from(element).save();
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