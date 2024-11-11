using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MDTasks.Models
{
    public class EmployeeTask
    {
        public string TaskId { get; set; }
        [BsonElement("TaskName")]
        [Required]
        public string TaskName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [BsonElement("StartDate")]
        public DateTime StartDate { get; set; } = DateTime.Now.Date;
        [DataType(DataType.Date)]
        [BsonElement("EndDate")]
        [Required]
        public DateTime EndDate { get; set; } = DateTime.Now.Date;
        [BsonElement("Description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("Completed")]
        [Required]
        public Boolean Completed { get; set; } = false;
    }
}
