using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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
        public ActionResult EditQuestions( string subject, int? topic)
        {

            string teacher = User.Identity.GetUserId();



            //We get a list of only subjects for this tutor
            List<string> subjectNames = new List<string>( from t in db.Teachings.Where(x => x.ExaminerId == teacher) select t.SubjectId);

            List<Subject> subjects = new List<Subject>(from s in db.Subjects where subjectNames.Contains(s.SubjectId) select s);


            List<Question> questionList;
            List<Topic> topics;


            if (String.IsNullOrEmpty(subject))
            {
            
            //and we select those Questions that match all the subjects
            questionList = new List<Question>(from q in db.Questions where subjectNames.Contains(q.SubjectId) select q);
                
                
            }
            else
            {

                if (topic==null) { 

                    questionList = new List<Question>(from q in db.Questions where q.SubjectId==subject select q);
                    
                }
                else
                {
                    questionList = new List<Question>(from q in db.Questions where q.SubjectId == subject && q.TopicId == topic select q);
                    
                    
                }

            }

            topics = new List<Topic>(from t in db.Topics where t.SubjectId == subject select t);

            List<int> SpecificQuestions = new List<int>(from q in questionList select q.QuestionId);



            //now we get the multiple choices we need for this exam
            List<MultipleChoice> multichoiceList = new List<MultipleChoice>(from m in db.MultipleChoices where SpecificQuestions.Contains(m.QuestionId) select m);

            ViewQuestionsViewModel viewmodel = new ViewQuestionsViewModel
            {
                
                Subjects = subjects,
                QuestionList = questionList,
                MultichoiceList = multichoiceList,
                Subject = subject,
                Topic= topic,
                Topics=topics 
            };
             



            return View(viewmodel);


        }



        public ActionResult PickSubject()
        {

            return RedirectToAction("EditPapers","Examiner");
        }







     // GET: Exam
     // GET: PaperQuestions
        public ActionResult EditPapers(string subject)
        {
            string teacher = User.Identity.GetUserId();



            //We get a list of only subjects for this tutor
            List<string> subjectNames = new List<string>(from t in db.Teachings.Where(x => x.ExaminerId == teacher) select t.SubjectId);

            List<Subject> subjects = new List<Subject>(from s in db.Subjects where subjectNames.Contains(s.SubjectId) select s);


            string session = (from e in db.ExamSessions where (e.SubjectId == subject && !e.FullyCorrected) select e.ExamId).SingleOrDefault();

            List<PaperQuestion> paperQuestions = new List<PaperQuestion>(from p in db.PaperQuestions orderby p.NumberInPaper where p.ExamId == session select p);

            List<int> questionsUsed =new List<int>(from p in db.PaperQuestions where p.ExamId ==session select p.QuestionId);

            List<Question> questionList= new List<Question>(from q in db.Questions where q.SubjectId ==subject && !questionsUsed.Contains(q.QuestionId) select q);

            IList<byte> marks = new List<byte> (from p in paperQuestions select p.MarksAllocated);

            int total = 0;
            foreach (PaperQuestion p in paperQuestions)
            {
                total = total+(int)p.MarksAllocated;
            }

            SetPaperViewModel viewmodel = new SetPaperViewModel
            {
                 
                Subject = subject,
            Subjects =subjects,
             Session  =session,
             AvailableQuestions  = questionList,
                Included = paperQuestions,
                Mark= total
       
             };




            
        return View(viewmodel);
         }

     


    }


}
