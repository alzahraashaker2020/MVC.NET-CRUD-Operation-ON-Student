using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestLab6.Models
{
    public class Course
    {
        [Key]
        public int CrsId { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string CrsName { get; set; }
        public virtual List<DepartmentCourse> department { get; set; }
        public virtual List<StudentCourse> Student { get; set; }
        
    }
}