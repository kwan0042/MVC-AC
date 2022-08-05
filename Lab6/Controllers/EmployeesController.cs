using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab6.Models.DataAccess;
using Lab6.Models.ViewModel;

namespace Lab6.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly StudentRecordContext _context;

        public EmployeesController(StudentRecordContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            
            var Employees = await _context.Employees.Include(a => a.EmployeeRole).ToListAsync();
            var EmployeesRO = await _context.Roles.ToListAsync();
            foreach  (Employee e in Employees)
            {
                foreach (EmployeeRole er in e.EmployeeRole)
                {
                    er.Role.Role1 = EmployeesRO.Find(a => a.Id == er.Role_Id).Role1;
                    
                }
            }

           return View(Employees);
        }

        



        // GET: Employees/Create
        public IActionResult Create()
        {
            EmployeeRoleSelections employeeRoleSelections = new EmployeeRoleSelections();
            return View(employeeRoleSelections);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,UserName,Password")] Employee employee)
        public async Task<IActionResult> Create(EmployeeRoleSelections employeeRoleSelections)
        {

            var checkem = await _context.Employees.ToListAsync();
            for (int i = 0; i < checkem.Count(); i++)
            {

                if (employeeRoleSelections.employee.UserName == checkem[i].UserName)
                {
                    ModelState.AddModelError("NameErr", "This Network ID already existed.");
                }
            }


            int roleSel = 0;
            for (int i = 0; i < employeeRoleSelections.roleSelections.Count(); i++)
            {
                
                if (employeeRoleSelections.roleSelections[i].Selected==false) 
                {
                    roleSel++;

                }
            }
            if (roleSel==3)
            {
                ModelState.AddModelError("RoleErr", "You must select at least one role!");
            }

            



            if (ModelState.IsValid)
            {
                _context.Add(employeeRoleSelections.employee);
                await _context.SaveChangesAsync();
                foreach (RoleSelection roleSelection in employeeRoleSelections.roleSelections)
                {
                    if (roleSelection.Selected)
                    {
                        EmployeeRole employeeRole = new EmployeeRole
                        {
                            Role_Id = roleSelection.role.Id,
                            Employee_Id = employeeRoleSelections.employee.Id
                        };
                        _context.EmployeeRole.Add(employeeRole);
                    }
                    
                }

                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeRoleSelections);
        }












        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employeeRole = await _context.EmployeeRole.Include(a => a.Employee).Include(a => a.Role).FirstOrDefaultAsync(a => a.Employee_Id == id);
            EmployeeRoleSelections employeeRoleSelections = new EmployeeRoleSelections();
            employeeRoleSelections.employee.Id = employeeRole.Employee.Id;
            employeeRoleSelections.employee.Name= employeeRole.Employee.Name;
            employeeRoleSelections.employee.UserName = employeeRole.Employee.UserName;
            employeeRoleSelections.employee.Password = employeeRole.Employee.Password;

            var employeeRolelist = await _context.EmployeeRole.Include(a => a.Employee).Include(a => a.Role).ToListAsync();
            foreach (RoleSelection roleSelection in employeeRoleSelections.roleSelections)
            {
                
                    foreach (EmployeeRole er in employeeRolelist)
                    {
                        if (er.Employee_Id==id)
                        {
                            if (roleSelection.role.Id == er.Role_Id)
                            {
                                roleSelection.Selected = true;
                            };
                        }
                    
                     }
                
                
                    
            }
            


            if (employeeRole == null)
            {
                return NotFound();
            }
            return View(employeeRoleSelections);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeRoleSelections employeeRoleSelections)
        {
            if (id != employeeRoleSelections.employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeRoleSelections.employee);
                    _context.Update(employeeRoleSelections.roleSelections);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employeeRoleSelections.employee.Id))
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
            return View(employeeRoleSelections.employee);
        }

















        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'StudentRecordContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            var employeeR = await _context.EmployeeRole.ToListAsync();

            foreach(EmployeeRole er in employeeR)
            {
                if (er.Employee_Id == id)
                {
                    
                    _context.EmployeeRole.Remove(er);
                    
                }
            }

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            
            
            return RedirectToAction(nameof(Index));
        }













        private bool EmployeeExists(int id)
        {
          return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
