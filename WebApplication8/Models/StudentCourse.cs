using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class StudentCourse
    {
        [Key]
        [Column(Order = 0)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Student")]
        public string StdId { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        [Range(0, 100)]
        public int Grade { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}