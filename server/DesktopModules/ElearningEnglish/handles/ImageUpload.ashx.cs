using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MindMapRelease.DesktopModules.ElearningEnglish.handles
{
    /// <summary>
    /// Summary description for ImageUpload
    /// </summary>
    public class ImageUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            var file = context.Request.Files["myImage"];

            //check file was submitted
            if ((file == null) || (file.ContentLength <= 0)) return;

            var fname = Path.GetFileName(file.FileName);
            file.SaveAs(context.Server.MapPath(Path.Combine("~/uploaded-img/", fname)));
            context.Response.Write(VirtualPathUtility.ToAbsolute(Path.Combine("~/uploaded-img/", fname)));
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