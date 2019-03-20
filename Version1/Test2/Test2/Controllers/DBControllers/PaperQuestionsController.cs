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
    public class PaperQuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PaperQuestions
        public async Task<ActionResult> Index()
        {
            var paperQuestions = db.PaperQuestions.Include(p => p.RelatedExamSession).Include(p => p.RelatedQuestion);
            return View(await paperQuestions.ToListAsync());
        }

        // GET: PaperQuestions/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaperQuestion paperQuestion = await db.PaperQuestions.FindAsync(id);
            if (paperQuestion == null)
            {
                return HttpNotFound();
            }
            return View(paperQuestion);
        }

        // GET: PaperQuestions/Create
        public ActionResult Create()
        {
            ViewBag.ExamId = new SelectList(db.ExamSessions, "ExamId", "ExamId");
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "QuestionText");
            return View();
        }

        // POST: PaperQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PaperQuestionId,ExamId,QuestionId,NumberInPaper,MarksAllocated")] PaperQuestion paperQuestion)
        {
            if (ModelState.IsValid)
            {
                db.PaperQuestions.Add(paperQuestion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ExamId = new SelectList(db.ExamSessions, "ExamId", "SubjectId", paperQuestion.ExamId);
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "SubjectId", paperQuestion.QuestionId);
            return View(paperQuestion);
        }

        // GET: PaperQuestions/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaperQuestion paperQuestion = await db.PaperQuestions.FindAsync(id);
            if (paperQuestion == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExamId = new SelectList(db.ExamSessions, "ExamId", "SubjectId", paperQuestion.ExamId);
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "SubjectId", paperQuestion.QuestionId);
            return View(paperQuestion);
        }

        // POST: PaperQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PaperQuestionId,ExamId,QuestionId,NumberInPaper,MarksAllocated")] PaperQuestion paperQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paperQuestion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ExamId = new SelectList(db.ExamSessions, "ExamId", "SubjectId", paperQuestion.ExamId);
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "SubjectId", paperQuestion.QuestionId);
            return View(paperQuestion);
        }

        // GET: PaperQuestions/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaperQuestion paperQuestion = await db.PaperQuestions.FindAsync(id);
            if (paperQuestion == null)
            {
                return HttpNotFound();
            }
            return View(paperQuestion);
        }

        // POST: PaperQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            PaperQuestion paperQuestion = await db.PaperQuestions.FindAsync(id);
            db.PaperQuestions.Remove(paperQuestion);
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
