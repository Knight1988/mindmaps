using System.Collections.Generic;
using Newtonsoft.Json;

namespace MindMap.Entity
{
    /// <summary>
    ///     Summary description for Category
    /// </summary>
    public class Category
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isPublic")]
        public bool IsPublic { get; set; }

        [JsonProperty("documents")]
        public List<Document> Documents { get; set; }
    }
}