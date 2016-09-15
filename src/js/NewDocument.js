/**
 * Creates a new NewDocumentView. This view shows a dialog for user
 * can enter new document Title.
 * 
 * @constructor
 */
mindmaps.NewDocumentView = function () {
    var self = this;

    // create dialog
    var $dialog = $("#template-new")
        .tmpl()
        .dialog({
            autoOpen: false,
            modal: true,
            zIndex: 5000,
            width: 550,
            close: function () {
                $(this).dialog("destroy");
                $(this).remove();
            }
        });

    this.showNewDialog = function () {
        $dialog.dialog("open");
        var $docTitle = $dialog.find("#document-title").select();
        $dialog.find("#button-new-document")
            .button({
                icons: {
                    primary: "ui-icon-document-b"
                }
            })
            .click(function () {
                var doc = new mindmaps.Document();
                doc.title = $docTitle.val();
                doc.mindmap.root.setCaption(doc.title);
                if (self.newDocumentClicked) self.newDocumentClicked(doc);
            });
    }

    this.hideNewDialog = function () {
        $dialog.dialog("close");
    }
};

/**
 * Creates a new NewDocumentPresenter. This presenter has no view associated
 * with it for now. It simply creates a new document. It could in the future
 * display a dialog where the user could chose options like document title and
 * such.
 * 
 * @constructor
 */
mindmaps.NewDocumentPresenter = function (eventBus, mindmapModel, view) {
    view.newDocumentClicked = function (doc) {
        mindmapModel.setDocument(doc);
        view.hideNewDialog();
        eventBus.publish(mindmaps.Event.DOCUMENT_CREATED);
    }

    this.go = function () {
        view.showNewDialog();
    };
};
