using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Test2.Models;
using Test2.Models.DBModels;

namespace Test2.Controllers
{
    public class LearnerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        [Authorize(Roles = "Student")]
        public ActionResult Schedule()
        {
            string student = User.Identity.GetUserName();

            string name = (from u in db.Users where u.Id == student select u.FirstName + " " + u.Surname).SingleOrDefault();

            List<string> studentSubjects = new List<string> (from e in db.Enrollments where e.StudentId ==student && e.FinalAssessment==Enrollment.Assessment.Pending select  e.SubjectId);

            //we get the list of invigilations that have not yet happened, related to this invigilation
            var timeTable = (from e in db.ExamSessions.Include(e => e.RelatedLocation).Include(e => e.RelatedSubject) where  studentSubjects.Contains(e.SubjectId)  && e.ExamDateTime >= DateTime.Now select e);


            ViewBag.Name = name;

            return View(timeTable.ToList());
        }



        [Authorize(Roles = "Student")]
        public ActionResult Results()
        {
            string student = User.Identity.GetUserName();

            string name = (from u in db.Users where u.Id == student select u.FirstName + " " + u.Surname).SingleOrDefault();

            List<Enrollment> studentResults = new List<Enrollment>(from e in db.Enrollments where e.StudentId == student && e.FinalAssessment > Enrollment.Assessment.Present && e.SessionStatus > Enrollment.Status.Dubious select e );

             

            ViewBag.Name = name;

            return View(studentResults);
        }



        [Authorize(Roles = "Student")]
        public async Task<ActionResult> Resit(string id)
        {
            Enrollment enrollment =(from e in db.Enrollments where e.EnrollmentId==id select e).SingleOrDefault();
            if (enrollment!=null)
            {
                enrollment.ExamMark = 0;
                enrollment.SessionStatus = Enrollment.Status.Unchecked;
                enrollment.FinalAssessment = Enrollment.Assessment.Pending;

                // in future versions the failed exam is archived here.

                db.Entry(enrollment).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }



            return RedirectToAction("Results", "Learner");
        }


    }
}