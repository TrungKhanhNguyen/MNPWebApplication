using MNPWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MNPWebApplication.Controllers
{
    public class TargetController : Controller
    {
        // GET: Target
        public ActionResult Index(string phone)
        {
            //DataQuery query = new DataQuery();
            //var tb = query.GetListTarget();
            //return View(tb);
            DataQuery query = new DataQuery();
            var tb = new DataTable();
            tb = query.GetTargetByPhone(phone);
            return View(tb);
        }

        public ActionResult Search(string phone)
        {
            DataQuery query = new DataQuery();
            
            var listTarget = new List<TargetObject>();
            var splitStr = phone.Split(',');
            foreach(var item in splitStr)
            {
                var tb = new DataTable();
                tb = query.GetTargetByPhone(item);
                if(tb!=null && tb.Rows.Count > 0)
                {
                    try
                    {
                        var tempTarget = new TargetObject();
                        tempTarget.MSISDN = tb.Rows[0]["MSISDN"].ToString();
                        tempTarget.Route = tb.Rows[0]["Recipient"].ToString();
                        listTarget.Add(tempTarget);
                    }
                    catch
                    {

                    }
                }
            }
            return View(listTarget);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.CreateSuccess = ViewBag.CreateFail = false;
            var temp = Request["id"];
            DataQuery query = new DataQuery();
            var table = query.GetTargetById(id);
            TargetObject tempObject = new TargetObject();
            if(table !=null && table.Rows.Count >0)
            {
                    var row = table.Rows[0];
                    //tempObject.Id = Convert.ToInt32(row["Id"]);
                    tempObject.MSISDN = row["MSISDN"].ToString();
                    tempObject.Route = row["Route"].ToString() ;
            }
            return View(tempObject);
        }
        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            ViewBag.CreateSuccess = ViewBag.CreateFail = false;
            var tempObject = new TargetObject();
            try
            {
                DataQuery query = new DataQuery();
                var phone = form["phone"];
                var route = form["route"];
                var id = form["Id"];
                query.CreateTarget(phone, route,Convert.ToInt32(id));
                ViewBag.CreateSuccess = true;
                //tempObject.Id = Convert.ToInt32(id);
                tempObject.MSISDN = phone;
                tempObject.Route = route;
                ViewBag.CreateSuccess = true;
            }
            catch
            {
                ViewBag.CreateFail = true;
            }
            return View(tempObject);
        }

        public ActionResult Create()
        {
            ViewBag.CreateSuccess = ViewBag.CreateFail = false;
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            ViewBag.CreateSuccess = ViewBag.CreateFail = false;
            try
            {
                DataQuery query = new DataQuery();
                var phone = form["phone"];
                var route = form["route"];
                query.CreateTarget(phone, route);
                ViewBag.CreateSuccess = true;
            }
            catch
            {
                ViewBag.CreateFail = true;
            }
            return View();
        }

        public string Delete(string id)
        {
            DataQuery query = new DataQuery();
            query.DeleteTarget(Convert.ToInt32(id));
            return "true";
        }

    }
}