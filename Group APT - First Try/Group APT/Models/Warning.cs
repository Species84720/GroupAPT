using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Group_APT.Models
{
    public class Warning
    {
        [Key] public string WarningId { get; set; }

        [Required] public string InvigilatorId { get; set; }
        [ForeignKey("InvigilatorId")]
        public virtual Invigilation RelatedInvigilation { get; set; }

        public string WarningText { get; set; }
        [Required] public int SeatNumber { get; set; }
    }
}
