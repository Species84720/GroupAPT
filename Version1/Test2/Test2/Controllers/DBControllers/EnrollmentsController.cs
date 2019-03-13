using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test2.Models;
using Test2.Models.DBModels;

namespace Test2.Controllers.DBControllers
{
    public class EnrollmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Enrollments
        public async Task<ActionResult> Index()
        {
            var enrollments = db.Enrollments.Include(e => e.RelatedStudent).Include(e => e.RelatedSubject);
            return View(await enrollments.ToListAsync());
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
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentId");
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");
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
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentId", enrollment.StudentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", enrollment.SubjectId);
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
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentId", enrollment.StudentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", enrollment.SubjectId);
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
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "SubjectId", enrollment.StudentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", enrollment.SubjectId);
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
