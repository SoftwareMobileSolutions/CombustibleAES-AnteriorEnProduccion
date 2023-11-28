//Obtiene el index  de todos los elementos sortables del panel
function getSortableAllIndex(element) {
    ordenAll = [];
    $.map(element.find('li'), function (el) {
        ordenAll.push({
            moduloid: parseInt(el.id),
            orden: $(el).index() + 1
        });
    });
}

var ordenAll = [];        //Lleva el orden de todos los modulos cuando se han movido de lugar o eliminados, lleva id y moduleid
var tempModulesId = [];
var tempModulesOrden = [];
var strModulesId = "";    //lleva el string moduleid de todos los modulos que se muestran (usado para eliminar - actualizar)
var strModulesOrden = ""; //lleva el string del odern de todos los modulos que se muestran (usado para eliminar - actualizar)
$(document).on("click", ".cerrar", function () {
    $(this).parent().remove();
    getSortableAllIndex($('.sortable-row'));
});

//Notificaciones
var notificacion;
$(window).load(function () {
    //Mover los modulos
    $('.sortable-row').sortable({
        connectWith: ".sortable-row",
        containment: $(".panel-body"),
        stop: function (e, ui) {
            getSortableAllIndex($(this));
        }
    });
    GenerarPanel();

});

function InfoAdicional() {
   
    /*$.post("../Panel/InfoAdicional", {}, function (data) {
        for (var i = 0, len = data.length; i < len; i++) {
            var txtFooter = "", obj = "", des = "";
            obj = $(".sortable-row [id='" + data[i].Moduleid + "'] span");
            txtFooter = data[i].Informacion;
            des = data[i].Descripcion;
            obj.attr("title", txtFooter + ". " + des);
            if (txtFooter.length >= 29) {
                txtFooter = txtFooter.substring(0, 26) + "..."
            }
            obj.text(txtFooter);
        }
    });*/
}

//Generar Panel
var panel = null; //Datos del procedimiento
var modulos = ""; //Creación de los módulos
var txtmenu = "";
var styleicon = "";
function GenerarPanel() {
    modulos = "";
    txtmenu = "";
    styleicon = "";
    panel = null;
    //$.post("../Panel/GenerarPanel", {}, function (data) {
    $.post((window.location.hostname == "localhost" ? ".." : "../../CombustibleAES") + "/Panel/GenerarPanel", {}, function (data) {
        panel = data;
        for (var i = 0, k = 0, len = panel.length; i < len; i++, k++) {
            if (bootstrapstyle.length === k) {//Si ya no existen mas colores que inicie de nuevo con el primero
                k = 0;
            }
            txtmenu = panel[i].Description.length > 30 ? (panel[i].Description.substring(0, 30) + "...") : panel[i].Description; //Limita a un maximo de 30 caracteres para la descripcion
            styleicon = panel[i].Icon.substring(0, 3) === "sms" ? "style= \"margin-bottom: 3px; \"" : "";

            modulos = modulos +
           '<li class="' + bootstrapstyle[k] + '" id="' + panel[i].Moduleid + '" >' +
               '<button type="button" class="close cerrar"></button>' +
               '<div class="top-info"> <a href="../' + panel[i].Urlpage + '">' + panel[i].Name + '</a><small>' + txtmenu + '</small> </div>' +
           //' <a href="../' + panel[i].Urlpage + '"> <i ' + styleicon + ' class="' + panel[i].Icon + '"></i> </a><span class="bottom-info bg-primary ">(Info adicional pendiente)</span>' +
           //' <a href="../' + panel[i].Urlpage + '"> <i ' + styleicon + ' class="' + panel[i].Icon + '"></i> </a><span class="bottom-info bg-primary " info></span>' +
     ' <a href="../' + panel[i].Urlpage + '"> <i ' + styleicon + ' class="' + panel[i].Icon + '"></i> </a><span class="bottom-info bg-primary " style="color: #32434D;">.</span>' +
                //' <a href="' + panel[i].Urlpage.replace("../", (window.location.hostname == "localhost" ? "../../" : "../../CombustibleAES")) + '"> <i ' + styleicon + ' class="' + panel[i].Icon + '"></i> </a><span class="bottom-info bg-primary " style="color: #32434D;">.</span>' +
           '</li>';
        }
        if (panel.length > 0) {
            $(".sortable-row").html(modulos);
        }
        else {
            $(".sortable-row").html('<div class="alert alert-info fade in">' +
                                        '<button type="button" class="close" data-dismiss="alert">×</button>' +
                                        '<i class="icon-info"></i> No hay módulos para mostrar!' +
                                    '</div>');
        }
        modulos + "";
        InfoAdicional();
    });

}


function ActualizarEliminarPanel() {
    getSortableAllIndex($('.sortable-row'));
    for (var i = 0; i < 10; i++) {
        tempModulesId[i] = "null";
        tempModulesOrden[i] = "null";
    }
    for (var i = 0, len = ordenAll.length; i < len; i++) {
        tempModulesId[i] = ordenAll[i].moduloid;
        tempModulesOrden[i] = ordenAll[i].orden;
    }
    $.post("../Panel/ActualizarEliminarPanel", { cadenaModuloid: tempModulesId.toString(), cadenaOrden: tempModulesOrden.toString() })
         .done(function () {
             $.jGrowl('Datos guardado correctamente', { sticky: false, theme: 'growl-success', header: 'Éxito', position: 'bottom-right', afterOpen: function () { jGrowjQUIClearMsg(); } });
             

         })
         .fail(function (xhr, textStatus, errorThrown) {
             $.jGrowl('Ha ocurrido un error: ' + errorThrown, { sticky: false, theme: 'growl-error', header: 'Error', position: 'bottom-right', afterOpen: function () { jGrowjQUIClearMsg(); } });
         });
    InfoAdicional();
}

//Setea una notificacion
/*function notificaciones(id, contenido, icono) {
    $("#" + id).data("kendoNotification").show(contenido, icono);
}
*/