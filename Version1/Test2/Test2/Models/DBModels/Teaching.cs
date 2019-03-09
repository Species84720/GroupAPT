using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Test2.Models;

namespace Group_APT.Models
{
    public class Teaching
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeachingId { get; set; }

        [Required] public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser RelatedUser { get; set; }

        [Required] public string SubjectCode { get; set; }
        [ForeignKey("SubjectCode")]
        public  Subject RelatedSubject { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
