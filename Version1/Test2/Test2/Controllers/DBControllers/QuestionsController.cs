using System;
using System.Collections;
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
    public class QuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Questions
        public async Task<ActionResult> Index()
        {
            var questions = db.Questions.Include(q => q.RelatedSubject).Include(q => q.RelatedTopic);
            return View(await questions.ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await db.Questions.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }



        // GET: Questions/Create
        [Authorize(Roles = "Examiner")]
        public ActionResult Create(string subject)
        {
            // limiting choices of Subject to those of Examiner
            string TeacherId = User.Identity.GetUserId();

          //  List<string> specificsubjects = new List<string>(from t in db.Teachings where t.ExaminerId == TeacherId select t.SubjectId);
            //IEnumerable<Subject> subjectlist = new List<Subject>(from s in db.Subjects where specificsubjects.Contains(s.SubjectId) select s);

            // limiting choices of Topics  to those of Examiner's Subjects 

            List<Topic> topics = new List<Topic>(from t in db.Topics where subject==t.SubjectId select t);

            IEnumerable<Subject> subjectchosen = new List<Subject>(from s in db.Subjects where s.SubjectId == subject select s);
            ViewBag.Subject = subject;
            ViewBag.SubjectId = new SelectList(subjectchosen, "SubjectId", "SubjectName");
            ViewBag.TopicId = new SelectList(topics, "TopicId", "TopicName");


            Question q = new Question();
            q.SubjectId = subject;
            
            return View(q);
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "QuestionId,SubjectId,TopicId,QuestionUsage,QuestionText,SampleAnswer,QuestionFormat")] Question question)
        {
            
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                await db.SaveChangesAsync();
                // if the question is a MultiChoice add the Multichoices else go back

                if (question.QuestionFormat == Question.QuestionType.MultipleChoice ) { return RedirectToAction("Create", "MultipleChoices", new{ questionid = question.QuestionId}); }

                return RedirectToAction("EditQuestions","Examiner", new { subject = question.SubjectId });


            }


            // limiting choices of Subject to those of Examiner
            string TeacherId = User.Identity.GetUserId();
             

            List<Topic> topics = new List<Topic>(from t in db.Topics where question.SubjectId==t.SubjectId select t);

            ViewBag.Subject= question.SubjectId;
            ViewBag.SubjectId = question.SubjectId; // new SelectList(question.SubjectId, "SubjectId", "SubjectName", question.SubjectId);
            ViewBag.TopicId = new SelectList(topics, "TopicId", "TopicName", question.TopicId);

            return View(question);
        }

        // GET: Questions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await db.Questions.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", question.SubjectId);
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName", question.TopicId);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "QuestionId,SubjectId,TopicId,QuestionUsage,QuestionText,SampleAnswer,QuestionFormat")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                await db.SaveChangesAsync();

                if (question.QuestionFormat == Question.QuestionType.MultipleChoice)
                {
                    int multiplechoiceid =
                        (from m in db.MultipleChoices
                            where m.QuestionId == question.QuestionId
                            select m.MultipleChoiceId).FirstOrDefault();
                    
                    return RedirectToAction("Edit", "MultipleChoices", new { id = multiplechoiceid });
                }

                return RedirectToAction("EditQuestions", "Examiner", new { subject = question.SubjectId});
            }
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", question.SubjectId);
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName", question.TopicId);
            return View(question);
        }

        // GET: Questions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await db.Questions.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Question question = await db.Questions.FindAsync(id);
            db.Questions.Remove(question);
            await db.SaveChangesAsync();
            return RedirectToAction("EditQuestions", "Examiner", new { subject = question.SubjectId });
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
