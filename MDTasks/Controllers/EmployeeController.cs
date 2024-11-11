using AutoMapper;
using MDTasks.Models;
using MDTasks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace MDTasks.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly EmployeeServices _employeeServices;
        private readonly DepartmentServices _departmentServices;
        private readonly CommonServices _commonServices;
        private readonly IMapper _mapper;
        public EmployeeController(ILogger<EmployeeController> logger,
            DepartmentServices departmentServices,
            EmployeeServices employeeServices,
            CommonServices commonServices,
            IMapper mapper)
        {
            _logger = logger;
            _employeeServices = employeeServices;
            _departmentServices = departmentServices;
            _commonServices = commonServices;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            List<EmployeeViewModel> Employees = _mapper.Map<List<Employee>, List<EmployeeViewModel>>(_employeeServices.Get());
            Employees.Select(x => {
                x.DepartmentName = !string.IsNullOrEmpty(x.DepartmentID) ? _departmentServices.Get(x.DepartmentID)?.DepartmentName : "";
                return x;
            }).ToList();

            return View(Employees);
        }

        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<Employee, EmployeeViewModel>(_employeeServices.Get(id));
            if (model == null)
            {
                return NotFound();
            }
            model.DepartmentName = !string.IsNullOrEmpty(model.DepartmentID) ? _departmentServices.Get(model.DepartmentID)?.DepartmentName : "";
            model.StrBirthday = model.Birthday.ToString("yyyy-MM-dd");

            ViewBag.Departments = _departmentServices.Get();

            return View(model);
        }

        public IActionResult Create()
        {
            var model = new EmployeeViewModel();
            ViewBag.Departments = _departmentServices.Get();

            return View(model);
        }

        public IActionResult Delete(string id)
        {
            try
            {
                var model = _employeeServices.Get(id);

                if (model == null)
                {
                    return NotFound();
                }
                _employeeServices.Remove(model.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        public IActionResult InsertOrUpdate(Employee model)
        {
            model.EmployeeID = _commonServices.GetEmployeeID(model);
            if (string.IsNullOrWhiteSpace(model.Id))
                _employeeServices.Create(model);
            else
                _employeeServices.Update(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
