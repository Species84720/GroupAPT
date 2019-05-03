using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Test2.Models.DBModels
{
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId { get; set; }

        public string WhoId { get; set; }
        [ForeignKey("WhoId")]
        public ApplicationUser RelatedUser { get; set; }

        public DateTime When { get; set; }

        public string Activity { get; set; }

        

        
    }
}