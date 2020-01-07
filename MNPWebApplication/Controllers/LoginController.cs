using MNPWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MNPWebApplication.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignIn()
        {
            ViewBag.AuthenFailed = false;
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(FormCollection form,string returnUrl)
        {
            DataQuery query = new DataQuery();
            var username = form["username"];
            var password = form["password"];
            var m = form["cbKeepLog"];
            var check = Convert.ToBoolean(form["cbKeepLog"]);
            var tb = query.Login(username, CreateMD5(password));
            if (tb != null && tb.Rows.Count > 0)
            {
                var idUser = tb.Rows[0][0];
                //FormsAuthentication.SetAuthCookie(username, true);
                //var user = dbUser.GetUserByName(txtUserName.Text);
                var ticket = new FormsAuthenticationTicket(
                   1,
                   username,
                   DateTime.Now,
                   DateTime.Now.AddHours(2),
                   check,
                   idUser.ToString());
                var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                if (ticket.IsPersistent)
                    cookie.Expires = ticket.Expiration;
                cookie.Path = FormsAuthentication.FormsCookiePath;
                Response.Cookies.Add(cookie);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index", "DataSearch");
            }
            else
            {
                ViewBag.AuthenFailed = true;
                return View();
            }
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn", "Login");
        }

        public ActionResult ChangePassword()
        {
            ViewBag.ErrorMessages = "";
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(FormCollection form)
        {
            DataQuery query = new DataQuery();
            var oldpass = CreateMD5(form["oldpassword"]);
            var newpass = form["newpassword"];
            var repass = form["repassword"];
            FormsIdentity id = (FormsIdentity)User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;
            var username = ticket.Name;
            var iduser = Convert.ToInt32(ticket.UserData);
            var tb = query.Login(username, oldpass);
            ViewBag.SuccessMessages = "";
            if (tb!=null && tb.Rows.Count > 0)
            {
                if(newpass == repass)
                {
                    query.UpdatePassword(iduser, CreateMD5(newpass));
                    ViewBag.ErrorMessages = "";
                    ViewBag.SuccessMessages = "Cập nhật thành công";
                    return View();
                }
                else
                {
                    ViewBag.ErrorMessages = "Nhập lại mật khẩu mới chưa đúng";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMessages = "Mật khẩu cũ không đúng";
                return View();
            }
            //return View();
        }

        public string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}