using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Test2.Models;
using Test2.Models.DBModels;

namespace Test2.Controllers.DBControllers
{
    public class ExamsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


         


        // GET: Exam
        public async Task<ActionResult> Index(string examid)
        {
            // checking that the request was posted on time.


            ViewData["ExamId"] = examid;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string AccessCode, string examid)
        {
            string username = User.Identity.GetUserName();



            ExamSession exam = new ExamSession();
            exam=(from e in db.ExamSessions where e.AccessCode==AccessCode && e.ExamId==examid select e ).FirstOrDefault();

            if (exam != null)
            {

                if(DateTime.Now.Minute>exam.CodeIssueDateTime.Value.Minute+6 || (DateTime.Now.Hour > exam.CodeIssueDateTime.Value.Hour) ) {
                    ViewData["ExamId"] = examid;
                        ViewData["Error"] = "This code has expired. " +
                                            "Ask the Invigilator for a new one";
                        return View();
                    
                }


                string id = examid;

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
                List<Question> questionList =
                    new List<Question>(from q in db.Questions where SpecificQuestions.Contains(q.QuestionId) select q);

                //now we get the multiple choices we need for this exam
                List<MultipleChoice> multichoiceList = new List<MultipleChoice>(from m in db.MultipleChoices
                                                                                where SpecificQuestions.Contains(m.QuestionId)
                                                                                select m);

                ViewBag.MultiChoices = multichoiceList;
                ViewBag.Questions = questionList;
                ViewBag.PaperQuestions = paper;

                //ViewBag.PaperQuestions = db.PaperQuestions.Where(x => x.ExamId == id).ToList();
                //ViewBag.Questions = db.Questions.ToList();

                //we get the StudentAnswers that concern us only
                string[] idSplit = id.Split('-');
                username = username + "-" + idSplit[0];
                IEnumerable<string> specificPaperQuestions = from p in paper select p.PaperQuestionId;
                List<StudentAnswer> answerList = new List<StudentAnswer>(from q in db.StudentAnswers where specificPaperQuestions.Contains(q.PaperQuestionId) select q);
                List<StudentAnswer> specificAnswerList = answerList.Where(x => x.EnrollmentId == username).ToList();
                ViewBag.Answers = specificAnswerList;
                ViewData["ExamId"] = id;

                ViewData["AccessCode"] = AccessCode;

                //now we get the subject
                string subjectid = (from q in questionList select q.SubjectId).FirstOrDefault();
                ViewBag.Subject = subjectid;

                return View();
            }
            else
            {
                Log log = new Log();
                log.Activity = "Attempted Exam Access Code for ExamSession" + examid;
                log.WhoId = User.Identity.GetUserId();
                log.When = DateTime.Now;

                db.Logs.AddOrUpdate(log);

                await db.SaveChangesAsync();

                ViewData["ExamId"] = examid;
                ViewData["Error"] = "This code is not correct";
                return View();
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Answer([Bind(Include = "AnswerId,EnrollmentId,PaperQuestionId,Answer,CommittedByStudent")] StudentAnswer studentAnswer)
        {
            string username = (string)Session["Username"];

            string exam = (from d in db.PaperQuestions where d.PaperQuestionId == studentAnswer.PaperQuestionId select d.ExamId).SingleOrDefault();

            // we get the paperquestions for this exam in a list
            List<PaperQuestion> paper = db.PaperQuestions.Where(x => x.ExamId == exam).ToList();

            //now we generate a list of QuestionId found in the list above
            IEnumerable<int> SpecificQuestions = from p in paper select p.QuestionId;

            //and we select those Questions that match the QuestionIds
            List<Question> questionList = new List<Question>(from q in db.Questions where SpecificQuestions.Contains(q.QuestionId) select q);

            //now we get the multiple choices we need for this exam
            List<MultipleChoice> multichoiceList = new List<MultipleChoice>(from m in db.MultipleChoices where SpecificQuestions.Contains(m.QuestionId) select m);

            ViewBag.MultiChoices = multichoiceList;
            ViewBag.Questions = questionList;
            ViewBag.PaperQuestions = paper;

            //we get the StudentAnswers that concern us only
            string[] idSplit = exam.Split('-');
            username = username + "-" + idSplit[0];
            IEnumerable<string> specificPaperQuestions = from p in paper select p.PaperQuestionId;
            List<StudentAnswer> answerList = new List<StudentAnswer>(from q in db.StudentAnswers where specificPaperQuestions.Contains(q.PaperQuestionId) select q);
            List<StudentAnswer> specificAnswerList = answerList.Where(x => x.EnrollmentId == username).ToList();
            ViewBag.Answers = specificAnswerList;
            ViewData["ExamId"] = exam;

            //now we get the subject
            string subjectid = (from q in questionList select q.SubjectId).FirstOrDefault();
            ViewBag.Subject = subjectid;

            if (ModelState.IsValid)
            {
                //check that student is really enrolled
                if (db.Enrollments.Find(username) != null)
                {
                    StudentAnswer newStudentAnswer = db.StudentAnswers
                        .Where(x => x.EnrollmentId == studentAnswer.EnrollmentId)
                        .Where(x => x.PaperQuestionId == studentAnswer.PaperQuestionId).FirstOrDefault();
                    if (newStudentAnswer != null)
                    {
                        studentAnswer.AnswerId = newStudentAnswer.AnswerId;
                    }

                    db.StudentAnswers.AddOrUpdate(studentAnswer);

                    await db.SaveChangesAsync();
                }
                else
                {
                    return HttpNotFound();
                }
            }

            return View("Index");
        }
    }
}
