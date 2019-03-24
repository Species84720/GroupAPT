using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Test2.Models;
using Test2.Models.DBModels;

namespace Test2.Controllers.DBControllers
{
    public class ExamsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Exam
        public async Task<ActionResult> Index(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamSession examSession = await db.ExamSessions.FindAsync(id);
            if (examSession == null)
            {
                return HttpNotFound();
            }

            // we get the paperquestions for this exam in a list
            List<PaperQuestion> paper = db.PaperQuestions.Where(x => x.ExamId == id).ToList();

            //now we generate a list of QuestionId found in the list above
            IEnumerable<int> SpecificQuestions = from p in paper select p.QuestionId;

            //and we select those Questions that match the QuestionIds
            List<Question> questionList = new List<Question>(from q in db.Questions where SpecificQuestions.Contains(q.QuestionId) select q);

            //now we get the multiple choices we need for this exam
            List<MultipleChoice> multichoiceList = new List<MultipleChoice>(from m in db.MultipleChoices where SpecificQuestions.Contains(m.QuestionId) select m);

            ViewBag.MultiChoices = multichoiceList;
            ViewBag.Questions = questionList;
            ViewBag.PaperQuestions = paper;

            //ViewBag.PaperQuestions = db.PaperQuestions.Where(x => x.ExamId == id).ToList();
            //ViewBag.Questions = db.Questions.ToList();
            ViewBag.Answers = db.StudentAnswers.ToList();
            ViewData["ExamId"] = id;
            
            return View();
        }
        
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind(Include = "AnswerId,EnrollmentId,PaperQuestionId,Answer,CommittedByStudent")] StudentAnswer studentAnswer)
        {
            if (ModelState.IsValid)
            {
                StudentAnswer newStudentAnswer = db.StudentAnswers.Where(x => x.EnrollmentId == studentAnswer.EnrollmentId).Where(x => x.PaperQuestionId == studentAnswer.PaperQuestionId).FirstOrDefault();
                if (newStudentAnswer != null)
                {
                    studentAnswer.AnswerId = newStudentAnswer.AnswerId;
                }
                db.StudentAnswers.AddOrUpdate(studentAnswer);

                await db.SaveChangesAsync();
            }

            return View();
        }
    }
}
