const solicitudDevolucionReporte = new Vue({

    el: '#solicitudDevolucionReporte',

    data: {
        mensaje: '',
        listaReporte: [],
        listaBodegas: [],
        ddlBodegas: '',
        dtFechaInicio: new Date().toISOString().substr(0, 10),
        dtFechaFin: new Date().toISOString().substr(0, 10),
        fechaHoy: new Date().toISOString().substr(0, 10)
    },


    methods: {

        //Método para buscar un reporte
        buscarReporte() {
            //Validacion de los campos de las fechas
            if (this.dtFechaInicio == "") {
                this.MostrarMensaje("Seleccione la Fecha de Inicio")
                return;
            }

            if (this.dtFechaFin == "") {
                this.MostrarMensaje("Seleccione la Fecha de Fin")
                return;
            }

            if (this.dtFechaFin < this.dtFechaInicio) {
                this.MostrarMensaje("La Fecha Final no puede ser menor a la Fecha Inicial")
                return;
            }

            if (this.ddlBodegas == 0) {
                this.MostrarMensaje("Seleccione una bodega")
                return;
            }

            //Invocar el ajax para llamar al reporte
            let self = this;
            $.post('SolicitudDevolucionAjax.aspx', {
                opcion: 'ObtenerReporteSolicitud',
                IdBodega: self.ddlBodegas,
                FechaInicio: self.dtFechaInicio,
                FechaFin: self.dtFechaFin
            }, function (data) {
                self.listaReporte = JSON.parse(data);

                //Crear la tabla de JQuery con los datos asignados
                $(document).ready(function () {
                    $('#table_reportes').DataTable({
                        data: self.listaReporte,
                        columns: [
                            { data: 'IdInterno' },
                            { data: 'Cantidad' },
                            { data: 'NombreArticulo' },
                        ],
                        destroy: true,
                        language: {
                            url: 'https://cdn.datatables.net/plug-ins/1.12.1/i18n/es-ES.json',
                        },

                    });
                });

                document.getElementById("DivTablaReportes").style.display = "block";

            });
        },

        cargarBodegas() {
            //Invocar el ajax para cargar las bodegas
            let self = this;
            $.post('SolicitudDevolucionAjax.aspx', {
                opcion: 'CargarBodegas',
            }, function (data) {
                self.listaBodegas = JSON.parse(data); //Agregar el arreglo
            });
        },

        //Método para generar un excel del reporte
        generarExcel() {

            //Validación de los campos
            if (this.listaReporte.length <= 0) {
                this.MostrarMensaje("Por favor, ingrese datos en la tabla para exportar")
                return;
            }

            const worksheet = XLSX.utils.json_to_sheet(this.listaReporte);
            const workbook = XLSX.utils.book_new();
            XLSX.utils.book_append_sheet(workbook, worksheet, "Solicitudes de devolucion");
            XLSX.writeFile(workbook, "SolicitudDevolucionReporte.xlsx");

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