using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MindMap;
using MindMap.Bussiness;

namespace MindMapRelease.DesktopModules.ElearningEnglish.handles
{
    /// <summary>
    /// Summary description for LoadFromDatabase
    /// </summary>
    public class LoadFromDatabase : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var userId = Convert.ToInt32(context.Request["userId"]);
            var datas = CategoryBussiness.GetPublicCategories(userId);

            context.Response.Write(datas.ToJson());
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