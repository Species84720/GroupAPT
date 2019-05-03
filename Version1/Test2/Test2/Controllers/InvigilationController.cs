using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Test2.Models;
using Test2.Models.DBModels;

namespace Test2.Controllers
{
    public class InvigilationController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Invigilation
        [Authorize(Roles = "Invigilator")]
        public ActionResult Schedule()
        {
            string ingivilator = User.Identity.GetUserId();

            string name = (from u in db.Users where u.Id==ingivilator select u.FirstName+" "+u.Surname ).SingleOrDefault();
            
            
        //we get the list of invigilations that have not yet happened, related to this invigilation
            List<Invigilation> invigilations  =new List<Invigilation>(from i in db.Invigilations where i.UserId==ingivilator && i.RelatedExamSession.ExamDateTime>=DateTime.Now select i);


            ViewBag.Name = name;

            return View(invigilations);
        }

        [Authorize(Roles = "Invigilator")]
        public ActionResult MakeCode()
        {
            string ingivilator = User.Identity.GetUserId();

            string code="" ;

            ExamSession session = (from i in db.Invigilations where i.UserId == ingivilator && i.RelatedExamSession.ExamDateTime<= DateTime.Now && i.RelatedExamSession.ExamEndTime >= DateTime.Now select i.RelatedExamSession ).FirstOrDefault();

            //we get the list of invigilations that have not yet happened, related to this invigilation
            //List<Invigilation> invigilations = new List<Invigilation>(from i in db.Invigilations where i.UserId == ingivilator && i.RelatedExamSession.ExamDateTime >= DateTime.Now select i);

            int num;

            if (session==null )
            {
                code = "You do not have any Active Exam  ";
                 

                ViewBag.Exam = "You are not during any exam right now";

                ViewBag.Code = code;
                return View();
            }

             
                Random rand = new Random();

                for (int i = 0; i <= 5; i++)
                {
                    num = rand.Next(0, 35);
                    if (num < 10)
                    {
                        code = code + num;
                    }
                    else
                    {
                        code = code + (char)('A' + num - 9);
                    }

                }//end for

                ExamSession toAlter = db.ExamSessions.Find(session.ExamId);

                if (toAlter != null) { //it shouldn't be null if session has a value

                    toAlter.AccessCode = code;
                toAlter.CodeIssueDateTime=DateTime.Now;
                db.Entry(toAlter).State = EntityState.Modified;
                db.SaveChanges();
                }

            

            ViewBag.Exam = session.RelatedSubject.SubjectName;
            ViewBag.Code = code;


            return View();
        }


        [Authorize(Roles = "Invigilator,Clerk")]
        public ActionResult IdCheck()
        {
             
                Enrollment enrollment = (from e in db.Enrollments.Include(e => e.Shots).Include(e => e.RelatedStudent)
                    where e.RelatedStudent.FacialImageTitle != null && e.Shots!=null && (e.SessionStatus == Enrollment.Status.Unchecked || e.SessionStatus == Enrollment.Status.Dubious)
                    select e).OrderBy(e => e.SessionStatus).First();

                if (enrollment == null) { return RedirectToAction("ShotsChecked", "Invigilation"); }

                
                return View(enrollment);
             
        }

        [Authorize(Roles = "Invigilator,Clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IdCheck([Bind(Include = "EnrollmentId,StudentId,SubjectId,ExamMark,SeatNumber,SessionStatus,FinalAssessment")] Enrollment enrollment)
        {
             
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return View();
            }


            return View(enrollment);

        }

 



        [Authorize(Roles = "Invigilator,Clerk")]
        public ActionResult ShotsChecked()
        {
            
            return View( );
        }


    }
}