mindmaps.EditController = function(eventBus, commandRegistry) {
    var command = commandRegistry.get(mindmaps.EditCommand);
    command.setHandler(function () {
        eventBus.publish(mindmaps.Event.DOCUMENT_EDIT);
    });
}