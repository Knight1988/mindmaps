/**
 * Creates a new Application Controller.
 * 
 * @constructor
 */
mindmaps.ApplicationController = function () {
    var eventBus = new mindmaps.EventBus();
    var shortcutController = new mindmaps.ShortcutController();
    var commandRegistry = new mindmaps.CommandRegistry(shortcutController);
    var undoController = new mindmaps.UndoController(eventBus, commandRegistry);
    var mindmapModel = new mindmaps.MindMapModel(eventBus, commandRegistry, undoController);
    var clipboardController = new mindmaps.ClipboardController(eventBus,
        commandRegistry, mindmapModel);
    var helpController = new mindmaps.HelpController(eventBus, commandRegistry);
    var testController = new mindmaps.TestController(eventBus, commandRegistry);
    var printController = new mindmaps.PrintController(eventBus,
        commandRegistry, mindmapModel);
    var autosaveController = new mindmaps.AutoSaveController(eventBus, mindmapModel);
    var filePicker = new mindmaps.FilePicker(eventBus, mindmapModel);
    var editController = new mindmaps.EditController(eventBus, commandRegistry);
    var leftMenuController = new mindmaps.LeftMenuController(eventBus, commandRegistry);

    // increase max listener limit
    eventBus.setMaxListeners(15);

    /**
     * Handles the new document command.
     */
    function doNewDocument() {
        if (!mindmapModel.isVip) {
            alert("You muse be VIP to create document.");
            return;
        }

        // close old document first
        var doc = mindmapModel.getDocument();
        doCloseDocument();

        var presenter = new mindmaps.NewDocumentPresenter(eventBus,
            mindmapModel, new mindmaps.NewDocumentView());
        presenter.go();
    }

    /**
     * Handles the save document command.
     */
    function doSaveDocument() {
        var presenter = new mindmaps.SaveDocumentPresenter(eventBus, mindmapModel, new mindmaps.SaveDocumentView(), autosaveController, filePicker);
        presenter.go();
    }

    /**
     * Handles the close document command.
     */
    function doCloseDocument() {
        var doc = mindmapModel.getDocument();
        if (doc) {
            // TODO for now simply publish events, should be intercepted by
            // someone
            mindmapModel.setDocument(null);
        }
    }

    /**
     * Handles the open document command.
     */
    function doOpenDocument() {
        var presenter = new mindmaps.OpenDocumentPresenter(eventBus,
            mindmapModel, new mindmaps.OpenDocumentView(), filePicker);
        presenter.go();
    }

    function doExportDocument() {
        var presenter = new mindmaps.ExportMapPresenter(eventBus,
            mindmapModel, new mindmaps.ExportMapView());
        presenter.go();
    }

    /**
     * Initializes the controller, registers for all commands and subscribes to
     * event bus.
     */
    this.init = function () {
        var newDocumentCommand = commandRegistry
            .get(mindmaps.NewDocumentCommand);
        newDocumentCommand.setHandler(doNewDocument);
        newDocumentCommand.setEnabled(true);

        var openDocumentCommand = commandRegistry
            .get(mindmaps.OpenDocumentCommand);
        openDocumentCommand.setHandler(doOpenDocument);
        openDocumentCommand.setEnabled(true);

        var saveDocumentCommand = commandRegistry
            .get(mindmaps.SaveDocumentCommand);
        saveDocumentCommand.setHandler(doSaveDocument);
        saveDocumentCommand.setVisible(false);

        var closeDocumentCommand = commandRegistry
            .get(mindmaps.CloseDocumentCommand);
        closeDocumentCommand.setHandler(doCloseDocument);

        var exportCommand = commandRegistry.get(mindmaps.ExportCommand);
        exportCommand.setHandler(doExportDocument);

        var testCommand = commandRegistry.get(mindmaps.TestCommand);

        eventBus.subscribe(mindmaps.Event.DOCUMENT_CLOSED, function () {
            closeDocumentCommand.setEnabled(false);
            exportCommand.setEnabled(false);
        });

        eventBus.subscribe(mindmaps.Event.DOCUMENT_OPENED, function () {
            closeDocumentCommand.setEnabled(true);
            exportCommand.setEnabled(true);
        });
    };

    /**
     * Launches the main view controller.
     */
    this.go = function () {
        var viewController = new mindmaps.MainViewController(eventBus,
            mindmapModel, commandRegistry, leftMenuController);
        viewController.go();

        var userId = Querystring.getInt("id", 0);
        window.MindMapServiceAPI.loadDefaultDocument(userId, function (document) {
            var doc = mindmaps.Document.fromObject(document);
            mindmapModel.setDocument(doc);
        });
    };

    this.init();
};
