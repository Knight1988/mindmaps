using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MindMap.Bussiness;
using MindMap.Entity;
using Newtonsoft.Json;

namespace MindMapRelease.DesktopModules.ElearningEnglish.handles
{
    /// <summary>
    /// Summary description for SaveToDatabase
    /// </summary>
    public class SaveToDatabase : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            // get parameters
            var userId = Convert.ToInt32(context.Request["userId"]);
            var doc = JsonConvert.DeserializeObject<Document>(context.Request["doc"]);

            DocumentBussiness.Save(userId, doc);
            context.Response.Write("true");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}