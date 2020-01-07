using MNPWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MNPWebApplication.Controllers
{
    public class DataSearchController : Controller
    {
        // GET: DataSearch
        public ActionResult Index(string pn, string fd, string td)
        {
            //if (Request.IsAuthenticated)
            //{
            //    // Render stuff for authenticated user
            //}
            //if (User.Identity.IsAuthenticated)
            //{
            //    //var m = "fdassdf";
            //}
            DataQuery query = new DataQuery();
            var tb = new DataTable();
            if (String.IsNullOrEmpty(pn) && String.IsNullOrEmpty(fd) && String.IsNullOrEmpty(td))
                tb = query.GetAll();
            else
                tb = query.GetDataQuery(pn, fd, td);
            return View(tb);
        }
        
        public ActionResult GetList(string msisdn, string fromdate, string todate)
        {
            DataQuery query = new DataQuery();
            var tb = query.GetDataQuery(msisdn, fromdate, todate);
            return PartialView(tb);
        }
    }
}