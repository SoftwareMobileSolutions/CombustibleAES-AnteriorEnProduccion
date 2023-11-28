//Nombre de usuario en la barra de menu

var produccion = 1;
$(document).ready(function () {
    //$("#username").html(usuario.username);
    ////////////////////////////GenerarMenu();
    ResizeIframe();
});

//Cambiar el tamanio a iframe
$(window).resize(function() {
    ResizeIframe();
});
//Lleva la lista de módulos a los que el usuario tiene permiso
function MenuModulos() {
    return menu;
}

//Actualizar los vehículos del estado actual para los circulos
function UpdateEstadoActualMobiles() {
    typeof (document.getElementById('sms_content_iframe').contentWindow.refreshGridEstadoActual) === "function" &&
    document.getElementById('sms_content_iframe').contentWindow.refreshGridEstadoActual();
}


var W, H, B;  
function ResizeIframe() {
    W = $(".navbar-inverse").width();  //Width de la barra menu
    H = $(".navbar-inverse").height(); //Height de la barra menu
    B = $(".full-width").height(); //Heigth total del Body
    $("#sms_content_iframe").attr("style", "height: " + (B - H) + "px; overflow:hidden;overflow-x:hidden;overflow-y:hidden;width:100%;position:absolute;top:0px; margin-top: " + H + "px; left:0px;right:0px;bottom:0px");
}

//Click en el menu, reemplaza el iframe
function MenuChange(MenuItem) {
    //console.log(MenuItem);
    //console.log($(MenuItem).attr("url"));
    //$("#sms_content_iframe").attr("src", $(MenuItem).attr("url"))
    //LoadingMenu();
    //setTimeout(function () { Loaded(); }, 5000);
    $("#VC_content_iframe").attr("src", $(MenuItem).attr("url"));
}

function MenuConfigfn() {
    return menuConfig;
}
//GENERAR EL MENU
var menu;
var menuHTML = "";
var categoria = "";
var subcategoria = "";
var subsubcategoria = "";
var styleicon = "";
var calclength = "435px";
var menuConfig = [];

function startTimer(duration) {
    var timer = duration, minutes, seconds;
    setInterval(function () {
        minutes = parseInt(timer / 60, 10)
        seconds = parseInt(timer % 60, 10);

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        $("#segundos").text(String(seconds))  /*minutes + ":" + */ ;
        console.log(seconds);
        seconds === "00" && (location = "/Login/LogOut");
        if (--timer < 0) {
            timer = duration;
        }
    }, 1000);
}

function sessionexpired() {
       
     startTimer((60 * 0.18));

    $.jGrowl('Su session ha expirado.<br /> Redireccionando en <label id="segundos">10</label> segundos', { sticky: true, theme: 'growl-info', header: 'AVISO', position: 'bottom-right', afterOpen: function () { jGrowjQUIClearMsg(); } });
}

function GenerarMenu() {
    //$.post("../Home/GenerarMenu", {},
    $.post((window.location.hostname == "localhost" ? ".." : "../../CombustibleAES") + "/Home/GenerarMenu", {},
           function (data) {
               menu = data;

                        categoria+= '<div class="dropdown profile-element" style="padding-left: 20px;">'+
                        '<span>'+
                           '<img alt="image" class="img-circle" src="' + (window.location.hostname == "localhost" ? "" : "../../CombustibleAES") + '/Imagenes/Login/logo-Vales.png" />'+
                        '</span>'+
                        '<a data-toggle="dropdown" class="dropdown-toggle" href="#">'+
                            '<span class="clear" style="padding-left: 10px;">'+
                                '<span class="block m-t-xs">'+
                                    '<strong class="font-bold" ><span id="UserName"></span></strong>'+
                                '</span> <span class="text-muted text-xs block" id="usertypeLogin"><b class="caret"></b></span>'+
                            '</span>'+
                        '</a>'+
                        '<ul class="dropdown-menu animated fadeInRight m-t-xs">'+
                            '<li><a href="' + (window.location.hostname == "localhost" ? ".." : "../../CombustibleAES") + '/Login/LogOut"><i class="fa fa-sign-out"></i>Salir</a></li>'+
                         '</ul>'+
                    '</div>';
                         
                        for (var i = 0, len1 = menu.length; i < len1; i++) {
                            
                        categoria+=' <li>'+
                       '<a href="#"><i class="'+menu[i].Icon+'"></i> <span class="nav-label">'+menu[i].Name+'</span> <span class="fa arrow"></span></a>'+
                          '<ul class="nav nav-second-level">';
                       for (var j = 0, len = menu[i].Children.length; j < len; j++) {
                           //categoria+='<li><a href="'+(menu[i].Children[j].PageUrl)+'"><i class="'+menu[i].Children[j].Icon+'"></i>'+menu[i].Children[j].Name+'</a></li>';
                           categoria += '<li><a href="' + (menu[i].Children[j].PageUrl).replace("../", (window.location.hostname != "localhost" ? "../../CombustibleAES/" : "../")) + '"><i class="' + menu[i].Children[j].Icon + '"></i>' + menu[i].Children[j].Name + '</a></li>';
                       }
                    categoria += '</ul></li>';
                   menuHTML = menuHTML + categoria;
                   categoria = "";
             
               }
              
               $('#side-menu').html($("#side-menu").html()+menuHTML);
               $('#side-menu').metisMenu();
                 $('#UserName').html(usuario.username);
              $('#usertypeLogin').html(usuario.userType);
           });
   
}

