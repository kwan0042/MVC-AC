using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using lab4.DataAccess;

namespace lab4.Pages.AcademicRecordManagement
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
        ViewData["CourseCode"] = new SelectList(_context.Courses, "Code", "Code");
        ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public AcademicRecord AcademicRecord { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.AcademicRecords == null || AcademicRecord == null)
            {
                return Page();
            }

            _context.AcademicRecords.Add(AcademicRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
