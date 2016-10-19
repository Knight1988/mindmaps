using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MindMap.Bussiness;

namespace MindMap.handles
{
    /// <summary>
    /// Summary description for Load
    /// </summary>
    public class Load : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var id = Guid.Parse(context.Request["id"]);
            var userId = Convert.ToInt32(context.Request["userId"]);
            var document = DocumentBussiness.Load(id, userId);
            
            context.Response.Write(document.ToJson());
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