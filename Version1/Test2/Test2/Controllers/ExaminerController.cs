using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Test2.Models;
using Test2.Models.DBModels;

namespace Test2.Controllers.DBControllers
{
    public class ExaminerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Exam
        public ActionResult EditQuestions(string subject, int? topic)
        {

            string teacher = User.Identity.GetUserId();



            //We get a list of only subjects for this tutor
            List<string> subjectNames =
                new List<string>(from t in db.Teachings.Where(x => x.ExaminerId == teacher) select t.SubjectId);

            List<Subject> subjects =
                new List<Subject>(from s in db.Subjects where subjectNames.Contains(s.SubjectId) select s);


            List<Question> questionList;
            List<Topic> topics;


            if (String.IsNullOrEmpty(subject))
            {

                //and we select those Questions that match all the subjects
                questionList =
                    new List<Question>(from q in db.Questions where subjectNames.Contains(q.SubjectId) select q);


            }
            else
            {

                if (topic == null)
                {

                    questionList = new List<Question>(from q in db.Questions where q.SubjectId == subject select q);

                }
                else
                {
                    questionList = new List<Question>(from q in db.Questions
                        where q.SubjectId == subject && q.TopicId == topic
                        select q);


                }

            }

            topics = new List<Topic>(from t in db.Topics where t.SubjectId == subject select t);

            List<int> SpecificQuestions = new List<int>(from q in questionList select q.QuestionId);



            //now we get the multiple choices we need for this exam
            List<MultipleChoice> multichoiceList = new List<MultipleChoice>(from m in db.MultipleChoices
                where SpecificQuestions.Contains(m.QuestionId)
                select m);

            ViewQuestionsViewModel viewmodel = new ViewQuestionsViewModel
            {

                Subjects = subjects,
                QuestionList = questionList,
                MultichoiceList = multichoiceList,
                Subject = subject,
                Topic = topic,
                Topics = topics
            };




            return View(viewmodel);


        }



        public ActionResult PickSubject() //function is redundant as fuck 
        {

            return RedirectToAction("EditPapers", "Examiner");
        }





        // GET: Exam
        // GET: PaperQuestions
        public ActionResult EditPapers(string subject)
        {
            string teacher = User.Identity.GetUserId();



            //We get a list of only subjects for this tutor
            List<string> subjectNames =
                new List<string>(from t in db.Teachings.Where(x => x.ExaminerId == teacher) select t.SubjectId);

            List<Subject> subjects =
                new List<Subject>(from s in db.Subjects where subjectNames.Contains(s.SubjectId) select s);


            string session =
                (from e in db.ExamSessions where (e.SubjectId == subject && !e.FullyCorrected) select e.ExamId)
                .SingleOrDefault();

            List<PaperQuestion> paperQuestions = new List<PaperQuestion>(from p in db.PaperQuestions
                orderby p.NumberInPaper
                where p.ExamId == session
                select p);

            List<int> questionsUsed =
                new List<int>(from p in db.PaperQuestions where p.ExamId == session select p.QuestionId);

            List<Question> questionList = new List<Question>(from q in db.Questions
                where q.SubjectId == subject && !questionsUsed.Contains(q.QuestionId)
                select q);

            //IList<byte> marks = new List<byte>(from p in paperQuestions select p.MarksAllocated);

            int total = 0;
            foreach (PaperQuestion p in paperQuestions)
            {
                total = total + (int) p.MarksAllocated;
            }

            SetPaperViewModel viewmodel = new SetPaperViewModel
            {

                Subject = subject,
                Subjects = subjects,
                Session = session,
                AvailableQuestions = questionList,
                Included = paperQuestions,
                Mark = total

            };





            return View(viewmodel);
        }




        public  ActionResult  CorrectExam (string subject)
        {
             

            string teacher = User.Identity.GetUserId();

            List<string> subjectNames;
            List<Subject> subjects;
            string session;
            // List<StudentAnswer> answers;

            session = (from e in db.ExamSessions where (e.SubjectId == subject) select e.ExamId).SingleOrDefault();

            CorrectingViewModel viewmodel;

            viewmodel = new CorrectingViewModel
            {

                AllQsCorrected = false,
                AllSessionsCorrected = false

            };


            //We get a list of only subjects for this tutor
            subjectNames =
                new List<string>(from t in db.Teachings.Where(x => x.ExaminerId == teacher) select t.SubjectId);

            List<string> uncorrected = new List<string>(from e in db.ExamSessions
                where subjectNames.Contains(e.SubjectId) && !e.FullyCorrected
                select e.SubjectId);

            subjects = new List<Subject>(from s in db.Subjects where uncorrected.Contains(s.SubjectId) select s);


            //if there are no uncorrected sessions
            if (uncorrected.Count == 0)
            {
                viewmodel.AllSessionsCorrected  = true;

               return RedirectToAction("Examiner", "Dashboard");
            }


            List<PaperQuestion> paperquestions = new List<PaperQuestion>(from q in db.PaperQuestions
                where q.ExamId == session && q.RelatedQuestion.QuestionFormat == Question.QuestionType.WrittenAnswer
                select q);

            List<string> paperquestionid = new List<string>(from q in paperquestions select q.PaperQuestionId);
            // List<int> questionid = new List<int>(from q in paperquestions select q.QuestionId);

            // answers = new List<StudentAnswer>(from a in db.StudentAnswers where paperquestionid.Contains( a.PaperQuestionId) && a.CorrectorId==null select a );

            StudentAnswer answer =
                (from a in db.StudentAnswers
                    where paperquestionid.Contains(a.PaperQuestionId) && a.CorrectorId == null
                    select a).FirstOrDefault();

            // List<Question> questions =new List<Question>(from q in db.Questions where questionid.Contains(q.QuestionId) select q);

            if (answer != null)
            {
                answer.CorrectorId = teacher;
                answer.CorrectedDateTime = DateTime.Now;
            }
            else
            { //if there are no answers to correct

                viewmodel.AllQsCorrected  = true;

                AutoCorrectMC(session);
            }

            viewmodel.Subject = subject;
            viewmodel.Subjects = subjects;
            viewmodel.Exam = session;
            //  viewmodel.Questions = questions;
            viewmodel.Answer = answer;



            return View(viewmodel);


        } // end CorrectExam



