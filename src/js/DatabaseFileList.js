﻿mindmaps.DatabaseFileList = function (eventBus, mindmapModel, commandRegistry) {
    var self = this;
    var $content = $("#db-filelist");
    var editCommand = commandRegistry.get(mindmaps.EditCommand);
    var saveCommand = commandRegistry.get(mindmaps.SaveDocumentCommand);

    /**
     * Sets the height of the file list to fit between header and footer.
     */
    this.setSize = function () {
        var windowHeight = $(window).height();
        var headerHeight = $("#topbar").outerHeight(true);
        var footerHeight = $("#bottombar").outerHeight(true);
        var height = windowHeight - headerHeight - footerHeight;
        $content.height(height);
    };

    this.tableEvent = function() {
        var $db = $content;
        $db.delegate("a.title",
                "click",
                function () {
                    if (self.documentClicked) {
                        var t = $(this).tmplItem();
                        self.documentClicked(t.data);
                    }
                })
            .delegate("a.delete",
                "click",
                function () {
                    if (self.deleteDocumentClicked) {
                        var t = $(this).tmplItem();
                        self.deleteDocumentClicked(t.data);
                    }
                });
    }

    this.loadFiles = function () {
        var userId = Querystring.getInt("id", 0);
        MindMapServiceAPI.getlist(userId, function (data) {
            data.sort(mindmaps.Document.sortByModifiedDateDescending);
            var $list = $content.find(".document-list-db");
            $list.empty();

            $("#template-open-table-item")
            .tmpl(data, {
                format: function (date) {
                    if (!date) return "";

                    var day = date.getDate();
                    var month = date.getMonth() + 1;
                    var year = date.getFullYear();
                    return day + "/" + month + "/" + year;
                }
            })
            .appendTo($list);
        });
    }

    this.init = function () {
        // recalculate size when window is resized.
        $(window).resize(function () {
            self.setSize();
        });

        this.setSize();
        this.loadFiles();
        this.tableEvent();
    };

    this.documentClicked = function(doc) {
        mindmaps.Util.trackEvent("Clicks", "localstorage-open");
        mindmapModel.setDocument(doc);
        editCommand.setEnabled(doc.canEdit);
        saveCommand.setEnabled(doc.canEdit);
    }
    
    this.deleteDocumentClicked = function(doc) {
        // TODO event
        MindMapServiceAPI.remove(doc);

        // re-render view
        this.loadFiles();
    }

    eventBus.subscribe(mindmaps.Event.DOCUMENT_SAVED, function () {
        self.loadFiles();
    });
}