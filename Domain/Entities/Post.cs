using Cosmonaut.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Domain.Entities
{
    [CosmosCollection("posts")]
    public class Post
    {
        //[Key]
        [CosmosPartitionKey]
        [JsonProperty("id")]
        public string Id { get; set; }
        public string Name { get; set; }

        public List<Tag> Tags { get; set; }

        public Post()
        {
            Id = System.Guid.NewGuid().ToString();
        }
    }

    public class Tag
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
