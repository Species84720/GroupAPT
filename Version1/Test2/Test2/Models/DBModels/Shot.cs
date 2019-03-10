using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Test2.Models.DBModels
{
    public class Shot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShotId { get; set; }

        [Required] public string EnrollmentId { get; set; }
        [ForeignKey("EnrollmentId")]
        public virtual Enrollment RelatedEnrollment { get; set; }

        [Required]
        [DisplayName("Time of Photo")]
        public DateTime ShotTiming { get; set; }

        public string ImageLocation { get; set; }
        public string ImageTitle { get; set; }

       // [Required] public Enrollment.Status SessionStatus { get; set; }


    }
}
