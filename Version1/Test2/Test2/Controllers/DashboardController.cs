using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Test2.Models;
using Test2.Models.DBModels;

namespace Test2.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize (Roles="Admin")]
        public ActionResult Admin( ApplicationUser user )
        {

            return View(user);
        }

        [Authorize(Roles = "Student")]
        public ActionResult Learner(ApplicationUser user)
        {
            string student = User.Identity.GetUserName();
            List<string> studentSubjects = new List<string>(from e in db.Enrollments where e.StudentId ==student && e.FinalAssessment == Enrollment.Assessment.Pending select e.SubjectId);
            // 

            ExamSession currentexam = new ExamSession();
            currentexam= (from e in db.ExamSessions
                where studentSubjects.Contains(e.SubjectId) && e.ExamDateTime <= DateTime.Now && e.ExamEndTime >= DateTime.Now
                          select e).FirstOrDefault() ;

            ViewBag.Exam = null;
            ViewBag.Subject = null;

            if (currentexam !=null)
            {
                ViewBag.Exam = currentexam.ExamId;
                ViewBag.Subject = studentSubjects[0];

                RedirectToAction("ValidateStudent", "Exams", new { examid = ViewBag.Exam }  );  //
            }

            
           

            return View(user);
        }


        [Authorize(Roles = "Clerk")]
        public ActionResult Management(ApplicationUser user)
        {

            return View(user);
        }


        [Authorize(Roles = "Examiner")]
        public ActionResult Examiner(ApplicationUser user)
        {


            return View(user);
        }


        [Authorize(Roles = "Invigilator")]
        public ActionResult Invigilator(ApplicationUser user)
        {

            return View(user);
        }

    }
}