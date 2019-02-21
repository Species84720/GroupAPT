using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_APT.Models
{
    public class Examination
    {
        [Key]
        public string Paper_Id { get; set; }
        public DateTime Time { get; set; }
        public string Location_Id { get; set; }
        public int QuestionAmount { get; set; }
        public string SelectionType { get; set; }
    }
}
