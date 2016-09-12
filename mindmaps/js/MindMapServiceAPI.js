// ReSharper disable InconsistentNaming
var MindMapServiceAPI;
(function (MindMapServiceAPI) {
    function load(id, success, error) {
        $.post("LoadFromDatabase.ashx", { id: id }, function (result) {
            if (result.success && typeof (success) === "function") {
                // success
                success(result.data);
            }
            else {
                // error
                error(result.message);
            }
        });
    }
    MindMapServiceAPI.load = load;
    function save(id, userId, title, data, success, error) {
        var obj = { id: id, userId: userId, title: title, data: data };
        $.post("SaveToDatabase.ashx", obj, function (result) {
            if (result.success && typeof (success) === "function") {
                // success
                success();
            }
            else {
                // error
                error(result.message);
            }
        });
    }
    MindMapServiceAPI.save = save;
    function remove(doc, success, error) {
        $.post("RemoveFromDatabase.ashx", { id: doc.id }, function (result) {
            if (result.success && typeof (success) === "function") {
                var datas = [];
                // get the document list
                for (var i = 0; i < result.data.length; i++) {
                    var data = result.data[i];
                    datas.push(mindmaps.Document.fromJSON(data.data));
                }
                // success
                success(datas);
            }
            else if (typeof (success) === "function") {
                // error
                error(result.message);
            }
        });
    }
    MindMapServiceAPI.remove = remove;
    function getlist(userId, success, error) {
        $.post("LoadFromDatabase.ashx", { userId: userId }, function (result) {
            if (result.success && typeof (success) === "function") {
                var datas = [];
                // get the document list
                for (var i = 0; i < result.data.length; i++) {
                    var data = result.data[i];
                    var doc = mindmaps.Document.fromJSON(data.data);
                    doc.canEdit = data.canEdit;
                    datas.push(doc);
                }
                // success
                success(datas);
            }
            else {
                // error
                error(result.message);
            }
        });
    }
    MindMapServiceAPI.getlist = getlist;
})(MindMapServiceAPI || (MindMapServiceAPI = {}));
// ReSharper restore InconsistentNaming 
//# sourceMappingURL=MindMapServiceAPI.js.map