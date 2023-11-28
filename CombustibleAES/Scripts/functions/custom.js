/*
Autor:       José María Santana Barrientos Recinos
Creación:    20/07/2015
Descripción: Funciones generales de javascript para ser utilizadas en cualquier parte del código
*/



//@REGION 'JavaScript Nativo'
//Calcular maximo valor de un array ejemplo: var p = []; p[0] = [35, 2, 65, 7, 8, 9, 12, 121, 33, 99]; p[0].max()
Array.prototype.max = function () {
    return Math.max.apply(null, this);
};

//Si no existe trim para IE crear la funcion trim
if (typeof String.prototype.trim !== 'function') {
    String.prototype.trim = function () {
        return this.replace(/^\s+|\s+$/g, '');
    }
}

//Calcular el minimo valor de un arraya, ejemplo: var p = []; p[0] = [35, 2, 65, 7, 8, 9, 12, 121, 33, 99]; p[0].min()
Array.prototype.min = function () {
    return Math.min.apply(null, this);
};

//Cambiar de posicion una matriz// ejemplo: var p = []; p[0] = [35, 2, 65, 7, 8, 9, 12, 121, 33, 99]; p[1] = [5, 200, 100]; p.transpose()
Array.prototype.transpose = function () {

    // Calculate the width and height of the Array
    var a = this,
      w = a.length ? a.length : 0,
      h = a[0] instanceof Array ? a[0].length : 0;

    // In case it is a zero matrix, no transpose routine needed.
    if (h === 0 || w === 0) { return []; }

    /**
     * @var {Number} i Counter
     * @var {Number} j Counter
     * @var {Array} t Transposed data is stored in this array.
     */
    var i, j, t = [];

    // Loop through every item in the outer array (height)
    for (i = 0; i < h; i++) {

        // Insert a new row (array)
        t[i] = [];

        // Loop through every item per item in outer array (width)
        for (j = 0; j < w; j++) {

            // Save transposed data.
            t[i][j] = a[j][i];
        }
    }

    return t;
};

//Remover un elemento de un array  Ejemplo: var ary = ['three', 'seven', 'eleven']; ary.remove('seven');
Array.prototype.remove = function () {
    var what, a = arguments, L = a.length, ax;
    while (L && this.length) {
        what = a[--L];
        while ((ax = this.indexOf(what)) !== -1) {
            this.splice(ax, 1);
        }
    }
    return this;
};

//Obtener parametros de la URL
var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};

//Remover valores repetidos en un array
Array.prototype.getUnique = function () {
    var u = {}, a = [];
    for (var i = 0, l = this.length; i < l; ++i) {
        if (u.hasOwnProperty(this[i])) {
            continue;
        }
        a.push(this[i]);
        u[this[i]] = 1;
    }
    return a;
}

//@REGION 'jQuery'
//#Textbox - TextArea -



//Contador de caracteres
function countChar(objtxt, objShowCount) {
    var cant = parseInt($(objtxt).attr("maxlength"));
    var left = cant - $(objtxt).val().length;
    (left < 0) && (left = 0);
    $(objShowCount).text(left + ' Caracteres disponibles');
};

//#ListBox
//Retornar Value de un ListBox
function ListBoxVal(IdList) {
    var listval = [];
    $.each($("#" + IdList + " option"), function (i) {
        listval[i] = $(this).val();
    });
    return listval + "";
}

//#ListBox
//Retornar selected values de un ListBox
function ListBoxSelectedVal(IdList) {
    var listval = [];
    $.each($("#" + IdList + " :selected"), function (i, s) {
        listval[i] = $(this).val();
    });
    return listval + "";
}


//Loading solo para Menu
function LoadingMenu() {
        $('.page-container').append('<div class="overlay"><div class="opacity"></div><i class="icon-spinner7 spin"></i></div>');
        $('.overlay').fadeIn(150);
}

//Loading
function Loading() {
    $('body').append('<div class="overlay"><div class="opacity"></div><i class="icon-spinner7 spin"></i></div>');
    $('.overlay').fadeIn(150);
}

