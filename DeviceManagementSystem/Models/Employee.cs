using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeviceManagementSystem.Models
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("FullName")]
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("DepartmentID")]
        [Required]
        public string DepartmentID { get; set; } = string.Empty;

        [BsonElement("Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [BsonElement("Phone")]
        [Required]
        public string Phone { get; set; } = string.Empty;

        [BsonElement("Birthday")]
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; } = DateTime.Now.Date;

        [BsonElement("Position")]
        [Required]
        [MaxLength(50)]
        public string Position { get; set; } = string.Empty;

        [BsonElement("Avatar")]
        [Url]
        public string? Avatar { get; set; }

        [BsonElement("BorrowedDevices")]
        public List<BorrowedDevice> BorrowedDevices { get; set; } = new List<BorrowedDevice>();
    }
}
