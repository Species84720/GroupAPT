using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Group_APT.Models
{
    public class Subject
    {
        [Key] public string SubjectCode { get; set; }
        [Required] public string SubjectName { get; set; }

        [Required] public string DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department RelatedDepartment { get; set; }

        [Required] public string Credits { get; set; }
    }
}
