using System.ComponentModel;
using Newtonsoft.Json;

namespace CarTestTask.Models
{
    public class CarDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        #nullable enable
        [DefaultValue("")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string? Description { get; set; }
    }
}
