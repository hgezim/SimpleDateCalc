using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

            DateTime dA = DateTime.Parse(dateA);
            DateTime dB = DateTime.Parse(dateB);

            // We only care about the difference and not if it's possitive or negative.
            int dateDiff = Math.Abs((dA - dB).Days);
            ViewBag.DateDiff = String.Format("{0:0}", dateDiff);

            return View();
        }
	}
}