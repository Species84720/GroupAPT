using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Test2.Models.DBModels
{
    public class ExamSession
    {
        [Key] public string ExamId { get; set; }

        [Required] public string SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        [DisplayName("Subject Code")]
        public virtual Subject RelatedSubject { get; set; }

        [DisplayName("Location")]
        public string LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location RelatedLocation { get; set; }

        [DisplayName("Exam Start Date-Time")]
        public DateTime ExamDateTime { get; set; }

        [DisplayName("Exam End Date-Time")]
        public DateTime ExamEndTime { get; set; }

        [DisplayName("Number of Questions")]
        public int QuestionAmount { get; set; }

        [StringLength(6)]
        public string AccessCode { get; set; }
        public DateTime CodeIssueDateTime { get; set; }

        public bool FullyCorrected { get; set; }

        public byte MaxMark { get; set; }
        public byte MinMark { get; set; }
        public float AvgMark { get; set; }
        public int NumOfParticipants { get; set; }
        public int NumOfFails { get; set; }

        public virtual ICollection<Invigilation> Invigilations { get; set; } //because Invigilation is between this and User

        public virtual ICollection<PaperQuestion> PaperQuestions { get; set; } //Many questions belong to this session


    }



}
