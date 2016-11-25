mindmaps.EditController = function(eventBus, commandRegistry) {
    var editCommand = commandRegistry.get(mindmaps.EditCommand);
    var saveDocumentCommand = commandRegistry.get(mindmaps.SaveDocumentCommand);
    editCommand.setHandler(function () {
        editCommand.setVisible(false);
        saveDocumentCommand.setVisible(true);
        eventBus.publish(mindmaps.Event.DOCUMENT_EDIT);
    });

    eventBus.subscribe(mindmaps.Event.DOCUMENT_CREATED, function() {
        editCommand.setVisible(false);
        eventBus.publish(mindmaps.Event.DOCUMENT_EDIT);
    });

    eventBus.subscribe(mindmaps.Event.DOCUMENT_OPENED, function (doc) {
        editCommand.setVisible(doc.canEdit);
        saveDocumentCommand.setVisible(false);
    });

    eventBus.subscribe(mindmaps.Event.DOCUMENT_CLOSED, function() {
        saveDocumentCommand.setVisible(false);
    });

    eventBus.subscribe(mindmaps.Event.INITIALIZED, function () {
        saveDocumentCommand.setVisible(false);
    });
}