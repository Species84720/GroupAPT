using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Group_APT.Models
{
    public class Invigilation
    {
        [Key] public string InvigilatorId { get; set; }

        [Required] public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User RelatedUser { get; set; }

        [Required] public string ExamId { get; set; }
        [ForeignKey("ExamId")]
        public virtual ExamSession RelatedExamSession { get; set; }
    }
}
