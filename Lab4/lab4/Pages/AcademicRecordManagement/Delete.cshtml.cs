using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab4.DataAccess;

namespace lab4.Pages.AcademicRecordManagement
{
    public class DeleteModel : PageModel
    {
        private readonly lab4.DataAccess.StudentRecordContext _context;

        public DeleteModel(lab4.DataAccess.StudentRecordContext context)
        {
            _context = context;
        }

        [BindProperty]
      public AcademicRecord AcademicRecord { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.AcademicRecords == null)
            {
                return NotFound();
            }

            var academicrecord = await _context.AcademicRecords.FirstOrDefaultAsync(m => m.StudentId == id);

            if (academicrecord == null)
            {
                return NotFound();
            }
            else 
            {
                AcademicRecord = academicrecord;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.AcademicRecords == null)
            {
                return NotFound();
            }
            var academicrecord = await _context.AcademicRecords.FindAsync(id);

            if (academicrecord != null)
            {
                AcademicRecord = academicrecord;
                _context.AcademicRecords.Remove(AcademicRecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
