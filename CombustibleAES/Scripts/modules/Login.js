


//Centrar en el resize el form login
function wWidthR() {
    window.addEventListener('resize', function (event) {
        calcularwidth();
    }, false);
}
//Centrar en el load el Form Login
function wWidthL() {
    calcularwidth();
}
//Calcular centrar login
function calcularwidth() {
    var w = window,
        d = document,
        e = d.documentElement,
        g = d.getElementsByTagName('body')[0],
        x = w.innerWidth || e.clientWidth || g.clientWidth,
        y = w.innerHeight || e.clientHeight || g.clientHeight;
    var leftLogin = (x - 555) / 2;
    var topLogin = ((y - 495) / 2) - 28.5;
    // var topLogin = ()
    document.getElementById('login').setAttribute("style", "width: 555px; height: 495px; z-index: 15; display: block; left: " + leftLogin + "px; top: " + topLogin + "px;");
}

