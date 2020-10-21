using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace CarTestTask.Models
{
    public class Car
    {
        [Required]
        [BsonId]
        public string Id { get; set; }

        [Required]
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        #nullable enable
        public string? Description { get; set; }
    }
}
