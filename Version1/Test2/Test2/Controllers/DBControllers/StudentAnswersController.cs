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
    public class StudentAnswersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentAnswers
        public async Task<ActionResult> Index()
        {
            var studentAnswers = db.StudentAnswers.Include(s => s.RelatedCorrector).Include(s => s.RelatedEnrollment).Include(s => s.RelatedPaperQuestion);
            return View(await studentAnswers.ToListAsync());
        }

        // GET: StudentAnswers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAnswer studentAnswer = await db.StudentAnswers.FindAsync(id);
            if (studentAnswer == null)
            {
                return HttpNotFound();
            }
            return View(studentAnswer);
        }

        // GET: StudentAnswers/Create
        public ActionResult Create()
        {
            ViewBag.CorrectorId = new SelectList(db.ApplicationUsers, "Id", "FirstName");
            ViewBag.EnrollmentId = new SelectList(db.Enrollments, "EnrollmentId", "StudentId");
            ViewBag.PaperQuestionId = new SelectList(db.PaperQuestions, "PaperQuestionId", "ExamId");
            return View();
        }

        // POST: StudentAnswers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AnswerId,EnrollmentId,PaperQuestionId,CorrectorId,Answer,ExaminerComments,MarksGained,CorrectedDateTime,CommittedByStudent")] StudentAnswer studentAnswer)
        {
            if (ModelState.IsValid)
            {
                db.StudentAnswers.Add(studentAnswer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CorrectorId = new SelectList(db.ApplicationUsers, "Id", "FirstName", studentAnswer.CorrectorId);
            ViewBag.EnrollmentId = new SelectList(db.Enrollments, "EnrollmentId", "StudentId", studentAnswer.EnrollmentId);
            ViewBag.PaperQuestionId = new SelectList(db.PaperQuestions, "PaperQuestionId", "ExamId", studentAnswer.PaperQuestionId);
            return View(studentAnswer);
        }

        // GET: StudentAnswers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAnswer studentAnswer = await db.StudentAnswers.FindAsync(id);
            if (studentAnswer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CorrectorId = new SelectList(db.ApplicationUsers, "Id", "FirstName", studentAnswer.CorrectorId);
            ViewBag.EnrollmentId = new SelectList(db.Enrollments, "EnrollmentId", "StudentId", studentAnswer.EnrollmentId);
            ViewBag.PaperQuestionId = new SelectList(db.PaperQuestions, "PaperQuestionId", "ExamId", studentAnswer.PaperQuestionId);
            return View(studentAnswer);
        }

        // POST: StudentAnswers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AnswerId,EnrollmentId,PaperQuestionId,CorrectorId,Answer,ExaminerComments,MarksGained,CorrectedDateTime,CommittedByStudent")] StudentAnswer studentAnswer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentAnswer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CorrectorId = new SelectList(db.ApplicationUsers, "Id", "FirstName", studentAnswer.CorrectorId);
            ViewBag.EnrollmentId = new SelectList(db.Enrollments, "EnrollmentId", "StudentId", studentAnswer.EnrollmentId);
            ViewBag.PaperQuestionId = new SelectList(db.PaperQuestions, "PaperQuestionId", "ExamId", studentAnswer.PaperQuestionId);
            return View(studentAnswer);
        }

        // GET: StudentAnswers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAnswer studentAnswer = await db.StudentAnswers.FindAsync(id);
            if (studentAnswer == null)
            {
                return HttpNotFound();
            }
            return View(studentAnswer);
        }

        // POST: StudentAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StudentAnswer studentAnswer = await db.StudentAnswers.FindAsync(id);
            db.StudentAnswers.Remove(studentAnswer);
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
