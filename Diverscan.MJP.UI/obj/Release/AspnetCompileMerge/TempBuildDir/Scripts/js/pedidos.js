
function mensajeExito(texto)
{
    Swal.fire(
    {
        position: 'top-right',
        icon: 'success',
        title: '!! Exito !!',
        text: texto,
        showConfirmButton: true,
        closeOnConfirm: true
    });
}

function mensajeError(texto)
{
    Swal.fire(
        {
            position: 'top-right',
            icon: 'error',
            title: '!! Error !!',
            text: texto,
            showConfirmButton: true,
            closeOnConfirm: true
        });
}

function mensajeInfo(texto)
{
    Swal.fire(
        {
            position: 'top-right',
            icon: 'question',
            title: '!! Info !!',
            text: texto,
            showConfirmButton: true,
            closeOnConfirm: true
        });
}

$(document).ready(function () {
    $('[data-toggle="popover"]').popover({
        html: true,
        trigger: "hover"
    });
});

$('#errorModal').on('hidden.bs.modal', function (e) {
    var ultLineaError = $('#ultLineaError').val();
    if (ultLineaError > 0)
        $("#cant_" + ultLineaError).focus();
});

$('#errorModal').on('shown.bs.modal', function () {
    if ($('#txtMotivo').is(":visible")) {
        $('#txtMotivo').focus();
    }

    $('#textareaID').focus();
})  

$('#enviarPedidoModal').on('hidden.bs.modal', function () {
    $('#btnCerrarEnviarPedido').html("No");
    $('#btnAceptarEnviarPedido').show();
}) 



$(function () {
    $('[data-toggle="tooltip"]').tooltip();
})

// Obtener los datos de una línea antes de actualizarla
var obtieneCantLinPedido = function (no_cia, no_docu, linea, autorizar) {
    var linea_refe = autorizar ? no_docu + "_" + linea : linea;
    if (linea_refe != $('#ultLineaError').val())
        $('#cantAnt_' + linea_refe).val($('#cant_' + linea_refe).val());

}

