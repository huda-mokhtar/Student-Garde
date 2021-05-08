using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Student
    {
        // [ForeignKey("ApplicationUser")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Age")]
        [Range(10, 40)]
        public int Age { get; set; }

        public string img { get; set; }
 
        public virtual Department Department { get; set; }
        [ForeignKey("Department")]
        public int? dId { get; set; }

        public virtual List<StudentCourse> Courses { get; set; }
    }
}