using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MDTasks.Models
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("FullName")]
        [Required]
        public string FullName { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string DepartmentID { get; set; } = string.Empty;

        [BsonElement("EmployeeID")]
        [Required]
        public string EmployeeID { get; set; } = string.Empty;

        [BsonElement("Email")]
        [Required]
        public string Email { get; set; } = string.Empty;

        [BsonElement("Birthday")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; } = DateTime.Now.Date;

        [BsonElement("Phone")]
        public string Phone { get; set; } = string.Empty;

        [BsonElement("Position")]
        public string Position { get; set; } = string.Empty;
    }
}
