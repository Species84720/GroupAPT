using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Test2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
         

        [Authorize]
        public ActionResult Dashboard()
        {
            if (Request.IsAuthenticated)
            {

                if (User.IsInRole("Student"))
                {
                     return RedirectToAction("Learner", "Dashboard");
                }


                if (User.IsInRole("Admin"))//Redirects based on Role
                {
                    return RedirectToAction("Admin", "Dashboard");
                }


                if (User.IsInRole("Examiner"))
                {
                    return RedirectToAction("Examiner", "Dashboard");
                }

                if (User.IsInRole("Clerk"))
                {
                    return RedirectToAction("Management", "Dashboard");
                }


                if (User.IsInRole("Invigilator"))
                {
                    return RedirectToAction("Invigilator", "Dashboard");
                }


                return RedirectToAction("Index", "Home");
            }





            return View();
        }

       
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
 

            return View();
        }
    }
}