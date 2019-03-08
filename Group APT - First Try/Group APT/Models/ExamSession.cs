using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Group_APT.Models
{
    public class ExamSession
    {
        [Key] public string ExamId { get; set; }

        [Required] public string SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject RelatedSubject { get; set; }

        [Required] public string LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location RelatedLocation { get; set; }

        [Required] public DateTime ExamDateTime { get; set; }
        [Required] public DateTime ExamEndTime { get; set; }
        [Required] public int QuestionAmount { get; set; }
        [StringLength(6)] public string AccessCode { get; set; }
        public DateTime CodeIssueDateTime { get; set; }
        [Required] public bool FullyCorrected { get; set; }
        public byte MaxMark { get; set; }
        public byte MinMark { get; set; }
        public float AvgMark { get; set; }
        public int NumOfParticipants { get; set; }
        public int NumOfFails { get; set; }
    }
}
