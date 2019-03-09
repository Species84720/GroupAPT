using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group_APT.Models
{
    public class Enrollment
    {
        public enum Status { Dubious, Confirmed, Denied, Unchecked }
        public enum Assessment { Pending, Passed, Failed, Compensated, Absent }

        [Key] public string EnrollmentId { get; set; }

        [Required] public string UserId { get; set; }
        [ForeignKey("UserId")]
        public Student RelatedStudent { get; set; }

        [Required]
        [DisplayName("Subject Code")]
        public string SubjectCode { get; set; }
        [ForeignKey("SubjectCode")]
        public Subject RelatedSubject { get; set; }

        [DisplayName("Result")]
        public byte ExamMark { get; set; }

        public int SeatNumber { get; set; } //Default=0 
        public Status SessionStatus { get; set; } //Default = Unchecked
        public Assessment FinalAssessment { get; set; } //Default = Pending


        //public virtual Subject Subject { get; set; }
       // public virtual Student Student { get; set; }

        public virtual ICollection<Shot> Shots { get; set; } //Multiple shots for each Enrollment
        public virtual ICollection<StudentAnswer> Answers { get; set; } //Multiple Answers for each Enrollment

    }
}
