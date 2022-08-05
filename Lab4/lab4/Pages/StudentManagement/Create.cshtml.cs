using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using lab4.DataAccess;

namespace lab4.Pages.StudentManagement
{
    public class CreateModel : PageModel
    {
        private readonly lab4.DataAccess.StudentRecordContext _context;

        public CreateModel(lab4.DataAccess.StudentRecordContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; } = default!;
        public string errorMsg;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Students == null || Student == null)
            {
                return Page();
            }
            
            Student student = _context.Students.Where(x => x.Id == Student.Id).FirstOrDefault();

            if (student != null)
            {
                errorMsg = "Student Id " +student.Id +" already exists";
                return Page();
            }
            else
            {
                _context.Students.Add(Student);
                await _context.SaveChangesAsync();
                return RedirectToPage("/StudentManagement/Index");
            }
        }

        
    }
}
