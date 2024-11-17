using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MDTasks.Models
{
    public class Tasks
    {
        public string Id { get; set; }
        [BsonElement("TaskName")]
        [Required]
        public string TaskName { get; set; }
        [Required]
        [BsonDateTimeOptions]
        [BsonElement("StartDate")]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [BsonDateTimeOptions]
        [BsonElement("EndDate")]
        [Required]
        public DateTime EndDate { get; set; } = DateTime.Now;
        [BsonElement("Description")]
        public string Description { get; set; } = string.Empty;

        public Boolean Completed { get; set; } = false;
    }
}
