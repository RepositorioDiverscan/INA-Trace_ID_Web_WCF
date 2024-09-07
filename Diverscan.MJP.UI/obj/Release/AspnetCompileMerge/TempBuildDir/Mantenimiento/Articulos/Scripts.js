var urlMaestroUbi = "wf_MaestroUbicaciones.aspx";
var buscar = '';

function buscarUbicacion() {
    buscar = document.getElementById("ctl00_MainContent_WinUsuarios_C_txtSearchUbicacion").value;
    function DisplayLoadingImage1() {
        document.getElementById("loading1").style.display = "block";
    }
}

function getExcel() {

    const date = new Date();

    let day = date.getDate();
    let month = date.getMonth() + 1;
    let year = date.getFullYear();

    let currentDate = `${day}-${month}-${year}`;

    $.post(urlMaestroUbi, {
        Metodo: "Buscar",
        Buscar: buscar,
    }, function (data, error) {

        var base64 = data;

        //Poner la primera letra del string en mayuscula
        //let nombreBod = document.getElementById(urlMaestroUbi).Value = document.getElementById('<%=nombreBodega.ClientID%>').Value;

        //Se crea el nombre del archivo
        let nombreArchivo = "Reporte Ubicaciones " + currentDate + ".xlsx";

        // Convertimos la cadena base 64 a un array de bytes
        var bytes = atob(base64);
        var byteArray = new Uint8Array(bytes.length);
        for (var i = 0; i < bytes.length; i++) {
            byteArray[i] = bytes.charCodeAt(i);
        }
        // Creamos un objeto Blob a partir del array de bytes
        var blob = new Blob([byteArray], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });

        // Creamos un enlace de descarga y simulamos un clic para descargar el archivo
        var url = window.URL.createObjectURL(blob);
        var a = document.createElement('a');
        a.href = url;
        a.download = nombreArchivo;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
    })
}


