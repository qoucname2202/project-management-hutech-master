using DeviceManagementSystem.Models;
using DeviceManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace DeviceManagementSystem.Controllers
{
    public class DeviceTypeController : Controller
    {

        private readonly DeviceTypeServices _deviceTypeService;

        public DeviceTypeController(DeviceTypeServices DeviceTypeService)
        {
            _deviceTypeService = DeviceTypeService;
        }

        // GET: /DeviceType
        public async Task<IActionResult> Index()
        {
            var DeviceTypes = await _deviceTypeService.GetAllAsync();
            return View(DeviceTypes);
        }

        // GET: /DeviceType/Details/{id}
        public async Task<IActionResult> Details(string id)
        {
            var DeviceType = await _deviceTypeService.GetByIdAsync(id);
            if (DeviceType == null)
            {
                return NotFound();
            }

            // Convert UTC time to local time
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Ho_Chi_Minh");
            DeviceType.created_at = TimeZoneInfo.ConvertTimeFromUtc(DeviceType.created_at, timeZone);
            DeviceType.updated_at = TimeZoneInfo.ConvertTimeFromUtc(DeviceType.updated_at, timeZone);

            return View(DeviceType);
        }

        // GET: /DeviceType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /DeviceType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeviceType DeviceType)
        {
            if (!ModelState.IsValid) return View(DeviceType);

            try
            {
                await _deviceTypeService.CreateAsync(DeviceType);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error creating device type: {ex.Message}";
                return View("Error");
            }
        }

        // GET: /DeviceType/Edit/{id}
        public async Task<IActionResult> Edit(string id)
        {
            var DeviceType = await _deviceTypeService.GetByIdAsync(id);
            if (DeviceType == null) return NotFound();
            return View(DeviceType);
        }

        // POST: /DeviceType/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, DeviceType DeviceType)
        {
            if (!ModelState.IsValid) return View(DeviceType);

            try
            {
                var existingDeviceType = await _deviceTypeService.GetByIdAsync(id);
                if (existingDeviceType == null)
                    return NotFound();

                existingDeviceType.name = DeviceType.name;
                existingDeviceType.description = DeviceType.description;
                existingDeviceType.updated_at = DateTime.UtcNow;

                await _deviceTypeService.UpdateAsync(id, existingDeviceType);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error updating device type: {ex.Message}";
                return View("Error");
            }
        }

        // POST: /DeviceType/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var existingDeviceType = await _deviceTypeService.GetByIdAsync(id);
                if (existingDeviceType == null)
                    return Json(new { success = false, message = "Device type not found." });
                await _deviceTypeService.DeleteAsync(id);
                return Json(new { success = true, message = "Device type deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }
    }
}
