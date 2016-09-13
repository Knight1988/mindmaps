using Newtonsoft.Json;

namespace MindMap.Entity
{
    /// <summary>
    ///     Summary description for Result
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Result
    {
        public Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class Result<T> : Result
    {
        public Result(bool success, string message, T data) : base(success, message)
        {
            Data = data;
        }

        public Result(bool success, T data) : base(success)
        {
            Data = data;
        }

        public Result(bool success, string message) : base(success, message)
        {
        }

        [JsonProperty("data")]
        public T Data { get; set; }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}