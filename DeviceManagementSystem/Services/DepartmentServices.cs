using DeviceManagementSystem.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;


/**************************************************************************
 * File: departmentService.cs
 * Description: Service for managing deparment entities in the system.
 * Author: Duong Quoc Nam
 * Date Created: 2024-11-21
 * Last Modified By: 2024-11-27
 * ************************************************************************/
namespace DeviceManagementSystem.Services
{
    public class DepartmentServices
    {
        private readonly IMongoCollection<Department> _departments;

        public DepartmentServices(IConfiguration config)
        {
            // Retrieve the database connection string from the configuration.
            string connectionString = config.GetConnectionString("DbConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("DbConnection", "Database connection string is missing or empty in the configuration");
            }
            // Create a MongoDB client and connect to the database instance.
            MongoClient client = new MongoClient(connectionString);

            // Retrieve the database name from the configuration (defaults to "db_hutech").
            string databaseName = config.GetSection("DatabaseSettings:DatabaseName").Value ?? "db_hutech";
            IMongoDatabase database = client.GetDatabase(databaseName);

            // Access the "department" collection in the database.
            _departments = database.GetCollection<Department>("departments");
        }

        /// <summary>
        /// [Retrieves all department records from the database that are not marked as removed]
        /// </summary>
        /// <returns>
        /// [A list of departments where the "is_removed" flag is false. If an error occurs, an empty list is returned]
        /// </returns>
        /// <exception cref="ArgumentNullException">[Condition for the exception]</exception>
        public async Task<List<Department>> GetAllAsync()
        {
            try
            {
                return await _departments.Find(item => !item.is_removed).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving departments: {ex.Message}");

                // Return an empty list to ensure the method fails gracefully without crashing the application
                return new List<Department>();
            }
        }

        /// <summary>
        /// [Retrieves a single department by its ID from the database, excluding deleted records]
        /// </summary>
        /// <param name="id">[The unique identifier of the department to retrieve]</param>
        /// <returns>[The department object if found and not marked as removed; otherwise, null]</returns>
        /// <exception cref="ArgumentNullException">[Condition for the exception]</exception>
        public async Task<Department?> GetByIdAsync(string id)
        {
            try
            {
                // query the department collection for a document that matches the given ID and is not marked as removed (is_removed == false)
                return await _departments
                    .Find(item => item.id == id && !item.is_removed)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving department by ID: {ex.Message}");

                // return null to indicate that the manufacturer could not be retrieved
                return null;
            }
        }

        /// <summary>
        /// [Retrieves a single department by its name from the database, excluding deleted records]
        /// </summary>
        /// <param name="name">[The unique identifier of the department to retrieve]</param>
        /// <returns>[the department object if found and not marked as removed; otherwise, null]</returns>
        /// <exception cref="ArgumentNullException">[Condition for the exception]</exception>
        public async Task<Department?> GetByNameAsync(string name)
        {
            // query the department collection for a document that matches the given name and is not marked as removed (is_removed == false)
            return await _departments
                .Find(item => item.name.ToLower() == name.ToLower() && !item.is_removed)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// [Inserts a new department record into the database]
        /// </summary>
        /// <returns>[A task representing the asynchronous operation]</returns>
        /// <exception cref="ArgumentNullException">[Condition for the exception]</exception>
        public async Task CreateAsync(Department department)
        {
            try
            {
                // Insert the department object into the "department" collection in the database
                await _departments.InsertOneAsync(department);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating department: {ex.Message}");
            }
        }

        /// <summary>
        /// [updates an existing department record in the database]
        /// </summary>
        /// <param name="id">[The unique identifier of the department to retrieve]</param>
        /// <param name="department">[The department object to be inserted into the database]</param>
        /// <returns>[A task representing the asynchronous operation]</returns>
        /// <exception cref="ArgumentNullException">[Condition for the exception]</exception>
        public async Task UpdateAsync(string id, Department updatedDepartment)
        {
            try
            {
                // Replace the existing department document in the database that matches the given ID
                // If no matching document exists, this operation will not insert a new one
                await _departments.ReplaceOneAsync(item => item.id == id, updatedDepartment);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating department: {ex.Message}");
            }
        }

        /// <summary>
        /// [Marks a department record as deleted by setting the "is_removed" flag to true]
        /// [Soft delete record => The department object to be inserted into the database]
        /// </summary>
        /// <param name="id">[The unique identifier of the department to retrieve]</param>
        /// <returns>[A task representing the asynchronous delete operation]</returns>
        /// <exception cref="ArgumentNullException">[Condition for the exception]</exception>
        public async Task DeleteAsync(string id)
        {
            try
            {
                // Create an update definition to perform a delete:
                // - Set the "is_removed" field to true, indicating the record is deleted
                // - Update the "updated_at" field with the current UTC time to track when the record was modified
                var update = Builders<Department>.Update
                 .Set(item => item.is_removed, true)
                 .Set(item => item.updated_at, DateTime.UtcNow);

                // Execute the update operation:
                // - Find the department record with the specified ID
                // - Apply the delete update
                var result = await _departments.UpdateOneAsync(item => item.id == id, update);

                // Check result ensure the operation succeeded
                if (result.ModifiedCount == 0)
                {
                    Console.WriteLine($"No manufacturer found with ID: {id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting department: {ex.Message}");
            }
        }
    }
}
