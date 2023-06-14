const inventariio = new Vue({
    el: '#inventario',

    data: {
        //tabla encabezado Inventario
        listaEncabezadoInventario: [],
        //tabla detalle Inventario
        listaDetalleInventario: [],

        //mensaje de error o de advertencia
        mensaje: '',

        //fechas
        dtF1: new Date().toISOString().substr(0, 10),
        dtF2: new Date().toISOString().substr(0, 10),
        fechaHoy: new Date().toISOString().substr(0, 10),

        //variable para detalle inventario
        id:'',
    },

    methods: {
        //metodo para mostrar encabezado de Inventario
        ObtenerEncabezadoInventario: function () {
            let self = this;
            $.post('InventarioAjax.aspx', {
                opcion: 'ObtenerInventario',
                f1: self.dtF1,
                f2: self.dtF2,
            }, function (data) {
                self.listaEncabezadoInventario = JSON.parse(data);
                $(document).ready(function () {
                    $('#table_listaEncabezadoInventario').DataTable({
                        data: self.listaEncabezadoInventario,
                        columns: [

                            { data: 'IdInventarioBasico' },
                            { data: 'Nombre' },
                            { data: 'Descripcion' },
                            { data: 'FechaPorAplicar' },
                            { data: 'Estado' },
                            

                        ],
                        destroy: true,
                        language: {
                            url: 'https://cdn.datatables.net/plug-ins/1.12.1/i18n/es-ES.json',
                        },

                    });
                });
            });
            document.getElementById("DivlistaEncabezadoInventario").style.display = "block";
        },

        ObtenerDetalleInventario: function (a) {

            var self = this;
            $.post('/Consultas/Administracion/InventarioAjax.aspx', {
                Opcion: 'ObtenerDetalle',
                id: a,
            }, function (data) {
                self.listaDetalleInventario = JSON.parse(data);
                $(document).ready(function () {
                    $('#table_listaDetalleInventario').DataTable({
                        data: self.listaDetalleInventario,
                        columns: [

                            { data: 'IdArticulo' },
                            { data: 'NombreArticulo' },
                            { data: 'Cantidad' },
                            { data: 'Descripcion' },
                            { data: 'FechaHoraRegistro' },
                          

                        ],
                        destroy: true,
                        language: {
                            url: 'https://cdn.datatables.net/plug-ins/1.12.1/i18n/es-ES.json',
                        },

                    });
                });

            });


            document.getElementById("DivlistaDetalleInventario").style.display = "block";
        },

        generarExcel() {

            //Validación de los campos
            if (this.listaEncabezadoInventario.length <= 0) {
                this.MostrarMensaje("Por favor, ingrese datos en la tabla para exportar")
                return;
            }
            const worksheet = XLSX.utils.json_to_sheet(this.listaEncabezadoInventario);
            const workbook = XLSX.utils.book_new();
            XLSX.utils.book_append_sheet(workbook, worksheet, "Inventario");
            XLSX.writeFile(workbook, "Inventario.xlsx");
        },
        generarExcel2() {

            //Validación de los campos
            if (this.listaDetalleInventario.length <= 0) {
                this.MostrarMensaje("Por favor, ingrese datos en la tabla para exportar")
                return;
            }
            const worksheet = XLSX.utils.json_to_sheet(this.listaDetalleInventario);
            const workbook = XLSX.utils.book_new();
            XLSX.utils.book_append_sheet(workbook, worksheet, "InventarioDetalle");
            XLSX.writeFile(workbook, "InventarioDetalle.xlsx");
        },
    },
    //Metodo para mostrar el modal de mensaje 
    mostrarMensaje(mensaje) {
        this.mensaje = mensaje;
        $('#ModalMensaje').modal('show');
    },
    
})