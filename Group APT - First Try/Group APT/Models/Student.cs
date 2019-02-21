using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_APT.Models
{
    public class Student
    {
        [Key]
        public string UniversityStudentId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string StudentId { get; set; }
        public string StudentImageLocation { get; set; }
    }
}
