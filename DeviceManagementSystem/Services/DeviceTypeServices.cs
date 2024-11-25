using DeviceManagementSystem.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace DeviceManagementSystem.Services
{
    public class DeviceTypeServices
    {
        private readonly IMongoCollection<DeviceType> _deviceTypes;

        public DeviceTypeServices(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("DbConnection"));
            IMongoDatabase database = client.GetDatabase("db_hutech");
            _deviceTypes = database.GetCollection<DeviceType>("device-types");
        }

        // Get all device types
        public async Task<List<DeviceType>> GetAllAsync()
        {
            try
            {
                return await _deviceTypes.Find(item => !item.is_removed).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving DeviceTypes: {ex.Message}");
                return new List<DeviceType>();
            }
        }

        // Get infomation device type by id
        public async Task<DeviceType?> GetByIdAsync(string id)
        {
            try
            {
                return await _deviceTypes.Find(item => item.id == id && !item.is_removed).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving device type by ID: {ex.Message}");
                return null;
            }
        }

        // Find device type by device name
        public async Task<DeviceType?> GetByNameAsync(string name)
        {
            return await _deviceTypes.Find(item => item.name.ToLower() == name.ToLower() && !item.is_removed).FirstOrDefaultAsync();
        }

        // Create device type
        public async Task CreateAsync(DeviceType DeviceType)
        {
            try
            {
                await _deviceTypes.InsertOneAsync(DeviceType);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating device type: {ex.Message}");
                throw;
            }
        }

        // Update infomation device type by id
        public async Task UpdateAsync(string id, DeviceType updatedDeviceType)
        {
            try
            {
                await _deviceTypes.ReplaceOneAsync(item => item.id == id, updatedDeviceType);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating device type: {ex.Message}");
                throw;
            }
        }
        // Delete device type by id
        public async Task DeleteAsync(string id)
        {
            try
            {
                var update = Builders<DeviceType>.Update
                 .Set(item => item.is_removed, true)
                 .Set(item => item.updated_at, DateTime.UtcNow);

                await _deviceTypes.UpdateOneAsync(item => item.id == id, update);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting DeviceType: {ex.Message}");
                throw;
            }
        }
    }
}
