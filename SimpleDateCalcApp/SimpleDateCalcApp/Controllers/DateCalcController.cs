using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SimpleDateCalcApp.Shared;

namespace SimpleDateCalcApp.Controllers
{
    public class DateCalcController : Controller
    {
        //
        // GET: /DateCalc/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string dateA, string dateB)
        {
            if (String.IsNullOrEmpty(dateA) || String.IsNullOrEmpty(dateB))
            {
                ViewBag.Error = "Please select 2 dates.";
                return View();
            }

            SimpleDate dA = new SimpleDate(dateA);
            SimpleDate dB = new SimpleDate(dateB);

            int dateDiff = dA - dB;
            ViewBag.DateDiff = String.Format("{0:0}", dateDiff);

            return View();
        }
	}
}