//Loaded
function Loaded() {
    $('.overlay').fadeOut(150, function () {
        $(this).remove();
    });
}

//Retornar Texto de un ListBox
function ListBoxText(IdList) {
    var listtxt = [];
    $.each($("#" + IdList + " option"), function (i) {
        listtxt[i] = $(this).text();
    });
    return listtxt + "";
}


//@REGION 'jQuery-UI'
//Elimina las clases que agrega jQueryUI a los mensajes  de $.jGrowl
function jGrowjQUIClearMsg() { $.each($(".ui-corner-all"), function () { $(this).removeClass("ui-state-highlight ui-corner-all"); }); }

//Hacer draggable un objeto
function drag(El, limite) {
    $(function () {
        $(El).draggable({
            containment: limite
        });
    });
}

//@Kendo-UI
//NumericTextBox
function setValNumtxt(id, val) {
    $("#" + id).data("kendoNumericTextBox").value(parseFloat(val));
}

//#DropDownlist
//Obtiene el value de un dropdown
function GetDDLVal(id) {
    return $("#" + id).data("kendoDropDownList").value();
}

//Obtiene el texto de un dropdown
function GetDDLText(id) {
    return $("#" + id).data("kendoDropDownList").text();
}

//Selecciona el texto de un dropdown
function SelectDDLTxt(id, txt) {
    $("#" + id).data("kendoDropDownList").text(txt);
}

//Selecciona el value de un dropdown
function SelectDDLVal(id, val) {
    $("#" + id).data("kendoDropDownList").value(val);
}

//Habilita o inhabilita un dropdown
function EnabledDDL(id, visible) {
    $("#" + id).data("kendoDropDownList").enable(visible);
}

//Selecciona el primer item. Ejemplo de uso: DLLSelectFirts("DDLid");
function DLLSelectFirts(id) {
    SelectDDLTxt(id, $("#" + id + "-list li").eq(0).text())
}

//Selecciona un elemento del dropdowlist
function DLLSelectAny(id, EQ) {
    SelectDDLTxt(id, $("#" + id + "-list li").eq(EQ).text())
}

//#DatePicker
//Obtener el valor de un DatePicker
function GetDatePickerVal(id) {
   return  $("#" + id).data("kendoDatePicker").value();
}


//Setear el valor de un DatePicker
function SetDatePickerVal(id, val) {
    //NOTA: esta funcion utiliza dateFormat() definida en este mismo custom.js
    //if (val === null || val === '') { val = new String((new Date()).toString()); val = dateFormat(val, "yyyy/mm/dd", "-6"); }
    return $("#" + id).data("kendoDatePicker").value(val);
}

//#DateTime
//Obtener el valor de un DateTime
function GetDateTimeVal(id) {
    return $("#" + id).data("kendoDatePicker").value();
}

//Setear el valor de un DatePicker
function SetDateTimePickerVal(id, val) {
    //NOTA: esta funcion utiliza dateFormat() definida en este mismo custom.js
    //if (val === null || val === '') { val = new String((new Date()).toString()); val = dateFormat(val, "yyyy/mm/dd", "-6"); }
    return $("#" + id).data("kendoDateTimePicker").value(val);
}

//#DateTimePicker
//Obtener el valor de un DateTimePicker
function GetDateTimePickerVal(id) {
    return $("#" + id).data("kendoDateTimePicker").value();
}

//#Grid
//Formato de filtro del Grid cuando son fechas
function DateTimeFilter(control) {//El Grid se llama esta funcion con .Filterable(f => f.UI("DateTimeFilter"));
    $(control).kendoDateTimePicker({
        format: "yyyy/MM/dd HH:mm:ss",
        timeFormat: "HH:mm:ss",
        culture: "es-MX"
    });
}

//Actualizar un grid segun id
function GridActualizar(id) { //Actualiza el Grid segun un id ejemplo: GridActualizar("GridId");
    $('#' + id).data('kendoGrid').dataSource.read();
}


