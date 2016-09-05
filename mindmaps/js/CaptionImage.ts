class CaptionImage {
    $img: JQuery;
    imageUpload: ImageUpload;
    isEditing: boolean;

    static find(selector: string) {
        const captionImage = new CaptionImage();
        captionImage.$img = $(selector);
        return captionImage;
    }

    setNode($node: JQuery, node: any) {
        var self = this;
        node.captionImage = this;
        this.imageUpload = new ImageUpload();
        this.imageUpload.onUploaded = url => {
            this.$img.attr("src", url);
            node.img = url;
        };
        // image caption
        this.$img = $("<img/>",
            {
                id: `node-img-${node.id}`,
                src: "img/upload-image.png"
            })
            .mousedown(e => {
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
            .click(() => {
                if (self.isEditing) this.imageUpload.selectImage();
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
        } else {
            if (node.img) {
                this.$img.attr("src", node.img);
                this.$img.css({ visibility: "" });
            }
            this.$img.prependTo($node);
        }
    }

    zoom() {

    }

    startEditMode() {
        this.isEditing = true;
        this.$img.css({
            cursor: "pointer",
            visibility: ""
        });
    }

    endEditMode() {
        this.isEditing = false;
        const src = this.$img.attr("src");
        const isUploaded = src.indexOf("img/favicon.png");
        this.$img.css({
            cursor: "default",
            visibility: isUploaded === -1 ? "" : "hidden"
        });
    }
}