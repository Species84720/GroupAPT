using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Group_APT.Models
{
    public class Teaching
    {
        [Key] public string TeachingId { get; set; }

        [Required] public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User RelatedUser { get; set; }

        [Required] public string SubjectCode { get; set; }
        [ForeignKey("SubjectCode")]
        public virtual Subject RelatedSubject { get; set; }
    }
}
