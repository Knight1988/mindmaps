var Querystring = {};
Querystring.getString = function(name, url) {
    url = url || window.location.href;
    url = url.toLowerCase();
    name = name.toLowerCase().replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    var val = "";
    if (results && results[2]) {
        val = decodeURIComponent(results[2].replace(/\+/g, " "));
    }
    if (typeof (val) !== "string") val = "";
    return val;
}
Querystring.getInt = function(name, defaultValue, url) {
    return parseInt(Querystring.getString(name, url)) || defaultValue;
}
Querystring.getFloat = function(name, defaultValue, url) {
    return parseFloat(Querystring.getString(name, url)) || defaultValue;
}