const GestionPedido = new Vue({

    el: '#GestionPedido',

    data: {
        pedidosEncabezados: [],
        pedidosDetalle: [],
        mensaje: '',
        detalleModificar: [],
        txt_cantidadModificar: 0,
        idPedidoOriginal: ''
    },

    methods: {

        //Metodo para obtener los encabezados del usuario
        CargarEncabezados() {
            //Invocar un ajax para mostrar el encabezado de los pedidos
            var self = this;
            $.post('DepurarPedidoAjax.aspx', {
                Opcion: 'ConsultaPedidoOriginalEncabezado'
            }, function (data) {
                self.pedidosEncabezados = JSON.parse(data);
            });
        },

        //Metodo para mostrar el detalle de cada encabezado
        CargarDetalles(idPedido) {
            this.idPedidoOriginal = idPedido
            //Invocar un ajax para mostrar el detalle del pedido
            var self = this;
            $.post('DepurarPedidoAjax.aspx', {
                Opcion: 'ObtenerPedidoOriginalDetalle',
                IdPedidoOriginal: idPedido
            }, function (data) {
                self.pedidosDetalle = JSON.parse(data);
            });

            document.getElementById("tablaDetallePedidos").style.display = "block";
        },


        //Metodo para mostrar el modal de modificar la cantidad
        MostrarModalModificarCantidad(pedido) {
            this.detalleModificar = pedido;
            $('#ModalCantidadModificar').modal('show');
        },


        //Metodo para modificar la cantidad
        ModificarCantidad(accion) {
            //Invocar un ajax para mostrar el detalle del pedido
            var self = this;
            $.post('DepurarPedidoAjax.aspx', {
                Opcion: 'ModificarCantidad',
                IdPedidoOriginal: self.idPedidoOriginal,
                IdArticulo: self.detalleModificar.IdArticulo,
                CantidadModificar: self.txt_cantidadModificar,
                Accion: accion
            }, function (data) {
                document.getElementById("tablaDetallePedidos").style.display = "none";
                $('#ModalCantidadModificar').modal('hide');
                self.txt_cantidadModificar = 0
                self.mostrarMensaje(data);
            });
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
        }
    },

    mounted: function() {
        this.CargarEncabezados();
        document.getElementById("tablaDetallePedidos").style.display = "none";
    }

})