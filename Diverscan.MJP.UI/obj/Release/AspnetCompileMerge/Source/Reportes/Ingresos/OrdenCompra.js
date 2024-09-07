const ordenCompra = new Vue({
    el: '#orden',

    data: {
        //tabla Orden Compra
        pedidoOrdenCompra: [],
        //tabla Detalle Orden Compra
        pedidoDetalleOrdenCompra: [],

        //mensaje de error o de advertencia 
        mensaje: '',

        //cargar bodegas
        bodegas: [],
        ddlBodegas: '',

        //fechas 
        dtF1: new Date().toISOString().substr(0, 10),
        dtF2: new Date().toISOString().substr(0, 10),
        fechaHoy: new Date().toISOString().substr(0, 10),

        //numero de orden de compra
        numero: "",

        //variablesd para detalle de orden de compra 
        idMaestro:''
    },

    methods: {
        //metodo para mostrar la orden de compra 
        ObtenerOrdenCompra: function () {
            let self = this;
            $.post('/Reportes/Ingresos/OrdenCompraAjax.aspx', {
                opcion: 'ObtenerOrdenCompra',
                idBodega: self.ddlBodegas,
                numero: self.numero,
                f1: self.dtF1,
                f2: self.dtF2,
            }, function (data) {
                self.pedidoOrdenCompra = JSON.parse(data);
                console.log(self.pedidoOrdenCompra);
                $(document).ready(function () {
                    $('#table_pedidoOrdenCompra').DataTable({
                        data: self.pedidoOrdenCompra,
                        columns: [

                            { data: 'IdMaestroOrdenCompra' },
                            { data: 'NumTransaccion' },
                            { data: 'NombreProveedor' },
                            { data: 'FechaCreacion' },
                            { data: 'FechaProcesamiento' },
                            { data: 'NumeroFactura' },
                            { data: 'Usuario' },
                            { data: 'PorcentajeRecepcion' },
                            { data: 'Procesada' },

                        ],
                        destroy: true,
                        language: {
                            url: 'https://cdn.datatables.net/plug-ins/1.12.1/i18n/es-ES.json',
                        },

                    });
                });
            });
            document.getElementById("DivpedidoOrdenCompra").style.display = "block";
            
        },

        ObtenerDetalleOrdenCompra: function (a) {
           
            var self = this;
            $.post('/Reportes/Ingresos/OrdenCompraAjax.aspx', {
                Opcion: 'ObtenerDetalleOrdenCompra',
                idMaestroArticulo: a,
            }, function (data) {
                self.pedidoDetalleOrdenCompra = JSON.parse(data);
                $(document).ready(function () {
                    $('#table_pedidoDetalleOrdenCompra').DataTable({
                        data: self.pedidoDetalleOrdenCompra,
                        columns: [

                            { data: 'IdInterno' },
                            { data: 'Gtin' },
                            { data: 'Nombre' },
                            { data: 'CantidadxRecibir' },
                            { data: 'CantidadRecibidos' },
                            { data: 'CantidadTransito' },
                            { data: 'CantidadBodega' },
        
                        ],
                        destroy: true,
                        language: {
                            url: 'https://cdn.datatables.net/plug-ins/1.12.1/i18n/es-ES.json',
                        },

                    });
                });

            });


            document.getElementById("DivDetallePedidos").style.display = "block";
        },
        cargarBodegas() {
            //Invocar el ajax para llamar a los alistadores
            let self = this;
            $.post('../ArticulosDisponiblesAjax.aspx', {

                opcion: 'CargarBodegas',

            }, function (data) {
                self.bodegas = JSON.parse(data); //Agregar el arreglo 
            });
        },

        generarExcel() {

            //Validación de los campos
            if (this.pedidoOrdenCompra.length <= 0) {
                this.MostrarMensaje("Por favor, ingrese datos en la tabla para exportar")
                return;
            }
            const worksheet = XLSX.utils.json_to_sheet(this.pedidoOrdenCompra);
            const workbook = XLSX.utils.book_new();
            XLSX.utils.book_append_sheet(workbook, worksheet, "pedidoOrdenCompra");
            XLSX.writeFile(workbook, "pedidoOrdenCompra.xlsx");
        },
        generarExcel2() {

            //Validación de los campos
            if (this.pedidoDetalleOrdenCompra.length <= 0) {
                this.MostrarMensaje("Por favor, ingrese datos en la tabla para exportar")
                return;
            }
            const worksheet = XLSX.utils.json_to_sheet(this.pedidoDetalleOrdenCompra);
            const workbook = XLSX.utils.book_new();
            XLSX.utils.book_append_sheet(workbook, worksheet, "pedidoDetalleOrdenCompra");
            XLSX.writeFile(workbook, "pedidoDetalleOrdenCompra.xlsx");
        },
    },
    //Metodo para mostrar el modal de mensaje 
    mostrarMensaje(mensaje) {
        this.mensaje = mensaje;
        $('#ModalMensaje').modal('show');
    },
    //metodo que se cargara al iniciar la pantalla 
    mounted() {

        this.cargarBodegas()
    },
})