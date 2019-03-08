using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_APT.Models
{
    public class Location
    {
        [Key] public string ExamId { get; set; }
        [Required] public string Campus { get; set; }
        [Required] public string Building { get; set; }
        [Required] public string Floor { get; set; }
        [Required] public string Block { get; set; }
        [Required] public string Room { get; set; }
    }
}