//Ejecutar una funcion para llenar un grid de forma externa
function GridRemoteRead(id, url, data) {//Recibe un id del grid, la url que se va a ejecutar y data son los parametros que debe ser de tipo function vacia con return Eje: GridRemoteRead('idGrid', /Controller/Action, function () {return param1: valor, param2: valor2})
    $('#' + id).data('kendoGrid').dataSource.transport.options.read = {
        url: url,
        type: 'POST',
        data: data
    };
    $('#' + id).data('kendoGrid').dataSource.read();
}
//Ejecutar una funcion para llenar un grid de forma externa, data compresa Prueba por: Franklin Arturo
//Llenar un grid con javascript su dataSource
function GridSetDataSource(idGrid, datos) {
    $('#' + idGrid).data("kendoGrid").dataSource.data(datos);
}

//Habilitar la seleccion de celdas de CSS -- NOTA: Debe estar activa la seleccion de celdas desde el razor para su funcionamiento
function GridSelectEnableCells(id) {
    $("#" + id + " tbody").on("click", "tr[role='row']", function () {
        $(this).addClass("k-state-selected");
    });
}

//Inhabilitar la seleccion de celdas de CSS -- NOTA: Debe estar activa la seleccion de celdas desde el razor para su funcionamiento
function GridSelectDisableCells() {
    $("#" + id + " tbody").on("click", "tr[role='row']", function () {
        $(this).removeClass("k-state-selected");
    });
}

//Reset filtros/Filter
function GridResetFilter(idGrid) {
    var currentFilters = $("#" + idGrid).data("kendoGrid").dataSource.filter();
    if (typeof currentFilters !== "undefined" && currentFilters !== null) {
        var newFilter = currentFilters.filters.splice(currentFilters.filters.length, 0);
        $("#" + idGrid).data("kendoGrid").dataSource.filter(newFilter);
    }
   /* else {
        console.warn("No existe filtros activos a limpiar");
    }*/
}

//Oculta y muestra columnas del grid dependiendo de un rango de inicio y fin para ocultar y una rango inicio y fin para mostrar.
function gridHS(id, Hini, Hfin, Sini, Sfin) {//Ejemplo gridHS("Gridid",1,20,1,10) oculta las columnas de 1 a la 20 y muestra las columnas de la 1 a la 10
    for (var i = Hini; i < Hfin; i++) {
        $("#" + id).data("kendoGrid").hideColumn(i);
    }
    for (var i = Sini; i < Sfin; i++) {
        $("#" + id).data("kendoGrid").showColumn(i);
    }
    $("#" + id).data("kendoGrid").refresh();
}

//#TreeView
//Obtener los cheques de un treeview segun el nivel
/*kendo.ui.TreeView.prototype.getCheckedItems = (function () {

    function getCheckedItems() {
        var nodes = this.dataSource.data();
        return getCheckedNodes(nodes);
    }

    function getCheckedNodes(nodes) {
        var node, childCheckedNodes;
        var checkedNodes = [];

        for (var i = 0; i < nodes.length; i++) {
            node = nodes[i];
            if (node.checked) {
                checkedNodes.push(node);
            }
            if (node.hasChildren) {
                childCheckedNodes = getCheckedNodes(node.children.data());
                if (childCheckedNodes.length > 0) {
                    checkedNodes = checkedNodes.concat(childCheckedNodes);
                }
            }

        }

        return checkedNodes;
    }

    return getCheckedItems;
})();*/
/*
function GetTreeViewChk(id) {
    var treeview = $('#' + id).data('kendoTreeView').dataSource.data();
    for()
    
   /* var treeview = $('#' + id).data('kendoTreeView').getCheckedItems();
    var IDs = [];
    for (var i = 0, len = treeview.length; i < len; i++) {
        (treeview[i].Nivel === nivel) && (IDs.push(treeview[i].codigo.substring(1, treeview[i].codigo.length)));
    }
    return (IDs + "");*/
//}*/


