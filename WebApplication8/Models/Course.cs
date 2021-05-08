using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Course
    {
        [Required]
        [Key]
        public int CId { get; set; }
        [Required]
        public string CName { get; set; }
        public virtual List<DepartmentCourse> Departments { get; set; }
        public virtual List<StudentCourse> students { get; set; }
    }
}