// Call the dataTables jQuery plugin
$(document).ready(function() {
    $('#dataTable').DataTable({
        "lengthMenu": [[-1, 25, 50, 100], ["Todos", 25, 50, 100]],
        "scrollX": true,
        "scrollY": 500,
        "language": {
            //"url": ($('base').attr('href') + "/lang/Spanish.json").replace('//', '/')
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "No hay datos disponibles en esta tabla",
            "sInfo": "Registro _START_ a _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Registro 0 a 0 de 0 registros",
            "sInfoFiltered": "(filtrado de _MAX_ registros totales)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            }
        }
    } );
});
