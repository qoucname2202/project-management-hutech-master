using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MDTasks.Models
{
    public class Department
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("DepartmentName")]
        [Required]
        public string DepartmentName { get; set; } = string.Empty;

        [BsonElement("Description")]
        [Required]
        public string Description { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string EmployeeID { get; set; }
    }
}
