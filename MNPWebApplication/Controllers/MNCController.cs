using MNPWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MNPWebApplication.Controllers
{
    public class MNCController : Controller
    {
        private DataQuery query = new DataQuery();
        // GET: MNC
        public ActionResult Index()
        {
            //query = new DataQuery();
            var tb = query.GetMNCInfo();
            return View(tb);
        }
        public ActionResult Create()
        {
            ViewBag.CreateFailed = false;
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                //DataQuery query = new DataQuery();
                var mcc = form["mcc"];
                var mnc = form["mnc"];
                var brand = form["brand"];
                var ope = form["operator"];
                var status = form["status"];
                var bands = form["bands"];
                query.GetMNCQuery(mcc, mnc, brand, ope, status, bands);
                return RedirectToAction("Index", "MNC");
            }
            catch (Exception ex)
            {
                ViewBag.CreateFailed = true;
                return View();
            }
        }

        public string Delete(string id)
        {
            //DataQuery query = new DataQuery();
            query.DeleteMNC(Convert.ToInt32(id));
            return "true";
        }

        public ActionResult ListMNCPartial()
        {
            var tb = query.GetMNCInfo();
            return View(tb);
        }
    }
}