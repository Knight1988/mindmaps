<%@ WebHandler Language="C#" Class="RemoveFromDatabase" %>

using System;
using System.Web;

public class RemoveFromDatabase : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "application/json";

        // get parameters
        var id = Guid.Parse(context.Request["id"]);
        var userId = Convert.ToInt32(context.Request["userId"]);

        var result = MindMapBussiness.Delete(new MindMapData(id, userId));
        context.Response.Write(result.ToJson());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}