using Microsoft.AspNetCore.Mvc;
using Progect.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Progect.BL.Helper;
using Microsoft.AspNetCore.Authorization;

namespace _3TierArchitecture.Controllers
{
    [Authorize]
    public class MailController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(MailVM model)
        {
            try
            {
                ViewBag.msg = MailSender.SendMail(model);
                return View();
            }
            // رجع الى راجع في التراي والكاتش
            catch 
            {
                ViewBag.msg = MailSender.SendMail(model);
                return View();
            }

        }
    }
}


//try
//{
//    //Host      //Port         
//    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

//    smtp.EnableSsl = true;

//    //الايميل الى هيبعت الرسايل                       Email             Password
//    smtp.Credentials = new NetworkCredential("ah449996@gmail.com", "wssass01012556787");

//    //مين الى هيستقبل//                  مين الى هيبعت 
//    smtp.Send("ah449996@gmail.com", "atharmohamed04@gmail.com", model.Title, model.Massage);

//    ViewBag.msg = "Mail Send Succssfully";
//    return View();
//}
//catch (Exception ex)
//{
//    ViewBag.msg = ex.Message;
//    return View();
//}