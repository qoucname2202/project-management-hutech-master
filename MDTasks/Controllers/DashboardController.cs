using AutoMapper;
using MDTasks.Models;
using MDTasks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace MDTasks.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly DepartmentServices _departmentServices;
        private readonly EmployeeServices _employeeServices;
        private readonly ProjectServices _projectServices;
        private readonly DashboardServices _dashboardServices;
        private readonly IMapper _mapper;
        public DashboardController(ILogger<DashboardController> logger,
            DepartmentServices departmentServices,
            EmployeeServices employeeService,
            ProjectServices projectServices,
            DashboardServices dashboardServices,
            IMapper mapper)
        {
            _logger = logger;
            _departmentServices = departmentServices;
            _employeeServices = employeeService;
            _projectServices = projectServices;
            _dashboardServices = dashboardServices;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            ViewBag.CountDepartment = _departmentServices.Get().Count();
            ViewBag.CountProject = _projectServices.Get().Count();
            ViewBag.CountEmployee = _employeeServices.Get().Count();

            ViewBag.ListChartDepartment = _dashboardServices.Get().Result;
            string strListChartDepartment = string.Empty;
            foreach (ChartViewModel item in ViewBag.ListChartDepartment)
                strListChartDepartment += $"['{item.department.DepartmentName}', {item.COUNT}],";

            ViewBag.StrListChartDepartment = strListChartDepartment;

            return View();
        }
    }
}
