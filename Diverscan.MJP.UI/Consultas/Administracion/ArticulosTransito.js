const transitoZone = new Vue({
    el: '#ktransito',

    data: {
        mensaje: '',
        listaArticulo: [],
        bodegas: [],
        ddlBodegas: '',
    },

    methods: {

        //Método para buscar un reporte
        buscarArticuloTransito() {
           
            if (this.ddlBodegas == 0) {
                this.MostrarMensaje("Debe seleccionar una bodega")
                return;
            }
            //Invocar el ajax para llamar a la Alerta de Vencimiento
            let self = this;
            $.post('ArticulosTransitoAjax.aspx', {
                opcion: 'ObtenerArticuloTransito',
                idBodega: self.ddlBodegas,
               

            }, function (data) {
                self.listaArticulo = JSON.parse(data);
                $(document).ready(function () {
                    $('#table_listaArticulo').DataTable({
                        data: self.listaArticulo,
                        columns: [

                            { data: 'NombreUsuario' },
                            { data: 'ApellidosUsuario' },
                            { data: 'UbicacionArticulo' },
                            { data: 'NombreArticulo' },
                            { data: 'IdInterno' },
                            { data: 'CantidadTransito' },
                            { data: 'Lote' },
                            { data: 'FechaVencimiento' },
                            
                        ],
                        destroy: true,
                        language: {
                            url: 'https://cdn.datatables.net/plug-ins/1.12.1/i18n/es-ES.json',
                        },

                    });
                });
                //Crear la tabla de JQuery con los datos asignados
                

                document.getElementById("DivTablalistaArticulo").style.display = "block";

            });
        },
        cargarBodegas() {
            //Invocar el ajax para llamar a los alistadores
            let self = this;
            $.post('ArticulosTransitoAjax.aspx', {

                opcion: 'CargarBodegas',

            }, function (data) {
                self.bodegas = JSON.parse(data); //Agregar el arreglo 
            });
        },
        //Método para generar un excel del reporte
        generarExcel() {

            //Validación de los campos
            if (this.listaArticulo.length <= 0) {
                this.MostrarMensaje("Por favor, ingrese datos en la tabla para exportar")
                return;
            }
            const worksheet = XLSX.utils.json_to_sheet(this.listaArticulo);
            const workbook = XLSX.utils.book_new();
            XLSX.utils.book_append_sheet(workbook, worksheet, "ArticuloTransitoBodega");
            XLSX.writeFile(workbook, "ArticuloTransitoBodega.xlsx");
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