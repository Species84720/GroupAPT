using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Group_APT.Models
{
    public class Question
    {
        public enum QuestionUse { Exam, Homework }
        public enum QuestionType { Text, MultipleChoice, Image }

        [Key] public string QuestionId { get; set; }

        [Required] public string SubjectCode { get; set; }
        [ForeignKey("SubjectCode")]
        public virtual Subject RelatedSubject { get; set; }

        [Required] public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User RelatedUser { get; set; }

        [Required] public string TopicId { get; set; }
        [ForeignKey("TopicId")]
        public virtual Topic RelatedTopic { get; set; }

        [Required] public QuestionUse QuestionUsage { get; set; }
        [Required] public string QuestionText { get; set; }
        [Required] public string SampleAnswer { get; set; }
        [Required] public QuestionType Type { get; set; }
    }
}