// Actualizar una linea del pedido
var actualizaLinPedido = function (no_cia, no_docu, linea, autorizar) {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    var linea_refe = autorizar ? no_docu + "_" + linea : linea;
    var cant = $('#cant_' + linea_refe).val();
    var cantAnt = $('#cantAnt_' + linea_refe).val();
    var url = $('#hiddenActualizaLineaUrl').val();
        
    if (cant != '' && cant != cantAnt) {

        // Valida que sea un número entero
        var entero = Math.floor(Number(cant));

        if (!(entero !== Infinity && String(entero) === cant && entero >= 0)) {

            $('#btnAceptar').hide();
            $('#btnCerrar').html('Cerrar');
            $('#btnCerrar').removeClass('btn-secondary');
            $('#btnCerrar').addClass('btn-danger');


            $('#errorModalLabel').html('<i class="fa fa-exclamation-circle"></i>&nbsp;Error');  
            $('#cant_' + linea_refe).val(cantAnt);
            $('#ultLineaError').val(linea_refe);
            $('#errorModal').modal('show');
            $('#txtError').html('La cantidad ingresada debe ser un número válido');
            return false;
        }

        $.ajax({
            type: "POST",
            //url: "/Pedidos/ActualizaLinea",
            url : url,
            data: {
                __RequestVerificationToken: token,
                no_cia: no_cia,
                no_docu: no_docu,
                no_linea: linea,
                cantidad: cant,
                autorizar: autorizar
            },
            //beforeSend: function () {
            //    $('#wait').show();
            //},
            success: function (result) {
                if (result.length > 0) {
                    if (result[0].error != 'null' && result[0].error != null) {
                        var cantidadDigitada = $('#cant_' + linea_refe).val();
                        if (result[0].cantidad != 'null')
                            $('#cant_' + linea_refe).val(result[0].cantidad);
                        else
                            $('#cant_' + linea_refe).val(cantAnt);
                        $('#ultLineaError').val(linea_refe);
                        $('#errorModal').modal('show');
                        $('#valMotivo').hide();
                        var errorProc = result[0].error;
                        $('#txtError').html(errorProc);
                        if (errorProc.indexOf('REVISIÓN') > -1 /*errorProc.includes('REVISIÓN')*/) {
                            $('#errorModalLabel').html('<i class="fa fa-user-slash"></i>&nbsp;Cantidad no autorizada');                            
                            $('#btnCerrar').html('No');
                            $('#btnCerrar').removeClass('btn-danger');
                            $('#btnCerrar').addClass('btn-secondary');
                            $('#btnAceptar').html('Sí');
                            $('#btnAceptar').show();
                            $('#spanMotivo').show();
                            $('#btnAceptar').off('click').on('click', function () {
                                solicitarAutorizacion(no_cia, no_docu, linea, cant, $('#txtMotivo').val());
                            });
                        }
                        else if (errorProc.indexOf('REPARTIR') > -1 /*errorProc.includes('REPARTIR')*/) {
                            $('#errorModalLabel').html('<i class="fa fa-box-open"></i>&nbsp;Repartir bulto entre tiendas');
                            $('#btnCerrar').html('Volver');
                            $('#btnCerrar').removeClass('btn-danger');
                            $('#btnCerrar').addClass('btn-secondary');
                            $('#btnAceptar').html('Repartir');
                            $('#spanMotivo').hide();
                            $('#valMotivo').hide();
                            $('#btnAceptar').show();
                            $('#btnAceptar').off('click').on('click', function () {                                
                                $('#hiddenNoDocu').val(no_docu);
                                $('#hiddenNoLinea').val(linea_refe);
                                $('#errorModal').modal('hide');
                                $('#repartirModal').modal('show');
                                // Prepara la pantalla de repartir
                                prepararRepartir(linea_refe, cantidadDigitada);
                            });
                        }
                        else {
                            $('#errorModalLabel').html('<i class="fa fa-exclamation-circle"></i>&nbsp;Error');    
                            $('#btnCerrar').html('Cerrar');
                            $('#btnCerrar').removeClass('btn-secondary');
                            $('#btnCerrar').addClass('btn-danger');
                            $('#btnAceptar').hide();
                            $('#spanMotivo').hide();
                            $('#valMotivo').hide();
                        }

                    }
                    else {
                        $('#ultLineaError').val(0);
                    }

                    if ($('#llegar_' + linea_refe).html() != result[0].cantidadPorLlegar && result[0].cantidadPorLlegar != 'null' ) {
                        $('#llegar_' + linea_refe).html(result[0].cantidadPorLlegar);
                        $('#llegar_' + linea_refe).animate({ fontSize: "2em" });
                        $('#llegar_' + linea_refe).animate({ fontSize: "1em" });
                    }
                    if ($('#pedir_' + linea_refe).html() != result[0].saldoDisponible && result[0].saldoDisponible != 'null') {
                        $('#pedir_' + linea_refe).html(result[0].saldoDisponible);
                        $('#pedir_' + linea_refe).animate({ fontSize: "2em" });
                        $('#pedir_' + linea_refe).animate({ fontSize: "1em" });
                    }
                    if ($('#inve_' + linea_refe).html() != result[0].saldoTienda && result[0].saldoTienda != 'null') {
                        $('#inve_' + linea_refe).html(result[0].saldoTienda);
                        $('#inve_' + linea_refe).animate({ fontSize: "2em" });
                        $('#inve_' + linea_refe).animate({ fontSize: "1em" });
                    }
                }
            },
            error: function (error) {
                alert("Error: Por favor refresque la pantalla o vuelva a iniciar sesión.");
            }
            //complete: function () {
            //    $('#wait').hide();
            //}
        })
    }
}

// Eliminar pedidos
var confirmarEliminar = function (no_docu) {
    $('#hiddenNoDocu').val(no_docu);
    $("#txtErrorConfirmar").hide();
    $('.modal').find('button#btnEliminar').prop('disabled', false);
    $('#confirmacionModal').modal('show');
}

var eliminarPedido = function () {
    var form = $('#__AjaxAntiForgeryForm');
    var no_docu = $('#hiddenNoDocu').val();
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    var url = $('#hiddenDeleteUrl').val();

    $('.modal').find('button#btnEliminarPedido').prop('disabled', 'true');

    $.ajax({
        type: "POST",
        //url: "/Pedidos/Eliminar",
        url: url,
        data: {
            __RequestVerificationToken: token,
            no_docu: no_docu
        },
        success: function (result) {
            if (result == true) {
                $('#confirmacionModal').modal('hide');
                $('#row_' + no_docu).remove();
            }
            else {
                $("#txtErrorConfirmar").html("Error: " + result);
                $("#txtErrorConfirmar").show();
            }
            $('.modal').find('button#btnEliminarPedido').prop('disabled', false);

        },
        error: function (error) {
            $("#txtErrorConfirmar").html("Error: Por favor refresque la pantalla o vuelva a iniciar sesión.");
            $("#txtErrorConfirmar").show();
            $('.modal').find('button#btnEliminarPedido').prop('disabled', false);
        }
    })

}

