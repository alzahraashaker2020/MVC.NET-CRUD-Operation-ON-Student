using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestLab6.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Name { get; set; }

        [Range(minimum: 20, maximum: 30)]
        public int Age { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Uname { get; set; }
        [Required]

        public string Password { get; set; }
        
        [Required]
        [RegularExpression(@"[a-zA-Z0-9]*@[a-zA-Z]+.[a-zA-Z]{2,4}")]
        public string Email { get; set; }
        public virtual Department Department { get; set; }
        [ForeignKey("Department")]
        public int? DeptNo { get; set; }

        public string  photoName { get; set; }
        public virtual List<StudentCourse> Course { get; set; }

    }
}