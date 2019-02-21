using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_APT.Models
{
    public class Lecturer
    {
        [Key]
        public string UniversityLecturerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string LecturerId { get; set; }
    }
}
