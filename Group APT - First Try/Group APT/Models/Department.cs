using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_APT.Models
{
    public class Department
    {
        [Key]
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
