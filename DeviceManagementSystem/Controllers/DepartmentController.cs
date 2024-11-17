using AutoMapper;
using DeviceManagementSystem.Models;
using DeviceManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace DeviceManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly DepartmentServices _departmentServices;
        private readonly EmployeeServices _employeeServices;
        private readonly IMapper _mapper;
        public DepartmentController(ILogger<DepartmentController> logger, 
            DepartmentServices departmentServices,
            EmployeeServices employeeService,
            IMapper mapper)
        {
            _logger = logger;
            _departmentServices = departmentServices;
            _employeeServices = employeeService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            List<DepartmentViewModel> departments = _mapper.Map<List<Department>, List<DepartmentViewModel>>(_departmentServices.Get());
            departments.Select(x => { 
                x.EmployeeName = _employeeServices.Get(x.EmployeeID)?.FullName;
                return x;
            }).ToList();

            return View(departments);
        }

        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<Department, DepartmentViewModel>(_departmentServices.Get(id));
            model.EmployeeName = _employeeServices.Get(model.EmployeeID)?.FullName;
            if (model == null)
            {
                return NotFound();
            }

            ViewBag.Employees = _employeeServices.Get();

            return View(model);
        }

        public IActionResult Create()
        {
            var model = new DepartmentViewModel();
            ViewBag.Employees = _employeeServices.Get();

            return View(model);
        }

        public IActionResult Delete(string id)
        {
            try
            {
                var model = _departmentServices.Get(id);

                if (model == null)
                {
                    return NotFound();
                }
                _departmentServices.Remove(model.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

        }


        [HttpPost]
        public IActionResult InsertOrUpdate(Department model)
        {
            if (string.IsNullOrWhiteSpace(model.Id))
                _departmentServices.Create(model);
            else
                _departmentServices.Update(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
