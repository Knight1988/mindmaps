<%@ WebHandler Language="C#" Class="RemoveFromDatabase" %>

using System;
using System.Web;
using MindMap;
using MindMap.Bussiness;

public class RemoveFromDatabase : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";

        // get parameters
        var id = Guid.Parse(context.Request["id"]);
        var userId = Convert.ToInt32(context.Request["userId"]);

        // Delete document
        DocumentBussiness.Delete(id, userId);
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