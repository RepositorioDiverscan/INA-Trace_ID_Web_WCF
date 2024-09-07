window.onload = iniciar();

var personas = [];

function iniciar()
{
   /* document.getElementById("btnAgregar").addEventListener("click", clickBtnAgregar); 
  document.getElementById("btnBorra").addEventListener("click", clickBtnBorrar); 
  var btnAgregar = 
   btnAgregar.addEventListener("click", clickBtnAgregar); 
   matriz(); 
  setTimeout(mostrarNotas, 3000);
  setTimeout(() => alert("prueba timers"), 3000); 
  var temporiza = setInterval(() => {alert("Tiempo Cumplido")}, 2000);
  alert(temporiza);
  setTimeout(() => {clearInterval(temporiza);}, 10000);   */
  const nom = "Lolo";
  let ape = "lulu";
  //let ape = "yyy";
  var apo = "lelo";
  //nom = "xxxx"
  cargarJson();

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









async function cargarUrl(url)
{
  let respuesta = await fetch(url);
  return respuesta.json();
}

async function cargarJson()
{
  let json = await cargarUrl('https://jsonplaceholder.typicode.com/todos/1');

  // fetch('https://jsonplaceholder.typicode.com/todos/1').then(response => response.json()).then(jsonCargado);
  //let resultado = await fetch('https://jsonplaceholder.typicode.com/todos/1');
 
  jsonCargado(json);
  

 /* let promesa = new Promise(resolver =>
                {
                  setTimeout(() =>
                  {
                    let json = 
                    {
                      "userId": 1,
                      "id": 1,
                      "title": "delectus aut autem",
                      "completed": false
                    }
                    resolver(json);
                  }, 5000);
                });

                promesa.then(json => {jsonCargado(json);})  */
}

function jsonCargado(json)
{
  let span01 = document.getElementById("txtNombrePost");
  let span02 = document.getElementById("completo");
  var estado = "";
  span01.innerText = json.title;

  span02.innerText = json.completed ? "COMPLETO" : "INCOMPLETO";

  /*if (json.completed)
  {
   estado = "COMPLETO";
  }
  else
  {
    estado = "INCOMPLETO";
  }

  span02.innerText = estado;  */

  console.log(json);
}

function mostrarNotas()
{
   alert("123456");

  var listado = document.getElementById("notas");

  var notas = [];
  if (localStorage.notas)
    notas = JSON.parse(localStorage.notas);

  var html = "";
  for (var nota of notas)
  {
    html += nota + "<br/>";
  }
 
  listado.innerHTML = html;
}


function muestraPersona()
{
  var persona =
   {
     "nombre": nombre,
     "apellido": apellido,
     "telefono": telef,
     "email": email,
     "direccion": "Av.siempre viva 123",
     "empleo":
     {
       "nombre":"programador",
       "localidad": "españa"
     }
   }
//alert(persona.empleo.nombre);
}

function clickBtnAgregar()
{
  nota = document.getElementById("txtNota").value;

  var notas = [];
  if (localStorage.notas)
    notas = JSON.parse(localStorage.notas);

  notas.push(nota);
  localStorage.notas = JSON.stringify(notas);

  mostrarNotas();
}

function clickBtnBorrar()
{
  localStorage.clear();
  mostrarNotas();
  alert("borrado");
}
 
  /* var nombre = document.getElementById("txtNombre").value;
  var apellido = document.getElementById("txtApellido").value;
  var telef = document.getElementById("txtTelefono").value;
  var email = document.getElementById("txtEmail").value;
  var persona =
  {
    "nombre": nombre,
    "apellido": apellido,
    "telefono": telef,
    "email": email,
    "direccion": "Av.siempre viva 123",
    "empleo":
    {
      "nombre":"programador",
      "localidad": "españa"
    }
  }
  personas.push(persona);
  mostrarPersonas();  */


  //tareas.push(tarea);* /
 
  //mostrarTareas();


function mostrarTareas()
{
  var listado = document.getElementById("listado");
  var html = "";
  for (var tarea of tareas)
  {
    html += tarea + "<br/>";
  }
  listado.innerHTML = html;
    
}

function mostrarPersonas()
{
  var listado = document.getElementById("listado");
  var html = "";
  for (var persona of personas)
  {
    html += persona.nombre + ' ' + persona.apellido + ' ' + persona.telefono + ' ' + persona.email + "<br/>";
  }
  listado.innerHTML = html;
}

function matriz()
{
  var nombre = [];  // arreglo vacío
    
    /* nombres[0] = "Lucas";
    nombres[1] = "José";
    nombres[2] = "María";  */

    nombre.push("Lucas");
    nombre.push("José");
    nombre.push("María");
    nombre.push("Eduardo");

/*    for (var i=0;i < nombre.length; i++)
    {
      alert(nombre[i]);
    } 

    for (var nombres of nombre)
      alert (nombres)  */
    
}




/* function clickBtnCalcular()
{
    var txtPeso = document.getElementById("txtPeso");
    var peso = txtPeso.value;

    var txtAltura = document.getElementById("txtAltura");
    var altura = txtAltura.value;

    var imc = peso/(altura ** 2)

    alert("Indice de Masa Corporal --> " + imc.toFixed(2)); 
} 

function lazoWhile()
{
  var contador = 0;
  while (contador < 3)
  {
    alert("Valor -->" + contador);
    contador ++
  }
  alert ("terminó...while");
}

function lazoDo()
{
  var contador = 0;
  do
  {
    alert("Valor -->" + contador);
    contador ++
  } while (contador < 3)

  alert ("terminó...do-while");
}

function sumaHastaDiez()
{
  var contador = 1;
  var sumatoria = 1;
  debugger;
  while (contador <= 100)
  {
    //alert("Valor -->" + contador);
    sumatoria *= contador;
    contador ++;
  }
  alert ("terminó...!100 = " + sumatoria);
}

function LazoFor()
{
  var mnsj = "";
  var sumatoria = 0;
  for (var i=0;i<1000;i++)
  {
    if (i%2 == 0)
     sumatoria += i;
  }

  alert("sumatoria de numeros pares, hasta mil es: " + sumatoria);
}   */





/* var nomusuario = prompt("Ingrese Nombre:");
var edad = prompt("Ingrese edad:");
var telef = prompt("Ingrese telefóno:");
var mensaje = "Nombre: " + nomusuario + " Edad: " + edad + " Tel: " + telef ;

alert(mensaje); */  
// comentario de linea
/*  comentario de bloque 

var contador = 0;
contador ++;  //  suma 1 automaticamente
contador --;  // resta 1 automaticamente
contador += 10;  // suma 10 automaticamente
contador -= 10;  // resta 10 automaticamente
contador *= 2;  // multiplica 2 automaticamente
contador /= 2;  // divide 2 automaticamente   */


/* var edad = prompt("Ingrese edad:");
var estatura = prompt("Ingrese su estatura en Mt.")
var hijo = prompt("Eres hijo del dueño(S/N):")
if ((edad >= 13 && estatura >= 1.20) || hijo.toUpperCase().trim() == "S")
{
    alert("Puede ingresar al juego")
}
else
{
    alert("NO Puede ingresar al juego")
}  */

  //  == igual
  //  != diferente

/*  function calcularSuperficie(ancho, alto)
  {
    /*var ancho = 10;
    var alto = 15;  
    var resultado = ancho * alto;
   return resultado;
  }

  var ancho = prompt("Acho del terreno --> ");
  var alto =  prompt("Alto del terreno --> ");
  var resul = calcularSuperficie(ancho,alto);
  alert("Area del terreno: " + resul);  */




 