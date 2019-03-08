using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Group_APT.Models
{
    public class User
    {
        [Key] public string UserId { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Surname { get; set; }
        [Required] public string UserType { get; set; }
        public string Email { get; set; }
        [Required] public string Password { get; set; }
        
        [Required] public string DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department RelatedDepartment{ get; set; }
    }
}
