using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab6.Models.DataAccess
{
    public partial class Employee
    {
        public Employee()
        {
            Roles = new HashSet<Role>();
        }


        [Required]
        
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z]*\s+[A-Za-z]*$", ErrorMessage = "Name must include First name and Last name, and using space to divide them.")]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "User name length should be more than 3 characters.")]
        public string UserName { get; set; } = null!;
        [Required]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Password length should be more than 5 characters.")]
        public string Password { get; set; } = null!;

        public virtual ICollection<Role> Roles { get; set; }

        public List<EmployeeRole>? EmployeeRole { get; set; }
    }
}
