using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using Lab6.Models.DataAccess;

namespace Lab6.Controllers
{
    public class AcademicRecordsController : Controller
    {
        private readonly StudentRecordContext _context;

        public AcademicRecordsController(StudentRecordContext context)
        {
            _context = context;
        }

        // GET: AcademicRecords
        public async Task<IActionResult> Index(string sortOrder)
        {
            if (sortOrder == null)
            {
                sortOrder = HttpContext.Session.GetString("sortOrder");
            }

            if (sortOrder != null)
            {
                HttpContext.Session.SetString("sortOrder", sortOrder);
            }
            var studentRecordContext = await _context.AcademicRecords.Include(a => a.CourseCodeNavigation).Include(a => a.Student).ToListAsync();
            
            switch (sortOrder)
            {
                case "CCode":
                    studentRecordContext = studentRecordContext.OrderBy(a => a.CourseCodeNavigation.Title).ThenBy(s => s.Student.Name).ToList();
                    break;
                case "SSName":
                    studentRecordContext = studentRecordContext.OrderBy(s => s.Student.Name).ThenBy(s => s.CourseCodeNavigation.Title).ToList();
                    break;
                
            }

            return View(studentRecordContext);
        }

        

        // GET: AcademicRecords/Create
        public IActionResult Create()
        {
            ViewData["CourseCode"] = new SelectList(_context.Courses, "Code", "Code");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            
            return View();
        }

        // POST: AcademicRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseCode,StudentId,Grade")] AcademicRecord academicRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(academicRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["CourseCode"] = new SelectList(_context.Courses, "Code", "Code", academicRecord.CourseCode);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", academicRecord.StudentId);
           
            return View(academicRecord);
        }


        // GET: AcademicRecords/Edit/5
        public async Task<IActionResult> Edit(string id, string code)
        {
            if (id == null || _context.AcademicRecords == null)
            {
                return NotFound();
            }

            //var academicRecord = await _context.AcademicRecords.FindAsync(id,code);
            var academicRecord = await _context.AcademicRecords.Include(a => a.CourseCodeNavigation).Include(a => a.Student).FirstOrDefaultAsync(a=>a.CourseCode==code && a.StudentId==id);

            if (academicRecord == null)
            {
                return NotFound();
            }
            

            return View(academicRecord);
        }

        // POST: AcademicRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,string code, int grade, AcademicRecord academicRecord)
        {
            
            if (id != academicRecord.StudentId)
            {
                return NotFound();
            }
            academicRecord = await _context.AcademicRecords.Include(a => a.CourseCodeNavigation).Include(a => a.Student).FirstOrDefaultAsync(a => a.CourseCode == code && a.StudentId == id);

            academicRecord.Grade = grade;


            if (ModelState.IsValid)
            {
                try
                {
                    _context.AcademicRecords.Update(academicRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademicRecordExists(academicRecord.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }



            return View(academicRecord);
        }








        // GET: AcademicRecords/EditAll
        public async Task<IActionResult> EditAll(string sortOrder, IList<AcademicRecord> academicRecord)
        {
            if (sortOrder == null)
            {
                sortOrder = HttpContext.Session.GetString("sortOrder");
            }

            if (sortOrder != null)
            {
                HttpContext.Session.SetString("sortOrder", sortOrder);
            }

            var academicRecordEdit = await _context.AcademicRecords.Include(a => a.CourseCodeNavigation).Include(a => a.Student).ToListAsync();

            

            switch (sortOrder)
            {
                case "CCode":
                    academicRecordEdit = academicRecordEdit.OrderBy(a => a.CourseCodeNavigation.Title).ThenBy(s => s.Student.Name).ToList();
                    break;
                case "SSName":
                    academicRecordEdit = academicRecordEdit.OrderBy(s => s.Student.Name).ThenBy(s => s.CourseCodeNavigation.Title).ToList();
                    break;

            }

           


            return View(academicRecordEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAll(IList<AcademicRecord> academicRecord)
        {

            foreach(var grade in academicRecord)
            {

                if ((grade.Grade > 100) || (grade.Grade < 0))
                {
                    ModelState.AddModelError("Grade1", "Must between 0 and 100");

                }
                
            }
            if (ModelState.IsValid)
            {
                for (var i = 0; i < academicRecord.Count; i++) { 
                    var academicRecordUpdate = await _context.AcademicRecords.Include(a => a.CourseCodeNavigation).Include(a => a.Student).FirstOrDefaultAsync(a => a.CourseCode == academicRecord[i].CourseCode && a.StudentId == academicRecord[i].StudentId);

                        academicRecordUpdate.Grade = academicRecord[i].Grade;
                        try
                        {
                        
                                _context.AcademicRecords.Update(academicRecordUpdate);
                                await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!AcademicRecordExists(academicRecord[i].StudentId))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                        
                }
                return RedirectToAction(nameof(Index));
            }
                        
        return View(academicRecord);



        }





        private bool AcademicRecordExists(string id)
        {
          return (_context.AcademicRecords?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}
