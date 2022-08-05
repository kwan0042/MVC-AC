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
    public class IndexModel : PageModel
    {
        private readonly lab4.DataAccess.StudentRecordContext _context;

        public IndexModel(lab4.DataAccess.StudentRecordContext context)
        {
            _context = context;
        }

        public IList<AcademicRecord> AcademicRecord { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.AcademicRecords != null)
            {
                AcademicRecord = await _context.AcademicRecords
                .Include(a => a.CourseCodeNavigation)
                .Include(a => a.Student).ToListAsync();
            }
        }
    }
}
