using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Test2.Models;
using Test2.Models.DBModels;

namespace Test2.Controllers.DBControllers
{
    public class ExamsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Exam
        public async Task<ActionResult> Index(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamSession examSession = await db.ExamSessions.FindAsync(id);
            if (examSession == null)
            {
                return HttpNotFound();
            }

            ViewBag.PaperQuestions = db.PaperQuestions.Where(x => x.ExamId == id).ToList();
            ViewBag.Questions = db.Questions.ToList();
            
            return View();
        }
    }
}