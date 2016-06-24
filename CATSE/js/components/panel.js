var Panel = function(parentID, newID) {
    var self = this;
    self.htmlWithHeading = PanelTemplateWithHeading;
    self.parentID = "";

    self.constructor= function(parentID, newID){
        self.parentID = parentID;
        self.myID = newID;
    };

    self.show = function(text) {
        var html = PanelTemplate.replace("{text}", text);
        self.addContent(html);
    };

    self.showWithHeading = function(heading,text) {
        var html = PanelTemplateWithHeading.replace("{text}", text);
        html = html.replace("{heading}", heading);
        self.addContent(html);
    };

    self.showWithImage = function(imageURL,text) {
        var html = PanelTemplateWithImage.replace("{text}", text);
        html = html.replace("{image}", imageURL);
        self.addContent(html);
    };

    self.addContent = function(html){
        var parent = document.getElementById(self.parentID);
        var node = document.createElement("div");
        node.id = self.id;
        node.className = "";
        node.innerHTML = html;
        parent.appendChild(node);
    };

    self.constructor(parentID, newID);
};