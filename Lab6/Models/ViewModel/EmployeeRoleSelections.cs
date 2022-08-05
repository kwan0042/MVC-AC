using Lab6.Models.ViewModel;

using Lab6.Models.DataAccess;

namespace Lab6.Models.ViewModel
{
    public class EmployeeRoleSelections
    {
        public Employee employee { get; set; }
        public List<RoleSelection> roleSelections { get; set; }

        public EmployeeRoleSelections()
        {
            employee = new Employee();
            roleSelections = new List<RoleSelection>();

            StudentRecordContext context = new StudentRecordContext();
            foreach (Role role in context.Roles)
            {
                RoleSelection roleSelection = new RoleSelection(role);

                roleSelections.Add(roleSelection);
            }

        }
    }
}