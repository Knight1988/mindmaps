using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MindMap.Bussiness;
using Newtonsoft.Json;

namespace MindMap.handles
{
    /// <summary>
    /// Summary description for IsVip
    /// </summary>
    public class IsVip : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var userId = Convert.ToInt32(context.Request["userId"]); ;

            context.Response.Write(PermissionBussiness.IsVip(userId).ToJson());
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