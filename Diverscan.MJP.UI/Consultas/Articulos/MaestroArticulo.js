const maestroArticulo = new Vue({
    el: '#maestro',

    data: {
        mensaje: '',
        listaMaestro: [],
    },

    methods: {

        //metodo para obtener el maestro articulo
        obtenerMaestroArticulo() {

            let self = this;
            $.post('MaestroArticuloAjax.aspx', {
                opcion: 'ObtenerMaestroArticulo',


            }, function (data) {     
                self.listaMaestro = JSON.parse(data);
                $(document).ready(function () {
                    $('#table_listaMaestro').DataTable({
                        data: self.listaMaestro,
                        columns: [

                            { data: 'IdArticulo' },
                            { data: 'IdInterno' },
                            { data: 'Nombre' },
                            { data: 'Gtin' },
                            { data: 'NombreFamilia' },
                            { data: 'Contenido' },
                            { data: 'Unidad_Medida' },
                            { data: 'Activo' },
                            { data: 'FechaRegistro' },

                        ],
                        columnDefs: [
                            {
                                targets: 7, // Índice de la columna "Activo"
                                render: function (data) {
                                    if (data) {
                                        return '<i class="fas fa-check"></i>'; // Icono de check
                                    } else {
                                        return '<i class="fas fa-times"></i>'; // Icono de x
                                    }
                                }
                            }
                        ],
                        destroy: true,
                        language: {
                            url: 'https://cdn.datatables.net/plug-ins/1.12.1/i18n/es-ES.json',
                        },

                    });
                });
               
                document.getElementById("DivtablaMaestroArticulo").style.display = "block";
            });
           
        },
        //Método para generar un excel del reporte
        generarExcel() {

            //Validación de los campos
            if (this.listaMaestro.length <= 0) {
                this.MostrarMensaje("Por favor, ingrese datos en la tabla para exportar")
                return;
            }
            const worksheet = XLSX.utils.json_to_sheet(this.listaMaestro);
            const workbook = XLSX.utils.book_new();
            XLSX.utils.book_append_sheet(workbook, worksheet, "MaestroArticulo");
            XLSX.writeFile(workbook, "MaestroArticulo.xlsx");
        },
        //Metodo para mostrar un mensaje en la pantalla por medio de un modal
        MostrarMensaje(mensaje) {
            this.mensaje = mensaje;
            $('#MensajeModal').modal('show')
        },
    },

    mounted: function () {
        this.obtenerMaestroArticulo();
    }
})