const olas = new Vue({
    el: '#ola',

    data: {
        mensaje: '',
        listaOla: [],
        dtF1: new Date().toISOString().substr(0, 10),
        dtF2: new Date().toISOString().substr(0, 10),
        fechaHoy: new Date().toISOString().substr(0, 10)
    },


    methods: {
        //metodo paraobtener ola
        buscarOla() {
            if (this.dtF1 == "") {
                this.MostrarMensaje("Seleccione la Fecha de Inicio")
                return;
            }

            if (this.dtF2 == "") {
                this.MostrarMensaje("Seleccione la Fecha de Fin")
                return;
            }

            if (this.dtF2 < this.dtF1) {
                this.MostrarMensaje("La Fecha Final no puede ser menor a la Fecha Inicial")
                return;
            }

            //invocar al ajax
            let self = this;
            $.post('OlaFinalizadaAjax.aspx', {
                opcion: 'ObtenerOlas',
                F1: self.dtF1,
                F2: self.dtF2,
            }, function (data) {
                self.listaOla = JSON.parse(data);
                //Crear la tabla de JQuery con los datos asignados
                $(document).ready(function () {
                    $('#table_listaOla').DataTable({
                        data: self.listaOla,
                        columns: [

                            { data: 'Fecha' },
                            { data: 'IdOla' },
                            { data: 'DiasFinalizados' },
                            { data: 'Avance' },
                        ],
                        destroy: true,
                        language: {
                            url: 'https://cdn.datatables.net/plug-ins/1.12.1/i18n/es-ES.json',
                        },

                    });
                });

                document.getElementById("DivTablaOla").style.display = "block";

            });
        },

        //Método para generar un excel del reporte
        generarExcel() {

            //Validación de los campos
            if (this.listaOla.length <= 0) {
                this.MostrarMensaje("Por favor, ingrese datos en la tabla para exportar")
                return;
            }
            const worksheet = XLSX.utils.json_to_sheet(this.listaOla);
            const workbook = XLSX.utils.book_new();
            XLSX.utils.book_append_sheet(workbook, worksheet, "OlasFinalizadas");
            XLSX.writeFile(workbook, "OlasFinalizadas.xlsx");
        },

        //Metodo para mostrar un mensaje en la pantalla por medio de un modal
        MostrarMensaje(mensaje) {
            this.mensaje = mensaje;
            $('#MensajeModal').modal('show')
        },

        FormatearFecha: function (fecha) {
            var fechaFormato = new Date(fecha);
            return fechaFormato.getDate() + "/" + (fechaFormato.getMonth() + 1) + "/" + fechaFormato.getFullYear();
        },
    },

})