var descartarPedido = function () {
    $('.modal').find('button#btnEliminar').prop('disabled', 'true');

    var form = $('#__AjaxAntiForgeryForm');
    var no_docu = $('#hiddenNoDocu').val();
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    var url = $('#hiddenDeleteUrl').val();
    var urlGen = $('#hiddenPedidosUrl').val();


    $.ajax({
        type: "POST",
        //url: "/Pedidos/Eliminar",
        url: url,
        data: {
            __RequestVerificationToken: token,
            no_docu: no_docu
        },
        success: function (result) {
            if (result == true) {
                window.location.href = urlGen;
            }
            else {
                $("#txtError").html("Error: " + result);
                $("#txtError").show();
                $('.modal').find('button#btnEliminar').prop('disabled', false);
            }

        },
        error: function (error) {
            $("#txtError").html("Error: Por favor refresque la pantalla o vuelva a iniciar sesión.");
            $("#txtError").show();
            $('.modal').find('button#btnEliminar').prop('disabled', false);
        }
    });

}

// Confirmar solicitud
var confirmarGenerarSolicitud = function () {
    //$('#hiddenNoDocu').val(no_docu);
    //$("#txtError").hide();
    $('.modal').find('button#btnAceptarEnviarPedido').prop('disabled', false);
    $("#txtErrorConfirmacion").hide();
    $('#enviarPedidoModal').modal('show');
}

// Genera los traslados de una solicitud
var generaSolicitud = function (no_cia, no_docu) {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    var url = $('#hiddenGenerarSolicitudUrl').val();
    var urlGen = $('#hiddenSolicitudGeneradaUrl').val();

    $('.modal').find('button#btnAceptarEnviarPedido').prop('disabled', 'true');
        
    $.ajax({
        type: "POST",
        //url: "/Pedidos/GenerarSolicitud",
        url: url,
        data: {
            __RequestVerificationToken: token,
            no_cia: no_cia,
            no_docu: no_docu
        },
        //beforeSend: function () {
        //    $('#wait').show();
        //},
        success: function (result) {

            if (result.length > 0) {
                if (result[0].error != 'null' && result[0].error != null) {
                    $("#txtErrorConfirmacion").html("Error: " + result[0].error);
                    $("#txtErrorConfirmacion").show();
                    $('#btnCerrarEnviarPedido').html("Volver");
                    $('#btnAceptarEnviarPedido').hide();
                }
                else {
                    window.location.href = urlGen;
                }
            }
        },
        error: function (error) {
            $("#txtErrorConfirmacion").html("Error: Por favor refresque la pantalla o vuelva a iniciar sesión.");
            $("#txtErrorConfirmacion").show();
            $('#btnCerrarEnviarPedido').html("Volver");
            $('#btnAceptarEnviarPedido').hide();
        }
        //complete: function () {
        //    $('#wait').hide();
        //}
    })
}

///////////////////////////
// Autorizar Pedidos
///////////////////////////

