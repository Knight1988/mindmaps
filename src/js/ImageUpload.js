var ImageUpload = (function () {
    function ImageUpload() {
        var self = this;
        this.$input = $("<input/>", { type: "file" })
            .change(function () {
            var file = this.files[0];
            // file no length
            if (file.name.length < 1) {
                return;
            }
            else if (file.type !== "image/png" &&
                file.type !== "image/jpg" &&
                file.type !== "image/gif" &&
                file.type !== "image/jpeg") {
                alert("The file does not match png, jpg or gif");
                return;
            }
            // upload file
            var formData = new FormData();
            var input = self.$input[0];
            formData.append("myImage", input.files[0]);
            $.ajax({
                url: "ImageUpload.ashx",
                type: "POST",
                // Form data
                data: formData,
                // Options to tell jQuery not to process data or worry about the content-type
                cache: false,
                contentType: false,
                processData: false,
                // Success
                success: function (data) {
                    // trigger uploaded event
                    if (typeof (self.onUploaded) == "function")
                        self.onUploaded(data);
                },
                error: function () {
                    if (typeof (self.onError) == "function")
                        self.onError();
                }
            });
        });
    }
    ImageUpload.prototype.onUploaded = function (url) { };
    ImageUpload.prototype.onError = function () { };
    ImageUpload.prototype.selectImage = function () {
        this.$input.click();
    };
    return ImageUpload;
}());
//# sourceMappingURL=ImageUpload.js.map