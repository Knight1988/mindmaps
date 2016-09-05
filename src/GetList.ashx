<%@ WebHandler Language="C#" Class="GetList" %>

using System;
using System.Web;

public class GetList : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";

        var userId = Convert.ToInt32(context.Request["userId"]);
        var datas = MindMapBussiness.GetList(userId);

        context.Response.Write(datas.ToJson());
    }

    public bool IsReusable
    {
        get { return false; }
    }
}