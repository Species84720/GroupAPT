using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Test2.Models.DBModels
{
    public class PaperQuestion
    {
        [Key] public string PaperQuestionId { get; set; }

        [Required] public string ExamId { get; set; }
        [ForeignKey("ExamId")]
        public virtual ExamSession RelatedExamSession { get; set; }

        [Required] public string QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question RelatedQuestion { get; set; }

        [Required] public byte NumberInPaper { get; set; }
        [Required] public byte MarksAllocated { get; set; }




        /*
        public byte MaxMark { get; set; }
        public byte MinMark { get; set; }
        public float AvgMark { get; set; }
         */
    }
}
