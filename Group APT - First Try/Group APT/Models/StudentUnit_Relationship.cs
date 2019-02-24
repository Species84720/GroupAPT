using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group_APT.Models
{
    public class StudentUnit_Relationship
    {
        [Key]
        public int RelationId { get; set; }

        public string UniversityStudentId { get; set; }
        [ForeignKey("UniversityStudentId")]
        public Student StudentRelation { get; set; }

        public string Code { get; set; }
        [ForeignKey("Code")]
        public Unit UnitRelation { get; set; }
    }
}
