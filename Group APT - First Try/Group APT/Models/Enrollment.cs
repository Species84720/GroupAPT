using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group_APT.Models
{
    public class Enrollment
    {
        public enum Status { Dubious, Confirmed, Denied, Unchecked }
        public enum Assessment { Passed, Failed, Compensated, Absent }

        [Key] public int EnrollmentId { get; set; }

        [Required] public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Student RelatedStudent { get; set; }

        [Required] public string SubjectCode { get; set; }
        [ForeignKey("SubjectCode")]
        public virtual Subject RelatedSubject { get; set; }

        public byte ExamMark { get; set; }
        public int SeatNumber { get; set; }
        public Status SessionStatus { get; set; }
        public Assessment FinalAssessment { get; set; }
    }
}