var ventana;
function jspanel_enviocomandos(url) {

   typeof ventana !== "undefined" &&  ( typeof ventana.close === "function"  && ventana.close());
    ventana = $.jsPanel({
        /*load: {
            url: "../Comandos/Comandos"
        },*/
        title:    '<label>Envío de comandos</label>',
        iframe: {
            src:    url,
            id:     "myFrame",
            style:  {"display": "none", "border": "10px solid transparent"},
            width:  '100%',
            height: '100%'
        },
        offset:   { top: 25 }, 
       // draggable: { containment: "body" },
        callback: function () {
            document.getElementById("myFrame").onload = function(){
                $("#myFrame").fadeIn(2000);
            }
        },

        size:     {width: $(window).width() -20, height: ($(window).height() -84-30 ) },
        position: {left: "center", top: "center"},
        theme:    "black"
    });
}
//Generar panel de administracion
var panel;
var menuopc = "";
var txtmenu = "";
var styleicon = "";
var len = 0;
function GenerarPanelAdmonMenu() {
    $.post("Home/PanelMenuAdmon", {}, function (data) {
        panel = data;
        len = panel.length;
        for (var i = 0; i < len; i++) {
            txtmenu = panel[i].Description.length > 30 ? (panel[i].Description.substring(0, 30) + "...") : panel[i].Description; //Limita a un maximo de 30 caracteres para la descripcion
            styleicon = panel[i].Icon.substring(0, 3) === "sms" ? "style= \"font-size: 20px; margin-top: -2.5px; margin-left: -2.8px; \"" : "";

            menuopc = menuopc +
            '<li class="subpanel"  url="' + panel[i].Urlpage + '" onclick="MenuChange(this);">' +
                '<a >' +
                    '<button type="button" class="user-face btn btn-success btn-icon" style="border-radius: 0px; -webkit-border-radius: 0px; -moz-border-radius: 0px;"><i ' + styleicon + ' class="' + panel[i].Icon + ' kontrol-i"></i></button>' +
                            '<strong>' + panel[i].Name + '</strong>' +
                            '<span> ' + txtmenu + '</span>' +
                '</a>' +
            '</li>';
        }
        $("#paneladmon").html(menuopc); //Creacion de los modulos del menu superior(Panel Admon)
        $("#paneladmoncount").html(len); //Cantidad de modulos del menu superior (Panel Admon)
    });
}
var emergencias = [];
var DataAlertas;
var ContextMenuDataAlertas = [];
//Obtener alertas de Parent a Hijos
function ObtenerDataAlertas() {
    return DataAlertas;
}

function actualizarGridEstadoActual() {
    typeof (document.getElementById('sms_content_iframe').contentWindow.refresh_btnPanico) === "function" &&
    document.getElementById('sms_content_iframe').contentWindow.refresh_btnPanico();
}

function actualizarMarkersEstadoActualMapa() {
      typeof (document.getElementById('sms_content_iframe').contentWindow.btnPanico_ForzarDesdeAlertasHome) === "function" &&
            document.getElementById('sms_content_iframe').contentWindow.btnPanico_ForzarDesdeAlertasHome(emergencias);
}

//function ObtenerBotonesEmergencia() {
//    return emergencias;
//}

//function GenerarAlertas() {
    
