using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Group_APT.Models
{
    public class Logs
    {
        [Key] public string LogId { get; set; }

        [Required] public string TargetedRole { get; set; }
        [ForeignKey("TargetedRole")]
        public virtual Role RelatedRole { get; set; }

        public string LogText { get; set; }
        [Required] public DateTime LogDateTime { get; set; }
    }
}
