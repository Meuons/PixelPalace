#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project.Data;
using Project.Models;
using System.Net.Mail;
using System.Net;

namespace Project.Controllers
{
    public class EmailController : Controller
    {
     

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string model)
        {
            SmtpClient smtp = new SmtpClient();
             var mailMessage = new MailMessage
             {
                 From = new MailAddress("mans16160@gmail.com"),

                 Subject = "subject",
                 Body = "<h1>Hello</h1>",
                 IsBodyHtml = true,
             };
            mailMessage.To.Add("mans99@live.se");
            smtp.Host = "smtp.gmail.com ";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("mans16160@gmail.com", "Tovvtw99!");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = NetworkCred;
            smtp.Send(mailMessage);
            return View();
        }
    }
}