//    //GENERAR LAS ALERTAS EN EL MENU
//    var alertas = "";
//    var styleicon2 = "";
//    //var alertaEmergencia = "<span class='sEmergencia'>Botón de Emergencia</span>";
//    var alertaEmergencia = '';
//    var alertaEmergenciaCounter = 0;
//    //var alertaExceso = "<span class'sExcesoVel'>Exceso de velocidad</span>";
//    var alertaExceso = '';
//    var alertaExcesoCounter = 0;
//    //var alertaLicencia = "<span class='sEstLic'>Estado licencia de conducir</span>";
//    var alertaLicencia = '';
//    var alertaLicenciaCounter = 0;
//    //var alertaTarjeta = "<span class='sEstTarjeta'>Estado de tarjeta de circulación</span>";
//    var alertaTarjeta = '';
//    var alertaTarjetaCounter = 0;
//    //
//    var alertaLowBattery = '';
//    var alertaLowBatCounter = 0;
//    //
//    var alertaDescBateria = '';
//    var alertaDescBateriaCounter = 0;

//    $.post("Home/GenerarAlertas", {}, function (data) {
//        if (typeof data === "string") {
//            sessionexpired();
//        }
//        else{
//            DataAlertas = data;
//            ContextMenuDataAlertas = _.groupBy(data, 'Mobileid');
//        emergencias = [];
//        for (var i = 0, len = data.length; i < len; i++) {
//            styleicon2 = typeof data[i].Iconcolor === 'undefined' ? "" : data[i].Iconcolor.substring(0, 3) === "sms" || data[i].Iconcolor.substring(0, 2) === "bf" ? "style= \"font-size: 20px; margin-top: -2.5px; margin-left: -2.8px; \"" : "";
//            //Exceso de velocidad
//            if (data[i].Iconcolor === "sms-car15") {
//                //alertas = alertas +
//                alertaExcesoCounter++;
//                alertaExceso = alertaExceso +
//                    /*'<li class="eventos ExcesoVel" style="display: none;">' +*/
//                    /*'<li class="eventos ExcesoVel">' +*/
//                    '<li class="eventos" id="'+ data[i].Statuslogid + '" title="Presione click sobre el icono para mostrar en el mapa">' +
//                        '<a class="exceso_a">' +
//                            '<button type="button" class="user-face btn btn-warning2 btn-icon" style="border-radius: 0px; -webkit-border-radius: 0px; -moz-border-radius: 0px;"><i ' + styleicon2 + ' class="' + data[i].Iconcolor + ' kontrol-i"></i></button>' +
//                            '<strong class="exceso_head" onclick="chkAlertas(this);">' +
//                            '<label class="checkbox-inline" >' +
//						        '<div class="checker"><span class="" ><input type="checkbox" statusid="' + data[i].Statuslogid + '" class="styled chkstatusid"></span></div>' +
//							'</label>' +
//                            'EXCESO DE VELOCIDAD' +
//						    '</strong>' + 
//                            '<span><l>PLACA:</l>' + data[i].Plate +  "(" + data[i].Alias + ")" + "</span>" + '<br />' +
//                            '<span><l>FECHA GPS:</l> ' +  moment(moment(data[i].Dategps)).format('YYYY/MM/DD HH:mm:ss')   +  space(1) +  "</span>" + '<br />'  +
//                            '<span><l>VELOCIDAD:</l> ' + data[i].Speed + ' Km/h</b>'  +  space(1) +  "</span>" + '<br />'  +
//                            '<span><l>UBICACIÓN:</l> ' + data[i].Location  +  space(1) +  "</span>" + '<br />'  +
//                            '<span><l>Lat/Lng:</l> ' + data[i].Lat + ", " + data[i].Lng +  '</span>' + '<br />'  +
//                        '</a>' +
//                    '</li>';
//            }
//            //Informacion sobre licencias vencidas
//            else if (data[i].Iconcolor === "icon-vcard")
//            {
//                //alertas = alertas +
//                alertaTarjetaCounter++;
//                alertaTarjeta = alertaTarjeta +
//                    /*'<li class="eventos EstTarjeta" style="display: none;">' +*/
//                    /*'<li class="eventos EstTarjeta">' +*/
//                    '<li class="eventos">' +
//                        '<a>' +
//                            '<button type="button" class="user-face btn btn-info btn-icon" style="border-radius: 0px; -webkit-border-radius: 0px; -moz-border-radius: 0px;"><i ' + styleicon2 + ' class="' + data[i].Iconcolor + ' kontrol-i"></i></button>' +
//                            '<strong class="licencia_head" onclick="chkAlertas(this);">' +
//                            'LICENCIA' +
//                            '</strong>' +
//                            '<span>' + data[i].Bandera + '</span>' +
//                        '</a>' +
//                    '</li>';
//            }
//            else if (data[i].Iconcolor === "sms-licenciasvencidas")
//            {
//                //alertas = alertas +
//                alertaLicenciaCounter++;
//                alertaLicencia = alertaLicencia +
//                    /*'<li class="eventos EstLic" style="display: none;">' +*/
//                    /*'<li class="eventos EstLic">' +*/
//                    '<li class="evento">' +
//                        '<a>' +
//                            '<button type="button" class="user-face btn btn-info btn-icon" style="border-radius: 0px; -webkit-border-radius: 0px; -moz-border-radius: 0px;"><i ' + styleicon2 + ' class="' + data[i].Iconcolor + ' kontrol-i"></i></button>' +
//                            '<strong class="licencia_head" onclick="chkAlertas(this);">' +
//                            'LICENCIA VEHÍCULO' +
//                            '</strong>' +
//                            '<span>' + data[i].Bandera + '</span>' +
//                        '</a>' +
//                    '</li>';
//            }
//            //Low Battery
//            else if (data[i].Iconcolor === "sms-battery_low") {
//                alertaLowBatCounter++;
//                alertaLowBattery = alertaLowBattery +
//                    '<li class="eventos" id="' + data[i].Statuslogid + '" title="Presione click sobre el icono para mostrar en el mapa">' +
//                        '<a class="low_battery">' +
//                            '<button type="button" class="user-face btn btn-danger btn-icon" style="border-radius: 0px; -webkit-border-radius: 0px; -moz-border-radius: 0px;"><i ' + styleicon2 + ' class="' + data[i].Iconcolor + ' kontrol-i"></i></button>' +
//                            '<strong class="low_battery_head" onclick="chkAlertas(this);">' +
//                            '<label class="checkbox-inline" >' +
//						        '<div class="checker"><span class="" ><input type="checkbox" statusid="' + data[i].Statuslogid + '" class="styled chkstatusid"></span></div>' +
//							'</label>' +
//                            'BATERÍA BAJA' +
//                            '</strong>' +

