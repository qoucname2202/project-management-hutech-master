using DeviceManagementSystem.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

/**************************************************************************
 * File: ManufacturerService.cs
 * Description: Service for managing manufacturer entities in the system.
 * Author: Duong Quoc Nam
 * Date Created: 2024-11-26
 * Last Modified By: 2024-12-05
 * ************************************************************************/
namespace DeviceManagementSystem.Services
{
    public class ManufacturerService
    {
        private readonly IMongoCollection<Manufacturer> _manufacturers;

        public ManufacturerService(IConfiguration config)
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

            // Access the "manufacturers" collection in the database.
            _manufacturers = database.GetCollection<Manufacturer>("manufacturers");
        }

        /// <summary>
        /// Retrieves a paginated list of manufacturers from the database, excluding soft-deleted records.
        /// </summary>
        /// <param name="pageNumber">The current page number (1-based index).</param>
        /// <param name="pageSize">The number of records to fetch per page.</param>
        /// <returns>A paginated list of manufacturers for the specified page.</returns>
        public async Task<PaginatedResult<Manufacturer>> GetAllAsync(int pageNumber, int pageSize)
        {
            try
            {
                // Ensure pageNumber and pageSize have valid values
                pageNumber = pageNumber < 1 ? 1 : pageNumber;
                pageSize = pageSize < 1 ? 10 : pageSize;

                // Calculate the total number of manufacturers (excluding removed ones)
                long totalRecords = await _manufacturers.CountDocumentsAsync(item => !item.is_removed);

                // Fetch the manufacturers for the specific page using Skip and Limit
                var manufacturers = await _manufacturers
                    .Find(item => !item.is_removed) // Filter to exclude deleted records
                    .Skip((pageNumber - 1) * pageSize) // Skip the records of previous pages
                    .Limit(pageSize) // Limit to the specified page size
                    .ToListAsync();

                // Return the paginated result
                return new PaginatedResult<Manufacturer>
                {
                    Data = manufacturers,
                    TotalRecords = totalRecords,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving manufacturers: {ex.Message}");
                return new PaginatedResult<Manufacturer>
                {
                    Data = new List<Manufacturer>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }
        }


        /// <summary>
        /// [Retrieves a single manufacturer by its ID from the database, excluding deleted records.]
        /// </summary>
        /// <param name="id">[The unique identifier of the manufacturer to retrieve.]</param>
        /// <returns>[The manufacturer object if found and not marked as removed; otherwise, null.]</returns>
        /// <exception cref="ArgumentNullException">[Condition for the exception]</exception>
        public async Task<Manufacturer?> GetByIdAsync(string id)
        {
            try
            {
                // query the manufacturers collection for a document that matches the given ID and is not marked as removed (is_removed == false).
                return await _manufacturers
                    .Find(item => item.id == id && !item.is_removed)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving manufacturer by ID: {ex.Message}");

                // return null to indicate that the manufacturer could not be retrieved.
                return null;
            }
        }

        /// <summary>
        /// [Retrieves a single manufacturer by its name from the database, excluding deleted records.]
        /// </summary>
        /// <param name="name">[The unique identifier of the manufacturer to retrieve.]</param>
        /// <returns>[the manufacturer object if found and not marked as removed; otherwise, null.]</returns>
        /// <exception cref="ArgumentNullException">[Condition for the exception]</exception>
        public async Task<Manufacturer?> GetByNameAsync(string name)
        {
            try
            {
                // query the manufacturers collection for a document that matches the given name and is not marked as removed (is_removed == false)
                return await _manufacturers
                .Find(item => item.name.ToLower() == name.ToLower() && !item.is_removed)
                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving manufacturer by name: {ex.Message}");

                // return null to indicate that the manufacturer could not be retrieved
                return null;
            }
        }

        /// <summary>
        /// [Inserts a new manufacturer record into the database]
        /// </summary>
        /// <returns>[A task representing the asynchronous operation]</returns>
        /// <exception cref="ArgumentNullException">[Condition for the exception]</exception>
        public async Task CreateAsync(Manufacturer manufacturer)
        {
            try
            {
                // Insert the manufacturer object into the "manufacturers" collection in the database.
                await _manufacturers.InsertOneAsync(manufacturer);
            }
            catch (Exception ex)
            {
                // log message to the console if an exception occours during query
                Console.WriteLine($"Error creating manufacturer: {ex.Message}");
            }
        }

        /// <summary>
        /// [updates an existing manufacturer record in the database]
        /// </summary>
        /// <param name="id">[The unique identifier of the manufacturer to retrieve]</param>
        /// <param name="manufacturer">[The manufacturer object to be inserted into the database]</param>
        /// <returns>[A task representing the asynchronous operation]</returns>
        /// <exception cref="ArgumentNullException">[Condition for the exception]</exception>
        public async Task UpdateAsync(string id, Manufacturer updatedManufacturer)
        {
            try
            {
                //  set the "updated_at" field to the current UTC time to track when the record was last modified
                updatedManufacturer.updated_at = DateTime.UtcNow;

                // Replace the existing manufacturer document in the database that matches the given ID
                // If no matching document exists, this operation will not insert a new one
                await _manufacturers
                    .ReplaceOneAsync(item => item.id == id, updatedManufacturer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating manufacturer: {ex.Message}");
            }
        }

        /// <summary>
        /// [Marks a manufacturer record as deleted by setting the "is_removed" flag to true]
        /// [Soft delete record => The manufacturer object to be inserted into the database]
        /// </summary>
        /// <param name="id">[The unique identifier of the manufacturer to retrieve]</param>
        /// <returns>[A task representing the asynchronous delete operation]</returns>
        /// <exception cref="ArgumentNullException">[Condition for the exception]</exception>
        public async Task DeleteAsync(string id)
        {
            try
            {
                // Create an update definition to perform a delete:
                // - Set the "is_removed" field to true, indicating the record is deleted
                // - Update the "updated_at" field with the current UTC time to track when the record was modified
                var update = Builders<Manufacturer>.Update
                 .Set(item => item.is_removed, true)
                 .Set(item => item.updated_at, DateTime.UtcNow);

                // Execute the update operation:
                // - Find the manufacturer record with the specified ID
                // - Apply the delete update
                var result = await _manufacturers.UpdateOneAsync(item => item.id == id, update);

                // Check result ensure the operation succeeded
                if (result.ModifiedCount == 0)
                {
                    Console.WriteLine($"No manufacturer found with ID: {id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting manufacturer: {ex.Message}");
            }
        }
    }
}