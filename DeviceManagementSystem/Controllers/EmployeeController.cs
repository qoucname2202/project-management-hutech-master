using AutoMapper;
using DeviceManagementSystem.Models;
using DeviceManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace DeviceManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly EmployeeServices _employeeServices;
        private readonly CommonServices _commonServices;
        private readonly IMapper _mapper;
        public EmployeeController(ILogger<EmployeeController> logger,
            EmployeeServices employeeServices,
            CommonServices commonServices,
            IMapper mapper)
        {
            _logger = logger;
            _employeeServices = employeeServices;
            _commonServices = commonServices;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(string id)
        {
            try
            {
                return View();
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Edit(string id)
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
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
            model.Id = _commonServices.GetEmployeeID(model);
            if (string.IsNullOrWhiteSpace(model.Id))
                _employeeServices.Create(model);
            else
                _employeeServices.Update(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
