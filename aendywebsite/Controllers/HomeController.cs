using aendywebsite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace aendywebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                if (Session["show-active"] == null)
                {
                    Session["show-active"] = "";
                }
                string filePathProduct = Server.MapPath("~/JsonModels/product.json");
                string jsonTextProduct = System.IO.File.ReadAllText(filePathProduct);
                List<Product> resultProduct = JsonConvert.DeserializeObject<List<Product>>(jsonTextProduct);

                string filePathCategories = Server.MapPath("~/JsonModels/categories.json");
                string jsonTextCategories = System.IO.File.ReadAllText(filePathCategories);
                List<Categories> resultCategories = JsonConvert.DeserializeObject<List<Categories>>(jsonTextCategories);
                Session["lst-Categories"] = resultCategories;

                string filePathHappyclient = Server.MapPath("~/JsonModels/happyclient.json");
                string jsonTextHappyclient = System.IO.File.ReadAllText(filePathHappyclient);
                List<Client> resultHappyclient = JsonConvert.DeserializeObject<List<Client>>(jsonTextHappyclient);
                long total = resultHappyclient.First().Total + 1;

                Client client = resultHappyclient.First();
                client.Total = total;

                string newJsonText = JsonConvert.SerializeObject(resultHappyclient);
                System.IO.File.WriteAllText(filePathHappyclient, newJsonText);
                Session["total-happlyclient"] = total;

                return View("Index", resultProduct);
            }
            catch (Exception)
            {
                return View("Error");
            }

        }
        public ActionResult ProductDetail(int id)
        {
            try
            {
                string filePathProduct = Server.MapPath("~/JsonModels/product.json");
                string jsonTextProduct = System.IO.File.ReadAllText(filePathProduct);
                List<Product> resultProduct = JsonConvert.DeserializeObject<List<Product>>(jsonTextProduct);
                var product = resultProduct.Find(p => p.ID == id);

                string filePathHappyclient = Server.MapPath("~/JsonModels/happyclient.json");
                string jsonTextHappyclient = System.IO.File.ReadAllText(filePathHappyclient);
                List<Client> resultHappyclient = JsonConvert.DeserializeObject<List<Client>>(jsonTextHappyclient);
                long total = resultHappyclient.First().Total + 1;

                Client client = resultHappyclient.First();
                client.Total = total;

                string newJsonText = JsonConvert.SerializeObject(resultHappyclient);
                System.IO.File.WriteAllText(filePathHappyclient, newJsonText);
                Session["total-happlyclient"] = total;

                return View("ProductDetail", product);
            }
            catch (Exception)
            {
                return View("Error");
            }

        }
        [HttpPost]
        public ActionResult sendMail(string name, string email, string subject, string message)
        {
            try
            {
                //Gửi mật khẩu đến email
                using (MailMessage mailMessage = new MailMessage("dnguyenhoang94@gmail.com", "dv.tuan3010@gmail.com"))
                {
                    mailMessage.Subject = subject;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = "Gửi từ: " + email + "<br/><br/>" + message;

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential cred = new NetworkCredential("dnguyenhoang94@gmail.com", "oloxoxcxdzyzpbph");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = cred;
                        smtp.Port = 587;

                        smtp.Send(mailMessage);
                    }
                }
                return Content("OK");
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }

        }
    }
}