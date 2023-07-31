const GestionPedido = new Vue({

    el: '#GestionPedido',

    data: {
        mensaje: '',
        detalle: '',
        txt_cantidadModificar: '',
        IdPedido: '',
        listaEncabezados: [],
        listaDetalle: [],
    },

    methods: {

        //Método para obtener el encabezado de Pedidos Cursos en estado de depuración
        CargarEncabezados() {
            var self = this;
            $.post('DepurarPedidoCursoAjax.aspx', {
                Opcion: 'ConsultaPedidoOriginalEncabezado',
            }, function (data) {
                self.listaEncabezados = JSON.parse(data);

                //Crear la tabla de JQuery con los datos asignados
                $(document).ready(function () {
                    $('#tableEncabezados').DataTable({
                        destroy: true,
                        language: {
                            url: 'https://cdn.datatables.net/plug-ins/1.12.1/i18n/es-ES.json',
                            data: self.listaEncabezados
                        }
                    });
                });
            });
        },


        //Método para obtener el detalle de un pedido en específico para su depuración
        CargarDetalles(idPedido) {
            this.IdPedido = idPedido
            var self = this;

            $.post('DepurarPedidoCursoAjax.aspx', {
                Opcion: 'ConsultaPedidoOriginalDetalle',
                IdPedido: idPedido
            }, function (data) {
                self.listaDetalle = JSON.parse(data);

                //Crear la tabla de JQuery con los datos asignados
                $(document).ready(function () {
                    $('#tablaDetalle').DataTable({
                        destroy: true,
                        language: {
                            url: 'https://cdn.datatables.net/plug-ins/1.12.1/i18n/es-ES.json',
                            data: self.listaDetalle
                        }
                    });
                });

                document.getElementById("TablaPedidosDetalle").style.display = "block"

            });
        },


        //Método para mostrar el modal de modificar la cantidad del detalle
        mostrarModalModificarDetalle(detalle) {
            this.detalle = detalle
            $('#ModalCantidadModificar').modal('show')
        },


        //Método para modificar la cantidad de un artículo
        ModificarCantidad(accion) {
            if ((this.detalle.Cantidad - this.txt_cantidadModificar) < 0 && accion == "Restar") {
                this.mostrarMensaje("La cantidad a restar no puede ser negativa, intente otra vez.")
            }
            else {
                var self = this;
                $.post('DepurarPedidoCursoAjax.aspx', {
                    Opcion: 'ModificarCantidadDetalle',
                    IdPedido: self.IdPedido,
                    IdArticulo: self.detalle.IdArticulo,
                    CantidadModificar: self.txt_cantidadModificar,
                    Accion: accion,
                }, function (data) {
                    self.mostrarMensaje(data)
                    $('#ModalCantidadModificar').modal('hide')
                    self.CargarDetalles(self.IdPedido)
                    self.txt_cantidadModificar = ''
                });
            }
          
        },


        //Metodo para mostrar el modal de mensaje 
        mostrarMensaje(mensaje) {
            this.mensaje = mensaje;
            $('#ModalMensaje').modal('show');
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
                    $.post('DepurarPedidoCursoAjax.aspx', {
                        Opcion: 'AceptarPedidoCurso',
                        IdPedidoOriginal: idPedido
                    }, function (data) {
                        self.ObtenerEncabezadosPedidos();
                        self.mostrarMensaje(data)
                    });
                }
            })
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
                    $.post('DepurarPedidoCursoAjax.aspx', {
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
    },

    

    mounted: function() {
        this.CargarEncabezados();
        document.getElementById("TablaPedidosDetalle").style.display = "none"
    }

})