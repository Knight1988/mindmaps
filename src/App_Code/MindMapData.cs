using System;
using Newtonsoft.Json;

/// <summary>
///     Summary description for MindMapData
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public class MindMapData
{
    public MindMapData(Guid id, int userId)
    {
        Id = id;
        UserId = userId;
    }

    [JsonProperty("id")]
    public Guid Id { get; set; }

    [JsonProperty("userId")]
    public int UserId { get; set; }

    [JsonProperty("data")]
    public string Data { get; set; }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.None, new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore
        });
    }
}