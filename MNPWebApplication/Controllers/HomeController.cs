using MNPWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MNPWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetLastUpdate()
        {
            try
            {
                DataQuery query = new DataQuery();
                var tb = query.GetLastUpdated();
                var returnString = "";
                foreach (DataRow r in tb.Rows)
                {
                    returnString = Convert.ToDateTime(r[0]).ToString("dd-MM-yyyy hh:mm");
                }
                ViewBag.Date =  returnString;
            }
            catch
            {
                ViewBag.Date = "";
            }
            return PartialView();
        }
    }
}