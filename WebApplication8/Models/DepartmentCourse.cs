using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class DepartmentCourse
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Department")]
        public int DeptId { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Department Department { get; set; }
        public virtual Course Course { get; set; }
    }
}