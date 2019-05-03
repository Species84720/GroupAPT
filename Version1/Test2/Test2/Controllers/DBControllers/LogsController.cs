using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Test2.Models;

namespace Test2.Controllers.DBControllers
{
    public class LogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Logs
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            
            return View(await db.Logs.Include(u=>u.RelatedUser).OrderByDescending(l=>l.When).ToListAsync());
        }
    }
}