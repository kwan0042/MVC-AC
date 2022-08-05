using Lab6.Models.DataAccess;

namespace Lab6.Models.ViewModel
{
    public class RoleSelection
    {
        public Role role { get; set; }
        public bool Selected { get; set; }

        public RoleSelection()
        {
            Selected = false;
            role = null;
        }
        public RoleSelection(Role role, bool Selected = false)
        {
            this.Selected = Selected;
            this.role = role;
        }
    }
}