using DeviceManagementSystem.Models;
using DeviceManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

/**************************************************************************
 * File: ManufacturerController.cs
 * Description: Controller for managing manufacturer entities in the system.
 * Author: Duong Quoc Nam
 * Date Created: 2024-11-26
 * Last Modified By: 2024-12-05
 * ************************************************************************/
namespace DeviceManagementSystem.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly ManufacturerService _manufacturerService;

        public ManufacturerController(ManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        /// <summary>
        /// Handles the request to display the list of manufacturers on the Index page.
        /// </summary>
        /// <returns>
        /// A ViewResult that renders the Index view, populated with the list of manufacturers retrieved from the database.
        /// </returns>
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 2)
        {
            var paginatedResult = await _manufacturerService.GetAllAsync(pageNumber, pageSize);

            ViewData["TotalPages"] = paginatedResult.TotalPages;
            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalRecords"] = paginatedResult.TotalRecords;

            return View(paginatedResult.Data);
        }


        /// <summary>
        /// Handles the request to display the details of a specific manufacturer.
        /// </summary>
        /// <param name="id">The unique identifier of the manufacturer to retrieve.</param>
        /// <returns>
        /// A ViewResult that renders the Details view, populated with the manufacturer data if found;
        /// otherwise, returns a NotFoundResult if the manufacturer does not exist.
        /// </returns>
        public async Task<IActionResult> Details(string id)
        {
            // Validate the ID parameter.
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Invalid manufacturer ID.");
            }

            // Retrieve the manufacturer by ID.
            var manufacturer = await _manufacturerService.GetByIdAsync(id);

            // Check if the manufacturer exists. If not, return a 404 Not Found response.
            if (manufacturer == null)
            {
                return NotFound();
            }
            // Render the Details view with the retrieved manufacturer data.
            return View(manufacturer);
        }

        // ViewResult that renders the Create view
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles the POST request to create a new manufacturer.
        /// Validates the input data and saves the manufacturer to the database if valid.
        /// </summary>
        /// <param name="manufacturer">The manufacturer object populated from the form data.</param>
        /// <returns>
        /// A RedirectToAction result to the Index action if the creation is successful.
        /// If the input data is invalid, re-displays the Create view with validation errors.
        /// </returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Manufacturer manufacturer)
        {
            // Check if the input data is valid based on the model's validation attributes
            if (!ModelState.IsValid)
            {
                return View(manufacturer);
            }

            // If the input is valid, call the service layer to save the new manufacturer to the database.
            await _manufacturerService.CreateAsync(manufacturer);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var manufacturer = await _manufacturerService.GetByIdAsync(id);
            if (manufacturer == null) return NotFound();
            return View(manufacturer);
        }


        /// <summary>
        /// Handles the POST request to edit an existing manufacturer.
        /// Validates the input, checks if the manufacturer exists, and updates it in the database.
        /// </summary>
        /// <param name="id">The unique identifier of the manufacturer to edit.</param>
        /// <param name="manufacturer">The updated manufacturer object populated from the form data.</param>
        /// <returns>
        /// A RedirectToAction result to the Index action if the update is successful.
        /// If the input data is invalid, re-displays the Edit view with validation errors.
        /// If the manufacturer is not found, returns a NotFoundResult.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Manufacturer manufacturer)
        {
            // Validate the input data against the model's validation attributes.
            if (!ModelState.IsValid)
            {
                return View(manufacturer);
            }

            // Retrieve the existing manufacturer from the database using the provided ID.
            var existingManufacturer = await _manufacturerService.GetByIdAsync(id);

            // Check if the manufacturer exists. If not, return a 404 Not Found response.
            if (existingManufacturer == null)
            {
                return NotFound();
            }

            // Call the service layer to update the manufacturer with the new data.
            await _manufacturerService.UpdateAsync(id, manufacturer);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Handles the POST request to delete a manufacturer by its ID.
        /// Performs a soft delete by setting the "is_removed" flag to true.
        /// </summary>
        /// <param name="id">The unique identifier of the manufacturer to delete.</param>
        /// <returns>
        /// A JSON response indicating whether the deletion was successful or if an error occurred.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                // Attempt to retrieve the manufacturer with the specified ID from the database.
                var existingDepartment = await _manufacturerService.GetByIdAsync(id);
                // Check if the manufacturer exists.
                if (existingDepartment == null)
                {
                    return Json(new { success = false, message = "Manufacturer not found" });
                }
                await _manufacturerService.DeleteAsync(id);
                return Json(new { success = true, message = "Manufacturer deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }
    }
}
