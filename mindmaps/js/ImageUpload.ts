class ImageUpload {
    $input: JQuery;
    asDataUrl = true;

    constructor() {
        var self = this;
        this.$input = $("<input/>", { type: "file" })
            .change(function() {
                const file = this.files[0];

                // file no length
                if (file.name.length < 1) {
                    return;
                }
                // check file type
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

    fileToDataUrl() {
        const self = this;
        const input = self.$input[0] as HTMLInputElement;
        const file = input.files[0];
        var reader = new FileReader();

        reader.addEventListener("load", () => self.onUploaded(reader.result), false);

        if (file) {
            reader.readAsDataURL(file);
        }
    }

    uploadFileToServer() {
        var self = this;
        const formData = new FormData();
        const input = self.$input[0] as HTMLInputElement;
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
            success(data) {
                // trigger uploaded event
                if (typeof (self.onUploaded) == "function") self.onUploaded(data);
            },
            error() {
                if (typeof (self.onError) == "function") self.onError();
            }
        });
    }

    onUploaded(url: string) {}

    onError() {}

    selectImage() {
        this.$input.click();
    }
}