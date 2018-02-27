using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegistrationProcess.Controllers
{
    public class ProductRegistrationController : Controller
    {
        // GET: ProductRegistration
        public ActionResult RegisterProduct()
        {
            return View();
        }
    }
}