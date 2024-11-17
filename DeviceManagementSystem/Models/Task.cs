using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeviceManagementSystem.Models
{
    public class Task
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProjectID { get; set; }

        [BsonElement("TaskName")]
        [Required]
        public string TaskName { get; set; }

        [Required]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("StartDate")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("EndDate")]
        [Required]
        public DateTime EndDate { get; set; } = DateTime.Now;

        [BsonElement("Description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("Completed")]
        public Boolean Completed { get; set; } = false;

        [BsonElement("Employees")]
        public List<EmployeeTemp> Employees { get; set; } = new List<EmployeeTemp>();
    }

    [BsonIgnoreExtraElements]
    public class EmployeeTemp
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string EmployeeID { get; set; }
    }
}