// Solicitar Autorizacion
var solicitarAutorizacion = function (no_cia, no_docu, linea, cantidad, motivo) {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    var url = $('#hiddenSolicitarAutorizacionUrl').val();

    if (!$('#txtMotivo').val()) {
        $('#valMotivo').html('Debe indicar un motivo para revisión.');
        $('#valMotivo').show();
    }
    else {

        $('.modal').find('button#btnAceptar').prop('disabled', 'true');

        $.ajax({
            type: "POST",
            //url: "/Pedidos/SolicitarAutorizacion",
            url: url,
            data: {
                __RequestVerificationToken: token,
                no_cia: no_cia,
                no_docu: no_docu,
                no_linea: linea,
                cantidad: cantidad,
                motivo: motivo
            },
            success: function (result) {

                if (result != true) {
                    $("#txtError").html($("#txtError").html + "<br><br>Error: " + result);
                    $('.modal').find('button#btnAceptar').prop('disabled', false);
                }
                else {
                    $('#errorModalLabel').html('<i class="fa fa-info-circle"></i>&nbsp;Artículo enviado a revisión');    
                    $("#txtError").html("La solicitud del artículo " + $("#arti_" + linea).html() + " - " + $("#desc_" + linea).html() + " ha sido enviada a revisión");
                    $('#btnAceptar').hide();
                    $('#spanMotivo').hide();
                    $('#btnCerrar').html("Cerrar");
                    $('#row_' + linea).remove();
                    $('.modal').find('button#btnAceptar').prop('disabled', false);
                }
            },
            error: function (error) {
                $("#txtError").html("Error: Por favor refresque la pantalla o vuelva a iniciar sesión.");
                $("#txtError").show();
                $('#btnAceptar').hide();
            }
        })
    }
}

// Cargar filtros
var cargarFiltros = function () {
    var table = $('#dataTable').DataTable();

    // tiendas
    var sTienda = $('#selectTienda');
    sTienda.append('<option value="">Todas las tiendas</option>');
    sTienda.on('change', function () {
        table.column(0)
            .search($(this).val())
            .draw();
    });

    table.column(0).data().unique().sort().each(function (d) {
        sTienda.append('<option value="' + d + '">' + d + '</option>')
    });

    // familias
    var sFamilia = $('#selectFamilia');
    sFamilia.append('<option value="">Todas las familias</option>');
    sFamilia.on('change', function () {
        table.column(1)
            .search($(this).val())
            .draw();
    });

    table.column(1).data().unique().sort().each(function (d) {
        sFamilia.append('<option value="' + d + '">' + d + '</option>')
    });
}

// Seleccionar todos los pedidos de la lista
var seleccionarTodo = function () {
    var chequeado = false;
    if ($('#btnSeleccionarTodo').html().indexOf("fa-check-square") > -1 /*$('#btnSeleccionarTodo').html().includes("fa-check-square")*/) {
        $('#btnSeleccionarTodo').html('<i class="fa fa-square"></i> Desmarcar todo');
        $('#btnSeleccionarTodoF').html('<i class="fa fa-square"></i> Desmarcar todo');
        chequeado = true;
    }
    else {
        $('#btnSeleccionarTodo').html('<i class="fa fa-check-square"></i> Seleccionar todo');
        $('#btnSeleccionarTodoF').html('<i class="fa fa-check-square"></i> Seleccionar todo');
        chequeado = false;
    }

    $(':checkbox').each(function () {
        this.checked = chequeado;
    });
}

// rechazar Artículos
var rechazarArticulosPedido = function () {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    var url = $('#hiddenAprobarRechazarUrl').val();
    var urlGen = $('#hiddenAutorizarUrl').val();
    var docus = "";
    var lineas = "";

    // obtiene los documentos y líneas de los artículos seleccionados

    $(':checkbox').each(function () {
        if (this.checked == true) {
            //var row = this.parentNode.parentNode.parentNode;
            //var nombreLinea = row.cells[6].id;
            var doclinea = this.id.split("_");
            docus = docus.concat(doclinea[1], ",");
            lineas = lineas.concat(doclinea[2], ",");
        }
    });

    if (docus.length <= 0) {

        $("#txtErrorConfirmar").html("Error: Por favor seleccione algún registro del listado.");
        $("#txtErrorConfirmar").show();

    }
    else {

        docus = docus.slice(0, -1);
        lineas = lineas.slice(0, -1);

        $('.modal').find('button#btnRechazar').prop('disabled', 'true');

        $.ajax({
            type: "POST",
            //url: "/Pedidos/AutorizarArticulosPedido",
            url: url,
            data: {
                __RequestVerificationToken: token,
                docus: docus,
                lineas: lineas,
                estadoAutorizacion: "R"
            },
            success: function (result) {
                if (result == true) {
                    window.location.href = urlGen;
                }
                else {
                    $("#txtErrorConfirmar").html("Error: " + result);
                    $("#txtErrorConfirmar").show();
                    $('.modal').find('button#btnRechazar').prop('disabled', false);
                }

            },
            error: function (error) {
                $("#txtErrorConfirmar").html("Error: Por favor refresque la pantalla o vuelva a iniciar sesión.");
                $("#txtErrorConfirmar").show();
                $('.modal').find('button#btnRechazar').prop('disabled', false);
            }
        });
    }

}


