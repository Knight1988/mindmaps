using System;
using Newtonsoft.Json;

namespace MindMap.Entity
{
    /// <summary>
    ///     Summary description for Document
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Document
    {
        public Document()
        {
        }

        public Document(Guid id, int categoryId, int userId)
        {
            Id = id;
            CategoryId = categoryId;
            UserId = userId;
        }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("categoryId")]
        public int? CategoryId { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("dates")]
        public Date Dates { get; set; }

        [JsonProperty("mindmap")]
        public object MindMap { get; set; }

        [JsonProperty("dimensions")]
        public object Dimensions { get; set; }

        [JsonProperty("autosave")]
        public bool Autosave { get; set; }

        [JsonProperty("canEdit")]
        public bool? CanEdit { get; set; }

        [JsonProperty("parentId")]
        public Guid? ParentId { get; set; }

        [JsonProperty("canDelete")]
        public bool CanDelete { get; set; }

        [JsonProperty("canRestore")]
        public bool CanRestore { get; set; }

        [JsonObject(MemberSerialization.OptIn)]
        public class Date
        {
            [JsonProperty("created")]
            public object Created { get; set; }

            [JsonProperty("modified")]
            public object Modified { get; set; }
        }
    }
}