/**
 * Creates a new ImageUpload.
 * 
 * @constructor
 */
mindmaps.ImageUpload = function () {
    var self = this;

    /**
    * Initializes the uploader.
    * event bus.
    */
    this.init = function () {
        this.asDataUrl = true;
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
                self.asDataUrl ? self.fileToDataUrl() : self.uploadFileToServer();
            });
    }

    /**
    * Convert uploaded image to base64
    */
    this.fileToDataUrl = function () {
        var self = this;
        var input = self.$input[0];
        var file = input.files[0];
        var reader = new FileReader();
        reader.addEventListener("load", function () { return self.onUploaded(reader.result); }, false);
        if (file) {
            reader.readAsDataURL(file);
        }
    };

    /**
    * Upload image to server
    */
    this.uploadFileToServer = function () {
        var self = this;
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
    };

    /**
    * Uploaded event
    */
    this.onUploaded = function (url) { };

    /**
    * On error event
    */
    this.onError = function () { };

    /**
    * Select image for upload
    */
    this.selectImage = function () {
        this.$input.click();
    };

    this.init();
}