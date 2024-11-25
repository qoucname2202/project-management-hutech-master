using DeviceManagementSystem.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;

namespace DeviceManagementSystem.Services
{
    public class DepartmentServices
    {
        private readonly IMongoCollection<Department> _departments;

        public DepartmentServices(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("DbConnection"));
            IMongoDatabase database = client.GetDatabase("db_hutech");
            _departments = database.GetCollection<Department>("departments");
        }

        // Get all department
        public async Task<List<Department>> GetAllAsync()
        {
            try
            {
                return await _departments.Find(item => !item.is_removed).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving departments: {ex.Message}");
                return new List<Department>();
            }
        }

        // Get infomation department by id
        public async Task<Department?> GetByIdAsync(string id)
        {
            try
            {
                return await _departments.Find(item => item.id == id && !item.is_removed).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving department by ID: {ex.Message}");
                return null;
            }
        }

        public async Task<Department?> GetByNameAsync(string name)
        {
            return await _departments.Find(item => item.name.ToLower() == name.ToLower() && !item.is_removed).FirstOrDefaultAsync();
        }

        // Create department
        public async Task CreateAsync(Department department)
        {
            try
            {
                await _departments.InsertOneAsync(department);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating department: {ex.Message}");
                throw;
            }
        }

        // Update infomation deparment by id
        public async Task UpdateAsync(string id, Department updatedDepartment)
        {
            try
            {
                await _departments.ReplaceOneAsync(item => item.id == id, updatedDepartment);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating department: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                var update = Builders<Department>.Update
                 .Set(item => item.is_removed, true)
                 .Set(item => item.updated_at, DateTime.UtcNow);

                await _departments.UpdateOneAsync(item => item.id == id, update);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting department: {ex.Message}");
                throw;
            }
        }
    }
}
