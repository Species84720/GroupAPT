using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Test2.Models.DBModels
{
    public class Topic
    {
        [Key] public string TopicId { get; set; }
        [Required] public string TopicName { get; set; }

        [Required] public string SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject RelatedSubject { get; set; }
    }
}
