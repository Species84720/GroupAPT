using System;
using System.Collections.Generic;
using System.ComponentModel;
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

         

        [DisplayName("Topic")]
        public int? TopicId { get; set; }
        [ForeignKey("TopicId")]
        public virtual Topic RelatedTopic { get; set; }

        [DisplayName("Usage")]
        public QuestionUse QuestionUsage { get; set; }

        [DisplayName("Text")]
        [Required] public string QuestionText { get; set; }

        [DisplayName("Sample Answer")]
        public string SampleAnswer { get; set; }
        [Required] public QuestionType QuestionFormat { get; set; } 
    }

    public class MultipleChoiceQuestion
    {
        public MultipleChoiceQuestion()
        {
            question = new Question();
            MQuestions = new MultipleChoice();
        }

        public Question question { get; set; }
        public MultipleChoice MQuestions { get; set; }

    }
}
