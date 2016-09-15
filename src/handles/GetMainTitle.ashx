<%@ WebHandler Language="C#" Class="GetMainTitle" %>

using System;
using System.Web;
using MindMap;
using MindMap.Bussiness;
using MindMap.Entity;
using Newtonsoft.Json;

public class GetMainTitle : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "application/json";

        // get parameters

        var mainTitle = MainTitleBussiness.GetMainTitle();
        context.Response.Write(mainTitle.ToJson());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}