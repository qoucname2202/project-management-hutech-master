using AutoMapper;
using DeviceManagementSystem.Models;
using DeviceManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace DeviceManagementSystem.Controllers
{
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly TaskServices _taskServices;
        private readonly EmployeeServices _employeeServices;
        private readonly DepartmentServices _departmentServices;
        private readonly ProjectServices _projectServices;
        private readonly IMapper _mapper;
        public TaskController(ILogger<TaskController> logger,
            TaskServices taskServices,
            ProjectServices projectServices,
            EmployeeServices employeeServices,
            DepartmentServices departmentServices,
            IMapper mapper)
        {
            _logger = logger;
            _taskServices = taskServices;
            _projectServices = projectServices;
            _employeeServices = employeeServices;
            _departmentServices = departmentServices;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            List<TaskViewModel> Tasks = _mapper.Map<List<Task>, List<TaskViewModel>>(_taskServices.Get());
            Tasks.Select(x =>
            {
                x.ProjectName = !string.IsNullOrEmpty(x.ProjectID) ? _projectServices.Get(x.ProjectID)?.ProjectName : string.Empty;
                return x;
            }).ToList();

            return View(Tasks);
        }

        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<Task, TaskViewModel>(_taskServices.Get(id));
            model.ProjectName = !string.IsNullOrEmpty(model.ProjectID) ? _projectServices.Get(model.ProjectID)?.ProjectName : string.Empty;
            foreach (var employeeIDs in model.Employees)
            {
                var employee = _mapper.Map<Employee, EmployeeViewModel>(_employeeServices.Get(employeeIDs.EmployeeID));
                if (employee != null)
                {
                    employee.DepartmentName = employee.DepartmentID != null ? _departmentServices.Get(employee.DepartmentID)?.DepartmentName : string.Empty;
                    model.EmployeesView.Add(employee);
                }
            }

            if (model == null)
            {
                return NotFound();
            }

            ViewBag.Employees = _employeeServices.Get();
            ViewBag.Projects = _projectServices.Get();

            return View(model);
        }

        public IActionResult Create()
        {
            var model = new TaskViewModel();
            ViewBag.Employees = _employeeServices.Get();
            ViewBag.Projects = _projectServices.Get();

            return View(model);
        }

        public IActionResult Delete(string id)
        {
            try
            {
                var model = _taskServices.Get(id);

                if (model == null)
                {
                    return NotFound();
                }
                _taskServices.Remove(model.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        public JsonResult DeleteEmployee(string employeeID, string taskID)
        {
            try
            {
                _taskServices.Remove(employeeID, taskID);

                var model = _mapper.Map<Task, TaskViewModel>(_taskServices.Get(taskID));
                model.ProjectName = !string.IsNullOrEmpty(model.ProjectID) ? _projectServices.Get(model.ProjectID)?.ProjectName : string.Empty;
                foreach (var employeeIDs in model.Employees)
                {
                    var employee = _mapper.Map<Employee, EmployeeViewModel>(_employeeServices.Get(employeeIDs.EmployeeID));
                    if (employee != null)
                    {
                        employee.DepartmentName = employee.DepartmentID != null ? _departmentServices.Get(employee.DepartmentID)?.DepartmentName : string.Empty;
                        model.EmployeesView.Add(employee);
                    }
                }


                return Json(new {isSuccess = true, employees = model.EmployeesView });
            }
            catch
            {
                return Json(new { isSuccess = false});
            }
        }

        [HttpPost]
        public JsonResult AddEmployee(string employeeID, string taskID)
        {
            try
            {
                _taskServices.Add(employeeID, taskID);

                var model = _mapper.Map<Employee, EmployeeViewModel>(_employeeServices.Get(employeeID));
                model.DepartmentName = !string.IsNullOrEmpty(model.DepartmentID) ? _departmentServices.Get(model.DepartmentID)?.DepartmentName : "";

                return Json(new {isSuccess = true, employee = model });
            }
            catch
            {
                return Json(new { isSuccess = false});
            }
        }


        [HttpPost]
        public IActionResult InsertOrUpdate(Task model)
        {
            if (string.IsNullOrWhiteSpace(model.Id))
                _taskServices.Create(model);
            else
                _taskServices.Update(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
