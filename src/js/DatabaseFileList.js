mindmaps.DatabaseFileList = function (eventBus, mindmapModel, commandRegistry) {
    var self = this;
    var $content = $("#db-filelist");
    var editCommand = commandRegistry.get(mindmaps.EditCommand);
    var testCommand = commandRegistry.get(mindmaps.TestCommand);
    var saveDocumentCommand = commandRegistry.get(mindmaps.SaveDocumentCommand);

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

    this.tableEvent = function () {
        $content.delegate("a.title",
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
                })
            .delegate("a.restore",
                "click",
                function () {
                    if (self.restoreDocumentClicked) {
                        var t = $(this).tmplItem();
                        self.restoreDocumentClicked(t.data);
                    }
                });
    }

    // show/hide documents when click the captions
    this.captionEvent = function () {
        $content.delegate("th.title", "click", function () {
            var canView = $(this).attr("data-can-view") === "true";
            if (canView) {
                $(this).parents("table").find(".document-list-db").toggle();
            }
            else {
                alert("You muse be VIP to view this.");
            }
        });
    }

    this.loadFiles = function (expandCategoryId) {
        var userId = Querystring.getInt("id", 0);
        window.MindMapServiceAPI.getCategories(userId, function (categories) {
            // Sort documents in categories
            for (var i = 0; i < categories.length; i++) {
                var category = categories[i];
                category.documents.sort(mindmaps.Document.sortByCreatedDateAscending);
            }

            // enable new document if VIP
            if (categories[categories.length - 1].canView) {
                mindmapModel.isVip = true;
                testCommand.setData({
                    "data-vip": mindmapModel.isVip
                });
            }
            testCommand.setVisible(mindmapModel.isVip);

            var $list = $content.find(".document-list-db");
            $list.empty();

            $("#template-open-table-item")
            .tmpl(categories)
            .appendTo($list);

            // hide all if no savedDoc
            if (expandCategoryId === undefined) $content.find("tbody.document-list-db").hide();
            else {
                // loop all category
                $content.find("tbody.document-list-db").each(function () {
                    // get  category
                    var category = $(this).tmplItem().data;
                    if (expandCategoryId === category.id) return;

                    // hide other category
                    $(this).hide();
                });
            }
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
        this.captionEvent();
    };

    this.documentClicked = function (doc) {
        mindmaps.Util.trackEvent("Clicks", "database-open");
        var userId = Querystring.getInt("id", 0);
        window.MindMapServiceAPI.load(doc.id, userId, function (document) {
            doc = mindmaps.Document.fromObject(document);
            mindmapModel.setDocument(doc);
            editCommand.setEnabled(doc.canEdit);
            saveDocumentCommand.setEnabled(doc.canEdit);

            testCommand.setData({
                "data-id": doc.id,
                "data-vip": mindmapModel.isVip
            });
        });
    }

    this.deleteDocumentClicked = function (doc) {
        var self = this;
        var result = confirm("Delete document '" + doc.title + "'?");
        if (result) {
            // remove the document
            window.MindMapServiceAPI.remove(doc, function () {
                // re-render view
                self.loadFiles(doc.category.id);
            });

        }
    }

    this.restoreDocumentClicked = function (doc) {
        var self = this;
        var result = confirm("Restore document '" + doc.title + "'?");
        if (result) {
            // remove the document
            window.MindMapServiceAPI.remove(doc, function () {
                // re-render view
                self.loadFiles(doc.category.id);
            });
        }
    }

    eventBus.subscribe(mindmaps.Event.DOCUMENT_SAVED, function (doc) {
        var id = 0;
        if (doc.category) id = doc.category.id;
        self.loadFiles(id);
        self.documentClicked(doc);
    });
}