//#Ejecutar una funcion para llenar un TreeView de forma externa
function TreeViewRemoteRead(id, url, data) {//Recibe un id del grid, la url que se va a ejecutar y data son los parametros que debe ser de tipo function vacia con return Eje: TreeViewRemoteRead('idTreeView', /Controller/Action, function () {return param1: valor, param2: valor2})
    $('#' + id).data('kendoTreeView').dataSource.transport.options.read = {
        url: url,
        type: 'POST',
        data: data
    };
    $('#' + id).data('kendoTreeView').dataSource.read();
}

//Permite obtener los ID del Nivel =  2 de un arbol
function GetTreeViewChk(TreeId) {
    var Ids = [], id = {}, treeChk = $('#' + TreeId).data('kendoTreeView').dataSource.data().at(0);
    for (var i = 0, len1 = treeChk.Children.length; i < len1; i++) {
        if (treeChk.Children[i].Nivel === 1 && treeChk.Children[i].checked === true) {
            for (var j = 0, len2 = treeChk.Children[i]._childrenOptions.data.Children.length; j < len2; j++) {
                id = treeChk.Children[i]._childrenOptions.data.Children[j].codigo;
                Ids.push(id.substring(1, id.lenght));
            }
        }
        if (treeChk.Children[i].Nivel === 1 && treeChk.Children[i].checked === false) {
            for (var j = 0, len2 = treeChk.Children[i].Children.length; j < len2; j++) {
                if (treeChk.Children[i].Children[j].checked === true) {
                    id = treeChk.Children[i].Children[j].codigo;
                    Ids.push(id.substring(1, id.lenght));
                }
            }
        }
    }
    Ids = Ids + "";
    return Ids;
}

//Permite obtener los ID del Nivel =  1 de un arbol
function GetTreeViewChkSubflotas(TreeId) {
    var Ids = [], id = {}, treeChk = $('#' + TreeId).data('kendoTreeView').dataSource.data();

    for (var i = 0, len1 = treeChk.length; i < len1; i++) {
        if (treeChk[i].Nivel === 0 && treeChk[i].checked === true) {
            for (var j = 0, len2 = treeChk[i]._childrenOptions.data.Children.length; j < len2; j++) {
                id = treeChk[i]._childrenOptions.data.Children[j].codigo;
                Ids.push(id.substring(1, id.lenght));
            }
        }
        if (treeChk[i].Nivel === 0 && treeChk[i].checked === false) {
            for (var j = 0, len2 = treeChk[i].Children.length; j < len2; j++) {
                if (treeChk[i].Children[j].checked === true) {
                    id = treeChk[i].Children[j].codigo;
                    Ids.push(id.substring(1, id.lenght));
                }
            }
        }
    }
    Ids = Ids + "";
    return Ids;
}
var cadenaFinal = [];
function getDeepSon(RamaSelected) {
    var cadFin = "";
    cadenaFinal = [];
                GetChildren(RamaSelected);
                for (var i = 0,con=cadenaFinal.length; i < con; i++) {
                    cadFin += cadenaFinal[i].substring(1) + ",";
                }
                return cadFin;
            }
            
            function GetChildren(trama) {
                if (trama.Nivel !== 1 && trama.Nivel !== 3 && trama.Nivel!==4) {
                    if (trama.Nivel > 0) {
                        if (trama.Children.length > 0) {
                            for (var i = 0, len = trama.Children.length; i < len; i++) {
                                GetChildren(trama.Children[i]);
                            }
                        } else {
                            cadenaFinal.push(trama.codigo);
                        }
                    } else {
                        var len0 = trama._childrenOptions.data.Children.length;
                        if (len0>0) {
                            for (var k = 0; k < len0; k++) {
                                GetChildren(trama._childrenOptions.data.Children[k]);
                            }
                        }
                    }
                }
                if (trama.Nivel === 1) {
                    if (trama.codigo.substring(0,1)!=="s") {
                         if (trama.Children.length > 0) {
                             for (var j = 0, len1 = trama.Children.length; j < len1; j++) {
                                GetChildren(trama.Children[j]);
                            }
                         } else {
                             cadenaFinal.push(trama.codigo);
                         }
                    } else {
                        cadenaFinal.push(trama.codigo);
                    }
                    }
                        
                if (trama.Nivel === 3) {
                    if (trama.HasChildren) {
                        for (var hh = 0, lon = trama.Children.length; hh < lon; hh++) {
                            cadenaFinal.push(trama.Children[hh].codigo);
                        }
                    }
                         //cadenaFinal.push(trama.codigo);
                }
                if (trama.Nivel===4) {
                    cadenaFinal.push(trama.codigo);
                }
                }
     

