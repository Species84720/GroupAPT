using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test2.Models.DBModels
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; }

        [Required]
        [DisplayName("Deparment Name")]
        public string DepartmentName { get; set; }

        [DisplayName("Sub-Deparment of")]
        public int? DepartmentParentId { get; set; }
        [ForeignKey("DepartmentParentId")]
        public virtual List<Department> Children { get; set; }
    }
}