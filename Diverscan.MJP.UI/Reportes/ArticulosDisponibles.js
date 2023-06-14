const articulosZone = new Vue({
    el: '#articulos',

    data: {
        mensaje: '',
        listaArticulos: [],
        bodegas: [],
        ddlBodegas: '',
       
    },

    methods: {

        //Método para buscar un reporte
        buscarListaArticulos() {
            
            if (this.ddlBodegas == 0) {
                this.MostrarMensaje("Debe seleccionar una bodega")
                return;
            }
            //Invocar el ajax para llamar a la Alerta de Vencimiento
            let self = this;
            $.post('ArticulosDisponiblesAjax.aspx', {
                opcion: 'ObtenerArticulosDisponibles',
                idBodega: self.ddlBodegas,
            }, function (data) {
                self.listaArticulos = JSON.parse(data);
                //Crear la tabla de JQuery con los datos asignados
                $(document).ready(function () {
                    $('#table_listaArticulos').DataTable({
                        data: self.listaArticulos,
                        columns: [

                            { data: 'IdTID' },
                            { data: 'IdERP' },
                            { data: 'Articulo' },
                            { data: 'UnidadesInventario' },
                            { data: 'Lote' },
                            { data: 'FechaVencimiento' },
                            { data: 'Ubicacion' },
                            { data: 'UnidadMedida' },

                        ],
                        destroy: true,
                        language: {
                            url: 'https://cdn.datatables.net/plug-ins/1.12.1/i18n/es-ES.json',
                        },

                    });
                });

                document.getElementById("DivTablalistaArticulos").style.display = "block";

            });
        },
        cargarBodegas() {
            //Invocar el ajax para llamar a los alistadores
            let self = this;
            $.post('ArticulosDisponiblesAjax.aspx', {

                opcion: 'CargarBodegas',

            }, function (data) {
                self.bodegas = JSON.parse(data); //Agregar el arreglo 
            });
        },
        //Método para generar un excel del reporte
        generarExcel() {

            //Validación de los campos
            if (this.listaArticulos.length <= 0 ) {
                this.MostrarMensaje("Por favor, ingrese datos en la tabla para exportar")
                return;
            }
            const worksheet = XLSX.utils.json_to_sheet(this.listaArticulos);
            const workbook = XLSX.utils.book_new();
            XLSX.utils.book_append_sheet(workbook, worksheet, "Articulos Disponibles Bodega");
            XLSX.writeFile(workbook, "ArticulosDisponibles.xlsx");
        },
        //Metodo para mostrar un mensaje en la pantalla por medio de un modal
        MostrarMensaje(mensaje) {
            this.mensaje = mensaje;
            $('#MensajeModal').modal('show')
        },
    },

    //Metodo del ciclo de vida que inicia la pagina
    mounted() {

        this.cargarBodegas()
    },
})