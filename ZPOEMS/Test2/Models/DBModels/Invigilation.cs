using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Test2.Models;

namespace Test2.Models.DBModels
{
    public class Invigilation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvigilationId { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser  RelatedUser { get; set; }

        [Required] public string ExamId { get; set; }
        [ForeignKey("ExamId")]
        public virtual ExamSession RelatedExamSession { get; set; }


    }
}
