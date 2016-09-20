mindmaps.EditController = function(eventBus, commandRegistry) {
    var editCommand = commandRegistry.get(mindmaps.EditCommand);
    editCommand.setHandler(function () {
        editCommand.setVisible(false);
        eventBus.publish(mindmaps.Event.DOCUMENT_EDIT);
    });

    eventBus.subscribe(mindmaps.Event.DOCUMENT_CREATED, function() {
        editCommand.setVisible(false);
        eventBus.publish(mindmaps.Event.DOCUMENT_EDIT);
    });

    eventBus.subscribe(mindmaps.Event.DOCUMENT_OPENED, function () {
        editCommand.setEnabled(true);
        editCommand.setVisible(true);
    });

    eventBus.subscribe(mindmaps.Event.DOCUMENT_CLOSED, function() {
        editCommand.setEnabled(false);
    });

    eventBus.subscribe(mindmaps.Event.INITIALIZED, function() {
        editCommand.setEnabled(false);
    });
}