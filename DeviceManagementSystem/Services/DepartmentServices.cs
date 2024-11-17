using DeviceManagementSystem.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace DeviceManagementSystem.Services
{
    public class DepartmentServices
    {
        private readonly IMongoCollection<Department> _department;
        public DepartmentServices(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("DbConnection"));
            IMongoDatabase database = client.GetDatabase("db_taskmanagement");
            _department = database.GetCollection<Department>("department");
        }

        public List<Department> Get()
        {
            return _department.Find(x => true).ToList();
        }

        public Department Get(string id)
        {
            return _department.Find(x => x.Id == id).FirstOrDefault();
        }

        public Department Create(Department department)
        {
            _department.InsertOne(department);
            return department;
        }

        public void Update(Department department)
        {
            var filter = Builders<Department>.Filter.Eq(s => s.Id, department.Id);
            var update = Builders<Department>.Update
                            .Set(s => s.DepartmentName, department.DepartmentName)
                            .Set(s => s.EmployeeID, department.EmployeeID)
                            .Set(s => s.Description, department.Description);
            _department.UpdateOne(filter, update);
        }

        public void Remove(Department department)
        {
            _department.DeleteOne(x => x.Id == department.Id);
        }

        public void Remove(string id)
        {
            _department.DeleteOne(x => x.Id == id);
        }
    }
}
