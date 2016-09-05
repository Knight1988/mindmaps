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
    function getlist(userId, success, error) {
        $.post("GetList.ashx", { userId: userId }, function (result) {
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
    MindMapServiceAPI.getlist = getlist;
})(MindMapServiceAPI || (MindMapServiceAPI = {}));
// ReSharper restore InconsistentNaming 
//# sourceMappingURL=MindMapServiceAPI.js.map