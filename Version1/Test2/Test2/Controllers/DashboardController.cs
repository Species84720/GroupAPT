using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Test2.Models;

namespace Test2.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize (Roles="Admin")]
        public ActionResult Admin( ApplicationUser user )
        {

            return View(user);
        }
         

       
         
    }
}