//                            '<span><l>MÓVIL:</l> ' + data[i].Alias + " (" + data[i].Plate + ")" + "</span>" + '<br />' +
//                            '<span><l>FECHA GPS:</l> ' + moment(moment(data[i].Dategps)).format('YYYY/MM/DD HH:mm:ss') + space(1) + " </span>" + '<br />' +
//                            '<span><l>VELOCIDAD:</l> ' + data[i].Speed + ' Km/h</b>' + space(1) + " </span>" + '<br />' +
//                            '<span><l>UBICACIÓN:</l> ' + data[i].Location + '</span>' + space(1) + "</span>" + '<br />' +
//                            '<span><l>Lat/Lng:</l> ' + data[i].Lat + ", " + data[i].Lng + ' </span>' + '<br />' +
//                        '</a>' +
//                    '</li>';
//            }
//            //Desconexion de bateria
//            else if (data[i].Iconcolor === "bf-battery-warning") {
//                alertaDescBateriaCounter++;
//                alertaDescBateria = alertaDescBateria +
//                    '<li class="eventos" id="' + data[i].Statuslogid + '" title="Presione click sobre el icono para mostrar en el mapa">' +
//                        '<a class="Desc_Bateria">' +
//                            '<button type="button" class="user-face btn btn-danger btn-icon" style="border-radius: 0px; -webkit-border-radius: 0px; -moz-border-radius: 0px;"><i ' + styleicon2 + ' class="' + data[i].Iconcolor + ' kontrol-i"></i></button>' +
//                            '<strong class="Desc_Bateria_head" onclick="chkAlertas(this);">' +
//                            '<label class="checkbox-inline" >' +
//						        '<div class="checker"><span class="" ><input type="checkbox" statusid="' + data[i].Statuslogid + '" class="styled chkstatusid"></span></div>' +
//							'</label>' +
//                            'DESCONEXIÓN DE BATERÍA' +
//                            '</strong>' +

