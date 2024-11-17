namespace DeviceManagementSystem.Models
{
    public class DepartmentViewModel
    {
        public string Id { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
    }
}
