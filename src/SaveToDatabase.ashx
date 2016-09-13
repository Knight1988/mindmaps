<%@ WebHandler Language="C#" Class="SaveToDatabase" %>

using System;
using System.Web;
using MindMap;
using MindMap.Bussiness;
using MindMap.Entity;
using Newtonsoft.Json;

public class SaveToDatabase : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";

        // get parameters
        var doc = JsonConvert.DeserializeObject<Document>(context.Request["doc"]);

        DocumentBussiness.Save(doc);
        context.Response.Write("true");
    }

    public bool IsReusable
    {
        get { return false; }
    }
}