//                            '<span><l>MÓVIL:</l> ' + data[i].Alias + " (" + data[i].Plate + ")" + "</span>" + '<br />' +
//                            '<span><l>FECHA GPS:</l> ' + moment(moment(data[i].Dategps)).format('YYYY/MM/DD HH:mm:ss') + space(1) + " </span>" + '<br />' +
//                            '<span><l>VELOCIDAD:</l> ' + data[i].Speed + ' Km/h</b>' + space(1) + " </span>" + '<br />' +
//                            '<span><l>UBICACIÓN:</l> ' + data[i].Location + '</span>' + space(1) + "</span>" + '<br />' +
//                            '<span><l>Lat/Lng:</l> ' + data[i].Lat + ", " + data[i].Lng + ' </span>' + '<br />' +
//                        '</a>' +
//                    '</li>';
//            }
//            //Boton de panico
//            else {
//                emergencias.push(data[i]);
//                //alertas = alertas +
//                alertaEmergenciaCounter++;
//                alertaEmergencia = alertaEmergencia +
//                    /*'<li class="eventos Emergencia" style="display: none;">' +*/
//                    /*'<li class="eventos Emergencia">' +*/
//                    '<li class="eventos" id="' + data[i].Statuslogid + '" title="Presione click sobre el icono para mostrar en el mapa">' +
//                        '<a class="btnPanico_a">' +
//                            '<button type="button" class="user-face btn btn-danger btn-icon" style="border-radius: 0px; -webkit-border-radius: 0px; -moz-border-radius: 0px;"><i ' + styleicon2 + ' class="' + data[i].Iconcolor + ' kontrol-i"></i></button>' +
//                            '<strong class="btnPanico_head" onclick="chkAlertas(this);">' +
//                            '<label class="checkbox-inline" >' +
//						        '<div class="checker"><span class="" ><input type="checkbox" statusid="' + data[i].Statuslogid + '" class="styled chkstatusid"></span></div>' +
//							'</label>' +
//                            'BOTÓN DE EMERGENCIA' +
//                            '</strong>' +

//                            '<span><l>PLACA:</l> ' + data[i].Plate +  "(" + data[i].Alias + ")" + "</span>" + '<br />'  +
//                            '<span><l>FECHA GPS:</l> ' + moment(moment(data[i].Dategps)).format('YYYY/MM/DD HH:mm:ss') +   space(1) + " </span>" + '<br />'  +
//                            '<span><l>VELOCIDAD:</l> ' + data[i].Speed + ' Km/h</b>' +  space(1) +" </span>" + '<br />'  +
//                            '<span><l>UBICACIÓN:</l> ' + data[i].Location + '</span>' +  space(1) +"</span>" + '<br />'  +
//                            '<span><l>Lat/Lng:</l> ' + data[i].Lat + ", " + data[i].Lng + ' </span>' +  '<br />'  +
//                        '</a>' +
//                    '</li>';
//            }
//        }

