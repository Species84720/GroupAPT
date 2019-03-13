using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Test2.Models.DBModels
{
    public class Question
    {
        public enum QuestionUse { AllContexts, Exam, Homework, ClassWork, ClassTest }
        public enum QuestionType { WrittenAnswer, MultipleChoice, Image }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }

        [Required] public string SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject RelatedSubject { get; set; }

        /*
        [Required] public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User RelatedUser { get; set; }
        */

        public int? TopicId { get; set; }
        [ForeignKey("TopicId")]
        public virtual Topic RelatedTopic { get; set; }

        public QuestionUse QuestionUsage { get; set; }
        [Required] public string QuestionText { get; set; }
         public string SampleAnswer { get; set; }
        [Required] public QuestionType QuestionFormat { get; set; } 

         
      //  public virtual MultipleChoice Choices { get; set; }

    }
}