var confirmarAprobar = function () {
    $("#txtErrorAprobar").hide();
    $('#aprobarModal').modal('show');
}

// Aprobar Artículos
var aprobarArticulosPedido = function () {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    var url = $('#hiddenAprobarRechazarUrl').val();
    var urlGen = $('#hiddenAutorizarGenUrl').val();
    var docus = "";
    var lineas = "";

    // obtiene los documentos y líneas de los artículos seleccionados

    $(':checkbox').each(function () {
        if (this.checked == true) {
            //var row = this.parentNode.parentNode.parentNode;
            //var nombreLinea = row.cells[6].id;
            var doclinea = this.id.split("_");
            docus = docus.concat(doclinea[1], ",");
            lineas = lineas.concat(doclinea[2], ",");
        }
    });

    if (docus.length <= 0) {

        $("#txtErrorAprobar").html("Error: Por favor seleccione algún registro del listado.");
        $("#txtErrorAprobar").show();

    }
    else {

        $('.modal').find('button#btnAprobar').prop('disabled', 'true');
        docus = docus.slice(0, -1);
        lineas = lineas.slice(0, -1);

        $.ajax({
            type: "POST",
            //url: "/Pedidos/AutorizarArticulosPedido",
            url: url,
            data: {
                __RequestVerificationToken: token,
                docus: docus,
                lineas: lineas,
                estadoAutorizacion: "A"
            },
            success: function (result) {
                if (result == true) {
                    window.location.href = urlGen;
                }
                else {
                    $("#txtErrorAprobar").html("Error: " + result);
                    $("#txtErrorAprobar").show();
                    $('.modal').find('button#btnAprobar').prop('disabled', false);
                }

            },
            error: function (error) {
                $("#txtErrorAprobar").html("Error: Por favor refresque la pantalla o vuelva a iniciar sesión.");
                $("#txtErrorAprobar").show();
                $('.modal').find('button#btnAprobar').prop('disabled', false);
            }
        });
    }

}

// Eliminar una línea de la autorización
var eliminarAutorizacion = function () {
    $('.modal').find('button#btnEliminar').prop('disabled', 'true');

    var form = $('#__AjaxAntiForgeryForm');
    var no_docu = $('#hiddenNoDocu').val();
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    var url = $('#hiddenDeleteUrl').val();


    var doclinea = no_docu.split("_");
    no_docu = doclinea[0];
    var no_linea = doclinea[1];


    $.ajax({
        type: "POST",
        //url: "/Pedidos/EliminarAutorizacion",
        url: url,
        data: {
            __RequestVerificationToken: token,
            no_docu: no_docu,
            no_linea: no_linea
        },
        success: function (result) {
            if (result == true) {
                $('#confirmacionModal').modal('hide');
                $('#row_' + no_docu + '_' + no_linea).remove();
            }
            else {
                $("#txtErrorConfirmar").html("Error: " + result);
                $("#txtErrorConfirmar").show();
                $('.modal').find('button#btnEliminar').prop('disabled', false);
            }

        },
        error: function (error) {
            $("#txtErrorConfirmar").html("Error: Por favor refresque la pantalla o vuelva a iniciar sesión.");
            $("#txtErrorConfirmar").show();
            $('.modal').find('button#btnEliminar').prop('disabled', false);
        }
    })

}


///////////////////////////
// Repartir Pedidos
///////////////////////////
// PrepararRepartir
var prepararRepartir = function (linea, cantidad) {

    var resurtido = $('#resurtido_' + linea).html();
    resurtido = resurtido.replace(',', '').replace('&nbsp;', '');
    $('#imgRepartir').attr('src', $('#img_' + linea).attr('src'));
    $('#articuloRepartir').html($('#arti_' + linea).html() + ' - ' + $('#desc_' + linea).html());
    $('#unidadResurtido').html(resurtido);
    $('#unidadesPendientes').html(resurtido - cantidad);

    $('#unidadesPendientes').removeClass('text-success');
    $('#unidadesPendientes').addClass('text-danger');
    $('#unidadesPendLabel').removeClass('text-success');
    $('#unidadesPendLabel').addClass('text-danger');

    $('.text-repartir').each(function () {
        var centro = this.id.split('_')[1];

        if ($('#hiddenCentro').val() == centro) {
            this.value = cantidad;
        }
        else
            this.value = 0;
    });
}

