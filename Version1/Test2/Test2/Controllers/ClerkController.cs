using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Test2.Models;
using Test2.Models.DBModels;

namespace Test2.Controllers
{
    public class ClerkController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clerk
        [Authorize(Roles = "Clerk")]
        public ActionResult ExamManager()
        {
             
            string user = User.Identity.GetUserId();

            int? department = (from u in db.Users where u.Id ==user select u.DepartmentId).FirstOrDefault();
             
            List<int> departments =new List<int>(from d in db.Departments where (d.DepartmentId ==department || d.DepartmentParentId==department) select d.DepartmentId );

            List<string> subjects = new List<string> (from s in db.Subjects where departments.Contains(s.DepartmentId) select s.SubjectId);
             
            List<ExamSession> exams = new List<ExamSession> (from e in db.ExamSessions where subjects.Contains(e.SubjectId) && e.Invigilations.Count==0 select e );

            List<string> examlist = new List<string>(from e in db.ExamSessions where subjects.Contains(e.SubjectId) select e.ExamId);

            
             
            

            List<Invigilation> invigilations = new List<Invigilation>(from i in db.Invigilations.Include(e=>e.RelatedExamSession).Include(l=>l.RelatedUser) where examlist.Contains(i.ExamId)  where examlist.Contains(i.ExamId) select i);

            ExamDetailsViewModel examDetails = new ExamDetailsViewModel {

                Sessions=exams,
                Invigilations= invigilations,
                Department = department
            };



            return View(examDetails);
        }


        [Authorize(Roles = "Clerk")]
        public ActionResult Statistics()
        {

            string user = User.Identity.GetUserId();

            int? department = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();

            List<int> departments = new List<int>(from d in db.Departments where (d.DepartmentId == department || d.DepartmentParentId == department) select d.DepartmentId);

            List<string> subjects = new List<string>(from s in db.Subjects where departments.Contains(s.DepartmentId) select s.SubjectId);

            IEnumerable<ExamSession> exams = new List<ExamSession>(from e in db.ExamSessions.Include(s =>s.RelatedSubject)
                where subjects.Contains(e.SubjectId) && e.FullyCorrected && e.ExamEndTime!=null && e.ExamEndTime<DateTime.Now 
                select e).OrderByDescending(e=>e.ExamDateTime.Value.Year);




            return View(exams);
        }

    }
}