using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RegistrationProcess.Models;
using System.Web.UI.WebControls;

namespace RegistrationProcess.Controllers
{
    public class ProductRegistrationController : Controller
    {
        // GET: ProductRegistration
        [HttpGet]
        public ActionResult RegisterProduct()
        {
            return View();
        }

        /// <summary>
        /// Registers the product.
        /// </summary>
        /// <param name="pInfo">The product information.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterProduct(ProductInformation pInfo, string[] chkService)
        {
            using (ebispocdatabaseEntities data = new ebispocdatabaseEntities())
            {
                foreach(string item in chkService)
                {
                    ViewBag.Message += item + "<br/>";
                }
                Guid g = Guid.NewGuid();
                string GuidString = Convert.ToBase64String(g.ToByteArray());
                GuidString = GuidString.Replace("=", "");
                GuidString = GuidString.Replace("+", "");

                ViewBag.Message += $"Successfully registered your product.";
                ViewBag.SecretMessage = "Your secret key is";
                ViewBag.SecretKey = GuidString;
            }
            return View(pInfo);
        }
    }
}