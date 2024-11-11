using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MDTasks.Models
{
    public class DepartmentTemp
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string DepartmentName { get; set; }
    }

    public class ChartViewModel
    {
        public DepartmentTemp department { get; set; }
        [BsonElement("COUNT(*)")]
        public int COUNT { get; set; }
    }

}
