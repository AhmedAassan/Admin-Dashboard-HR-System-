using Progect.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Progect.BL.Helper
{
    public static class  MailSender
    {
        public static string SendMail(MailVM model)
        {
            try
            {
                //Host      //Port         
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                smtp.EnableSsl = true;

                //الايميل الى هيبعت الرسايل                       Email             Password
                smtp.Credentials = new NetworkCredential("ah449996@gmail.com", "wssass01022");

                //مين الى هيستقبل//                  مين الى هيبعت 
                smtp.Send("ah449996@gmail.com", model.Mail, model.Title, model.Massage);

                var result = "Mail Send Succssfully";
                return result;
            }
            catch (Exception ex)
            {
                var result = ex.Message;
                return result;
            }
        }
    }
}
