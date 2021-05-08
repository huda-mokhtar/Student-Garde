using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Department
    {
        [Required]
        [Key]
        public int dId { get; set; }
        [Required]
        public string dName { get; set; }
       
        public virtual List< Instructor> Instructors { get; set; }
        public virtual List<Student> Students { get; set; }

        public virtual List<DepartmentCourse> Courses { get; set; }
    }
}