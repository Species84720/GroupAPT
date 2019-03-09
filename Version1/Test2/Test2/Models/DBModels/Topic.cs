using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Group_APT.Models
{
    public class Topic
    {
        [Key] public string TopicId { get; set; }
        [Required] public string TopicName { get; set; }

        [Required] public string SubjectCode { get; set; }
        [ForeignKey("SubjectCode")]
        public virtual Subject RelatedSubject { get; set; }
    }
}
