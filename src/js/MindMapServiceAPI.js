// ReSharper disable InconsistentNaming
var MindMapServiceAPI;
(function (MindMapServiceAPI) {
    function load(id, success, error) {
        $.ajax({
            type: "POST",
            url: "LoadFrom.ashx",
            data: { id: id },
            success: success,
            error: error,
            dataType: "json"
        });
    }
    MindMapServiceAPI.load = load;
    function save(doc, success, error) {
        $.ajax({
            type: "POST",
            url: "SaveToDatabase.ashx",
            data: { doc: doc.serialize() },
            success: success,
            error: error,
            dataType: "json"
        });
    }
    MindMapServiceAPI.save = save;
    function remove(doc, success, error) {
        $.ajax({
            type: "POST",
            url: "RemoveFromDatabase.ashx",
            data: { userId: doc.userId, id: doc.id },
            success: success,
            error: error,
            dataType: "json"
        });
    }
    MindMapServiceAPI.remove = remove;
    function getCategories(userId, success, error) {
        $.post("LoadFromDatabase.ashx", { userId: userId }, function (result) {
            var categories = parseData(userId, result);
            // success
            success(categories);
        });
    }
    MindMapServiceAPI.getCategories = getCategories;
    function parseData(userId, datas) {
        var categories = [];
        // get the document list
        for (var i = 0; i < datas.length; i++) {
            // get category
            var category = mindmaps.Category.fromObject(datas[i]);
            categories.push(category);
        }
        return categories;
    }
})(MindMapServiceAPI || (MindMapServiceAPI = {}));
// ReSharper restore InconsistentNaming 
//# sourceMappingURL=MindMapServiceAPI.js.map