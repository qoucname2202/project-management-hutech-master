using DeviceManagementSystem.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeviceManagementSystem.Services
{
    public class EmployeeServices
    {
        private readonly IMongoCollection<Employee> _employees;

        public EmployeeServices(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("DbConnection"));
            IMongoDatabase database = client.GetDatabase("db_hutech");
            _employees = database.GetCollection<Employee>("employee");
        }

        //public List<Employee> Get()
        //{
        //    return _employees.Find().ToList();
        //}

        public Employee Get(string id)
        {
            return _employees.Find(em => em.Id == id).FirstOrDefault();
        }

        public Employee Create(Employee employee)
        {
            _employees.InsertOne(employee);
            return employee;
        }

        public void Update(Employee employee)
        {
            var filter = Builders<Employee>.Filter.Eq(s => s.Id, employee.Id);
            var update = Builders<Employee>.Update
                            .Set(s => s.FullName, employee.FullName)
                            .Set(s => s.Email, employee.Email)
                            .Set(s => s.Birthday, employee.Birthday)
                            .Set(s => s.DepartmentID, employee.DepartmentID)
                            .Set(s => s.Phone, employee.Phone)
                            .Set(s => s.Position, employee.Position);
            _employees.UpdateOne(filter, update);
        }

        public void Remove(Employee employee)
        {
            _employees.DeleteOne(em => em.Id == employee.Id);
        }

        public void Remove(string id)
        {
            _employees.DeleteOne(em => em.Id == id);
        }

        //public void CreateTask(string id, EmployeeTask employeeTask)
        //{
        //    try
        //    {
        //        var filter = Builders<Employee>.Filter.Eq(s => s.Id, id);
        //        var update = Builders<Employee>.Update.Push("Tasks", employeeTask);
        //        _employees.UpdateOne(filter, update);
        //    }
        //    catch
        //    {

        //        throw;
        //    }

        //}
        //public void UpdateTask(string id,  EmployeeTask task)
        //{
        //    try
        //    {
        //        var filter = Builders<Employee>.Filter.And(
        //            Builders<Employee>.Filter.Eq(x => x.Id, id),
        //            Builders<Employee>.Filter.ElemMatch(x => x.Tasks, x => x.TaskId == task.TaskId));

        //        var update = Builders<Employee>.Update.Set(x => x.Tasks[-1], task);

        //        _employees.UpdateOne(filter, update);
        //    }
        //    catch 
        //    {

        //        throw;
        //    }

        //}
        //public void DeleteTask(string id, string taskid)
        //{
        //    try
        //    {
        //        var filter = Builders<Employee>.Filter.Eq(x=> x.Id, id);
        //        var update = Builders<Employee>.Update.PullFilter(x => x.Tasks, x => x.TaskId == taskid);
        //        _employees.UpdateOne(filter,update);
        //    }
        //    catch 
        //    {

        //        throw;
        //    }

        //}
        ////-------------------------------------
        ////SEARCH


        //public List<Employee> SearchByEmpDepart(string EmpName, string DepName)
        //{
        //        var filter = Builders<Employee>.Filter.And(
        //            Builders<Employee>.Filter.Regex("FullName", new BsonRegularExpression(EmpName, "i")),
        //            Builders<Employee>.Filter.Regex("Department", new BsonRegularExpression(DepName, "i")));

        //        return _employees.Find(filter).ToList();
        //}
        //public List<ResultEmployeeTask> SearchByTask(string TaskName, bool completed)
        //{

        //    var filter = Builders<Employee>.Filter.ElemMatch(x => x.Tasks, x => x.Completed ==completed);
        //    List<Employee> emp= _employees.Find(filter).ToList();

        //    List<ResultEmployeeTask> _Tasks = new List<ResultEmployeeTask>();
        //    foreach (var p1 in emp)
        //    {
        //        foreach (var item in p1.Tasks)
        //        {
        //            if (item.Completed == completed)
        //            { 
        //                _Tasks.Add(new ResultEmployeeTask
        //                {
        //                    FullName = p1.FullName,
        //                    Department = p1.DepartmentID,
        //                    TaskName = item.TaskName,
        //                    StartDate = item.StartDate,
        //                    EndDate = item.EndDate,
        //                    Completed = item.Completed,
        //                    Description = item.Description
        //                });
        //            }
        //        }
        //    }

        //    return _Tasks;
        //}
    }
}
