using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Test2.Models.DBModels
{
    public class Teaching
    {
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeachingId { get; set; }
        
         
        
        public string ExaminerId { get; set; }
        [ForeignKey("ExaminerId")]
        public ApplicationUser Examiner { get; set; }
        
        public string SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject Examinable { get; set; }

       

    }
}
