<%@ WebHandler Language="C#" Class="LoadFromDatabase" %>

using System;
using System.Web;

public class LoadFromDatabase : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "application/json";

        var id = Guid.Parse(context.Request["id"]);
        var data = MindMapBussiness.Load(id);

        context.Response.Write(data.ToJson());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}