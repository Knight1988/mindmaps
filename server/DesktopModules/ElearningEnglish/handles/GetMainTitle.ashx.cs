using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MindMap;
using MindMap.Bussiness;

namespace MindMapRelease.DesktopModules.ElearningEnglish.handles
{
    /// <summary>
    /// Summary description for GetMainTitle
    /// </summary>
    public class GetMainTitle : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var mainTitle = MainTitleBussiness.GetMainTitle();
            context.Response.Write(mainTitle.ToJson());
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