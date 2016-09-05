using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
///     Summary description for MindMapBussiness
/// </summary>
public static class MindMapBussiness
{
    public static string GetFilePath(Guid id)
    {
        return HttpContext.Current.Server.MapPath(string.Format("~/App_Data/mindmap/{0}.json", id));
    }

    /// <summary>
    ///     Save mindmap data to file & database
    /// </summary>
    /// <param name="id">data id</param>
    /// <param name="userId">user's id</param>
    /// <param name="title">mindmap's title</param>
    /// <param name="data">mindmap data in json</param>
    /// <returns></returns>
    public static MindMapResult Save(Guid id, int userId, string title, string data)
    {
        // save data to json file
        var path = GetFilePath(id);
        File.WriteAllText(path, data);

        // save to database
        try
        {
            MindMapDataAccess.Save(new MindMapData(id, userId, title));
            return new MindMapResult(true);
        }
        catch (Exception e)
        {
            return new MindMapResult(false, e.Message);
        }
    }

    public static MindMapResult<MindMapData> Load(Guid id)
    {
        try
        {
            var data = MindMapDataAccess.Load(id);
            // get the json data
            var path = GetFilePath(id);
            if (File.Exists(path))
            {
                data.Data = File.ReadAllText(path);
                return new MindMapResult<MindMapData>(true, data);
            }

            return new MindMapResult<MindMapData>(false, @"Data file are deleted.", null);
        }
        catch (Exception e)
        {
            return new MindMapResult<MindMapData>(false, e.Message);
        }
    }

    public static MindMapResult<List<MindMapData>> GetList(int userId)
    {
        try
        {
            return new MindMapResult<List<MindMapData>>(true, MindMapDataAccess.GetList(userId).ToList());
        }
        catch (Exception e)
        {
            return new MindMapResult<List<MindMapData>>(false, e.Message, null);
        }
    }
}