//#Chart
//Permite cambiar el texto de un chart de kendo
function ChartChangeTitle(id, title) {
    var chart = $('#' + id).data('kendoChart');
    chart.options.title.text = title;
    chart.options.title.font = "bold 16px Arial,Helvetica,sans-serif";
    chart.options.title.color = "#000";
    chart.refresh();
}

//Leyenda personalizada para dashboard
function legend_(array) {
    return { derecha: Math.ceil(array.length / 2), izquierda: array.length - Math.ceil(array.length / 2) }
}

function ChartRead(id, url, data) {
    $('#' + id).data('kendoChart').dataSource.read = {
        url: url,
        type: 'POST',
        data: data
    };
}
function ChartWriteRead(id, URL, fn) {
    $('#' + id).data('kendoChart').dataSource.options.transport.read = {
        data: fn,
        type: "POST",
        url: URL
    };
    $('#' + id).data('kendoChart').dataSource.read();
}

//kendoNumericTextBox, evitar vacios// Si está vacío el cambpo enviará un valor por default
function NumericTxtEmpty(id, valMin) {
    $("#" + id).blur(function () {
        var kn = $(this).data("kendoNumericTextBox");
        if (kn.value() === null) {
            kn.value(valMin); // set default
        }
    });
}

//$('#' + id).data('kendoChart').dataSource.read.url


//Validar solo numeros
//elementId: id del elemento o elementos a validar;
//si solo es un elemento se envia como id o #id, si son varios se envian como id1,id2,id2,idn-1 o #id1,#id2,#id2,#idn-1
//Se valida en el momento que presionan las teclas
function validateNumbersOnKeypress(elementId) {
    var idSplit = elementId.split(',');
    idSplit.forEach(function (elementId) {
        elementId.replace(' ', '');
        elementId = (elementId.startsWith('#')) ? elementId : '#' + elementId;
        $(elementId).keypress(function (e) {
            var charValue = String.fromCharCode(e.keyCode);
            var valid = /^[0-9]+$/.test(charValue);
            if (!valid) { e.preventDefault(); }
        });
    });
}

function customValidateOnKeypress(elementId, regex) {
    var idSplit = elementId.split(',');
    idSplit.forEach(function (elementId) {
        elementId.replace(' ', '');
        elementId = (elementId.startsWith('#')) ? elementId : '#' + elementId;
        $(elementId).keypress(function (e) {
            var charValue = String.fromCharCode(e.keyCode);
            var valid = regex.test(charValue);
            if (!valid) { e.preventDefault(); }
        });
    });
}

function customStringValidator(myString, unwantedChars, returnValidatedString) {
    var bOk = false;
    var unwantedFound = false;
    if (returnValidatedString) { for (var i = 0, l = unwantedChars.length; i < l; i++) { myString = myString.replace(unwantedChars[i], ''); } }
    else {
        //var char;
        for (var i = 0, l = myString.length; i < l; i++) {
            for (var j = 0, l2 = unwantedChars.length; j < l2; j++) {
                //char = unwantedChars[j];
                if (myString[i] === unwantedChars[j]) { unwantedFound = true; break; }
            }
            //if (myString[i] === char) { bOk = false; break; }
            //bOk = true;
            if (unwantedFound) { bOk = false; break; }
            bOk = true;
        }
    }
    return (returnValidatedString) ? myString : bOk;
}

function customStringValidator2(myString, patt) {
    var bRes = false;
    if ((myString.match(patt))) { bRes = true; }
    else { bRes = false; } return bRes
}

