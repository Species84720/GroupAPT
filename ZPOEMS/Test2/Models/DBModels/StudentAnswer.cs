﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
 

namespace Test2.Models.DBModels
{
    public class StudentAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswerId { get; set; }

        [Required] public string EnrollmentId { get; set; }
        [ForeignKey("EnrollmentId")]
        public virtual Enrollment RelatedEnrollment { get; set; }

        [Required] public string PaperQuestionId { get; set; }
        [ForeignKey("PaperQuestionId")]
        public virtual PaperQuestion RelatedPaperQuestion { get; set; }

        public string CorrectorId { get; set; }
        [ForeignKey("CorrectorId")]
        public virtual ApplicationUser RelatedCorrector { get; set; }

        public string Answer { get; set; }
        public string ExaminerComments { get; set; }

        [DefaultValue(0)]
        public byte MarksGained { get; set; }
        public DateTime? CorrectedDateTime { get; set; }

        [DefaultValue(0)]
        public bool CommittedByStudent { get; set; }



    }
}
