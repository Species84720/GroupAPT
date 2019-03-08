using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Group_APT.Models
{
    public class StudentAnswer
    {
        [Key] public string AnswerId { get; set; }

        [Required] public string EnrollmentId { get; set; }
        [ForeignKey("EnrollmentId")]
        public virtual Enrollment RelatedEnrollment { get; set; }

        [Required] public string PaperQuestionId { get; set; }
        [ForeignKey("PaperQuestionId")]
        public virtual PaperQuestion RelatedPaperQuestion { get; set; }

        [Required] public string CorrectorId { get; set; }
        [ForeignKey("UserId")]
        public virtual User RelatedCorrector { get; set; }

        public string Answer { get; set; }
        public string ExaminerComments { get; set; }
        public int MarksGained { get; set; }
        public DateTime CorrectedDateTime { get; set; }
        [Required] public bool CommittedByStudent { get; set; }
    }
}
