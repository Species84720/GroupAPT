using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Test2.Models.DBModels;

namespace Group_APT.Models
{
    public class Subject
    {
        [Key]
        [DisplayName("Subject Code")]
        public string SubjectCode { get; set; }

        [Required]
        [DisplayName("Subject Title")]
        public string SubjectName { get; set; }

        [Required] public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        [DisplayName("Department")]
        public Department RelatedDepartment { get; set; }

        [Required]
        public string Credits { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
       // public virtual ICollection<ExamSession> ExamSessions { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }

    }
}
