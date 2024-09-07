window.onload = iniciar();

function iniciar()
{
  let btnSelecciona = document.getElementById("btnCargar");
  btnSelecciona.addEventListener  ('click', elijePais);

  setInterval(() => {
    var reloj = document.getElementsByClassName("reloj")[0];
    var horaActual = new Date();
    var hora = horaActual.getHours();
    if (hora < 10)
      hora = `0${hora}`

    var minu = horaActual.getMinutes();
    if (minu < 10)
      minu = `0${minu}`

    var segu = horaActual.getSeconds();
    if (segu < 10)
      segu = `0${segu}`

    reloj.innerHTML = `${hora}:${minu}:${segu}`;
  }, 1000);  
}

async function elijePais()
{
  let pais  = document.getElementById('selectPais').value;
  let fecha = document.getElementById('inputFecha').value;

  let url = `https://api.covid19tracking.narrativa.com/api/${fecha}/country/${pais}`;
  let json = await cargarUrl(url);
  
  let estadisticas = json.dates[fecha].countries[pais];

  document.getElementById('tc').innerHTML = estadisticas.today_confirmed;
  document.getElementById('td').innerHTML = estadisticas.today_deaths;
  document.getElementById('th').innerHTML = estadisticas.today_hospitalised_patients_with_symptoms; 
  document.getElementById('ic').innerHTML = estadisticas.today_intensive_care;
  document.getElementById('nc').innerHTML = estadisticas.today_new_confirmed;
  document.getElementById('nd').innerHTML = estadisticas.today_new_deaths;
  document.getElementById('nh').innerHTML = estadisticas.today_new_hospitalised_patients_with_symptoms;
  document.getElementById('ni').innerHTML = estadisticas.today_new_intensive_care;
  document.getElementById('no').innerHTML = estadisticas.today_new_open_cases;
  document.getElementById('nr').innerHTML = estadisticas.today_new_recovered;
  document.getElementById('nt').innerHTML = estadisticas.today_new_total_hospitalised_patients;
}

async function cargarUrl(url)
{
   let respuesta = await fetch(url);
   return respuesta.json();
}

function MuestraVentana()
{
    $("#TblCantidad").show();
}