//        //alertaEmergencia = '<span style="margin-left: 5px; cursor: pointer; font-size: 12pt;" onclick="Alerta1(this);">Botón de Emergencia (' + alertaEmergenciaCounter.toString() + ')</span><ul class="popup-messages">' + alertaEmergencia + '</ul><hr/>';
//        //alertaExceso = '<span style="margin-left: 5px; cursor: pointer; font-size: 12pt;" onclick="Alerta2(this);">Exceso de velocidad (' + alertaExcesoCounter.toString() + ')</span><ul class="popup-messages">' + alertaExceso + '</ul><hr/>';
//        //alertaLicencia = '<span style="margin-left: 5px; cursor: pointer; font-size: 12pt;" onclick="Alerta3(this);">Licencia de conducir (' + alertaLicenciaCounter.toString() + ')</span><ul class="popup-messages">' + alertaLicencia + '</ul><hr/>';
//        //alertaTarjeta = '<span style="margin-left: 5px; cursor: pointer; font-size: 12pt;" onclick="Alerta4(this);">Tarjeta de circulación (' + alertaTarjetaCounter.toString() + ')</span><ul class="popup-messages">' + alertaTarjeta + '</ul><hr/>';
//        //alertaEmergencia = '<div class="panel panel-default"><div class="panel-heading"><h6 class="panel-title panel-trigger"><a data-toggle="collapse" href="#question1">Botón de emergencia (' + alertaEmergenciaCounter.toString() + ')</a></h6></div> <div id="question1" class="panel-collapse collapse"> <div class="panel-body"> <ul class="popup-messages" id=""> ' + alertaEmergencia + ' </ul> </div> </div> </div>';
//        //alertaExceso = '<div class="panel panel-default"> <div class="panel-heading"><h6 class="panel-title panel-trigger"><a data-toggle="collapse" href="#question2">Exceso de velocidad (' + alertaExcesoCounter.toString() + ')</a></h6></div> <div id="question2" class="panel-collapse collapse"><div class="panel-body"> <ul class="popup-messages" id="">' + alertaExceso + '</ul> </div></div></div>';
//        //alertaLicencia = '<div class="panel panel-default"> <div class="panel-heading"><h6 class="panel-title panel-trigger"><a data-toggle="collapse" href="#question3">Licencia de conducir (' + alertaLicenciaCounter.toString() + ')</a></h6></div> <div id="question3" class="panel-collapse collapse"><div class="panel-body"> <ul class="popup-messages" id="">' + alertaLicencia + '</div> </div></div> </div>';
//        //alertaTarjeta = '<div class="panel panel-default"> <div class="panel-heading"><h6 class="panel-title panel-trigger"><a data-toggle="collapse" href="#question4">Tarjeta de circulación (' + alertaTarjetaCounter.toString() + ')</a></h6></div><div id="question4" class="panel-collapse collapse"><div class="panel-body"><ul class="popup-messages" id="">' + alertaTarjeta + '</div></div></div></div>';
//        alertaEmergencia = '<div class="panel-default"><div class="panel-heading"><h6 onclick="Alerta1(this);" class="panel-title panel-trigger"><a >Botón de emergencia (' + alertaEmergenciaCounter.toString() + ')</a></h6></div><div style="display: none !important;" class="Emergencia"><div class="panel-body"><ul class="popup-messages">' + alertaEmergencia + '</ul></div></div></div>';
//        alertaExceso = '<div class="panel-default"><div class="panel-heading"><h6 onclick="Alerta2(this);" class="panel-title panel-trigger"><a >Exceso de velocidad (' + alertaExcesoCounter.toString() + ')</a></h6></div><div style="display: none !important;" class="ExcesoVel"><div class="panel-body"><ul class="popup-messages">' + alertaExceso + '</ul></div></div></div>';
//        alertaLicencia = '<div class="panel-default"><div class="panel-heading"><h6 onclick="Alerta3(this);" class="panel-title panel-trigger"><a >Licencia de conducir (' + alertaLicenciaCounter.toString() + ')</a></h6></div><div style="display: none !important;" class="EstLic"><div class="panel-body"><ul class="popup-messages">' + alertaLicencia + '</ul></div></div></div>';
//        alertaTarjeta = '<div class="panel-default"><div class="panel-heading"><h6 onclick="Alerta4(this);" class="panel-title panel-trigger"><a >Tarjeta de circulación (' + alertaTarjetaCounter.toString() + ')</a></h6></div><div style="display: none !important;" class="EstTarjeta"><div class="panel-body"><ul class="popup-messages">' + alertaTarjeta + '</ul></div></div></div>';
//        //alertaEmergencia = '<div class="panel-default"><div class="panel-heading"><h6 class="panel-title"><a onclick="Alerta1(this);">Botón de emergencia (' + alertaEmergenciaCounter.toString() + ')</a></h6></div> <div style="display: none !important;" class="Emergencia"><div class="panel-body"><ul class="popup-messages">' + alertaEmergencia + '</ul></div></div> </div>';
//        //alertaExceso = '<div class="panel-default"><div class="panel-heading"><h6 class="panel-title"><a onclick="Alerta2(this);">Exceso de velocidad (' + alertaExcesoCounter.toString() + ')</a><div style="display: none !important;" class="ExcesoVel"><div class="panel-body"><ul class="popup-messages">' + alertaExceso + '</ul></div></div></h6></div></div>';
//        //alertaLicencia = '<div class="panel-default"><div class="panel-heading"><h6 class="panel-title"><a onclick="Alerta3(this);">Licencia de conducir (' + alertaLicenciaCounter.toString() + ')</a><div style="display: none !important;" class="EstLic"><div class="panel-body"><ul class="popup-messages">' + alertaLicencia + '</ul></div></div></h6></div></div>';
//        //alertaTarjeta = '<div class="panel-default"><div class="panel-heading"><h6 class="panel-title"><a onclick="Alerta4(this);">Tarjeta de circulación (' + alertaTarjetaCounter.toString() + ')</a><div style="display: none !important;" class="EstTarjeta"><div class="panel-body"><ul class="popup-messages">' + alertaTarjeta + '</ul></div></div></h6></div></div>';
//        alertaLowBattery = '<div class="panel-default"><div class="panel-heading"><h6 onclick="Alerta5(this);" class="panel-title panel-trigger"><a >Batería baja(' + alertaLowBatCounter.toString() + ')</a></h6></div><div style="display: none !important;" class="LowBattery"><div class="panel-body"><ul class="popup-messages">' + alertaLowBattery + '</ul></div></div></div>';
//        alertaDescBateria = '<div class="panel-default"><div class="panel-heading"><h6 onclick="Alerta6(this);" class="panel-title panel-trigger"><a >Desconexión de Batería(' + alertaDescBateriaCounter.toString() + ')</a></h6></div><div style="display: none !important;" class="DescBateria"><div class="panel-body"><ul class="popup-messages">' + alertaDescBateria + '</ul></div></div></div>';

