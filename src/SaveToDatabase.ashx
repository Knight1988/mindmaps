<%@ WebHandler Language="C#" Class="SaveToDatabase" %>

using System;
using System.Web;

public class SaveToDatabase : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";

        // get parameters
        var id = Guid.Parse(context.Request["id"]);
        var userId = Convert.ToInt32(context.Request["userId"]);
        var title = context.Request["title"];
        var data = context.Request["data"];

        var result = MindMapBussiness.Save(id, userId, title, data);
        context.Response.Write(result.ToJson());
    }

    public bool IsReusable
    {
        get { return false; }
    }
}