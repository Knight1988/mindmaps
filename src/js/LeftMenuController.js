mindmaps.LeftMenuController = function (eventBus, commandRegistry) {
    var self = this;
    var leftMenuCommand = commandRegistry.get(mindmaps.LeftMenuCommand);
    var isExpanding = true;
    leftMenuCommand.setHandler(function () {
        if (isExpanding) {
            self.collapse();
        } else {
            self.expand();
        }
    });

    this.collapse = function() {
        $("#db-filelist").slideUp("slow");
        isExpanding = false;
        leftMenuCommand.setLabel("Show Menu");
    }

    this.expand = function () {
        isExpanding = true;
        $("#db-filelist").slideDown("slow");
        leftMenuCommand.setLabel("Hide Menu");
    }

    eventBus.subscribe(mindmaps.Event.INITIALIZED, function () {
        leftMenuCommand.setEnabled(true);
        leftMenuCommand.setCss({
            position: "absolute",
            left: 10
        });
    });
}