using DeviceManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using DeviceManagementSystem.Services;

namespace DeviceManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DepartmentServices _departmentService;

        public DepartmentController(DepartmentServices departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: /Department
        public async Task<IActionResult> Index()
        {
            var departments = await _departmentService.GetAllAsync();
            return View(departments);
        }

        // GET: /Department/Details/{id}
        public async Task<IActionResult> Details(string id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            // Convert UTC time to local time
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Ho_Chi_Minh");
            department.created_at = TimeZoneInfo.ConvertTimeFromUtc(department.created_at, timeZone);
            department.updated_at = TimeZoneInfo.ConvertTimeFromUtc(department.updated_at, timeZone);

            return View(department);
        }

        // GET: /Department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (!ModelState.IsValid) return View(department);

            try
            {
                await _departmentService.CreateAsync(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error creating department: {ex.Message}";
                return View("Error");
            }
        }

        // GET: /Department/Edit/{id}
        public async Task<IActionResult> Edit(string id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null) return NotFound();
            return View(department);
        }

        // POST: /Department/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Department department)
        {
            if (!ModelState.IsValid) return View(department);

            try
            {
                var existingDepartment = await _departmentService.GetByIdAsync(id);
                if (existingDepartment == null)
                    return NotFound();

                existingDepartment.name = department.name;
                existingDepartment.location = department.location;
                existingDepartment.updated_at = DateTime.UtcNow;

                await _departmentService.UpdateAsync(id, existingDepartment);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error updating department: {ex.Message}";
                return View("Error");
            }
        }

        // POST: /Department/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var existingDepartment = await _departmentService.GetByIdAsync(id);
                if (existingDepartment == null)
                    return Json(new { success = false, message = "Department not found." });
                await _departmentService.DeleteAsync(id);
                return Json(new { success = true, message = "Department deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }
    }
}
