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
        [DisplayName("Department")]
        public string DepartmentId { get; set; }
        

        public string DepartmentParent { get; set; }
        [ForeignKey("DepartmentParent")]
        public virtual List<Department> Children { get; set; }
    }
}