//Validar Email
//strEmail (string): Value del campo email
//bIsRequired (boolean): true/false.
//---------------------Si el valor enviado es requerido(true) se valida que no este vacio y que este correctamente escrito.
//---------------------Si el valor enviado no es requerido(false) retorna true si el value del email esta vacio, sino esta vacio se verifica que este correctamente escrito.
function testEmail(strEmail, bIsRequired) { strEmail = strEmail.trim(); if (bIsRequired) { if (strEmail === '' || strEmail === ' ' || strEmail === undefined || strEmail === 'undefined' || strEmail === null || strEmail === 'null') { return false; } else { return /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$/i.test(strEmail); } } else { if (strEmail === '' || strEmail === ' ' || strEmail === undefined || strEmail === 'undefined' || strEmail === null || strEmail === 'null') { return true; } else { return /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$/i.test(strEmail); } } }



//Evento focusout de kendoNumericTextBox
//si solo es un elemento se envia como id o #id, si son varios se envian como id1,id2,id2,idn-1 o #id1,#id2,#id2,#idn-1
function onKNTFocusOut(elementId) {
    var idSplit = elementId.split(',');
    idSplit.forEach(function (elementId) {
        elementId.replace(' ', '');
        elementId = (elementId.startsWith('#')) ? elementId : '#' + elementId;
        $(elementId).on('focusout', function () {
            var val = getNumericTextboxVal(elementId);
            if (val === undefined || val === 'undefined' || val === null || val === 'null' || val === 0 || val === '0') { setNumericTextboxVal(elementId,0); }
        });

    });
}


//11/09/2015 Alexander Paniagua
//Setear cheked a RadioButton normal
//elementoV: elemento checked si valor===valorComparar
//elementoF: elemento cheked si valor!==valorcomparar
//valor: valor enviado
//valorComparar: valor que se evaluara
function SetRbtnChecked(elementoV, elementoF, valor, valorComparar) {
    return (valor === valorComparar) ? $('#' + elementoV).click()/*attr('checked', 'true')*/ : $('#' + elementoF).click()/*attr('checked', 'true')*/;
}

//11/09/2015
//Limpiar elementos simples
//id: id's de los elementos entre '' a limpiar en formato '#id1,#id2,#id3'
function clearSimpleElements(id) { $(id).val(''); }

//11/09//2015 Alexander Paniagua
//Setear valor a NumericTextbox
//id: id de elemento
//val: valor a asignar
function setNumericTextboxVal(id, val) {
    id = (id.startsWith('#')) ? id : '#' + id;
    val = ((val === null) || (val === 'undefined') || (val === '')) ? '' : val;
    $(id).data("kendoNumericTextBox").value(val);
}

function setKendoNumericTextboxVal(id, val) {
    id = (id.startsWith('#')) ? id : '#' + id;
    val = ((val === null) || (val === 'undefined') || (val === undefined) || (val === '')) ? '' : val;
    $(id).data("kendoNumericTextBox").value(val);
}

//11/09//2015 Alexander Paniagua
//Obtener valor NumericTextbox
//id: id de elemento
function getNumericTextboxVal(id) { id = (id.startsWith('#')) ? id : '#' + id; var val = $(id).data("kendoNumericTextBox").value(); val = (val === null || val === 'undefined') ? 0 : val; return val; }


//11/09/2015 Alexander Paniagua
//Asignar valor a MaskedTextBox de kendo
//id: id de elemento
//val: valor a asignar
function setMaskedTextBoxVal(id, val) {
    id = (id.startsWith('#')) ? id : '#' + id;
    val = ((val === null) || (val === 'undefined') || (val === '')) ? '' : val;
    $(id).data("kendoMaskedTextBox").value(val);
}
//11/09/2015 Alexander Paniagua
//Obtener valor de kendoMaskedTextBox
//id: id de elemento
function getMaskedTextBoxVal(id) { id = (id.startsWith('#')) ? id : '#' + id; var val = $(id).data("kendoMaskedTextBox").value(); val = (val === null || val === 'undefined') ? 0 : val; return val; }

//11/09/2015
//Limpiar valor de kendoNumericTextbox
//id: id's de los elementos entre '' a limpiar en formato '#id1,#id2,#id3'
function clearNumericTextbox(id) {
    var idSplit = id.split(',');
    for (var i = 0, idLen = idSplit.length; i < idLen; i++) { $(idSplit[i]).data("kendoNumericTextBox").value(''); /*$(idSplit[i]).data("kendoNumericTextBox").value('0');*/ }
}

