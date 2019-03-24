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


}
