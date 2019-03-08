using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Group_APT.Models
{
    public class MultipleChoice
    {
        [Key] public string ChoiceId { get; set; }

        [Required] public string QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question RelatedQuestion { get; set; }

        [Required] public string OptionText { get; set; }
    }
}
