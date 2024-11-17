using System;

namespace DeviceManagementSystem.Models
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }
        
        public string FullName { get; set; } = string.Empty;

        public string DepartmentID { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

        public string EmployeeID { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime Birthday { get; set; } = DateTime.Now.Date;
        public string StrBirthday { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Position { get; set; } = string.Empty;
    }
}
