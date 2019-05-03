using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using Microsoft.AspNet.Identity;
using Test2.Models;
using Test2.Models.DBModels;

namespace Test2.Controllers.DBControllers
{
    public class ExamsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string examid)
        {
            ViewData["ExamId"] = examid;
            return View();
        }

        /*
        public ActionResult ValidateStudent(string examid)
        {
            // checking that the request was posted on time.


            ViewData["ExamId"] = examid;
            return View();
        }
        */

        [HttpPost]
        public async Task<ActionResult> Index(string AccessCode, string examid, string imagename)
        {
            string username = User.Identity.GetUserId();

            if (examid == null)
            {
                RedirectToAction("Index", "Home");
            }

            //we get the subject code
            string[] idSplit = examid.Split('-');

            if (AccessCode != null)
            {
                Log log = new Log();
                log.Activity = "Attempted Exam Access Code for ExamSession: " + examid;
                log.WhoId = username;
                log.When = DateTime.Now;

                db.Logs.AddOrUpdate(log);

                await db.SaveChangesAsync();
            }

            ExamSession exam = new ExamSession();
            exam = (from e in db.ExamSessions where e.ExamId == examid select e).FirstOrDefault();

            Enrollment enroller = new Enrollment();
            string enrollerid = User.Identity.GetUserName() + "-" + idSplit[0];
            enroller = (from e in db.Enrollments where e.EnrollmentId == enrollerid select e).FirstOrDefault();

            if (imagename == "")
            {
                ViewData["ExamId"] = examid;
                ViewData["Error"] = "You have not submitted an image";
                return View();
            }

            if (enroller == null)
            {
                ViewData["ExamId"] = examid;
                ViewData["Error"] = "There was an error";
                return View();
            }

            if (exam == null)
            {
                ViewData["ExamId"] = examid;
                ViewData["Error"] = "Exam not found";
                return View();
            }

            if (DateTime.Now.Minute > exam.CodeIssueDateTime.Value.Minute + 6 ||
                (DateTime.Now.Hour > exam.CodeIssueDateTime.Value.Hour))
            {
                ViewData["ExamId"] = examid;
                ViewData["Error"] = "The last Access Code has expired. " +
                                    "Ask the Invigilator for a new one";
                return View();
            }
            else
            {
                if (AccessCode != exam.AccessCode)
                {

                    ViewData["ExamId"] = examid;
                    ViewData["Error"] = "This code is not correct";
                    return View();
                }
            }


            ViewData["ExamId"] = examid;

            ViewData["AccessCode"] = AccessCode;

            //now we get the subject

            ViewBag.Subject = exam.SubjectId;

            if (AccessCode == exam.AccessCode)
            {
                //getting the amount of pictures he already took
                //stick it to the user to get enrollment
                string enrollment = User.Identity.GetUserName() + "-" + idSplit[0];
                int imageAmount = db.Shots.Where(x => x.EnrollmentId == enrollment).Count() + 1;
                //set the file name
                string imageData = enrollment + "_" + imageAmount;
                //creating the file itself
                string[] trimmedImageName = imagename.Split(',');
                byte[] contents = Convert.FromBase64String(trimmedImageName[1]);
                System.IO.File.WriteAllBytes(Server.MapPath("/Captures/" + imageData + ".png"), contents);

                //placing location on database
                var UserImageDetails = new Shot
                {
                    EnrollmentId = enrollment,
                    ImageTitle = imageData,
                    ImageLocation = "/Captures/" + imageData + ".png",
                    ShotTiming = DateTime.Now
                };
                db.Shots.Add(UserImageDetails);
                db.SaveChanges();

                ViewData["ExamId"] = examid;
                ViewData["Error"] = "";
                //return RedirectToAction("Index","Snap",new { examid=exam.ExamId} );

                //redirects to a get
                //return RedirectToAction("ExamPage", new { examid=exam.ExamId } );

                List<PaperQuestion> paper = ExamPage(examid);
                return View(paper);
            }

            return View();
        }

        /*
        public ActionResult ExamPage(string examid)
        {        

            ExamSession exam = new ExamSession();
            exam = (from e in db.ExamSessions where e.ExamId == examid && e.FullyCorrected==false select e).FirstOrDefault();

            ViewData["ExamId"] = examid;
            ViewBag.Subject = exam.SubjectId;
            List<PaperQuestion> paper = new List<PaperQuestion>();

            paper =db.PaperQuestions.Where(x => x.ExamId == examid).ToList();

            return View(paper);
        }
        */

        //[HttpPost]
        //public ActionResult ExamPage(List<PaperQuestion> paper)
        //public ActionResult ExamPage(string examid)
        public List<PaperQuestion> ExamPage(string examid)
        {
            ExamSession exam = new ExamSession();
            exam = (from e in db.ExamSessions where e.ExamId == examid && e.FullyCorrected == false select e).FirstOrDefault();

            ViewData["ExamId"] = examid;
            ViewBag.Subject = exam.SubjectId;
            List<PaperQuestion> paper = new List<PaperQuestion>();

            paper = db.PaperQuestions.Where(x => x.ExamId == examid).OrderBy(x => x.NumberInPaper).ToList();

            //ViewBag.PaperQuestions = paper;

            string username = User.Identity.GetUserName();
            //string id = paper.First().ExamId;

            // we get the paperquestions for this exam in a list
            // List<PaperQuestion> paper = db.PaperQuestions.Where(x => x.ExamId == id).ToList();

            //now we generate a list of QuestionId found in the list above
            IEnumerable<int> SpecificQuestions = new List<int>(from p in paper select p.QuestionId);

            //and we select those Questions that match the QuestionIds
            List<Question> questionList = new List<Question>(from q in db.Questions where SpecificQuestions.Contains(q.QuestionId) select q);

            //now we get the multiple choices we need for this exam
            List<MultipleChoice> multichoiceList = new List<MultipleChoice>(from m in db.MultipleChoices
                where SpecificQuestions.Contains(m.QuestionId)
                select m);

            ViewBag.MultiChoices = multichoiceList;
            ViewBag.Questions = questionList;


            //ViewBag.PaperQuestions = db.PaperQuestions.Where(x => x.ExamId == id).ToList();
            //ViewBag.Questions = db.Questions.ToList();

            //we get the StudentAnswers that concern us only
            string[] idSplit = examid.Split('-');
            username = username + "-" + idSplit[0];
            IEnumerable<string> specificPaperQuestions = from p in paper select p.PaperQuestionId;
            List<StudentAnswer> answerList = new List<StudentAnswer>(from q in db.StudentAnswers
                where specificPaperQuestions.Contains(q.PaperQuestionId)
                select q);
            List<StudentAnswer> specificAnswerList = answerList.Where(x => x.EnrollmentId == username).ToList();
            ViewBag.Answers = specificAnswerList;
            ViewData["ExamId"] = examid;

            //now we get the subject
            string subjectid = (from q in questionList select q.SubjectId).FirstOrDefault();
            ViewBag.Subject = subjectid;

            //return View(paper);
            return paper;
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
