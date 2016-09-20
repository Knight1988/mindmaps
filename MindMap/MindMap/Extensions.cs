using System;
using Newtonsoft.Json;

namespace MindMap
{
    /// <summary>
    ///     Summary description for Extensions
    /// </summary>
    public static class Extensions
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        /// <summary>
        ///     Convert Js time ticks to date time
        /// </summary>
        /// <example>
        ///     1302203200000.JsTimeTicksToDateTime() --> "4/7/2011"
        /// </example>
        /// <param name="jsTimeTicks">time tick value</param>
        /// <returns>Converted DateTime</returns>
        public static DateTime JsTimeTicksToDateTime(this long jsTimeTicks)
        {
            return new DateTime(1970, 1, 1) + new TimeSpan(jsTimeTicks*10000);
        }

        /// <summary>
        ///     Convert Js time ticks to date time
        /// </summary>
        /// <example>
        ///     1302203200000.JsTimeTicksToDateTime() --> "4/7/2011"
        /// </example>
        /// <param name="jsTimeTicks">time tick value</param>
        /// <returns>Converted DateTime</returns>
        public static DateTime JsTimeTicksToDateTime(this int jsTimeTicks)
        {
            return new DateTime(1970, 1, 1) + new TimeSpan(jsTimeTicks*10000);
        }

        /// <summary>
        ///     Convert C# DateTime to js time tick
        /// </summary>
        /// <example>
        ///     DateTime.Parse("4/7/2011").ToJsTimeTicks() --> 1302134400000
        /// </example>
        /// <param name="dateTime">Datetime value</param>
        /// <returns>Converted time ticks</returns>
        public static long ToJsTimeTicks(this DateTime dateTime)
        {
            return (long) dateTime.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
    }
}