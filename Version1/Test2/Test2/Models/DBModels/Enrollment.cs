using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test2.Models.DBModels
{
    public class Enrollment
    {


        public enum Status { Unchecked, Dubious, Confirmed, Denied }
        /* Keeps track of whether the exam session was regular or not
         * (a Future version may decouple this, to have multiple tests for the same Enrolled Subject
         *
         *Unchecked: default setting. No checking has been done
         *Dubious: There are suspicions of foul play during the exam.
         *          Future checking is necessary
         * Confirmed:  All is regular with the exam. No suspicion of foul play
         * Denied:  The student's exam result is disqualified because of foul play
         */

        public enum Assessment { Pending, Absent, Present, Failed, Compensated, Passed }
        /* Keeps track of exam attendance and assesment
         * (a Future version may decouple this, to have multiple tests for the same Enrolled Subject
         *
         *Pending:  default setting. No checking was done on student's attendance
         * Absent:  the student did not attend the exam (or failed to get verified)
         * Present:  The student logged into the exam and was verified
         * Failed:  Exam was attended but failed to pass
         * Compensated:  like Failed but the failure is being treated like a pass for some reason
         * Passed:  Exam was attended and the student passed this subject
         */


        [Key] public string EnrollmentId { get; set; }

        [Required] public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student RelatedStudent { get; set; }

        [Required]
        [DisplayName("Subject Code")]
        public string SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject RelatedSubject { get; set; }

        [DisplayName("Result")]
        public byte? ExamMark { get; set; }

        [DefaultValue(0)]
        public int? SeatNumber { get; set; } //Default=0 
        [DefaultValue(0)]
        public Status SessionStatus { get; set; } //Default = Unchecked
        [DefaultValue(0)]
        public Assessment FinalAssessment { get; set; } //Default = Pending


        //public virtual Subject Subject { get; set; }
       // public virtual Student Student { get; set; }

        public virtual ICollection<Shot> Shots { get; set; } //Multiple shots for each Enrollment
        public virtual ICollection<StudentAnswer> Answers { get; set; } //Multiple Answers for each Enrollment

    }
}
