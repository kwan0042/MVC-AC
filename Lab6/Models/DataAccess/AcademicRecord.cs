using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Lab6.Models.DataAccess
{
    public partial class AcademicRecord
    {
        public string CourseCode { get; set; } = null!;
        public string StudentId { get; set; } = null!;

        //[Range(0, 100, ErrorMessage = "Must between 0and 100")]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "The value '' is not vaild for Grade")]
        public int? Grade { get; set; }

        public virtual Course CourseCodeNavigation { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
