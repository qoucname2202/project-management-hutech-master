using MDTasks.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;

namespace MDTasks.Services
{
    public class ProjectServices
    {
        private readonly IMongoCollection<Project> _Project;
        public ProjectServices(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("DbConnection"));
            IMongoDatabase database = client.GetDatabase("db_taskmanagement");
            _Project = database.GetCollection<Project>("project");
        }

        public List<Project> Get()
        {
            return _Project.Find(x => true).ToList();
        }

        public Project Get(string id)
        {
            return _Project.Find(x => x.Id == id).FirstOrDefault();
        }

        public Project Create(Project Project)
        {
            _Project.InsertOne(Project);
            return Project;
        }

        public void Update(Project Project)
        {
            var filter = Builders<Project>.Filter.Eq(s => s.Id, Project.Id);
            var update = Builders<Project>.Update
                            .Set(s => s.ProjectName, Project.ProjectName)
                            .Set(s => s.EmployeeID, Project.EmployeeID)
                            .Set(s => s.Description, Project.Description);
            _Project.UpdateOne(filter, update);
        }

        public void Remove(Project Project)
        {
            _Project.DeleteOne(x => x.Id == Project.Id);
        }

        public void Remove(string id)
        {
            _Project.DeleteOne(x => x.Id == id);
        }
    }
}
