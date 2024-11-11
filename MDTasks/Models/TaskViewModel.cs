using System;
using System.Collections.Generic;

namespace MDTasks.Models
{
    public class TaskViewModel
    {
        public string Id { get; set; }

        public string ProjectID { get; set; }

        public string ProjectName { get; set; }

        public string TaskName { get; set; }

        public DateTime? StartDate { get; set; }

        public string StrStartDate { get { return this.StartDate?.ToString("yyyy-MM-dd"); } }

        public DateTime? EndDate { get; set; }

        public string StrEndDate { get { return this.EndDate?.ToString("yyyy-MM-dd"); } }

        public string Description { get; set; } = string.Empty;

        public Boolean Completed { get; set; } = false;

        public List<EmployeeTemp> Employees { get; set; }

        public List<EmployeeViewModel> EmployeesView { get; set; } = new List<EmployeeViewModel>();
    }
}
