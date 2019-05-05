using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Test2.Models;
using Test2.Models.DBModels;

namespace Test2.Controllers.DBControllers
{
    public class EnrollmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Enrollments
        public  ActionResult Index()
        {
            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();
            List<int> depts = new List<int>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d.DepartmentId);

            
            List<string> subjects = new List<string >(from s in db.Subjects where depts.Contains(s.RelatedDepartment.DepartmentId) select s.SubjectId);

            var enrollments = (from e in db.Enrollments.Include(s => s.RelatedStudent).Include(s => s.RelatedSubject) where subjects.Contains(e.SubjectId) select e);
             
            
            return View( enrollments.ToList() );
        }

        // GET: Enrollments/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollments/Create
        [Authorize(Roles = "Clerk")]
        public ActionResult Create()
        {
            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();
            List<int> depts = new List<int>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d.DepartmentId);

         List<Student> students =new List<Student>(from s in db.Students where depts.Contains(s.RelatedUser.RelatedDepartment.DepartmentId)   select s);

            List<Subject> subjects = new List<Subject>(from s in db.Subjects where depts.Contains(s.RelatedDepartment.DepartmentId ) select s);

            ViewBag.StudentId = new SelectList(students, "StudentId", "StudentId");
            ViewBag.SubjectId = new SelectList(subjects, "SubjectId", "SubjectName");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EnrollmentId,StudentId,SubjectId,ExamMark,SeatNumber,SessionStatus,FinalAssessment")] Enrollment enrollment)
        {
            enrollment.EnrollmentId = enrollment.StudentId + "-" + enrollment.SubjectId;
            enrollment.SessionStatus = Enrollment.Status.Unchecked;
            enrollment.FinalAssessment = Enrollment.Assessment.Pending;
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();
            List<int> depts = new List<int>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d.DepartmentId);

            List<Student> students = new List<Student>(from s in db.Students where depts.Contains(s.RelatedUser.RelatedDepartment.DepartmentId) select s);

            List<Subject> subjects = new List<Subject>(from s in db.Subjects where depts.Contains(s.RelatedDepartment.DepartmentId) select s);

            ViewBag.StudentId = new SelectList(students, "StudentId", "StudentId");
            ViewBag.SubjectId = new SelectList(subjects, "SubjectId", "SubjectName");
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();
            List<int> depts = new List<int>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d.DepartmentId);

            List<Student> students = new List<Student>(from s in db.Students where depts.Contains(s.RelatedUser.RelatedDepartment.DepartmentId) select s);

            List<Subject> subjects = new List<Subject>(from s in db.Subjects where depts.Contains(s.RelatedDepartment.DepartmentId) select s);

            ViewBag.StudentId = new SelectList(students, "StudentId", "StudentId");
            ViewBag.SubjectId = new SelectList(subjects, "SubjectId", "SubjectName");
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EnrollmentId,StudentId,SubjectId,ExamMark,SeatNumber,SessionStatus,FinalAssessment")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();
            List<int> depts = new List<int>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d.DepartmentId);

            List<Student> students = new List<Student>(from s in db.Students where depts.Contains(s.RelatedUser.RelatedDepartment.DepartmentId) select s);

            List<Subject> subjects = new List<Subject>(from s in db.Subjects where depts.Contains(s.RelatedDepartment.DepartmentId) select s);

            ViewBag.StudentId = new SelectList(students, "StudentId", "StudentId");
            ViewBag.SubjectId = new SelectList(subjects, "SubjectId", "SubjectName");
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            db.Enrollments.Remove(enrollment);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
