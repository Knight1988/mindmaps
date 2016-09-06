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
            var mindMapData = new MindMapData(id, userId);
            if (MindMapDataAccess.Exist(id))
            {
                MindMapDataAccess.Update(mindMapData);
            }
            else
            {
                MindMapDataAccess.Insert(mindMapData);
            }
            return new MindMapResult(true);
        }
        catch (Exception e)
        {
            return new MindMapResult(false, e.Message);
        }
    }

    public static MindMapResult Delete(MindMapData data)
    {
        // save to database
        try
        {
            // delete the mindmap
            MindMapDataAccess.Delete(data.Id);
            // delete json file
            var path = GetFilePath(data.Id);
            if (File.Exists(path)) File.Delete(path);

            // fetch the list
            return GetList(data.UserId);
        }
        catch (Exception e)
        {
            return new MindMapResult(false, e.Message);
        }
    }

    public static MindMapResult<List<MindMapData>> GetList(int userId)
    {
        try
        {
            // get the mind map list
            var datas = MindMapDataAccess.GetList(userId).Where(HasData).ToList();

            // return the datas
            return new MindMapResult<List<MindMapData>>(true, datas);
        }
        catch (Exception e)
        {
            // return fail message
            return new MindMapResult<List<MindMapData>>(false, e.Message, null);
        }
    }

    private static bool HasData(MindMapData data)
    {
        // get file path
        var path = GetFilePath(data.Id);

        // get json data
        if (File.Exists(path))
        {
            data.Data = File.ReadAllText(path);
            return true;
        }

        // delete the data on error
        Delete(data);
        return false;
    }
}