//        alertas = alertaEmergenciaCounter > 0 ? alertaEmergencia : '';
//        alertas += alertaExcesoCounter > 0 ? alertaExceso : '';
//        alertas += alertaLicenciaCounter > 0 ? alertaLicencia : '';
//        alertas += alertaTarjetaCounter > 0 ? alertaTarjeta : '';
//        alertas += alertaLowBatCounter > 0 ? alertaLowBattery : '';
//        alertas += alertaDescBateriaCounter > 0 ? alertaDescBateria : '';

//        $("#alertas").html('');
//        $("#alertas").html(alertas); //Mostrar las alertas segun base de datos

//        $("#alertascount").html(data.length); //Cantidad de alertas para msotrarse en el icono
//        $("#AlertWidget").resizable({//Cambiar de tamanio la ventana de las alertas
//            maxHeight: 600,
//            minHeight: 197,
//            minWidth: 300,
//            maxWidth: 300,
//            resize: function (event, ui) {
//                $("#alertas").height(ui.size.height - 5);
//            }
//        });
//        $("#alertasDiv").draggable(); //Permitir mover el area de las alertas

//        //Forzar circulos en el mapa para boton de panico
//        actualizarGridEstadoActual();
//        actualizarMarkersEstadoActualMapa();
//        GenerarClicksDinamicosMapa();
//        }
           
//    });
   
//}

function space(count) {
    var str = "";
    for (var i = 0; i < count; i++) {
        str+= "&nbsp;"
    }
    return str;
}

function GenerarClicksDinamicosMapa() {
    $(".Emergencia ul >li").on("click", function(e) {
        var me = $(e.target);

        if (me.hasClass("btnPanico_head") || me.parents().hasClass("btnPanico_head")) {

        }
        else {
            var id = $(this).attr("id"), d = getDataByStatusId(parseInt(id));
            typeof (document.getElementById('sms_content_iframe').contentWindow.CrearMarkerAlertas) === "function" &&
            document.getElementById('sms_content_iframe').contentWindow.CrearMarkerAlertas(d.Lat, d.Lng, "panico.png", "Botón de emergencia", d.Plate, d.Alias, d.Dategps, d.Speed, d.Location, d.Subflota, d.Motorista);
        }
    });

    $(".ExcesoVel ul >li").on("click", function(e) {
        var me = $(e.target);
        if (me.hasClass("exceso_head") || me.parents().hasClass("exceso_head")) {
          
        }
        else {

            var id = $(this).attr("id"), d = getDataByStatusId(parseInt(id));
            typeof (document.getElementById('sms_content_iframe').contentWindow.CrearMarkerAlertas) === "function" &&
            document.getElementById('sms_content_iframe').contentWindow.CrearMarkerAlertas(d.Lat, d.Lng, "exceso.png", "Exceso de velocidad", d.Plate, d.Alias, d.Dategps, d.Speed, d.Location, d.Subflota, d.Motorista);
        }
    });
    $(".LowBattery ul >li").on("click", function (e) {
        var me = $(e.target);
        if (me.hasClass("low_battery_head") || me.parents().hasClass("low_battery_head")) {

        }
        else {


            var id = $(this).attr("id"), d = getDataByStatusId(parseInt(id));
            typeof (document.getElementById('sms_content_iframe').contentWindow.CrearMarkerAlertas) === "function" &&
            document.getElementById('sms_content_iframe').contentWindow.CrearMarkerAlertas(d.Lat, d.Lng, "b2.png", "Batería baja", d.Plate, d.Alias, d.Dategps, d.Speed, d.Location, d.Subflota, d.Motorista);
        }
    });
    $(".DescBateria ul >li").on("click", function (e) {
        var me = $(e.target);
        if (me.hasClass("Desc_Bateria_head") || me.parents().hasClass("Desc_Bateria_head")) {

        }
        else {


            var id = $(this).attr("id"), d = getDataByStatusId(parseInt(id));
            typeof (document.getElementById('sms_content_iframe').contentWindow.CrearMarkerAlertas) === "function" &&
            document.getElementById('sms_content_iframe').contentWindow.CrearMarkerAlertas(d.Lat, d.Lng, "battery_desc.png", "Desconexión de Batería", d.Plate, d.Alias, d.Dategps, d.Speed, d.Location, d.Subflota, d.Motorista);
        }
    });
}

