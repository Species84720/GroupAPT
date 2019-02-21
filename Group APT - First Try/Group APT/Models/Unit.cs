using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_APT.Models
{
    public class Unit
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