//11/09/2015
//Limpiar valor a MaskedTextBox de kendo
//id: id's de los elementos entre '' a limpiar en formato '#id1,#id2,#id3'
function clearMaskedTextBoxVal(id) {
    var idSplit = id.split(',');
    idSplit.forEach(function (elementId) { $(elementId).data("kendoMaskedTextBox").value(''); /*$(elementId).data("kendoMaskedTextBox").value('0');*/ });
    //for (var i = 0, idLen = id.length; i < idLen; i++) { $(elementId).data("kendoMaskedTextBox").value(''); $(elementId).data("kendoMaskedTextBox").value('0'); }
}

//11/09/2015
//Asignar indice a kendoDropDownList
//id: id de elemento a asignar indice enviado
//index: indice a asignar
function setkendoDropDownListIndex(id, index) { id = (id.startsWith('#')) ? id : '#' + id; $(id).data('kendoDropDownList').select(index); }

//14/09/2015 Alexander Paniagua
//Asignar valor a kendoDropDownList
//id: id de elemento a asignar valor enviado
//val: valor a asignar
function setkendoDropDownListVal(id, val) { id = (id.startsWith('#')) ? id : '#' + id; $(id).data('kendoDropDownList').value(val); }

function getkendoDropDownListVal(id) {
    id.replace(' ', '');
    id = (id.startsWith('#')) ? id : '#' + id;
    var val = $(id).data('kendoDropDownList').value();
    val = val.trim();
    val = (val === '' || val === ' ' || val === undefined || val === 'undefined' || val === null || val === 'null' || val === 'NULL') ? '' : val;
    return val;
}

function getkendoDropDownListIndex(id) {
    id.replace(' ', '');
    id = (id.startsWith('#')) ? id : '#' + id;
    var val = $(id).data('kendoDropDownList').select();
    val = (val === '' || val === ' ' || val === undefined || val === 'undefined' || val === null || val === 'null' || val === 'NULL' || val < 0) ? -1 : val;
    return val;
}

function getKendoDropDownListVal(id) {
    id.replace(' ', '');
    id = (id.startsWith('#')) ? id : '#' + id;
    var val = $(id).data('kendoDropDownList').value();
    val = val.trim();
    val = (val === '' || val === ' ' || val === undefined || val === 'undefined' || val === null || val === 'null' || val === 'NULL') ? '' : val;
    return val;
}
function setKendoDropDownListVal(id, val) {
    id.replace(' ', '');
    id = (id.startsWith('#')) ? id : '#' + id;
    $(id).data('kendoDropDownList').value(val);
}
function getKendoDropDownListIndex(id) {
    id.replace(' ', '');
    id = (id.startsWith('#')) ? id : '#' + id;
    var val = $(id).data('kendoDropDownList').select();
    val = (val === '' || val === ' ' || val === undefined || val === 'undefined' || val === null || val === 'null' || val === 'NULL' || val < 0) ? -1 : val;
    return val;
}
function setKendoDropDownListIndex(id, index) {
    id.replace(' ', '');
    id = (id.startsWith('#')) ? id : '#' + id;
    $(id).data('kendoDropDownList').select(index);
}

//14/09/2015 Alexander Paniagua
//Asignar id a elementos
//strQuerySelector: selector
//strPatron: patron del id que desea asignar
function setElementId(strQuerySelector, strPatron) {
    var iContador = 0; var strTagId = "";
    /*strQuerySelector = (strQuerySelector.startsWith('#')) ? strQuerySelector : '#' + strQuerySelector;*/
    $(strQuerySelector).each(function (index) {
        iContador++;
        strTagId = strPatron + iContador.toString();
        $(this).attr('id', strTagId);
    });
}

//14/09/2015 Alexander Paniagua
//Quitar seleccion a checkbox
//id: id de elemento a quitar seleccion
function setCheckBoxUnchecked(id) { id = (id.startsWith('#')) ? id : '#' + id; if ($(id).is(':checked')) { $(id).click(); } }

