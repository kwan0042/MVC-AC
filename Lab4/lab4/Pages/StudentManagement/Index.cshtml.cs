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
    public class IndexModel : PageModel
    {
        private readonly lab4.DataAccess.StudentRecordContext _context;

        public IndexModel(lab4.DataAccess.StudentRecordContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; } = default!;
        public IList<AcademicRecord> AcademicRecords { get; set; }

        public string OName { get; set; }
        public string OCount { get; set; }
        public string OAvgG { get; set; }
        
        public async Task OnGetAsync(string sortOrder)
        {
            if (sortOrder != null)
            {
                HttpContext.Session.SetString("sortOrder", sortOrder);
            }

            if (_context.Students != null)
            {
                Student = await _context.Students.ToListAsync();
                AcademicRecords = await _context.AcademicRecords.ToListAsync();
               
            }


            OName = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            OCount = sortOrder == "NoC" ? "" : "NoC";
            OAvgG = sortOrder == "OAvgG" ? "" : "OAvgG";

            sortOrder = HttpContext.Session.GetString("sortOrder");

            switch (sortOrder)
            {
                case "Name":
                    Student = Student.OrderBy(s => s.Name).ToList();
                    break;
                    
                case "NoC":
                    Student = Student.OrderBy(s => s.AcademicRecords.Count).ToList();
                    break;
                case "OAvgG":
                    List<Student> students = new List<Student>(Student);
                    students.Sort((a, b) => a.AcademicRecords.Average(a => a.Grade).ToString().CompareTo(b.AcademicRecords.Average(b => b.Grade).ToString()));
                    Student = students;
                    break;
            }
        }
        public async Task<IActionResult> OnGetDeleteAsync(string Id)
        {
            var student = await _context.Students.FindAsync(Id);
            var academicrecord = _context.AcademicRecords.Where(m => m.StudentId == Id).ToList();
            if (academicrecord != null)
            {
                _context.AcademicRecords.RemoveRange(academicrecord);
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToPage("/StudentManagement/Index");
        }

    }
}
