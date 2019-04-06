using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;
using System.Web.UI.WebControls;
using Test2.Models.DBModels;

namespace Test2.Models
{

    public class ViewQuestionsViewModel
    {
         
        [Display(Name = "Pick Subject")]
        public string Subject { get; set; }

       

        public List<Subject> Subjects { get; set; }



        public int? Topic { get; set; }

        public List<Topic> Topics { get; set; }


        public List<Question> QuestionList { get; set; }

        

        public List<MultipleChoice> MultichoiceList { get; set; }

        

    }



    public class SetPaperViewModel
    {

        //SubjectId of Session being composed
        public string Subject { get; set; }
        public List<Subject> Subjects { get; set; }

        public string Exam { get; set; }

        //Session for which paper is being composed
        public string Session { get; set; }

       

       // public int? Topic { get; set; }
        //public List<Topic> Topics { get; set; }


        public List<Question> AvailableQuestions { get; set; }
        //public int QuestionToInclude { get; set; }

        public List<PaperQuestion> Included { get; set; }


        // Total marks in the Paper
        public int? Mark { get; set; }



    }

    public class CorrectingViewModel
    {

        [Display(Name = "Pick Subject")]
        public string Subject { get; set; }
        public List<Subject> Subjects { get; set; }

        public string Exam { get; set; }

        //Session for which paper is being composed
       // public string Session { get; set; }

         

       // public List<StudentAnswer> Answers { get; set; }
       
        public StudentAnswer Answer { get; set; }

        // public List<Question> Questions { get; set; }


        // Mark the tutor gives to the Answer
        //public int? MarkGiven { get; set; }


        // if all exams are corrected
        public bool AllSessionsCorrected { get; set; }

        //if all questions from a subject are corrected
        public bool AllQsCorrected { get; set; }

    }


   



    public class ExamDetailsViewModel
    {
        //public ExamSession Session {get;set;}
        
        

        public int? Department { get; set; }

        public List<ExamSession> Sessions { get; set; }

        public List<Location> Locations { get; set; }

        public List<ApplicationUser> Invigilators { get; set; }

        public List<Invigilation> Invigilations { get; set; }

      


    }


}
