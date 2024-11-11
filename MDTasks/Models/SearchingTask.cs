using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MDTasks.Models
{
     public class SearchingTask
    {
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Display(Name = "Task Name")]
        public string TaskName { get; set; }
        [Display(Name = "Completed")]
        public Boolean Completed { get; set; } = false;
    }
}
