using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab4.DataAccess;

namespace lab4.Pages.StudentManagement
{
    public class DetailsModel : PageModel
    {
        private readonly lab4.DataAccess.StudentRecordContext _context;

        public DetailsModel(lab4.DataAccess.StudentRecordContext context)
        {
            _context = context;
        }

        public Student Student { get; set; } = default!;
      

        public async Task<IActionResult> OnGetAsync(string sortOrder, string id)
        {

            if (id == null || _context.Students== null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(s => s.AcademicRecords).ThenInclude(a => a.CourseCodeNavigation).FirstOrDefaultAsync(m => m.Id == id);
           
            if (student == null)
            {
                return NotFound();
            }
            else
            {
                Student = student;
            }
           

            if (sortOrder== "OCourse")
            {
                Student.AcademicRecords = Student.AcademicRecords.OrderBy(ac => ac.CourseCodeNavigation.Title).ToList();
            }
            if (sortOrder == "OGrade")
            {
                Student.AcademicRecords = Student.AcademicRecords.OrderBy(ac => ac.Grade).ToList();
            }

            
            return Page();
        }
    }
}
