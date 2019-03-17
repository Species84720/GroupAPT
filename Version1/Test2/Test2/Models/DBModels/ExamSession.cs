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
        public int? LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location RelatedLocation { get; set; }

        [DisplayName("Exam Start Date-Time")]
        public DateTime? ExamDateTime { get; set; }

        [DisplayName("Exam End Date-Time")]
        public DateTime? ExamEndTime { get; set; }

        [DisplayName("Number of Questions")]
        [DefaultValue(0)]
        public int QuestionAmount { get; set; }

        [StringLength(6)]
        public string AccessCode { get; set; }
        public DateTime? CodeIssueDateTime { get; set; }

        [DefaultValue(0)]
        public bool FullyCorrected { get; set; }

        [DefaultValue(0)]
        public byte MaxMark { get; set; }

        [DefaultValue(0)]
        public byte MinMark { get; set; }

        [DefaultValue(0)]
        public float AvgMark { get; set; }

        [DefaultValue(0)]
        public int NumOfParticipants { get; set; }

        [DefaultValue(0)]
        public int NumOfFails { get; set; }

        public virtual ICollection<Invigilation> Invigilations { get; set; } //because Invigilation is between this and User

        public virtual ICollection<PaperQuestion> PaperQuestions { get; set; } //Many questions belong to this session


    }



}
