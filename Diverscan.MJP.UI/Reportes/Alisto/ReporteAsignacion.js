const alisto = new Vue({
    el: '#alisto',

    data: {
        mensaje: '',
        id: '',
        listaAlisto: [],
        dtF1: new Date().toISOString().substr(0, 10),
        dtF2: new Date().toISOString().substr(0, 10),
        fechaHoy: new Date().toISOString().substr(0, 10)

    },


    methods: {
        //metodo para  buscar y cargar la tabla
        buscarAlisto() {
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

            if (this.id == "") {
                this.MostrarMensaje("Debe ingresar un Usuario")
                return;
            }

            //se llama al ajax 
            let self = this;
            $.post('ReporteAsignacionAjax.aspx', {
                opcion: 'ObtenerAlisto',
                F1: self.dtF1,
                F2: self.dtF2,
                id: self.id,
                
            }, function (data) {
                self.listaAlisto = JSON.parse(data);
                //se crea la tabla de Jquery con los datos asignados
                $(document).ready(function () {
                    $('#table_listaAlisto').DataTable({
                        data: self.listaAlisto,
                        columns: [
                            { data: 'ConsecutivoSSCC' },
                            { data: 'Sku' },
                            { data: 'NombreArticulo' },
                            { data: 'UnidadesAsignadas' },
                            { data: 'UnidadesPendiente' },
                            { data: 'EstadoSSCC' },
                            { data: 'SPorcAlisto' },
                        ],
                        destroy: true,
                        languaje: {
                            url: 'https://cdn.datatables.net/plug-ins/1.12.1/i18n/es-ES.json',
                        },
                    });
                });
                document.getElementById("DivTablaAlisto").style.display = "block";
            });
        },

        
        //Método para generar un excel del reporte
        generarExcel() {

            //Validación de los campos
            if (this.listaAlisto.length <= 0) {
                this.MostrarMensaje("Por favor, ingrese datos en la tabla para exportar")
                return;
            }
            const worksheet = XLSX.utils.json_to_sheet(this.listaAlisto);
            const workbook = XLSX.utils.book_new();
            XLSX.utils.book_append_sheet(workbook, worksheet, "Asignacion");
            XLSX.writeFile(workbook, "Asignacion.xlsx");
        },
        //Metodo para mostrar un mensaje en la pantalla por medio de un modal
        MostrarMensaje(mensaje) {
            this.mensaje = mensaje;
            $('#MensajeModal').modal('show')
        },

    },
    //Metodo del ciclo de vida que inicia la pagina
    mounted() {


    },
})