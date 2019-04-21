using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Test2.Models;
using Test2.Models.DBModels;

namespace Test2.Controllers.DBControllers
{
    [Authorize(Roles = "Examiner")]
    public class PaperQuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PaperQuestions
        public async Task<ActionResult> Index()
        {
            var paperQuestions = db.PaperQuestions.Include(p => p.RelatedExamSession).Include(p => p.RelatedQuestion).OrderBy(p =>p.NumberInPaper);

            


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
        public ActionResult Create(int questionid, string examid, string subject)
        {
            if (examid == null || subject==null ) { return RedirectToAction("EditPapers", "Examiner", new { subject = subject }); }

            PaperQuestion paperQuestion = new PaperQuestion();

            paperQuestion.PaperQuestionId = questionid + "-" + examid;
            paperQuestion.ExamId = examid;
            paperQuestion.QuestionId = questionid;
            paperQuestion.MarksAllocated = 0;
                byte nextNumber = (byte)(from p in db.PaperQuestions where p.ExamId == examid select p.ExamId).Count();
            nextNumber++;
            paperQuestion.NumberInPaper = nextNumber;

            db.PaperQuestions.Add(paperQuestion);
            db.SaveChanges();

            

            return RedirectToAction("EditPapers","Examiner", new{ subject=subject }); 
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
        public  ActionResult Up(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaperQuestion paperQuestionToUp = db.PaperQuestions.Find(id);
            if (paperQuestionToUp == null)
            {
                  return RedirectToAction("EditPapers", "Examiner");
            }


            PaperQuestion paperQuestionToDown = (from p in db.PaperQuestions
                where p.NumberInPaper== paperQuestionToUp.NumberInPaper-1 && p.ExamId == paperQuestionToUp.ExamId
                select p).FirstOrDefault() ;

            if (paperQuestionToDown != null)
            {
                paperQuestionToDown.NumberInPaper++;
                paperQuestionToUp.NumberInPaper--;
                //return RedirectToAction("EditPapers", "Examiner", new { subject = paperQuestionToUp.RelatedQuestion.SubjectId });
            }

            
            db.SaveChanges();


            return RedirectToAction("EditPapers", "Examiner", new { subject = paperQuestionToUp.RelatedQuestion.SubjectId });
        }



        public ActionResult Down(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaperQuestion paperQuestionToDown = db.PaperQuestions.Find(id);

            if (paperQuestionToDown == null)
            {
                return RedirectToAction("EditPapers", "Examiner" );
            }


            PaperQuestion paperQuestionToUp = (from p in db.PaperQuestions
                where p.NumberInPaper == paperQuestionToDown.NumberInPaper+1 && p.ExamId == paperQuestionToDown.ExamId
                select p).FirstOrDefault();

            if (paperQuestionToUp != null)
            {
                paperQuestionToUp.NumberInPaper--;
                paperQuestionToDown.NumberInPaper++;
                // return RedirectToAction("EditPapers", "Examiner", new { subject = paperQuestionToDown.RelatedQuestion.SubjectId });
            }

            
            db.SaveChanges();


            return RedirectToAction("EditPapers", "Examiner", new { subject = paperQuestionToDown.RelatedQuestion.SubjectId });
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
                string Subject = (from q in db.Questions where q.QuestionId == paperQuestion.QuestionId select q.SubjectId).SingleOrDefault();
                db.Entry(paperQuestion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("EditPapers", "Examiner", new { subject = Subject });
            }
            ViewBag.ExamId = new SelectList(db.ExamSessions, "ExamId", "SubjectId", paperQuestion.ExamId);
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "SubjectId", paperQuestion.QuestionId);
            return View(paperQuestion);
        }


        // GET: PaperQuestions/Delete/5
        public async Task<ActionResult> Delete(string id, string session)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // we find the question to be deleted
            PaperQuestion paperQuestion = await db.PaperQuestions.FindAsync(id);
            if (paperQuestion == null)
            {
                return HttpNotFound();
            }

            // we find the last question
            byte lastQNumber = (from p in db.PaperQuestions where p.ExamId==session select p.NumberInPaper).Max();
            PaperQuestion lastQuestion = (from p in db.PaperQuestions where p.NumberInPaper==lastQNumber && p.ExamId==session select p).SingleOrDefault();

            // we give the last question the same number as the one to be deleted.
            // this way the last question will jump to its position
            lastQuestion.NumberInPaper = paperQuestion.NumberInPaper;
            db.SaveChanges();

            return View(paperQuestion);
        }

        // POST: PaperQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id )
        {
            PaperQuestion paperQuestion = await db.PaperQuestions.FindAsync(id);

            string Subject = (from q in db.Questions where q.QuestionId == paperQuestion.QuestionId select q.SubjectId).SingleOrDefault();

            db.PaperQuestions.Remove(paperQuestion);

            

            await db.SaveChangesAsync();


            return RedirectToAction("EditPapers", "Examiner", new { subject = Subject });
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
