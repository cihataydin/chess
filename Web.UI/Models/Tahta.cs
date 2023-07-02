using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;

namespace Web.UI.Models
{
    public class Tahta
    {
        [BsonId]
        public string? Id { get; set; }

        [BsonElement("Kareler")]
        public string Kareler { get; set; }
    }
}
