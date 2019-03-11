using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
 

namespace Test2.Models.DBModels
{
    public class Student
    {
        [Key] 
		public string StudentId { get; set; }

        [Required]
		public string UserId { get; set; }
		[ForeignKey("UserId")]
        public ApplicationUser RelatedUser { get; set; }

        public string FacialImageDirectory { get; set; }
		public string FacialImageTitle { get; set; }
		
		 public virtual ICollection<Enrollment> Enrollments { get; set; }
		
    }
}
