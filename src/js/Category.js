/**
 * Creates a new Category.
 * 
 * @constructor
 */
mindmaps.Category = function () {
    this.id = mindmaps.Util.createUUID();
    this.name = "New Category";
    this.documents = [];
};

/**
 * Creates a new Category object from a JSON string.
 * 
 * @static
 * @param {String} json
 * @returns {mindmaps.Category}
 */
mindmaps.Category.fromJSON = function (json) {
    return mindmaps.Category.fromObject(JSON.parse(json));
};

/**
 * Creates a new Category object from a generic object.
 * 
 * @static
 * @param {Object} json
 * @returns {mindmaps.Category}
 */
mindmaps.Category.fromObject = function (obj) {
    var doc = new mindmaps.Category();
    doc.id = obj.id;
    doc.name = obj.name;

    for (var i = 0; i < obj.documents.length; i++) {
        var objDoc = mindmaps.Document.fromObject(obj.documents[i]);
        doc.documents.push(objDoc);
    }

    return doc;
};