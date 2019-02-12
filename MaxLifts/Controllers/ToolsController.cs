using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaxLifts.Controllers
{
    public class ToolsController : Controller
    {
        // GET: Tools
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Max()
        {
            return View();
        }

        public ActionResult Reps()
        {
            return View();
        }
    }
}