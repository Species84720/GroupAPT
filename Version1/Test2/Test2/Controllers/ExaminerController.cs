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

            
            List<Subject> subjects = new List<Subject>(from s in db.Subjects where subjectNames.Contains(s.SubjectId) select s);


            List<Question> questionList;
            List<Topic> topics;
            string topicname="";

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

            if (topic != null)
            {
              topicname = (from t in db.Topics where t.TopicId == topic select t.TopicName).FirstOrDefault(); }

            ViewQuestionsViewModel viewmodel = new ViewQuestionsViewModel
            {

                Subjects = subjects,
                QuestionList = questionList,
                MultichoiceList = multichoiceList,
                Subject = subject,
                Topic = topic,
                Topics = topics,
                TopicName =topicname
            };




            return View(viewmodel);


        }

         




        // GET: Exam
        // GET: PaperQuestions
        public async Task<ActionResult> EditPapers(string subject)
        {
            string teacher = User.Identity.GetUserId();

            //We get a list of only subjects for this tutor
            List<string> subjectNames =
                new List<string>(from t in db.Teachings.Where(x => x.ExaminerId == teacher) select t.SubjectId);

            List<Subject> subjects =
                new List<Subject>(from s in db.Subjects where subjectNames.Contains(s.SubjectId) select s);

            SetPaperViewModel viewmodel;
            if (subject == null)
            {
                viewmodel = new SetPaperViewModel(){ Subjects = subjects};

                ViewBag.Alert = "";

                return View(viewmodel);
            }
            else if (subject == "")
            {
                viewmodel = new SetPaperViewModel() { Subjects = subjects };

                ViewBag.Alert = "Please select subject from the dropdown!";

                return View(viewmodel);
            }

            ExamSession examsession =
                (from e in db.ExamSessions where (e.SubjectId == subject && !e.FullyCorrected) select e)
                .SingleOrDefault();

            string session;

            //if there is no session for this subject, a new ExamSession is Created.
            if (examsession == null)
            {
                ExamSession newExamSession = new ExamSession();

                session = subject + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;

                newExamSession.ExamId = session;
                newExamSession.SubjectId = subject;

                db.ExamSessions.Add(newExamSession);
                await db.SaveChangesAsync();

                //regetting the examsession
                examsession =
                    (from e in db.ExamSessions where (e.SubjectId == subject && !e.FullyCorrected) select e)
                    .SingleOrDefault();
            }
            else
            {
                session = examsession.ExamId;
            }
            
            //if the exam is in a day or less
            if (examsession.ExamDateTime != null && examsession.ExamDateTime.Value.DayOfYear-DateTime.Now.DayOfYear <= 1  )
            {
                viewmodel = new SetPaperViewModel() { Subjects = subjects };

                //we don't permit editting of the paper.
                ViewBag.Alert = "Changing of a Paper is not permitted 24 hours prior to an exam. Call the Administrator if necessary";
                return View(viewmodel);
            }

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

             viewmodel = new SetPaperViewModel
            {

                Subject = subject,
                Subjects = subjects,
                Session = session,
                AvailableQuestions = questionList,
                Included = paperQuestions,
                Mark = total

            };


             ViewBag.Alert = "";


            return View(viewmodel);
        }




        public  ActionResult  CorrectExam (string subject)
        {
            CorrectingViewModel viewmodel;

            viewmodel = new CorrectingViewModel
            {

                AllQsCorrected = false,
                AllSessionsCorrected = false

            };


            string teacher = User.Identity.GetUserId();
             
            List<string> subjectNames;
            List<Subject> subjects;

                 

            //We get a list of only subjects for this tutor
            subjectNames =
                new List<string>(from t in db.Teachings.Where(x => x.ExaminerId == teacher) select t.SubjectId);
            
            subjects = new List<Subject>(from s in db.Subjects where subjectNames.Contains(s.SubjectId) select s);
                
            //and pass them to the viewmodel
                viewmodel.Subjects = subjects;
                
                //if no subject has been selected the view is returned without querying for questions
              if(subject==null)
              {
                  return View(viewmodel);
              }
            
            //if a subject has been selected we find its last session provided it is in the past
            string session  = (from e in db.ExamSessions where (e.SubjectId == subject  && e.FullyCorrected==false  && e.ExamEndTime<DateTime.Now && e.ExamEndTime!=null) select e.ExamId).SingleOrDefault();

            if (session == null)
            {

                session = (from e in db.ExamSessions where (e.SubjectId == subject && e.FullyCorrected == false) select e.ExamId).SingleOrDefault();

                if (session == null) viewmodel.ExamFullyCorrected = true;
                else  viewmodel.ExamNotEnded = true;


                string sessionCheck = (from e in db.ExamSessions where (e.SubjectId == subject && e.FullyCorrected == false && (e.ExamEndTime > DateTime.Now || e.ExamEndTime == null)) select e.ExamId).SingleOrDefault();
                if (sessionCheck == null)
                {
                    viewmodel.AllQsCorrected = true;
                    viewmodel.AllSessionsCorrected = true;
                }
                else
                { 
                    ViewBag.Error = "The exam sessions for this subject is still not yet finished.";
                }

                return View(viewmodel);
            }

            //if a session was found we find the PaperQuestions that are to be manually corrected
            List<PaperQuestion> paperquestions = new List<PaperQuestion>(from q in db.PaperQuestions
                where q.ExamId == session && q.RelatedQuestion.QuestionFormat == Question.QuestionType.WrittenAnswer
                select q);


            //if there are no PaperQuestions to be corrected manually
            if (paperquestions.Count != 0)
            {
                //  find   the student answer for this paper than need to be corrected manually
                List<string> paperquestionid = new List<string>(from q in paperquestions select q.PaperQuestionId);


                StudentAnswer answer =
                    (from a in db.StudentAnswers
                        where paperquestionid.Contains(a.PaperQuestionId) && a.CorrectorId == null
                        select a).FirstOrDefault();

                if (answer != null)
                {
                    answer.CorrectorId = teacher;
                    answer.CorrectedDateTime = DateTime.Now;
                    viewmodel.Answer = answer;
                }
                else
                { //if there are no answers to correct

                    viewmodel.AllQsCorrected = true;

                    AutoCorrectMC(session);
                }
            }
            else
            {
                //if there are no answers to correct

                viewmodel.AllQsCorrected = true;

                AutoCorrectMC(session);
            }


            viewmodel.Subject = subject;
            viewmodel.Exam = session;
            //  viewmodel.Questions = questions;
            



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

            //first we get the MultipleChoice PaperQuestions for the session
            List<PaperQuestion> paperquestions = new List<PaperQuestion>(from q in db.PaperQuestions
                where q.ExamId == session && q.RelatedQuestion.QuestionFormat == Question.QuestionType.MultipleChoice
                select q);

            List<string> paperquestionid = new List<string>(from q in paperquestions select q.PaperQuestionId);
            // List<int> questionid = new List<int>(from q in paperquestions select q.QuestionId);

            //then we get the StudentAnswers that match the paperquestions.  These need to be corrected
            List<StudentAnswer> answers = new List<StudentAnswer>(from a in db.StudentAnswers where paperquestionid.Contains( a.PaperQuestionId) && a.CorrectedDateTime==null select a );

            // now we update each of them depending on the student's answer and whether it corresponds to the right answer
            

            if (answers.Count != 0)
            {
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

                } //end for

            }  //end if


            //now we take all answers for this exam session to calculate the student's total
            answers = new List<StudentAnswer>(from a in db.StudentAnswers where a.RelatedPaperQuestion.RelatedExamSession.ExamId==session select a);


            Enrollment enrollment = new Enrollment();
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




            //now we make sure all Enrollment's FinalAssesment is updated, including those students that were absent
            //Absent students have their enrollment pending or Absent and have produced no StudentAnswer

            string subject = (from x in db.ExamSessions where x.ExamId==session select x.SubjectId).FirstOrDefault() ; 

            IEnumerable<Enrollment> enrollments = new List<Enrollment>(from e in db.Enrollments where e.SubjectId==subject && e.FinalAssessment<Enrollment.Assessment.Passed select e) ;

            foreach (Enrollment e in enrollments)
            {
                enrollment = e;

                if (enrollment.FinalAssessment == Enrollment.Assessment.Pending ||
                    enrollment.FinalAssessment == Enrollment.Assessment.Absent)
                {
                    enrollment.ExamMark = 0;
                    enrollment.SessionStatus = Enrollment.Status.Confirmed;
                    enrollment.FinalAssessment = Enrollment.Assessment.Absent;
                }
                if (enrollment.ExamMark == null) enrollment.ExamMark = 0;

                if (enrollment.ExamMark>= 45) enrollment.FinalAssessment = Enrollment.Assessment.Passed;
                else enrollment.FinalAssessment = Enrollment.Assessment.Failed;

                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();

            }


            //now we find the MAX mark and other stats using only those who sat for the exam
            enrollments = new List<Enrollment>(from a in answers select a.RelatedEnrollment).Distinct();

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

            if (count == 0) count = 1; //avoiding DIV by zero case;
            float avg = sum / count;


            //Now we add the found details and mark the exam as fully corrected

            ExamSession exam =db.ExamSessions.Find(session);

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
             
            
            return RedirectToAction("ExamDone", new { id=subject});


        }


        [Authorize(Roles = "Examiner")]
        public ActionResult Statistics()
        {

            string teacher = User.Identity.GetUserId();

            List<string> subjectNames =new List<string>(from t in db.Teachings.Where(x => x.ExaminerId == teacher) select t.SubjectId);

            IEnumerable<ExamSession> exams = new List<ExamSession>(from e in db.ExamSessions.Include(s => s.RelatedSubject)
                where subjectNames.Contains(e.SubjectId) && e.FullyCorrected && e.ExamEndTime != null && e.ExamEndTime < DateTime.Now
                select e).OrderByDescending(e => e.ExamDateTime.Value.Year);




            return View(exams);
        }


        [Authorize(Roles = "Examiner")]
        public ActionResult ExamDone(string id)
        {
            ViewBag.Subject = id;



            return View( );
        }



    }




}
