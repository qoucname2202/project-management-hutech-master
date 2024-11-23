﻿using AutoMapper;
using DeviceManagementSystem.Models;
using DeviceManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace DeviceManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly DepartmentServices _departmentServices;
        private readonly EmployeeServices _employeeServices;
        private readonly DashboardServices _dashboardServices;
        private readonly IMapper _mapper;
        public DashboardController(ILogger<DashboardController> logger,
            DepartmentServices departmentServices,
            EmployeeServices employeeService,
            DashboardServices dashboardServices,
            IMapper mapper)
        {
            _logger = logger;
            _departmentServices = departmentServices;
            _employeeServices = employeeService;
            _dashboardServices = dashboardServices;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            ViewBag.CountDepartment = _departmentServices.Get().Count();
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