//Remueve un registro de la lsita mostrada
function DDLRemoveData(id, ini, fin) {
    $("#" + id).data("kendoDropDownList").dataSource.data().splice(ini, fin);
}

function setkendoDropDownListValue(id, val) { id = (id.startsWith('#')) ? id : '#' + id; $(id).data('kendoDropDownList').select(val); }

//Remueve un registro de la lsita mostrada
function DDLRemoveData(id, ini, fin) {
    $("#" + id).data("kendoDropDownList").dataSource.data().splice(ini, fin);
}

//Agregar la ultima hh:mm a la lista de horas de un kendoDateTimePicker
//elementSelector: selector del elemento, en este caso sera el id del dropdownlist que se muestra al hacer clic en el boton de reloj
function addLastDayHour(elementSelector) {
    elementSelector = (elementSelector.startsWith('#')) ? elementSelector : '#' + elementSelector;
    //elementSelector = (elementSelector.endsWith('li:last-child')) ? elementSelector : elementSelector + ' li:last-child';
    var lastDayHour = $(elementSelector + ' li:last-child').text();
    if (lastDayHour === '11:30 p. m.') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">11:59 p. m.</li>'); }
    else if (lastDayHour === '11:30 p.m.') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">11:59 p.m.</li>'); }
    else if (lastDayHour === '11:30p.m.') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">11:59p.m.</li>'); }
    else if (lastDayHour === '11:30 p m') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">11:59 p m</li>'); }
    else if (lastDayHour === '11:30 pm') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">11:59 pm</li>'); }
    else if (lastDayHour === '11:30pm') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">11:59pm</li>'); }
    else if (lastDayHour === '11:30 P. M.') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">11:59 P. M.</li>'); }
    else if (lastDayHour === '11:30 P.M.') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">11:59 P.M.</li>'); }
    else if (lastDayHour === '11:30P.M.') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">11:59P.M.</li>'); }
    else if (lastDayHour === '11:30 P M') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">11:59 P M</li>'); }
    else if (lastDayHour === '11:30 PM') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">11:59 PM</li>'); }
    else if (lastDayHour === '11:30PM') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">11:59PM</li>'); }
    else if (lastDayHour === '23:30 p. m.') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">23:30 p. m.</li>'); }
    else if (lastDayHour === '23:30 p.m.') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">23:59 p.m.</li>'); }
    else if (lastDayHour === '23:30p.m.') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">23:59p.m.</li>'); }
    else if (lastDayHour === '23:30 p m') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">23:59 p m</li>'); }
    else if (lastDayHour === '23:30 pm') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">23:59 pm</li>'); }
    else if (lastDayHour === '23:30pm') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">23:59pm</li>'); }
    else if (lastDayHour === '23:30 P. M.') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">23:59 P. M.</li>'); }
    else if (lastDayHour === '23:30 P.M.') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">23:59 P.M.</li>'); }
    else if (lastDayHour === '23:30P.M.') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">23:59P.M.</li>'); }
    else if (lastDayHour === '23:30 P M') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">23:59 P M</li>'); }
    else if (lastDayHour === '23:30 PM') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">23:59 PM</li>'); }
    else if (lastDayHour === '23:30PM') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">23:59PM</li>'); }
    else if (lastDayHour === '23:30') { $(elementSelector).append('<li tabindex="-1" role="option" class="k-item" unselectable="on">23:59</li>'); }
    else { }
}



//Quitar opciones de click derecho 'copy cut paste' u otros
function disableRightClickOptions(elementId, options) {
    var idSplit = elementId.split(',');
    idSplit.forEach(function (elementId) {
        elementId.replace(' ', '');
        elementId = (elementId.startsWith('#')) ? elementId : '#' + elementId;
        $(elementId).bind(options, function (e) { e.preventDefault(); });
    });
}

//Revisar numero de caracteres minimo
function checkMinLenght(str, minLen) { var strLen = str.length; return (strLen >= minLen); }

