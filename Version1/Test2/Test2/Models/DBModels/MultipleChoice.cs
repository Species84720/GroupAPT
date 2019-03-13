using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Test2.Models.DBModels
{
    public class MultipleChoice
    {

        public enum PossibleAnswer : byte {A=1 , B , C , D};

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChoiceId { get; set; }

        /*
        [Required] public string QuestionId { get; set; }
       [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
        */

        [Required]
        [DisplayName("Text for First Choice")]
        public string OptionText1 { get; set; }

        [Required]
        [DisplayName("Text for Second Choice")]
        public string OptionText2 { get; set; }

        [Required]
        [DisplayName("Text for Third Choice")]
        public string OptionText3 { get; set; }

        [Required]
        [DisplayName("Text for Fourth Choice")]
        public string OptionText4 { get; set; }

        [Required]
        [DisplayName("Correct Choice")]
        public PossibleAnswer CorrectChoice { get; set; }

        [Required]
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question RelatedQuestion { get; set; }

    }



}
