namespace Lab6.Models.DataAccess 
{ 

    public partial class EmployeeRole
    {
        public int Employee_Id { get; set; }
        public Employee Employee { get; set; }
        public int Role_Id { get; set; }    
        public Role Role { get; set; }
    }
}