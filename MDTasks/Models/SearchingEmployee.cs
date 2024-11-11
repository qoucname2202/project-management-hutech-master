using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MDTasks.Models
{
    public class SearchingEmployee
    {
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
    }
}
