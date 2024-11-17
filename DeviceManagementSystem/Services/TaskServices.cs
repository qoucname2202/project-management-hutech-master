using DeviceManagementSystem.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DeviceManagementSystem.Services
{
    public class TaskServices
    {
        private readonly IMongoCollection<Task> _task;

        public TaskServices(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("DbConnection"));
            IMongoDatabase database = client.GetDatabase("db_taskmanagement");
            _task = database.GetCollection<Task>("task");
        }

        public List<Task> Get()
        {
            return _task.Find(x => true).ToList();
        }

        public Task Get(string id)
        {
            return _task.Find(x => x.Id == id).FirstOrDefault();
        }

        public Task Create(Task Task)
        {
            _task.InsertOne(Task);
            return Task;
        }

        public void Update(Task task)
        {
            var filter = Builders<Task>.Filter.Eq(s => s.Id, task.Id);
            var update = Builders<Task>.Update
                            .Set(s => s.TaskName, task.TaskName)
                            .Set(s => s.ProjectID, task.ProjectID)
                            .Set(s => s.StartDate, task.StartDate)
                            .Set(s => s.EndDate, task.EndDate)
                            .Set(s => s.Description, task.Description)
                            .Set(s => s.Completed, task.Completed)
                            .Set(s => s.Employees, task.Employees);
            _task.UpdateOne(filter, update);
        }

        public void Remove(Task Task)
        {
            _task.DeleteOne(em => em.Id == Task.Id);
        }
        
        public void Remove(string employeeID, string taskID)
        {
            var task = Get(taskID);
            var employeeDelete = task.Employees.SingleOrDefault(x => x.EmployeeID == employeeID);
            if(employeeDelete != null)
                task.Employees.Remove(employeeDelete);

            var filter = Builders<Task>.Filter.Eq(s => s.Id, task.Id);
            var update = Builders<Task>.Update
                            .Set(s => s.TaskName, task.TaskName)
                            .Set(s => s.ProjectID, task.ProjectID)
                            .Set(s => s.StartDate, task.StartDate)
                            .Set(s => s.EndDate, task.EndDate)
                            .Set(s => s.Description, task.Description)
                            .Set(s => s.Completed, task.Completed)
                            .Set(s => s.Employees, task.Employees);
            _task.UpdateOne(filter, update);
        }

        public void Add(string employeeID, string taskID)
        {
            var task = Get(taskID);
            if (task != null)
            {
                task.Employees = task.Employees == null ? new List<EmployeeTemp>() : task.Employees;
                if (task.Employees.Where(x => x.EmployeeID == employeeID).Count() == 0)
                {
                    task.Employees.Add(new EmployeeTemp() { EmployeeID = employeeID}) ;
                }
                var filter = Builders<Task>.Filter.Eq(s => s.Id, task.Id);
                var update = Builders<Task>.Update
                                .Set(s => s.TaskName, task.TaskName)
                                .Set(s => s.ProjectID, task.ProjectID)
                                .Set(s => s.StartDate, task.StartDate)
                                .Set(s => s.EndDate, task.EndDate)
                                .Set(s => s.Description, task.Description)
                                .Set(s => s.Completed, task.Completed)
                                .Set(s => s.Employees, task.Employees);
                _task.UpdateOne(filter, update);
            }
        }

        public void Remove(string id)
        {
            _task.DeleteOne(em => em.Id == id);
        }
    }
}
