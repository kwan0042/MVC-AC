using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AcademicManagement;

namespace lab2.Pages
{
    public class RegistrationModel : PageModel
    {
        [BindProperty]
        public string StudentName { get; set; }

        [BindProperty]
        public Student Student { get; set; }

        [BindProperty]
        public List<SelectListItem> Options { get; set; }

        [BindProperty]
        public List<Course> Courselist { get; set; }

        public string Message { get; set; }
        public int RecordCount{ get; set; }
        public List<AcademicRecord> academicRecord { get; set; }



        public void OnGet()
        {
        }
        public void OnPostStudentSelected()
        {

            if (StudentName != "-1")
            {

                Student = DataAccess.GetAllStudents().First(s => s.StudentId == StudentName);
                academicRecord = DataAccess.GetAcademicRecordsByStudentId(Student.StudentId);
                RecordCount = academicRecord.Count;
                if (RecordCount == 0)
                {
                    Message = "The Student has not registerd any course. Select course(s) to register.";
                }
                else
                {
                    Message = "The student has registered the following course:";
                    Courselist = new List<Course>();
                    for (var i = 0; i < academicRecord.Count; i++)
                    {
                        Courselist.Add(DataAccess.GetAllCourses().First(c => c.CourseCode == academicRecord[i].CourseCode));
                    }
                    
                }

            }
            else
            {
                Student = null;
                Message = "You must select a Student!";
            }
            

        }

        public void OnPostRegister()
        {
            
            int selectedCount = 0;
            Courselist = new List<Course>();
            foreach (SelectListItem item in Options)
            {
                if (item.Selected)
                {
                    AcademicRecord academicRecord = new AcademicRecord(StudentName, item.Value);
                    DataAccess.AddAcademicRecord(academicRecord);
                    Courselist.Add(DataAccess.GetAllCourses().First(c => c.CourseCode == academicRecord.CourseCode));
                    selectedCount++;
                }
            }
            if (selectedCount > 0)
            {
                Message = "The student has registered the following course:";
            }
            else
            {
                Message = "You must select a least one Course!";
            }
            

        }
        

    }
    

}
