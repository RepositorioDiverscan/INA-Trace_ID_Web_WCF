const kardexZone = new Vue({
    el: '#kardex',

    data: {
        mensaje: '',
        listakardex: [],
        bodegas: [],
        ddlBodegas: '',
        sku: "",
        lote: "",
        transito: 0,
        dtF1: new Date().toISOString().substr(0, 10),
        dtF2: new Date().toISOString().substr(0, 10),
        fechaHoy: new Date().toISOString().substr(0, 10)
        
    },

    methods: {

        //Método para buscar un reporte
        buscarKardex() {
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
            if (this.ddlBodegas == 0) {
                this.MostrarMensaje("Debe seleccionar una bodega")
                return;
            }
            //Invocar el ajax para llamar a la Alerta de Vencimiento
            let self = this;
            $.post('kardexAjax.aspx', {
                opcion: 'ObtenerKardex',
                idBodega: self.ddlBodegas,
                sku: self.sku,
                lote: self.lote,
                transito: self.transito,
                f1: self.dtF1,
                f2: self.dtF2,

            }, function (data) {

                if (self.sku == '')
                {
                    self.MostrarMensaje("Debe ingresar un Id Interno");
                }
                else if (data == "El Id Interno no existe")
                {
                    self.MostrarMensaje(data);
                }
                else
                {
                    self.listakardex = JSON.parse(data);
                    //Crear la tabla de JQuery con los datos asignados
                    $(document).ready(function () {
                        $('#table_listakardex').DataTable({
                            data: self.listakardex,
                            columns: [

                                { data: 'IdTrazabilidad' },
                                { data: 'IdUbicacion' },
                                { data: 'Ubicacion' },
                                { data: 'IdMetodoAccion' },
                                { data: 'MetodoDescripcion' },
                                { data: 'Cantidad' },
                                { data: 'Saldo' },
                                { data: 'FechaRegistro' },
                                { data: 'Lote' },
                                { data: 'FechaVencimiento' },
                                { data: 'IdInterno' },
                                { data: 'Nombre' },

                            ],
                            destroy: true,
                            language: {
                                url: 'https://cdn.datatables.net/plug-ins/1.12.1/i18n/es-ES.json',
                            },

                        });
                    });
                }

                document.getElementById("DivTablalistakardex").style.display = "block";

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
            if (this.listakardex.length <= 0) {
                this.MostrarMensaje("Por favor, ingrese datos en la tabla para exportar")
                return;
            }
            const worksheet = XLSX.utils.json_to_sheet(this.listakardex);
            const workbook = XLSX.utils.book_new();
            XLSX.utils.book_append_sheet(workbook, worksheet, "Kardex");
            XLSX.writeFile(workbook, "Kardex.xlsx");
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