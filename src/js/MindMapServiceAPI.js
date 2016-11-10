var MindMapServiceAPI;
(function (MindMapServiceAPI) {
    function getMainTitle(success, error) {
        $.ajax({
            type: "POST",
            url: "handles/GetMainTitle.ashx",
            success: success,
            error: error,
            dataType: "json"
        });
    }
    MindMapServiceAPI.getMainTitle = getMainTitle;
    function load(id, userId, success, error) {
        $.ajax({
            type: "POST",
            url: "handles/Load.ashx",
            data: { id: id, userId: userId },
            success: success,
            error: error,
            dataType: "json"
        });
    }
    MindMapServiceAPI.load = load;
    function loadDefaultDocument(userId, success, error) {
        $.ajax({
            type: "POST",
            url: "handles/Load.ashx",
            data: { userId: userId },
            success: success,
            error: error,
            dataType: "json"
        });
    }
    MindMapServiceAPI.loadDefaultDocument = loadDefaultDocument;
    function save(userId, doc, success, error) {
        $.ajax({
            type: "POST",
            url: "handles/SaveToDatabase.ashx",
            data: {
                userId: userId,
                doc: doc.serialize()
            },
            success: success,
            error: error,
            dataType: "json"
        });
    }
    MindMapServiceAPI.save = save;
    function remove(doc, success, error) {
        $.ajax({
            type: "POST",
            url: "handles/RemoveFromDatabase.ashx",
            data: { userId: doc.userId, id: doc.id },
            success: success,
            error: error,
            dataType: "json"
        });
    }
    MindMapServiceAPI.remove = remove;
    function getCategories(userId, success, error) {
        $.post("handles/LoadFromDatabase.ashx", { userId: userId }, function (result) {
            var categories = parseData(userId, result);
            success(categories);
        });
    }
    MindMapServiceAPI.getCategories = getCategories;
    function parseData(userId, datas) {
        var categories = [];
        for (var i = 0; i < datas.length; i++) {
            var category = mindmaps.Category.fromObject(datas[i]);
            categories.push(category);
        }
        return categories;
    }
})(MindMapServiceAPI || (MindMapServiceAPI = {}));