// Actualizar los pendientes por Repartir
var actualizarPendRepartir = function () {

    var resurtido = parseInt($('#unidadResurtido').html());

    var total = 0;
    $('.text-repartir').each(function () {

        if (!(/^\d+$/.test(this.value))) {
            alert('La cantidad ingresada debe ser un número válido sin decimales');
            this.value = '';
            this.focus();
        }
        else {
            var cant = parseInt(this.value);
            total = total + cant;
        }
    });

    if (resurtido - total == 0) {
        $('#unidadesPendientes').html(0);
        $('#unidadesPendientes').removeClass('text-danger');
        $('#unidadesPendientes').addClass('text-success');
        $('#unidadesPendLabel').removeClass('text-danger');
        $('#unidadesPendLabel').addClass('text-success');
    }
    else {
        $('#unidadesPendientes').html(resurtido - total);
        $('#unidadesPendientes').removeClass('text-success');
        $('#unidadesPendientes').addClass('text-danger');
        $('#unidadesPendLabel').removeClass('text-success');
        $('#unidadesPendLabel').addClass('text-danger');

    }
}


// Repartir pedido
var repartirPedido = function () {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    var url = $('#hiddenGenerarSolicitudRepartirUrl').val();
    var no_docu = $('#hiddenNoDocu').val();
    var no_linea = $('#hiddenNoLinea').val();
    var centros = "";
    var cantidades = "";

    // obtiene los centros y cantidades de los artículos seleccionados

    $('.text-repartir').each(function () {
        if (this.value && this.value != "0") {
            var rowCentro = this.id.split("_");
            centros = centros.concat(rowCentro[1], ",");
            cantidades = cantidades.concat(this.value, ",");
        }
    });

    if ($('#unidadesPendientes').html() !== "0") {
        $("#txtErrorRepartir").html("Error: La suma de las cantidades a repartir tiene que ser igual a la unidad de resurtido.");
        $("#txtErrorRepartir").show();
    }

    else if (centros.length <= 0) {
        $("#txtErrorRepartir").html("Error: Por favor ingrese las cantidades para cada centro.");
        $("#txtErrorRepartir").show();
    }
    else {

        centros = centros.slice(0, -1);
        cantidades = cantidades.slice(0, -1);

        $('.modal').find('button#btnRepartirPedido').prop('disabled', 'true');

        $.ajax({
            type: "POST",
            //url: "/Pedidos/GeneraSolicitudRepartir",
            url: url,
            data: {
                __RequestVerificationToken: token,
                no_docu: no_docu,
                no_linea: no_linea,
                centros: centros,
                cantidades: cantidades
            },
            success: function (result) {

                if (result.length > 0) {
                    if (result[0].error != 'null' && result[0].error != null) {
                        $("#txtErrorRepartir").html("Error: " + result[0].error);
                        $("#txtErrorRepartir").show();
                        $('.modal').find('button#btnRepartirPedido').prop('disabled', false);
                    }
                    else {
                        // mostrar los traslados generados
                        $('#errorModalLabel').html('<i class="fa fa-share-square"></i>&nbsp;Solicitud generada satisfactoriamente');
                        $('#btnCerrar').html('Cerrar');
                        $('#btnCerrar').removeClass('btn-secondary');
                        $('#btnCerrar').addClass('btn-danger');
                        $('#btnAceptar').hide();
                        $('#spanMotivo').hide();
                        $('#valMotivo').hide();
                        $('#txtError').html('Se han ingresado los traslados: ' + result[0].noTraslados);
                        $('#repartirModal').modal('hide');
                        $('#row_' + no_linea).remove();
                        $('#errorModal').modal('show');
                        $('.modal').find('button#btnRepartirPedido').prop('disabled', false);
                    }
                }

                if (result == true) {
                    window.location.href = urlGen;
                }
                else {
                }

            },
            error: function (error) {
                $("#txtErrorRepartir").html("Error: Por favor refresque la pantalla o vuelva a iniciar sesión.");
                $("#txtErrorRepartir").show();
                $('.modal').find('button#btnRepartirPedido').prop('disabled', false);
            }
        });
    }

}


