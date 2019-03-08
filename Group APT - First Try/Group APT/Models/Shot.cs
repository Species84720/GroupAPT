using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Group_APT.Models
{
    public class Shot
    {
        [Key] public string ShotId { get; set; }

        [Required] public string EnrollmentId { get; set; }
        [ForeignKey("EnrollmentId")]
        public virtual Enrollment RelatedEnrollment { get; set; }

        [Required] public DateTime ShotTiming { get; set; }
        [Required] public string ImageLocation { get; set; }
        [Required] public Enrollment.Status SessionStatus { get; set; }
    }
}
