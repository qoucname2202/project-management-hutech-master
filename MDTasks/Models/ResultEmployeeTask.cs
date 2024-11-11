using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MDTasks.Models
{
    public class ResultEmployeeTask
    {
        [Display(Name = "Employee Name")]
        public string FullName { get; set; }
        [Display(Name = "Department")]
        public string Department { get; set; }
        [Display(Name = "Task Name")]
        public string TaskName { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Completed")]
        public Boolean Completed { get; set; }
    }
}
