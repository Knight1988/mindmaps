/**
 * <pre>
 * Listens to TEST_COMMAND.
 * </pre>
 * 
 * @constructor
 * @param {mindmaps.EventBus} eventBus
 * @param {mindmaps.commandRegistry} commandRegistry
 */
mindmaps.TestController = function (eventBus, commandRegistry) {
    var testCommand = commandRegistry.get(mindmaps.TestCommand);
    testCommand.setHandler(function () {
        alert("data-id:" + testCommand["data-id"] + "\r\n" + "data-vip:" + testCommand["data-vip"]);
    });

    eventBus.subscribe(mindmaps.Event.DOCUMENT_OPENED, function (doc) {
        // test command
        testCommand.setData({ "data-id": doc.id });
        testCommand.setVisible(!doc.isPrivate);
    });
}