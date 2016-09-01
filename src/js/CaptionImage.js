var CaptionImage = (function () {
    function CaptionImage() {
    }
    CaptionImage.find = function (selector) {
        var captionImage = new CaptionImage();
        captionImage.$img = $(selector);
        return captionImage;
    };
    CaptionImage.prototype.setNode = function ($node, node) {
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
            src: "img/favicon.png"
        })
            .mousedown(function (e) {
            // avoid premature canceling
            e.stopPropagation();
        })
            .css({
            "border-radius": "50%",
            width: 100,
            height: 100,
            visibility: "hidden"
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
                left: -15,
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
    CaptionImage.prototype.zoom = function () {
    };
    CaptionImage.prototype.startEditMode = function () {
        this.isEditing = true;
        this.$img.css({
            cursor: "pointer",
            visibility: ""
        });
    };
    CaptionImage.prototype.endEditMode = function () {
        this.isEditing = false;
        var src = this.$img.attr("src");
        var isUploaded = src.indexOf("img/favicon.png");
        this.$img.css({
            cursor: "default",
            visibility: isUploaded === -1 ? "" : "hidden"
        });
    };
    return CaptionImage;
}());
//# sourceMappingURL=CaptionImage.js.map