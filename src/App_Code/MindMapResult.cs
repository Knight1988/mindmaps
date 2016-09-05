using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Summary description for MindMapResult
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public class MindMapResult
{
    public MindMapResult(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public MindMapResult(bool success)
    {
        Success = success;
    }

    [JsonProperty("message")]
    public string Message { get; set; }
    [JsonProperty("success")]
    public bool Success { get; set; }

    public virtual string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.None, new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore
        });
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class MindMapResult<T> : MindMapResult
{
    public MindMapResult(bool success, string message, T data) : base(success, message)
    {
        Data = data;
    }

    public MindMapResult(bool success, T data) : base(success)
    {
        Data = data;
    }

    public MindMapResult(bool success, string message) : base(success, message)
    {
    }

    [JsonProperty("data")]
    public T Data { get; set; }

    public override string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.None, new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore
        });
    }
}