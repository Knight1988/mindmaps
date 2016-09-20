/**
 * Creates a new CaptionImage.
 * 
 * @constructor
 */
mindmaps.CaptionImage = function () {

    /**
    * Set the node which this image belong to.
    * 
    * @param {JQuery} $node
    * @param {DOM} node
    */
    this.setNode = function ($node, node) {
        var _this = this;
        var self = this;
        node.captionImage = this;
        this.imageUpload = new ImageUpload();
        this.imageUpload.onUploaded = function (url) {
            _this.$img.attr("src", url);
            node.img = url;
        };
        // image caption
        this.$img = $("<img/>", {
            id: "node-img-" + node.id,
            src: "img/upload-image.png"
        })
            .mousedown(function (e) {
                // avoid premature canceling
                e.stopPropagation();
            })
            .css({
                "border-radius": "50%",
                width: 100,
                height: 100,
                visibility: "hidden",
                "z-index": 100
            })
            .click(function () {
                if (self.isEditing)
                    _this.imageUpload.selectImage();
            });
        if (node.isRoot()) {
            if (node.img) {
                this.$img.attr("src", node.img);
                this.$img.css({ visibility: "" });
            }
            this.$img.css({
                width: 120,
                height: 120,
                top: -120,
                left: -25,
                position: "relative"
            });
            this.$img.appendTo($node);
        }
        else {
            if (node.img) {
                this.$img.attr("src", node.img);
                this.$img.css({ visibility: "" });
            }
            this.$img.prependTo($node);
        }
    };

    /**
    * Changes the size of the image to match with with the new zoom
    * factor.
    */
    this.zoom = function () { };

    /**
    * Start the edit mode
    */
    this.startEditMode = function () {
        this.isEditing = true;
        this.$img.css({
            cursor: "pointer",
            visibility: ""
        });
    };

    /**
    * End of edit mode
    */
    this.endEditMode = function () {
        this.isEditing = false;
        var src = this.$img.attr("src");
        var isUploaded = src.indexOf("img/upload-image.png");
        this.$img.css({
            cursor: "default",
            visibility: isUploaded === -1 ? "" : "hidden"
        });
    };
}

/**
* Find the image
* 
* @param {JQuery} selector
*/
mindmaps.CaptionImage.find = function (selector) {
    var captionImage = new CaptionImage();
    captionImage.$img = $(selector);
    return captionImage;
};