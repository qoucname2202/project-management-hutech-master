namespace DeviceManagementSystem.Models
{
    public class ProjectViewModel
    {
        public string Id { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
    }
}
