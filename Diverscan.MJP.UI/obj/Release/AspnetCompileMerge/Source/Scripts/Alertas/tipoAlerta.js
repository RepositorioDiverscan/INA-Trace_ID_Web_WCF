
        function alerta(){
            //un alert
            alertify.alert("<b>Blog Reaccion Estudio</b> probando Alertify", function () {
                //aqui introducimos lo que haremos tras cerrar la alerta.
                //por ejemplo -->  location.href = 'http://www.google.es/';  <-- Redireccionamos a GOOGLE.
            });
        }
			
function confirmar(){
    //un confirm
    alertify.confirm("<p>Aquí confirmamos algo.<br><br><b>ENTER</b> y <b>ESC</b> corresponden a <b>Aceptar</b> o <b>Cancelar</b></p>", function (e) {
        if (e) {
            alertify.success("Has pulsado '" + alertify.labels.ok + "'");
        } else { alertify.error("Has pulsado '" + alertify.labels.cancel + "'");
        }
    });
    return false;
}
			
function datos(){
    //un prompt
    alertify.prompt("Esto es un <b>prompt</b>, introduce un valor:", function (e, str) { 
        if (e){
            alertify.success("Has pulsado '" + alertify.labels.ok + "'' e introducido: " + str);
        }else{
            alertify.error("Has pulsado '" + alertify.labels.cancel + "'");
        }
    });
    return false;
}
			
function notificacion(mensaje){
    alertify.log(mensaje); 
    return false;
}
			
function ok(mensaje) {
    alertify.success(mensaje); 
    return false;
}
			
function error(mensaje) {
    alertify.error(mensaje); 
    return false; 
}
