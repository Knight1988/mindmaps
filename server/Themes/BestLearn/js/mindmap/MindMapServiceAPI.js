// ReSharper disable InconsistentNaming
var MindMapServiceAPI;
(function (MindMapServiceAPI) {
    function getMainTitle(success, error) {
        $.ajax({
            type: "POST",
            url: "/DesktopModules/ElearningEnglish/handles/GetMainTitle.ashx",
            success: success,
            error: error,
            dataType: "json"
        });
    }
    MindMapServiceAPI.getMainTitle = getMainTitle;
    function load(id, userId, success, error) {
        $.ajax({
            type: "POST",
            url: "/DesktopModules/ElearningEnglish/handles/Load.ashx",
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
            url: "/DesktopModules/ElearningEnglish/handles/Load.ashx",
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
            url: "/DesktopModules/ElearningEnglish/handles/SaveToDatabase.ashx",
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
            url: "/DesktopModules/ElearningEnglish/handles/RemoveFromDatabase.ashx",
            data: { userId: doc.userId, id: doc.id },
            success: success,
            error: error,
            dataType: "json"
        });
    }
    MindMapServiceAPI.remove = remove;
    function getCategories(userId, success, error) {
        $.post("/DesktopModules/ElearningEnglish/handles/LoadFromDatabase.ashx", { userId: userId }, function (result) {
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