        public ActionResult Correct(CorrectingViewModel viewmodel)
        {

            
            int id = viewmodel.Answer.AnswerId;

            StudentAnswer studentAnswer = db.StudentAnswers.Find(id);

            //studentAnswer.ExaminerComments = viewmodel.Answer.ExaminerComments;

            if (studentAnswer == null) { return RedirectToAction("CorrectExam", "Examiner", new { subject = viewmodel.Subject });  }
            studentAnswer.ExaminerComments = viewmodel.Answer.ExaminerComments;
            studentAnswer.MarksGained = viewmodel.Answer.MarksGained;
            studentAnswer.CorrectedDateTime = viewmodel.Answer.CorrectedDateTime;
            studentAnswer.CorrectorId = viewmodel.Answer.CorrectorId;



            db.Entry(studentAnswer).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("CorrectExam", "Examiner", new { subject = viewmodel.Subject });
            
            //return RedirectToAction("Examiner", "Dashboard");
        }

        public ActionResult AutoCorrectMC(string session)
        {// this function recieves the session for which all Written Answers have been corrected
         // it then automatically corrects Multiple CHoice questions
         // finally marking the exam as fully corrected

            //first we get the PaperQuestions for the session
            List<PaperQuestion> paperquestions = new List<PaperQuestion>(from q in db.PaperQuestions
                where q.ExamId == session && q.RelatedQuestion.QuestionFormat == Question.QuestionType.MultipleChoice
                select q);

            List<string> paperquestionid = new List<string>(from q in paperquestions select q.PaperQuestionId);
            // List<int> questionid = new List<int>(from q in paperquestions select q.QuestionId);

            //then we get the StudentAnswers that match the paperquestions.  These need to be corrected
            List<StudentAnswer> answers = new List<StudentAnswer>(from a in db.StudentAnswers where paperquestionid.Contains( a.PaperQuestionId) && a.CorrectedDateTime==null select a );

            // now we update each of them depending on the student's answer and whtether it corresponds to the right answer
            StudentAnswer temp = new StudentAnswer();
            foreach (StudentAnswer ans in answers)
            {
                temp = db.StudentAnswers.Find(ans.AnswerId);

                temp.CorrectedDateTime=DateTime.Now;


                temp.ExaminerComments = "Corrected Automatically";

                int correctAnswer = (int) (from m in db.MultipleChoices
                    where (ans.RelatedPaperQuestion.QuestionId == m.QuestionId)
                    select m.CorrectChoice).SingleOrDefault();

                if (ans.Answer == correctAnswer.ToString()) temp.MarksGained = ans.RelatedPaperQuestion.MarksAllocated;
                else temp.MarksGained = 0;



                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();


            }


            //now we take all answers for this exam session to calculate the students total
            answers = new List<StudentAnswer>(from a in db.StudentAnswers where a.RelatedPaperQuestion.RelatedExamSession.ExamId==session select a);

            Enrollment enrollment =new Enrollment();

            foreach (StudentAnswer ans in answers)
            {
                enrollment = ans.RelatedEnrollment;

                if (enrollment.ExamMark == null) enrollment.ExamMark = 0;
                 int a = (int)enrollment.ExamMark;
                int b =  ans.MarksGained;
                int c = a + b;
                enrollment.ExamMark = (byte) c;

                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();

            }

            //now we find the MAX mark and other stats 
           

            IEnumerable<Enrollment> enrollments = new List<Enrollment>(from a in answers select a.RelatedEnrollment).Distinct();

            byte? max = (from e in enrollments select e.ExamMark).Max();
            byte? min = (from e in enrollments select e.ExamMark).Min();
            List<byte?>  allmarks = new List<byte?> (from e in enrollments select e.ExamMark);

            float sum = 0;
            float count = 0;
            int fails = 0;

            foreach (byte? m in allmarks)
            {
                if (m != null) sum = sum + (float) m;
                else sum = sum + 0;

                count++;
                if (m < 45) fails++;
            }

            float avg = sum / count;


            //Now we add the found details and mark the exam as fully corrected

            ExamSession exam = new ExamSession();

            exam =db.ExamSessions.Find(session);

            if(exam==null) { return RedirectToAction("Examiner", "Dashboard"); }


            if (max != null) exam.MaxMark = (byte) max;
            else exam.MaxMark = 0;

            if (min != null) exam.MinMark = (byte)min;
            else exam.MinMark = 0;

            exam.AvgMark = avg;

            exam.NumOfParticipants = (int) count;

            exam.NumOfFails =  fails;

            exam.FullyCorrected = true;
            db.Entry(exam).State = EntityState.Modified;
            db.SaveChanges();
             



            return RedirectToAction("Examiner", "Dashboard");


        }



    }
}
