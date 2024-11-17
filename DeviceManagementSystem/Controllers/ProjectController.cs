using AutoMapper;
using DeviceManagementSystem.Models;
using DeviceManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace DeviceManagementSystem.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly ProjectServices _projectServices;
        private readonly EmployeeServices _employeeServices;
        private readonly IMapper _mapper;
        public ProjectController(ILogger<ProjectController> logger,
            ProjectServices projectServices,
            EmployeeServices employeeServices,
            IMapper mapper)
        {
            _logger = logger;
            _projectServices = projectServices;
            _employeeServices = employeeServices;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            List<ProjectViewModel> projects = _mapper.Map<List<Project>, List<ProjectViewModel>>(_projectServices.Get());
            projects.Select(x => {
                x.EmployeeName = !string.IsNullOrEmpty(x.EmployeeID)? _employeeServices.Get(x.EmployeeID)?.FullName : "";
                return x;
            }).ToList();

            return View(projects);
        }

        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<Project, ProjectViewModel>(_projectServices.Get(id));
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
            var model = new ProjectViewModel();
            ViewBag.Employees = _employeeServices.Get();

            return View(model);
        }

        public IActionResult Delete(string id)
        {
            try
            {
                var model = _projectServices.Get(id);

                if (model == null)
                {
                    return NotFound();
                }
                _projectServices.Remove(model.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        public IActionResult InsertOrUpdate(Project model)
        {
            if (string.IsNullOrWhiteSpace(model.Id))
                _projectServices.Create(model);
            else
                _projectServices.Update(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
