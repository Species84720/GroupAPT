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
        public ActionResult ExamManager()
        {
            string user = User.Identity.GetUserId();

            int? department = (from u in db.Users where u.Id ==user select u.DepartmentId).FirstOrDefault();

            List<int> departments =new List<int>(from d in db.Departments where (d.DepartmentId ==department || d.DepartmentParentId==department) select d.DepartmentId );

            List<string> subjects = new List<string> (from s in db.Subjects where departments.Contains(s.DepartmentId) select s.SubjectId);

            
            List<ExamSession> exams = new List<ExamSession> (from e in db.ExamSessions where subjects.Contains(e.SubjectId) select e );

            List<Location> locations = new List<Location>(from l in db.Locations select l);


            List<ApplicationUser> invigilators =new List<ApplicationUser>(from u in db.Users select u );//I found no way of filtering by Role :((

             


            ExamDetailsViewModel examDetails = new ExamDetailsViewModel
            {
                Sessions=exams,
                Invigilators = invigilators,
                Locations = locations
            };


            return View(examDetails);
        }
    }
}