function getDataByStatusId(statuslogid) {
    for (var i = 0, len = DataAlertas.length; i < len; i++) {
        if (DataAlertas[i].Statuslogid === statuslogid) {
            return DataAlertas[i];
        }
    }
}

//Atender las alertas 
//Chequear deschequear
var chk = null;
function chkAlertas(chkObj) {
    chk = $(chkObj).find("label div span");
    if (chk.attr("class") === "checked") {
        chk.attr("class", "");
    }
    else {
        chk.attr("class", "checked");
    }
}

//Boton cancelar de atender alertas
function CancelarAtender() {
    $("#atendervisible").click();
}

//Boton aceptar de atender alertas
var statusid = [];
function AceptarAtender() { 
    $.each($(".chkstatusid"), function (i) {
        ($(this).parent().attr("class") === "checked") && (statusid[i] = $(this).attr("statusid")); //Si esta chequeado se alamacena en statusid ese valor de la propiedad statusid
    });
    (!$.trim($("#comentario").val())) //Si la caja de comentarios esta vacia o solo contiene espacios en blanco
    ?
    $.jGrowl('La caja de comentarios no debe quedar vacía', { sticky: false, theme: 'growl-warning', header: 'Aviso', position: 'bottom-right', afterOpen: function () { jGrowjQUIClearMsg(); } })
    :
        (statusid.length < 1) //Si no existen alertas chequeadas
        ?
            $.jGrowl('Debe seleccionar almenos una alerta para ser atendida', { sticky: false, theme: 'growl-warning', header: 'Aviso', position: 'bottom-right', afterOpen: function () { jGrowjQUIClearMsg(); } })
        :
        $.post("Home/AtenderAlertasConChk", { statuslogIDs: (statusid + ""), comentario: $("#comentario").val() }, function () { })
        .done(function () {
            $.jGrowl('Operación realizada correctamente', { sticky: false, theme: 'growl-success', header: 'Éxito', position: 'bottom-right', afterOpen: function () { jGrowjQUIClearMsg(); } });
            $.each($(".chkstatusid"), function (i) {
                  ($(this).parent().attr("class") === "checked") && ($(this).closest(".eventos").remove()); //Remueve el li que esta chequeado para quitar alerta y no vuelvana  chequear el mismo elemento
            });
            GenerarAlertas();
        })
    .fail(function (xhr, textStatus, errorThrown) { $.jGrowl('Ha ocurrido un error: ' + errorThrown, { sticky: false, theme: 'growl-error', header: 'Error', position: 'bottom-right', afterOpen: function () { jGrowjQUIClearMsg(); } }); });
    statusid = [];
}


//Boton para ver reporte de alertas - Ventana modal para atender las alertas
//function VerReporteDeAlertas() {
//    $("#modalVerReporte").click(); //Muestra la vntana modal
//    $("#sms_content_alertas").attr("src") === "" ? $("#sms_content_alertas").attr("src", GlobalPath + "Home/rpAlertas") : $("#sms_content_alertas").get(0).contentWindow.GridAlertasRead(); //Reemplaza el iframe por el del grid y si ya esta cargado solo se queda actualizandolo mediante el llamado de la funcion GridAlertasRead() que es un script que esta dentro de rpAlertas
//}