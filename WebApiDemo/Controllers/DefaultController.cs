using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace WebApiDemo.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            // The steps for customising the Swagger UI are set out in this blog:
            // http://brazilianldsjag.com/2015/07/24/how-to-add-swagger-ui-to-web-api-2/
            ViewBag.Title = "WebApiDemo";
            return View();
        }
    }
}
