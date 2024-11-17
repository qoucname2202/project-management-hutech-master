using DeviceManagementSystem.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeviceManagementSystem.Services
{
    public class DashboardServices
    {
        private readonly IMongoCollection<BsonDocument> _chart;

        public DashboardServices(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("DbConnection"));
            IMongoDatabase database = client.GetDatabase("db_taskmanagement");
            _chart = database.GetCollection<BsonDocument>("employee");
        }

        public async Task<List<ChartViewModel>> Get()
        {
            var options = new AggregateOptions()
            {
                AllowDiskUse = true
            };

            PipelineDefinition<BsonDocument, BsonDocument> pipeline = new BsonDocument[]
            {
                new BsonDocument("$project", new BsonDocument()
                        .Add("_id", 0)
                        .Add("employee", "$$ROOT")),
                new BsonDocument("$lookup", new BsonDocument()
                        .Add("localField", "employee.DepartmentID")
                        .Add("from", "department")
                        .Add("foreignField", "_id")
                        .Add("as", "department")),
                new BsonDocument("$unwind", new BsonDocument()
                        .Add("path", "$department")
                        .Add("preserveNullAndEmptyArrays", new BsonBoolean(false))),
                new BsonDocument("$group", new BsonDocument()
                        .Add("_id", new BsonDocument()
                                .Add("department\u1390_id", "$department._id")
                                .Add("department\u1390DepartmentName", "$department.DepartmentName")
                        )
                        .Add("COUNT(*)", new BsonDocument()
                                .Add("$sum", 1)
                        )),
                new BsonDocument("$project", new BsonDocument()
                        .Add("department._id", "$_id.department\u1390_id")
                        .Add("department.DepartmentName", "$_id.department\u1390DepartmentName")
                        .Add("COUNT(*)", "$COUNT(*)")
                        .Add("_id", 0))
            };
            List<ChartViewModel> result = new List<ChartViewModel>();
            using (var cursor = await _chart.AggregateAsync(pipeline, options))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (BsonDocument document in batch)
                    {
                        ChartViewModel chartViewModel = BsonSerializer.Deserialize<ChartViewModel>(document.ToJson());
                        result.Add(chartViewModel);
                    }
                }
            }

            return result;
        }
    }
}
