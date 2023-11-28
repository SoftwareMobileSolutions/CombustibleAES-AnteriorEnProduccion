using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace CombustibleVales.Utilidades
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple=false)]
    public class ThrottleAttribute:ActionFilterAttribute
    {
        public string Name { get; set; }
        public int Seconds { get; set; }
        public string Message { get; set; }
        public override void OnActionExecuting(ActionExecutingContext c)
        {
            c.HttpContext.Response.TrySkipIisCustomErrors = true; // por el mensaje de "The page was not displayed because there was a conflict." http://www.it1me.com/it-answers?id=33969&ttl=Best+way+to+implement+request+throttling+in+ASP.NET+MVC%3F
            var key = string.Concat(Name, "-", c.HttpContext.Request.UserHostAddress);
            var allowExecute = false;

            if (HttpRuntime.Cache[key] == null)
            {
                HttpRuntime.Cache.Add(key,
                    true, // is this the smallest data we can have?
                    null, // no dependencies
                    DateTime.Now.AddSeconds(Seconds), // absolute expiration
                    Cache.NoSlidingExpiration,
                    CacheItemPriority.Low,
                    null); // no callback

                allowExecute = true;
            }

            if (!allowExecute)
            {
                if (String.IsNullOrEmpty(Message))
                    Message = "Solo puedes realizar esta accion cada {n} segundos.";

                c.Result = new ContentResult { Content = Message.Replace("{n}", Seconds.ToString()) };
                // see 409 - http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html

                c.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;

            }
        }
    }
}