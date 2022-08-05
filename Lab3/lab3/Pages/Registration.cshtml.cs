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
        public string Message { get; set; }
        [BindProperty]
        public int RecordCount { get; set; }
        [BindProperty]
        public List<AcademicRecord> academicRecord { get; set; }

        [BindProperty]
        public List<AcademicRecord> GradeList { get; set; }


        
        public void OnGet(string sortOrder)
        {
            if (sortOrder != null)
            {
                StudentName = HttpContext.Session.GetString("StudentName");
                HttpContext.Session.SetString("sortOrder",sortOrder);
                if (StudentName !=null && StudentName !="-1")
                {
                    
                    GradeList = DataAccess.GetAcademicRecordsByStudentId(StudentName);
                    if (GradeList.Count>0)
                    {
                        SortAR();
                    }
                    else
                    {
                        CSort();
                    }

                }
            }
            
        }
        public void OnPostStudentSelected()
        {
            
            if (StudentName != "-1")
            {
                HttpContext.Session.SetString("StudentName", StudentName);
                GradeList = DataAccess.GetAcademicRecordsByStudentId(StudentName);
                if (GradeList.Count == 0)
                {
                    Message = "The Student has not registerd any course. Select course(s) to register.";
                    CSort();
                }
                else
                {
                    Message = "The student has registered the following course. You can enter or edit the grades";
                    SortAR();
                }
            }
            else
            {
                GradeList = null;
                Student = null;
                Message = "You must select a Student!";
                HttpContext.Session.Remove("StudentName");
            }

        }

        

        public void OnPostRegister()
        {
            int selectedCount = 0;
            GradeList = new List<AcademicRecord>();
            foreach (SelectListItem item in Options)
            {
                if (item.Selected)
                {
                    AcademicRecord academicRecord = new AcademicRecord(StudentName, item.Value);
                    DataAccess.AddAcademicRecord(academicRecord);
                    selectedCount++;
                }
            }
            GradeList = DataAccess.GetAcademicRecordsByStudentId(StudentName);
            if (selectedCount > 0)
            {
                Message = "The student has registered the following course. You can enter or edit the grades";
                SortAR();
            }
            else
            {
                Message = "You must select a least one Course!";
                CSort();
            }
            
            

        }

        public void OnPostGrade()
        {
            academicRecord = DataAccess.GetAcademicRecordsByStudentId(StudentName);
            Message = "The student has registered the following course:";
            for (int i = 0; i < academicRecord.Count; i++)
            {
                GradeList[i].CourseCode = academicRecord[i].CourseCode;
                GradeList[i].StudentId = academicRecord[i].StudentId;
                for (int j = 0; j < GradeList.Count; j++)
                {
                    academicRecord[j].Grade = GradeList[j].Grade;
                }
            }
        }

        private void CSort()
        {
            Options = new List<SelectListItem>();
            foreach (AcademicManagement.Course c in AcademicManagement.DataAccess.GetAllCourses())
            {
                Options.Add(new SelectListItem(c.CourseTitle, c.CourseCode, false));
            }

            
            string sortOrder = HttpContext.Session.GetString("sortOrder");

            if (sortOrder == "Ccode")
            {
                Options.Sort((a, b) => a.Value.CompareTo(b.Value));
            }
            else if (sortOrder == "Ctitle")
            {
                Options.Sort((a, b) => a.Text.CompareTo(b.Text));
            }
        }

        private void SortAR()
        {
            
            string sortOrder = HttpContext.Session.GetString("sortOrder");

            if (sortOrder == "Ccode")
            {
                
                GradeList.Sort((a, b) => a.CourseCode.CompareTo(b.CourseCode));
            }
            else if (sortOrder == "Ctitle")
            {
                
                GradeList.Sort(this.ARTitleSort);
            }
            else if (sortOrder == "Cgrade")
            {
                
                GradeList.Sort((a, b) => a.Grade.CompareTo(b.Grade));
            }
        }

        public int ARTitleSort(AcademicRecord x, AcademicRecord y)
        {
            Course xCourse = DataAccess.GetAllCourses().First(c => c.CourseCode == x.CourseCode);
            Course yCourse = DataAccess.GetAllCourses().First(c => c.CourseCode == y.CourseCode);

            return xCourse.CourseTitle.CompareTo(yCourse.CourseTitle);
        }


       

    }


}
