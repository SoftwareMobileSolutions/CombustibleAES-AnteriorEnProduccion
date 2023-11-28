// Compatibilidad de algunas funciones nativas con todos los navegadores
//<![CDATA[	
//Console
(function() { 
	 try {
	 	var method;
    	var noop = function () {};
    	var methods = [
        'assert', 'clear', 'count', 'debug', 'dir', 'dirxml', 'error',
        'exception', 'group', 'groupCollapsed', 'groupEnd', 'info', 'log',
        'markTimeline', 'profile', 'profileEnd', 'table', 'time', 'timeEnd',
        'timeStamp', 'trace', 'warn'
    	];
   	 var length = methods.length;
    	var console = (window.console = window.console || {});

    	while (length--) {
    	    method = methods[length];

    	    // Only stub undefined methods.
     	   if (!console[method]) {
       	     console[method] = noop;
     	   }
    	}
	 }
	 catch (err) {
	 	log(err.message);	
	 }
}());

//Log
function log(El) {
		if (!window.console) console = {log: function() {}};
		return window.console&&console.log(El);
}
//]]>