using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Charanjit_Mortgage_MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Appointment()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Contact(string username,string lastname, string Email, string phn, string message)
        {
            SendMail(username+ " "+lastname, Email, phn, message,"contact");
            TempData["Message"] = "Employee Created Successfully";
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Appointment(string username, string Email, string phn, string date, string message)
        {
            SendMail(username, Email, phn, message, "appointment", date);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Mortgage(string username, string Email, string time, string date, string day,string amount)
        {
            SendMail(username, Email, "", "", "mortgage", date,day,time,amount);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RequestBack(string username, string Email, string Time, string Day)
        {
            SendMail(username, Email, Time,"", "request", "","",Day);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public bool SendMail(string Name, string Email="", string Phone="", string Message="",string Type="", string date="",string Day="",string time="",string amount="")
        {

            MailMessage message = new MailMessage();
            message.To.Add(WebConfigurationManager.AppSettings["FromEmailID"]);
            message.From = new MailAddress(WebConfigurationManager.AppSettings["FromEmailID"]);
            message.Subject = "Contact Mail";

            string body = "";
            body = "<p>Person Name : " + Name + "</p>";
            body = body + "<p>Email ID : " + Email + "</p>";
            if(Type!="mortgage")
            {
                body = body + "<p>Phone No : " + Phone + "</p>";
            }
           
            if(Type=="appointment")
            {
                body = body + "<p>Appointment Date : " + date + "</p>";
            }
            if (Type == "mortgage")
            {
                body = body + "<p>Renewal Date : " + date + "</p>";
                body = body + "<p>Time : " + time + "</p>";
                body = body + "<p>Amount : " + amount + "</p>";
                body = body + "<p>Day : " + Day + "</p>";
            }
            if (Type=="request")
            {
                body = body + "<p>Day : " + Day + "</p>";
            }
                if (Type != "mortgage")
            {
                body = body + "<p>Message : " + Message + "</p>";
            }
            message.Body = body;
            message.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(WebConfigurationManager.AppSettings["FromEmailID"], WebConfigurationManager.AppSettings["FromEmailPassword"]);
            smtpClient.EnableSsl = true;
            smtpClient.Send(message);
            return true;

        }
    }
}