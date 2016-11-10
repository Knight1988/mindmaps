using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MindMap.Bussiness;
using MindMap.Entity;

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

            var userId = Convert.ToInt32(context.Request["userId"]);
            Document document;
            if (context.Request["id"] != null)
            {
                var id = Guid.Parse(context.Request["id"]);
                document = DocumentBussiness.Load(id, userId);
            }
            else
            {
                document = DocumentBussiness.LoadDefaultDocument(userId);
            }
            
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