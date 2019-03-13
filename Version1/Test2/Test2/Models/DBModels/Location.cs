using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Test2.Models.DBModels
{
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationId { get; set; }

        public string Campus { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Block { get; set; }
        [Required] public string Room { get; set; }

        public virtual ICollection<ExamSession> ExamSessions { get; set; } //to query which exams are planne din the place

    }
}
